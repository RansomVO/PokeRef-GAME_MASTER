using System.Collections.Generic;

using PokemonGO.GAME_MASTER.Templates;

namespace VanOrman.PokemonGO.GAME_MASTER.Parser.Templates
{
    public class PlayerLevelTemplate : TranslationTemplate
    {
        #region Loaded Properties

        public PlayerLevel PlayerLevelSettings { get; private set; }

        #endregion Loaded Properties

        #region Computed Properties

        public List<double> CPMultiplier { get { return PlayerLevelSettings.cpMultiplier; } }

        #endregion Computed Properties

        #region ctor

        public PlayerLevelTemplate(ItemTemplate itemTemplate)
            : base(itemTemplate.templateId, null)    // There is only one of these so don't bother with a key.
        {
            PlayerLevelSettings = itemTemplate.playerLevel;
        }

        #endregion ctor
    }
}
