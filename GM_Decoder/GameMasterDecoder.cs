using System;
using System.IO;

using ProtoBuf;
using Newtonsoft.Json;
using VanOrman.Utils;

using GameMaster = POGOProtos.Networking.Responses.DownloadItemTemplatesResponse;

namespace VanOrman.PokemonGO.GAME_MASTER.Decoder
{
    [ProtoContract]
    public static class GameMasterDecoder
    {
        /// <summary>
        /// Static method to load a GAME_MASTER file.
        /// </summary>
        /// <param name="filePathGameMaster">Path to the GAME_MASTER file.</param>
        /// <returns></returns>
        /// <remarks>
        /// If a .json exists, it will be loaded.
        /// If not, the GAME_MASTER file will be loaded and the .json will be created.
        /// </remarks>
        public static GameMaster GetGameMaster(string filePathGameMaster)
        {
            string filePathJson = filePathGameMaster + ".json";

            if (File.Exists(filePathJson))
                return ReadGameMasterJson(filePathJson);
            else
            {
                GameMaster gameMaster = ReadGameMaster(filePathGameMaster);
                TimeStampUtils.FixFileTime(filePathGameMaster);
                WriteGameMasterJson(gameMaster, filePathJson);

                return gameMaster;
            }
        }

        /// <summary>
        /// Reads a GAME_MASTER file into an object.
        /// </summary>
        /// <param name="filePathGameMaster">Path to the GAME_MASTER file.</param>
        /// <returns>The GameMaster object.</returns>
        internal static GameMaster ReadGameMaster(string filePathGameMaster)
        {
            try
            {
				using (var stream = File.OpenRead(filePathGameMaster))
                    return Serializer.Deserialize<GameMaster>(stream);
            }
            catch (Exception ex)
            {
                ConsoleOutput.OutputException(ex, "Error decoding {0}:", filePathGameMaster);
            }

            return null;
        }

        /// <summary>
        /// Reads a GAME_MASTER .json file into an object .
        /// </summary>
        /// <param name="filePathJson">Path to the .json file.</param>
        /// <returns>The GameMaster object</returns>
        internal static GameMaster ReadGameMasterJson(string filePathJson)
        {
            try
            {
                using (var reader = new StreamReader(filePathJson))
                    return JsonConvert.DeserializeObject<GameMaster>(reader.ReadToEnd());
            }
            catch (Exception ex)
            {
                ConsoleOutput.OutputException(ex, "Error reading {0}:", filePathJson);
            }

            return null;
        }

        /// <summary>
        /// Writes the the current GameMaster object into a .json file.
        /// </summary>
        /// <param name="filePathJson"></param>
        internal static void WriteGameMasterJson(GameMaster gameMaster, string filePathJson)
        {
            using (StreamWriter writer = new StreamWriter(filePathJson))
                writer.Write(JsonConvert.SerializeObject(gameMaster, Formatting.Indented));

            TimeStampUtils.FixFileTime(filePathJson);
        }
    }
}
