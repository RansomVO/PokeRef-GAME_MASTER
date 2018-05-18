using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace PokestatParser
{
    public class PokemonStats
    {
        #region Data

        private const string DefaultPokemonStatsFileName = "pokemongo.json";
        private const string QuickMoveMarker = "_FAST";

        private static readonly KeyValuePair<string, string>[] pokemonNameFixes = new KeyValuePair<string, string>[]
        {
            // TODO QZX: There is a bug that the male/female signs are corrupted.
            //  It appears issue is with Excel because it works fine when I:
            //      1) Open the .csv in Notepad
            //      2) Replace "," with "\t"
            //      3) Copy and paste into an Excel spreadsheet
            new KeyValuePair<string, string>("Nidoran Male", "Nidoran\u2642"),
            new KeyValuePair<string, string>("Nidoran Female", "Nidoran\u2640"),
            new KeyValuePair<string, string>("Farfetchd", "Farfetch'd"),
            new KeyValuePair<string, string>("Mr Mime", "Mr. Mime"),
            new KeyValuePair<string, string>("Ho oh", "Ho-oh"),
        };


        #endregion Data

        #region Properties

        public Dictionary<string, Pokemon> Pokemon
        {
            get
            {
                if (_Pokemon == null)
                {
                    _Pokemon = new Dictionary<string, Pokemon>();
                }

                return _Pokemon;
            }
        }
        private Dictionary<string, Pokemon> _Pokemon;

        public Dictionary<string, Move> MovesQuick
        {
            get
            {
                if (_MovesQuick == null)
                {
                    _MovesQuick = new Dictionary<string, Move>();
                }

                return _MovesQuick;
            }
        }
        private Dictionary<string, Move> _MovesQuick;

        public Dictionary<string, Move> MovesCharge
        {
            get
            {
                if (_MovesCharge == null)
                {
                    _MovesCharge = new Dictionary<string, Move>();
                }

                return _MovesCharge;
            }
        }
        private Dictionary<string, Move> _MovesCharge;

        public decimal[] CPModifiers { get; private set; }

        #endregion Properties

        #region ctor

        public PokemonStats() : this(DefaultPokemonStatsFileName)
        { }

        public PokemonStats(string filePath)
        {
            Parse(ReadStats(filePath));
        }

        #endregion ctor

        #region Private Methods

        private string ReadStats(string filePath)
        {
            using (StreamReader streamReader = new StreamReader(filePath))
            {
                return streamReader.ReadToEnd();
            }
        }

        private void Parse(string pokemonStatsText)
        {
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            var pokemonStatsDictionary = javaScriptSerializer.Deserialize<dynamic>(pokemonStatsText);

            foreach (dynamic itemTemplate in pokemonStatsDictionary["itemTemplates"])
            {
                string templateIdRaw = itemTemplate["templateId"];

                // Filter out stuff we don't deal with.
                //  (This is for maintenance purposes. It lists things that are in the list, but we don't deal with right now.)
                if (templateIdRaw.StartsWith("AVATAR") ||
                    templateIdRaw.StartsWith("BADGE") ||
                    templateIdRaw.StartsWith("BATTLE") ||
                    templateIdRaw.StartsWith("ENCOUNTER") ||
                    templateIdRaw.StartsWith("FORMS") ||     // This is valuable for Unown ([A-Z],!,?)
                    templateIdRaw.StartsWith("GYM") ||
                    templateIdRaw.StartsWith("IAP") ||
                    templateIdRaw.StartsWith("ITEM") ||
                    templateIdRaw.StartsWith("POKEMON_TYPE") ||
                    templateIdRaw.StartsWith("POKEMON_UPGRADE") ||
                    templateIdRaw.StartsWith("QUEST") ||
                    templateIdRaw.StartsWith("SPAWN") ||
                    templateIdRaw.StartsWith("camera") ||
                    templateIdRaw.StartsWith("android.test") ||
                    templateIdRaw.StartsWith("incenseordinary") ||
                    templateIdRaw.StartsWith("incubatorbasic") ||
                    templateIdRaw.StartsWith("itemstorageupgrade") ||
                    templateIdRaw.StartsWith("luckyegg") ||
                    templateIdRaw.StartsWith("pokeball") ||
                    templateIdRaw.StartsWith("pokecoin") ||
                    templateIdRaw.StartsWith("pokemonstorageupgrade") ||
                    templateIdRaw.StartsWith("sequence"))
                {
                    continue;
                }

                // There are some exact matches we will deal with.
                if (string.Equals(templateIdRaw, "PLAYER_LEVEL_SETTINGS", StringComparison.OrdinalIgnoreCase))
                {
                    CPModifiers = ReadCPModifiers(itemTemplate);
                    continue;
                }


                // In other cases, it is a pattern game.
                string[] templateId = templateIdRaw.Split('_');
                if (templateId.Count() < 2)
                {
                    throw new Exception("Unexpected small templateId");
                }

                if (templateId[0].StartsWith("V") &&
                    string.Equals(templateId[1], "POKEMON", StringComparison.OrdinalIgnoreCase))
                {
                    Pokemon pokemon = ReadPokemon(itemTemplate);
                    Pokemon.Add(pokemon.Name, pokemon);
                }
                else if (templateId[0].StartsWith("V") &&
                    string.Equals(templateId[1], "MOVE", StringComparison.OrdinalIgnoreCase))
                {
                    Move move = ReadMove(itemTemplate);
                    if (move.MoveType == MoveType.Quick)
                    {
                        MovesQuick.Add(move.Name, move);
                    }
                    else if (move.MoveType == MoveType.Charge)
                    {
                        MovesCharge.Add(move.Name, move);
                    }
                    else
                    {
                        throw new Exception("Invalid Move Type!");
                    }
                }
                else
                {
                    throw new Exception("Code needs to be updated to deal with " + itemTemplate["templateId"]);
                }
            }
        }

        #region Item Readers.

        private Pokemon ReadPokemon(dynamic itemTemplate)
        {
            Pokemon pokemon = new Pokemon();

            // Get the Pokemon Number
            pokemon.Id = int.Parse(itemTemplate["templateId"].Split('_')[0].Substring(1));

            // Get the Pokemon Name
            pokemon.Name = FixPokemonName(FixString(itemTemplate["pokemonSettings"]["pokemonId"]));

            // Get the Pokemon's Type(s).
            pokemon.Types[0] = FixString(itemTemplate["pokemonSettings"]["type"].Split('_')[2]);
            if (itemTemplate["pokemonSettings"].ContainsKey("type2"))
            {
                pokemon.Types[1] = FixString(itemTemplate["pokemonSettings"]["type2"].Split('_')[2]);
            }

            // Get the type of Candy for the Pokemon
            pokemon.Candy = FixString(itemTemplate["pokemonSettings"]["familyId"].Split('_')[1]);

            // Get the # of Km you must walk with the Pokemon as your buddy to get a Candy.
            pokemon.BuddyKmForCandy = (int)itemTemplate["pokemonSettings"]["kmBuddyDistance"];

            // Get the Base values.
            foreach (var v in itemTemplate["pokemonSettings"]["stats"])
            {
                pokemon.BaseStamina = itemTemplate["pokemonSettings"]["stats"]["baseStamina"];
                pokemon.BaseAttack = itemTemplate["pokemonSettings"]["stats"]["baseAttack"];
                pokemon.BaseDefense = itemTemplate["pokemonSettings"]["stats"]["baseDefense"];
            }

            // Get the Height and Weight stats.
            pokemon.Height = itemTemplate["pokemonSettings"]["pokedexHeightM"];
            pokemon.HeightStandardDeviation = itemTemplate["pokemonSettings"]["heightStdDev"];
            pokemon.Weight = itemTemplate["pokemonSettings"]["pokedexWeightKg"];
            pokemon.WeightStandardDeviation = itemTemplate["pokemonSettings"]["weightStdDev"];

            // Get the Quick Moves.
            foreach (var quickMove in itemTemplate["pokemonSettings"]["quickMoves"])
            {
                MoveType unusedMoveType;
                pokemon.QuickMoves.Add(FixMoveName(quickMove, out unusedMoveType));
            }

            // Get the Charge Moves.
            foreach (var chargeMove in itemTemplate["pokemonSettings"]["cinematicMoves"])
            {
                MoveType unusedMoveType;
                pokemon.ChargeMoves.Add(FixMoveName(chargeMove, out unusedMoveType));
            }

            // TODO QZX: Get the Evolutions for the Pokemon.
            //Evolutions

            // TODO QZX: It would be nice if I could get an "Evolves from" value.
            //      (This would probably require a second pass.)

            return pokemon;
        }

        private Move ReadMove(dynamic itemTemplate)
        {
            Move move = new Move();

            // Get the Move Name.
            MoveType moveType;
            move.Name = FixMoveName(itemTemplate["moveSettings"]["movementId"], out moveType);
            move.MoveType = moveType;

            // Get the Move's Type.
            move.Type = FixString(itemTemplate["moveSettings"]["pokemonType"].Split('_')[2]);

            // Get the Energy the move generates or uses.
            //  (Positive for generates, negative for uses.)
            move.Energy = itemTemplate["moveSettings"].ContainsKey("energyDelta") ? itemTemplate["moveSettings"]["energyDelta"] : 0;

            // Get the Damage the Move inflicts.
            move.Power = itemTemplate["moveSettings"].ContainsKey("power") ? (int)itemTemplate["moveSettings"]["power"] : 0;

            // Get the Time the Move takes.
            move.Duration = itemTemplate["moveSettings"]["durationMs"];

            // Get the Damage Window.
            move.DamageWindowStart = itemTemplate["moveSettings"]["damageWindowStartMs"];
            move.DamageWindowEnd = itemTemplate["moveSettings"]["damageWindowEndMs"];

            return move;
        }

        private decimal[] ReadCPModifiers(dynamic itemTemplate)
        {
            return ((object[])itemTemplate["playerLevel"]["cpMultiplier"]).Select(Convert.ToDecimal).ToArray();
        }

        #endregion Item Readers.

        #region Support Methods.

        private string FixString(string text)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (var part in text.Split('_'))
            {
                if (stringBuilder.Length != 0)
                {
                    stringBuilder.Append(" ");
                }

                stringBuilder.Append(part.Substring(0, 1).ToUpper() + part.Substring(1).ToLower());
            }

            return stringBuilder.ToString();
        }

        private string FixMoveName(string rawMoveName, out MoveType moveType)
        {
            if (rawMoveName.EndsWith(QuickMoveMarker, StringComparison.OrdinalIgnoreCase))
            {
                rawMoveName = rawMoveName.Substring(0, rawMoveName.Length - QuickMoveMarker.Length);
                moveType = MoveType.Quick;
            }
            else
            {
                moveType = MoveType.Charge;
            }

            return FixString(rawMoveName);
        }

        private string FixPokemonName(string pokemonName)
        {
            foreach(var entry in pokemonNameFixes)
            {
                if (string.Equals(entry.Key, pokemonName, StringComparison.OrdinalIgnoreCase))
                {
                    return entry.Value;
                }
            }

            return pokemonName;
        }

        #endregion Support Methods.

        #endregion Private Methods
    }
}
