using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonGO.GAME_MASTER.Templates;

namespace VanOrman.PokemonGO.GAME_MASTER.Parser.Templates
{
    public class PlayerLevelTranslator : TemplateTranslator
    {
        #region Loaded Properties

        public PlayerLevel PlayerLevelSettings { get; private set; }

        #endregion Loaded Properties

        #region Computed Properties

        public List<double> CPMultiplier { get { return PlayerLevelSettings.cpMultiplier; } }

        #endregion Computed Properties

        #region ctor

        public PlayerLevelTranslator(ItemTemplate itemTemplate)
            : base(itemTemplate.templateId)
        {
            PlayerLevelSettings = itemTemplate.playerLevel;
        }

        #endregion ctor
    }
}
