using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POGOProtos.Settings.Master;
using static POGOProtos.Networking.Responses.DownloadItemTemplatesResponse;
using POGOProtos.Enums;
using POGOProtos.Inventory.Item;
using System.Globalization;
using VanOrman.Utils;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates
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

        public PokemonSettings PokemonSettings { get; private set; }

        #region Computed Properties

        public int Key { get { return (int)PokemonSettings.pokemon_id + (1000 * (int)PokemonSettings.form); } }

        public int Id { get { return int.Parse(TemplateId.Substring(1, 4)); } }

        public string Name { get { return FixPokemonName(PokemonSettings.pokemon_id.ToString()); } }

        public Form Form { get { return PokemonSettings.form; } }

        public string FormName { get { return GetFormName(Form, PokemonSettings.pokemon_id.ToString()); } }

        public string Type1 { get { return FixName(PokemonSettings.type.ToString().Substring(MARKER_TYPE.Length)); } }

        public string Type2 { get { return PokemonSettings.type_2 == PokemonType.POKEMON_TYPE_NONE ? string.Empty : FixName(PokemonSettings.type_2.ToString().Substring(MARKER_TYPE.Length)); } }

        public string CandyType { get { return FixName(PokemonSettings.family_id.ToString().Substring(MARKER_CANDY.Length)); } }

        public string Rarity { get { return PokemonSettings.rarity == PokemonRarity.NORMAL ? string.Empty : FixName(PokemonSettings.rarity.ToString().Substring(MARKER_RARITY.Length)); } }

        #endregion Computed Properties

        #region Assigned Properties

        public string GenderRatio { get; private set; }

        public int EvolvesFromId { get; private set; }

        public string EvolvesFrom { get; private set; }

        // TODO QZX: Currently, it appears PokemonSettings.parent_form is always FORM_UNSET.
        //  If this ever changes, it would be cool to leverage it.
        public Form EvolvesFromForm { get; private set; }

        public string EvolvesFromFormName { get { return GetFormName(EvolvesFromForm, EvolvesFrom); } }

        public int CandiesToEvolve { get; private set; }

        public string EvolveSpecialItem { get; private set; }

        public List<PokemonMove> LegacyFastMoves { get; private set; }

        public List<PokemonMove> LegacyChargedMoves { get; private set; }

        #endregion Assigned Properties

        #region ctor

        public PokemonTranslator(ItemTemplate itemTemplate)
            : base(itemTemplate.template_id)
        {
            PokemonSettings = itemTemplate.pokemon_settings;
        }

        #endregion ctor

        public void AssignProperties(Dictionary<int, PokemonTranslator> pokemon, GenderRatioTranslator genderRatio, List<PokemonMove> legacyFastMoves, List<PokemonMove> legacyChargedMoves)
        {
            // Add all but the current moves as legacy moves.
            LegacyFastMoves = new List<PokemonMove>();
            if (legacyFastMoves != null)
                foreach (var move in legacyFastMoves)
                    if (!PokemonSettings.quick_moves.Contains(move))
                        LegacyFastMoves.Add(move);

            LegacyChargedMoves = new List<PokemonMove>();
            if (legacyChargedMoves != null)
                foreach (var move in legacyChargedMoves)
                    if (!PokemonSettings.cinematic_moves.Contains(move))
                        LegacyChargedMoves.Add(move);

            // Collect EvolvesFrom info.
            if (PokemonSettings.parent_pokemon_id != PokemonId.MISSINGNO)
            {
                EvolvesFromId = (int)PokemonSettings.parent_pokemon_id;
                var parent = pokemon[EvolvesFromId];
                EvolvesFrom = parent.Name;
                EvolvesFromForm = PokemonSettings.parent_form;
                foreach (var evolution in parent.PokemonSettings.evolution_branch)
                    if (evolution.evolution == PokemonSettings.pokemon_id)
                    {
                        CandiesToEvolve = evolution.candy_cost;
                        EvolveSpecialItem = evolution.evolution_item_requirement == ItemId.ITEM_UNKNOWN ? null : FixName(evolution.evolution_item_requirement.ToString().Substring(MARKER_EVOLVE_SPECIAL_ITEM.Length));

                        break;
                    }
            }

            // Set the Gender Ratio
            if (genderRatio != null)
                GenderRatio = genderRatio.Ratio;
        }

        public static string GetFormName(Form formId)
        {
            if (formId == Form.FORM_UNSET)
                return null;

            string formIdText = formId.ToString();
            return FixName(formIdText.Substring(formIdText.IndexOf("_") + 1));
        }

        public static string GetFormName(Form formId, string name)
        {
            if (formId == Form.FORM_UNSET)
                return "";

            int index = name.IndexOf(" (");
            if (index > 0)
                name = name.Substring(0, index);
            try
            {
                return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(
                        formId.ToString().Substring(name.Length + 1)
                        .Replace('_', ' ')
                        .ToLower());
            }
            catch (Exception ex)
            {
                ConsoleOutput.OutputException(ex, $"ERROR: Unknown Form: \"{formId.ToString()}\" ({name})\r\n\tNeed to update POGOProtos\\Enums\\Form.proto, then run GM_protogen.");
                throw;
            }
        }

        public static string FixPokemonName(string rawName)
        {
            if (string.IsNullOrWhiteSpace(rawName))
                return rawName;

            if (string.Equals(rawName, "NIDORAN_FEMALE"))
                return "Nidoran" + PokeConstants.Gender.Female;

            if (string.Equals(rawName, "NIDORAN_MALE"))
                return "Nidoran" + PokeConstants.Gender.Male;

            if (string.Equals(rawName, "FARFETCHD"))
                return "Farfetch'd";

            if (string.Equals(rawName, "MR_MIME"))
                return "Mr. Mime";

            if (string.Equals(rawName, "HO_OH"))
                return "Ho-Oh";

            if (string.Equals(rawName, "MIME_JR"))
                return "Mime Jr.";

            if (string.Equals(rawName, "PORYGON_Z"))
                return "Porygon-Z";


            return FixName(rawName);
        }
    }
}
