using System;
using System.IO;

using GameMaster = POGOProtos.Networking.Responses.DownloadItemTemplatesResponse;

using VanOrman.PokemonGO.GAME_MASTER.Decoder;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates
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
			string rawFilePath = Path.Combine(Path.GetDirectoryName(filePath), FileName);
            HaveRawGameMaster = File.Exists(rawFilePath);
            GameMaster = GameMasterDecoder.GetGameMaster(rawFilePath);
        }

        #endregion ctor
    }
}
