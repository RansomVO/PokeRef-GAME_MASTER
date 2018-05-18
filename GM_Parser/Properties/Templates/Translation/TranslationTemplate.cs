using System.Text;

namespace VanOrman.PokemonGO.GAME_MASTER.Parser.Templates
{
    public class TranslationTemplate
    {
        #region Properties

        public string TemplateId { get; private set; }

        public string Key { get; private set; }

        #endregion Properties

        #region ctor

        public TranslationTemplate(string templateId, string key)
        {
            TemplateId = templateId;
            Key = key;
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
