using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POGOProtos.Settings.Master;
using static POGOProtos.Networking.Responses.DownloadItemTemplatesResponse;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates
{
    public class PlayerLevelTranslator : TemplateTranslator
    {
        #region Loaded Properties

        public PlayerLevelSettings PlayerLevelSettings { get; private set; }

        #endregion Loaded Properties

        #region Computed Properties

        public float[] CPMultiplier { get { return PlayerLevelSettings.cp_multiplier; } }

        #endregion Computed Properties

        #region ctor

        public PlayerLevelTranslator(ItemTemplate itemTemplate)
            : base(itemTemplate.template_id)
        {
            PlayerLevelSettings = itemTemplate.player_level;
        }

        #endregion ctor
    }
}
