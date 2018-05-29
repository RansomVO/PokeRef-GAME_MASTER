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
    public class GenderRatioTranslator : TemplateTranslator
    {

        public PokemonId Key { get { return GenderSettings.pokemon; } }

        public GenderSettings GenderSettings { get; private set; }


        #region Computed Properties

        public string Ratio
        {
            get
            {
                if (GenderSettings.gender.genderless_percent == 1)
                    return PokeConstants.Gender.Neutral;

                if (GenderSettings.gender.male_percent == 1)
                    return PokeConstants.Gender.Male;

                if (GenderSettings.gender.female_percent == 1)
                    return PokeConstants.Gender.Female;

                if (GenderSettings.gender.male_percent > GenderSettings.gender.female_percent)
                    return ((int)(GenderSettings.gender.male_percent / GenderSettings.gender.female_percent)).ToString() + " : 1";

                return "1 : " + ((int)(GenderSettings.gender.female_percent / GenderSettings.gender.male_percent)).ToString();
            }
        }

        #endregion Computed Properties

        #region ctor

        public GenderRatioTranslator(ItemTemplate itemTemplate)
            : base(itemTemplate.template_id)
        {
            GenderSettings = itemTemplate.gender_settings;
        }

        #endregion ctor
    }
}
