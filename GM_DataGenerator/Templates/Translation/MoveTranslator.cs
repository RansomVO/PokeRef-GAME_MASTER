using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POGOProtos.Settings.Master;
using static POGOProtos.Networking.Responses.DownloadItemTemplatesResponse;
using POGOProtos.Enums;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates
{
    public class MoveTranslator : TemplateTranslator
    {
        #region Data

        private const string MARKER_MOVE_ID = "V0000_MOVE_";
        private const string MARKER_FAST_MOVE_ID = "_FAST";
        private const string MARKER_TYPE = "POKEMON_TYPE_";

        #endregion Data

        public PokemonMove Key { get { return MoveSettings.movement_id; } }

        public MoveSettings MoveSettings { get; private set; }

        #region Computed Properties

        public bool IsFast { get { return MoveSettings.power == 0 || MoveSettings.energy_delta > 0; } }

        public bool IsCharged { get { return !IsFast; } }

        public string Type { get { return FixName(MoveSettings.pokemon_type.ToString()); } }

        public string Name { get { return FixName(MoveSettings.vfx_name.ToString()); } }

        public int Energy { get { return MoveSettings.energy_delta; } }

        public int Power { get { return (int)MoveSettings.power; } }

        public double Duration { get { return ((double)MoveSettings.duration_ms) / 1000.0; } }

        public int DamageWindowStart { get { return MoveSettings.damage_window_start_ms; } }

        public int DamageWindowEnd { get { return MoveSettings.damage_window_end_ms; } }

        #endregion Computed Properties

        #region ctor

        public MoveTranslator(ItemTemplate itemTemplate)
            : base(itemTemplate.template_id)
        {
            MoveSettings = itemTemplate.move_settings;
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
            return TemplateTranslator.FixName(rawName);
        }
    }
}
