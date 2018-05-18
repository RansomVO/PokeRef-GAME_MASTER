using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

using VanOrman.PokemonGO.GAME_MASTER.Parser.Templates;

namespace VanOrman.PokemonGO.GAME_MASTER.Parser
{
    public class GameMasterReader
    {
        private const string GAME_MASTER_SEARCH_PATTERN = "00000*_GAME_MASTER";

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
            // Make sure all of the GAME_MASTER files have been converted to .json.
            PerformConversions(inputPath);

            // For each GAME_MASTER file in PokeRef, in order of timestamp:
            IEnumerable<string> files = GetFileList(inputPath);
            foreach (var file in files)
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
        /// <param name="inputPath"></param>
        public void PerformConversions(string inputPath)
        {
            foreach (var filePath in Directory.EnumerateFiles(inputPath, GAME_MASTER_SEARCH_PATTERN))
            {
                if (!File.Exists(filePath + ".json"))
                    GameMasterConverter.Convert(filePath);
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
            foreach (var filePath in Directory.EnumerateFiles(folder, GAME_MASTER_SEARCH_PATTERN + ".json"))
            {
                // Add it if the name contains a valid timestamp.
                long dummy;
                if (Int64.TryParse(Path.GetFileName(filePath).Substring(0, 16), System.Globalization.NumberStyles.HexNumber, null, out dummy))
                    filePaths.Add(filePath);
            }

            // Sort by the HEX value on the front of the file name.
            return filePaths.OrderBy(filePath => 0 - (Int64.Parse(Path.GetFileName(filePath).Substring(0, 16), System.Globalization.NumberStyles.HexNumber)));
        }
    }
}