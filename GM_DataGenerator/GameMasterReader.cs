using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using VanOrman.PokemonGO.GAME_MASTER.Decoder;
using VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator
{
    public class GameMasterReader
    {
        private const string GAME_MASTER_SEARCH_PATTERN = "00000*_GAME_MASTER.*";

        #region Properties

        /// <summary>
        /// 
        /// </summary>
        public GameMasterTemplate GameMasterTemplate { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public List<GameMasterTemplate> LegacyGameMasterTemplates
        {
            get
            {
                if (_legacyGameMasterTemplates == null)
                {
                    _legacyGameMasterTemplates = new List<GameMasterTemplate>();
                }

                return _legacyGameMasterTemplates;
            }
        }
        private List<GameMasterTemplate> _legacyGameMasterTemplates;

        #endregion Properties

        /// <summary>
        /// 
        /// </summary>
        /// <param name="inputPath"></param>
        public void Read(string inputPath)
        {
            // For each GAME_MASTER file in PokeRef, in order of timestamp:
            foreach (var file in GetFileList(inputPath))
            {
                GameMasterTemplate gameMasterTemplate = new GameMasterTemplate(file);
                if (gameMasterTemplate.GameMaster == null)
                    continue;

                if (GameMasterTemplate == null)
                    GameMasterTemplate = gameMasterTemplate;
                else
                    LegacyGameMasterTemplates.Add(gameMasterTemplate);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        private IEnumerable<string> GetFileList(string folder)
        {
            // Get the desirable filePaths.
            List<string> filePaths = new List<string>();
            foreach (var filePath in Directory.EnumerateFiles(folder, GAME_MASTER_SEARCH_PATTERN))
            {
                string fileName = Path.GetFileNameWithoutExtension(filePath);

                // Add it if the name contains a valid timestamp.
                if (Int64.TryParse(fileName.Substring(0, 16), System.Globalization.NumberStyles.HexNumber, null, out long dummy))
                {
                    string filePathGameMaster = Path.Combine(folder, fileName);
                    if (!filePaths.Contains(filePathGameMaster))
                        filePaths.Add(filePathGameMaster);
                }
            }

            // Sort by the HEX value on the front of the file name, adjusted for Mangling.
            return filePaths.OrderBy(filePath => 0 - GameMasterTimestampUtils.FileNameToDateTime(filePath).Ticks);
        }
    }
}