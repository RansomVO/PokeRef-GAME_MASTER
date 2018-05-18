using PokemonGO.GAME_MASTER.Templates;

namespace VanOrman.PokemonGO.GAME_MASTER.Parser.Templates
{
    public class MoveTemplate : TranslationTemplate
    {
        #region Data

        private const string MARKER_MOVE_ID = "V0000_MOVE_";
        private const string MARKER_FAST_MOVE_ID = "_FAST";
        private const string MARKER_TYPE = "POKEMON_TYPE_";

        #endregion Data

        public MoveSettings MoveSettings { get; private set; }

        #region Computed Properties

        public bool IsFast { get { return MoveSettings.power == 0 || MoveSettings.energyDelta > 0; } }

        public bool IsCharged { get { return !IsFast; } }

        public string Type { get { return FixName(MoveSettings.pokemonType); } }

        public string Name { get { return FixName(MoveSettings.vfxName); } }

        public int Energy { get { return MoveSettings.energyDelta; } }

        public int Power { get { return (int)MoveSettings.power; } }

        public double Duration { get { return ((double)MoveSettings.durationMs) / 1000.0; } }

        public int DamageWindowStart { get { return MoveSettings.damageWindowStartMs; } }

        public int DamageWindowEnd { get { return MoveSettings.damageWindowEndMs; } }

        #endregion Computed Properties

        #region ctor

        public MoveTemplate(ItemTemplate itemTemplate)
            : base(itemTemplate.templateId, itemTemplate.moveSettings.movementId)
        {
            MoveSettings = itemTemplate.moveSettings;
        }

        #endregion ctor

        public static new string FixName(string rawName)
        {
            if (string.IsNullOrWhiteSpace(rawName))
                return rawName;

            // Special cases.
            if (string.Equals(rawName, "FUTURESIGHT"))
                return "Future Sight";

            // Is this the Type of the move?
            if (rawName.ToUpper().StartsWith(MARKER_TYPE))
                return FixName(rawName.Substring(MARKER_TYPE.Length));

            // Is this a Fast Move?
            if (rawName.ToUpper().EndsWith(MARKER_FAST_MOVE_ID))
                return FixName(rawName.Substring(0, rawName.Length - MARKER_FAST_MOVE_ID.Length));

            // Default case.
            return FixName(rawName);
        }
    }
}
