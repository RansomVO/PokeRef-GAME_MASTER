using PokemonGO.GAME_MASTER.Templates;

namespace VanOrman.PokemonGO.GAME_MASTER.Parser.Templates
{
    public class GenderRatio : TranslationTemplate
    {
        public GenderSettings GenderSettings { get; private set; }

        #region Computed Properties

        public string Ratio
        {
            get
            {
                if (GenderSettings.gender.genderlessPercent == 1)
                    return "\"=GENDER_NEUTRAL\"";

                if (GenderSettings.gender.malePercent == 1)
                    return "\"=GENDER_MALE\"";

                if (GenderSettings.gender.femalePercent == 1)
                    return "\"=GENDER_FEMALE\"";

                if (GenderSettings.gender.malePercent > GenderSettings.gender.femalePercent)
                    return "=\"" + ((int)(GenderSettings.gender.malePercent / GenderSettings.gender.femalePercent)).ToString() + " : 1\"";

                return "=\"1 : " + ((int)(GenderSettings.gender.femalePercent / GenderSettings.gender.malePercent)).ToString() + "\"";
            }
        }

        #endregion Computed Properties

        #region ctor

        public GenderRatio(ItemTemplate itemTemplate)
            : base(itemTemplate.templateId, itemTemplate.genderSettings.pokemon)
        {
            GenderSettings = itemTemplate.genderSettings;
        }

        #endregion ctor
    }
}
