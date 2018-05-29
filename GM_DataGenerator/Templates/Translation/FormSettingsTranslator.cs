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
    public class FormSettingsTranslator : TemplateTranslator
    {
        #region Data

        private const string MARKER_KEY = "FORMS_V";

        #endregion Data

        #region Properties

        public PokemonId Key { get { return FormSettings.pokemon; } }

        public FormSettings FormSettings { get; private set; }

        #endregion Properties

        #region ctor

        public FormSettingsTranslator(ItemTemplate itemTemplate)
            : base(itemTemplate.template_id)
        {
            FormSettings = itemTemplate.form_settings;
        }

        #endregion ctor
    }
}
