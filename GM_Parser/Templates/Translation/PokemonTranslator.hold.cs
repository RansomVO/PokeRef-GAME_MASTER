using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonGO.GAME_MASTER.Templates;

namespace VanOrman.PokemonGO.GAME_MASTER.Parser.Templates
{
    public class PokemonTranslator : TemplateTranslator
    {
        #region Data
        private static readonly int MARKER_ID_LEN = "V0000_POKEMON_".Length;
        private const string MARKER_TYPE = "POKEMON_TYPE_";
        private const string MARKER_CANDY = "FAMILY_";
        private const string MARKER_EVOLVE_SPECIAL_ITEM = "ITEM_";
        private const string MARKER_RARITY = "POKEMON_RARITY_";
        private const string MARKER_GENDER = "MALE";

        #endregion Data

        public override string Key
        {
            get
            {
                switch (PokemonSettings.pokemonId)
                {
                    case "CASTFORM":
                    case "DEOXYS":
                        return TemplateId.Substring(MARKER_ID_LEN);

                    default:
                        return PokemonSettings.pokemonId;
                }
            }
        }

        public PokemonSettings PokemonSettings { get; private set; }

        #region Computed Properties

        public int Id { get { return int.Parse(TemplateId.Substring(1, 4)); } }

        public string Name { get { return FixName(PokemonSettings.pokemonId); } }

        public string Form { get { return Key.Length == PokemonSettings.pokemonId.Length || Key.EndsWith(MARKER_GENDER) ? null : Key.Substring(Name.Length + 1); } }

        public string Type1 { get { return FixName(PokemonSettings.type.Substring(MARKER_TYPE.Length)); } }

        public string Type2 { get { return string.IsNullOrEmpty(PokemonSettings.type2) ? string.Empty : FixName(PokemonSettings.type2.Substring(MARKER_TYPE.Length)); } }

        public string CandyType { get { return FixName(PokemonSettings.familyId.Substring(MARKER_CANDY.Length)); } }

        public string Rarity { get { return string.IsNullOrEmpty(PokemonSettings.rarity) ? string.Empty : FixName(PokemonSettings.rarity.Substring(MARKER_RARITY.Length)); } }

        #endregion Computed Properties

        #region Assigned Properties

        public string GenderRatio { get; private set; }

        public int EvolvesFromId { get; private set; }

        public string EvolvesFrom { get; private set; }

        public int CandiesToEvolve { get; private set; }

        public string EvolveSpecialItem { get; private set; }

        public List<string> LegacyFastMoves { get; private set; }

        public List<string> LegacyChargedMoves { get; private set; }

        #endregion Assigned Properties

        #region ctor

        public PokemonTranslator(ItemTemplate itemTemplate)
            : base(itemTemplate.templateId)
        {
            PokemonSettings = itemTemplate.pokemonSettings;
        }

        #endregion ctor

        public void AssignProperties(Dictionary<string, PokemonTranslator> pokemon, GenderRatioTranslator genderRatio, List<string> legacyFastMoves, List<string> legacyChargedMoves)
        {
            // Remove current moves from known legacy moves.
            if (legacyFastMoves == null)
            {
                LegacyFastMoves = new List<string>();
            }
            else
            {
                LegacyFastMoves = new List<string>(legacyFastMoves);
                foreach (var move in PokemonSettings.quickMoves)
                    if (LegacyFastMoves.Contains(move))
                        LegacyFastMoves.Remove(move);
            }

            if (legacyChargedMoves == null)
            {
                LegacyChargedMoves = new List<string>();
            }
            else
            {
                LegacyChargedMoves = new List<string>(legacyChargedMoves);
                foreach (var move in PokemonSettings.cinematicMoves)
                    if (LegacyChargedMoves.Contains(move))
                        LegacyChargedMoves.Remove(move);
            }

            // Set EvolvesFrom on the pokemon that evolve from me.
            if (PokemonSettings.evolutionBranch != null)
            {
                foreach (var evolution in PokemonSettings.evolutionBranch)
                {
                    if (pokemon.ContainsKey(evolution.evolution))
                    {
                        pokemon[evolution.evolution].EvolvesFromId = Id;
                        pokemon[evolution.evolution].EvolvesFrom = Name;
                        pokemon[evolution.evolution].CandiesToEvolve = evolution.candyCost;
                        pokemon[evolution.evolution].EvolveSpecialItem = FixName(evolution.evolutionItemRequirement);
                    }
                }
            }

            // Set the Gender Ratio
            if (genderRatio != null)
                GenderRatio = genderRatio.Ratio;
        }

        public static new string FixName(string rawName)
        {
            if (string.IsNullOrWhiteSpace(rawName))
                return rawName;

            if (string.Equals(rawName, "NIDORAN_FEMALE"))
                return "Nidoran" + Constants.Gender.Female;

            if (string.Equals(rawName, "NIDORAN_MALE"))
                return "Nidoran" + Constants.Gender.Male;

            if (string.Equals(rawName, "FARFETCHD"))
                return "Farfetch'd";

            if (string.Equals(rawName, "MR_MIME"))
                return "Mr. Mime";

            if (string.Equals(rawName, "HO_OH"))
                return "Ho-Oh";

            if (rawName.StartsWith(MARKER_EVOLVE_SPECIAL_ITEM))
                rawName = rawName.Substring(MARKER_EVOLVE_SPECIAL_ITEM.Length);

            return TemplateTranslator.FixName(rawName);
        }
    }
}
