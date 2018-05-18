using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonGO.GAME_MASTER.Templates;

namespace VanOrman.PokemonGO.GAME_MASTER.Parser.Templates
{
    public class FormSettingsTranslator : TemplateTranslator
    {
        #region Data

        private const string MARKER_KEY = "FORMS_V";

        #endregion Data

        #region Properties

        public override string Key { get { return TemplateId.Substring(MARKER_KEY.Length, 4); } }

        public FormSettings FormSettings { get; private set; }

        #endregion Properties

        #region ctor

        public FormSettingsTranslator(ItemTemplate itemTemplate)
            : base(itemTemplate.templateId)
        {
            FormSettings = itemTemplate.formSettings;
        }

        #endregion ctor
    }
}
