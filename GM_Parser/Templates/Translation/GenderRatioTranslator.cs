using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PokemonGO.GAME_MASTER.Templates;

namespace VanOrman.PokemonGO.GAME_MASTER.Parser.Templates
{
    public class GenderRatioTranslator : TemplateTranslator
    {

        public override string Key { get { return string.Equals(base.Key, "NIDORAN") ? GenderSettings.pokemon : base.Key; } }

        public GenderSettings GenderSettings { get; private set; }


        #region Computed Properties

        public string Ratio
        {
            get
            {
                if (GenderSettings.gender.genderlessPercent == 1)
                    return Constants.Gender.Neutral;

                if (GenderSettings.gender.malePercent == 1)
                    return Constants.Gender.Male;

                if (GenderSettings.gender.femalePercent == 1)
                    return Constants.Gender.Female;

                if (GenderSettings.gender.malePercent > GenderSettings.gender.femalePercent)
                    return ((int)(GenderSettings.gender.malePercent / GenderSettings.gender.femalePercent)).ToString() + " : 1";

                return "1 : " + ((int)(GenderSettings.gender.femalePercent / GenderSettings.gender.malePercent)).ToString();
            }
        }

        #endregion Computed Properties

        #region ctor

        public GenderRatioTranslator(ItemTemplate itemTemplate)
            : base(itemTemplate.templateId)
        {
            GenderSettings = itemTemplate.genderSettings;
        }

        #endregion ctor
    }
}
