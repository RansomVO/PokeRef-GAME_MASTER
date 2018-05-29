using System.Collections.Generic;
using System.Text;
using System.Text.RegularExpressions;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates
{
    public class TemplateTranslator
    {
        #region Data

        public static readonly KeyValuePair<string, int>[] MARKERS_TEMPLATE_KEY_ = new[]
        {
            new KeyValuePair<string, int>("AVATAR_", -1),                   //  avatarCustomization
            new KeyValuePair<string, int>("BADGE_", -1),                    //  badgeSettings
            new KeyValuePair<string, int>("BATTLE_SETTINGS", 0),            //  battleSettings
            new KeyValuePair<string, int>("ENCOUNTER_SETTINGS", 0),         //  encounterSettings
            new KeyValuePair<string, int>("FORMS_V...._POKEMON_", -1),      //  formSettings
            new KeyValuePair<string, int>("GYM_BADGE_SETTINGS", 0),         //  gymBadgeSettings
            new KeyValuePair<string, int>("GYM_LEVEL_SETTINGS", 0),         //  gymLevel
            new KeyValuePair<string, int>("IAP_SETTINGS", 0),               //  iapSettings
            new KeyValuePair<string, int>("ITEM_", -1),                     //  itemSettings
            new KeyValuePair<string, int>("PLAYER_LEVEL_SETTINGS", 0),      //  playerLevel
            new KeyValuePair<string, int>("POKEMON_TYPE_", -1),             //  typeEffective
            new KeyValuePair<string, int>("POKEMON_UPGRADE_SETTINGS", -1),  //  pokemonUpgrades
            new KeyValuePair<string, int>("QUEST_", -1),                    //  questSettings
            new KeyValuePair<string, int>("SPAWN_V...._POKEMON_", 20),      //  genderSettings
            new KeyValuePair<string, int>("V...._POKEMON_", 14),            //  pokemonSettings
            new KeyValuePair<string, int>("V...._MOVE_", -1),               //  moveSettings
            new KeyValuePair<string, int>("camera_", -1),                   //  camera
            new KeyValuePair<string, int>("sequence_", -1),                 //  moveSequenceSettings

            //  iapItemDisplay
            new KeyValuePair<string, int>("android.test.canceled", 0),
            new KeyValuePair<string, int>("android.test.item_unavailable", 0),
            new KeyValuePair<string, int>("android.test.purchased", 0),
            new KeyValuePair<string, int>("incenseordinary.1", 0),
            new KeyValuePair<string, int>("incenseordinary.4", 0),
            new KeyValuePair<string, int>("incenseordinary.8", 0),
            new KeyValuePair<string, int>("incenseordinary.25", 0),
            new KeyValuePair<string, int>("incubatorbasic.1", 0),
            new KeyValuePair<string, int>("incubatorbasic.100", 0),
            new KeyValuePair<string, int>("itemstorageupgrade.1", 0),
            new KeyValuePair<string, int>("luckyegg.1", 0),
            new KeyValuePair<string, int>("luckyegg.5", 0),
            new KeyValuePair<string, int>("luckyegg.20", 0),
            new KeyValuePair<string, int>("luckyegg.25", 0),
            new KeyValuePair<string, int>("maxpotion.10", 0),
            new KeyValuePair<string, int>("maxrevive.6", 0),
            new KeyValuePair<string, int>("pokeball.20", 0),
            new KeyValuePair<string, int>("pokeball.100", 0),
            new KeyValuePair<string, int>("pokecoin.100", 0),
            new KeyValuePair<string, int>("pokecoin.550", 0),
            new KeyValuePair<string, int>("pokecoin.1000", 0),
            new KeyValuePair<string, int>("pokecoin.1200", 0),
            new KeyValuePair<string, int>("pokecoin.2500", 0),
            new KeyValuePair<string, int>("pokecoin.5200", 0),
            new KeyValuePair<string, int>("pokecoin.14500", 0),
            new KeyValuePair<string, int>("pokemonstorageupgrade.1", 0),

            // (Empty/Unused)
            new KeyValuePair<string, int>("IAP_CATEGORY_BUNDLE", 0),
            new KeyValuePair<string, int>("IAP_CATEGORY_ITEMS", 0),
            new KeyValuePair<string, int>("IAP_CATEGORY_POKECOINS", 0),
            new KeyValuePair<string, int>("IAP_CATEGORY_UPGRADES", 0),
            new KeyValuePair<string, int>("POKEMON_SCALE_SETTINGS_", 0),
            new KeyValuePair<string, int>("WEATHER_AFFINITY_", -1),
            new KeyValuePair<string, int>("WEATHER_BONUS_SETTINGS", 0),
        };

        #endregion Data

        #region Properties

        public string TemplateId { get; private set; }

        #endregion Properties

        #region ctor

        public TemplateTranslator(string templateId)
        {
            TemplateId = templateId;
        }

        #endregion ctor

        public static string FixName(string rawName)
        {
            if (string.IsNullOrWhiteSpace(rawName))
                return string.Empty;

            StringBuilder stringBuilder = new StringBuilder();

            foreach (var section in rawName.Split(' ', '_'))
                stringBuilder.Append(char.ToUpper(section[0]) + section.Substring(1).ToLower() + " ");

            return stringBuilder.ToString().Trim();
        }
    }
}
