using System;
using System.IO;

using Newtonsoft.Json;

using PokemonGO.GAME_MASTER.Templates;
using VanOrman.Utils;

namespace VanOrman.PokemonGO.GAME_MASTER.Parser.Templates
{
    public class GameMasterTemplate
    {
        #region Properties

        public string FileName { get; set; }

        public bool HaveRawGameMaster { get; set; }

        public GameMaster GameMaster { get; set; }

        #endregion Properties

        #region ctor

        public GameMasterTemplate(string filePath)
        {
            FileName = Path.GetFileNameWithoutExtension(filePath);
            HaveRawGameMaster = File.Exists(Path.Combine(Path.GetDirectoryName(filePath), FileName));
            GameMaster = LoadGameMasterJson(filePath);
        }

        #endregion ctor

        /// <summary>
        /// 
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private GameMaster LoadGameMasterJson(string filePath)
        {
            try
            {
                using (TextReader reader = new StreamReader(filePath))
                {
                    string json = reader.ReadToEnd();
                    return JsonConvert.DeserializeObject<GameMaster>(json);
                }
            }
            catch (Exception ex)
            {
                ConsoleOutput.OutputException(ex, "Error in {0}:", filePath);
                return null;
            }
        }

    }
}
