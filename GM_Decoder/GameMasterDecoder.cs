using System;
using System.IO;

using ProtoBuf;
using Newtonsoft.Json;

using VanOrman.Utils;

using VanOrman.PokemonGO.GAME_MASTER.Old;
using GameMaster = POGOProtos.Networking.Responses.DownloadItemTemplatesResponse;
#if MULTI_PROTO
using GameMaster_v2_27_0 = POGOProtos.v2_27_0.Networking.Responses.DownloadItemTemplatesResponse;
using GameMaster_v2_9_1 = POGOProtos.v2_9_1.Networking.Responses.DownloadItemTemplatesResponse;
using GameMaster_v2_9_0 = POGOProtos.v2_9_0.Networking.Responses.DownloadItemTemplatesResponse;
using GameMaster_v2_8_0 = POGOProtos.v2_8_0.Networking.Responses.DownloadItemTemplatesResponse;
using GameMaster_v2_7_0 = POGOProtos.v2_7_0.Networking.Responses.DownloadItemTemplatesResponse;
using GameMaster_v2_6_1 = POGOProtos.v2_6_1.Networking.Responses.DownloadItemTemplatesResponse;
using GameMaster_v2_6_0 = POGOProtos.v2_6_0.Networking.Responses.DownloadItemTemplatesResponse;
using GameMaster_v2_5_0 = POGOProtos.v2_5_0.Networking.Responses.DownloadItemTemplatesResponse;
using GameMaster_v2_5_1 = POGOProtos.v2_5_1.Networking.Responses.DownloadItemTemplatesResponse;
using GameMaster_v2_4_2 = POGOProtos.v2_4_2.Networking.Responses.DownloadItemTemplatesResponse;
using GameMaster_v2_4_1 = POGOProtos.v2_4_1.Networking.Responses.DownloadItemTemplatesResponse;
using GameMaster_v2_4_0 = POGOProtos.v2_4_0.Networking.Responses.DownloadItemTemplatesResponse;
using GameMaster_v2_3_0 = POGOProtos.v2_3_0.Networking.Responses.DownloadItemTemplatesResponse;
using GameMaster_v2_2_0 = POGOProtos.v2_2_0.Networking.Responses.DownloadItemTemplatesResponse;
using GameMaster_v2_1_0 = POGOProtos.v2_1_0.Networking.Responses.DownloadItemTemplatesResponse;
using GameMaster_v2_0_2 = POGOProtos.v2_0_2.Networking.Responses.DownloadItemTemplatesResponse;
using GameMaster_v2_0_1 = POGOProtos.v2_0_1.Networking.Responses.DownloadItemTemplatesResponse;
using GameMaster_v2_0_0 = POGOProtos.v2_0_0.Networking.Responses.DownloadItemTemplatesResponse;
#endif

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
                if (gameMaster != null)
                {
                    GameMasterTimestampUtils.FixGameMasterFileTime(filePathGameMaster);
                    WriteGameMasterJson(gameMaster, filePathJson);
                }

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
#if MULTI_PROTO
            catch (Exception)
            { }

            try
            {
                ConsoleOutput.OutputWarning($"Error decoding {filePathGameMaster}. Attempting v2.27.0");
                using (var stream = File.OpenRead(filePathGameMaster))
                    return Serializer.ChangeType<GameMaster_v2_27_0, GameMaster>(Serializer.Deserialize<GameMaster_v2_27_0>(stream));
            }
            catch (Exception)
            { }

            try
            {
                ConsoleOutput.OutputWarning($"Error decoding {filePathGameMaster}. Attempting v2.9.1");
                using (var stream = File.OpenRead(filePathGameMaster))
                    return Serializer.ChangeType<GameMaster_v2_9_1, GameMaster>(Serializer.Deserialize<GameMaster_v2_9_1>(stream));
            }
            catch (Exception)
            { }

            try
            {
                ConsoleOutput.OutputWarning($"Error decoding {filePathGameMaster}. Attempting v2.9.0");
                using (var stream = File.OpenRead(filePathGameMaster))
                    return Serializer.ChangeType<GameMaster_v2_9_0, GameMaster>(Serializer.Deserialize<GameMaster_v2_9_0>(stream));
            }
            catch (Exception)
            { }

            try
            {
                ConsoleOutput.OutputWarning($"Error decoding {filePathGameMaster}. Attempting v2.8.0");
                using (var stream = File.OpenRead(filePathGameMaster))
                    return Serializer.ChangeType<GameMaster_v2_8_0, GameMaster>(Serializer.Deserialize<GameMaster_v2_8_0>(stream));
            }
            catch (Exception)
            { }

            try
            {
                ConsoleOutput.OutputWarning($"Error decoding {filePathGameMaster}. Attempting v2.7.0");
                using (var stream = File.OpenRead(filePathGameMaster))
                    return Serializer.ChangeType<GameMaster_v2_7_0, GameMaster>(Serializer.Deserialize<GameMaster_v2_7_0>(stream));
            }
            catch (Exception)
            { }

            try
            {
                ConsoleOutput.OutputWarning($"Error decoding {filePathGameMaster}. Attempting v2.6.1");
                using (var stream = File.OpenRead(filePathGameMaster))
                    return Serializer.ChangeType<GameMaster_v2_6_1, GameMaster>(Serializer.Deserialize<GameMaster_v2_6_1>(stream));
            }
            catch (Exception)
            { }
            try
            {
                ConsoleOutput.OutputWarning($"Error decoding {filePathGameMaster}. Attempting v2.6.0");
                using (var stream = File.OpenRead(filePathGameMaster))
                    return Serializer.ChangeType<GameMaster_v2_6_0, GameMaster>(Serializer.Deserialize<GameMaster_v2_6_0>(stream));
            }
            catch (Exception)
            { }
            try
            {
                ConsoleOutput.OutputWarning($"Error decoding {filePathGameMaster}. Attempting v2.5.1");
                using (var stream = File.OpenRead(filePathGameMaster))
                    return Serializer.ChangeType<GameMaster_v2_5_1, GameMaster>(Serializer.Deserialize<GameMaster_v2_5_1>(stream));
            }
            catch (Exception)
            { }

            try
            {
                ConsoleOutput.OutputWarning($"Error decoding {filePathGameMaster}. Attempting v2.5.0");
                using (var stream = File.OpenRead(filePathGameMaster))
                    return Serializer.ChangeType<GameMaster_v2_5_0, GameMaster>(Serializer.Deserialize<GameMaster_v2_5_0>(stream));
            }
            catch (Exception)
            { }

            try
            {
                ConsoleOutput.OutputWarning($"Error decoding {filePathGameMaster}. Attempting v2.4.2");
                using (var stream = File.OpenRead(filePathGameMaster))
                    return Serializer.ChangeType<GameMaster_v2_4_2, GameMaster>(Serializer.Deserialize<GameMaster_v2_4_2>(stream));
            }
            catch (Exception)
            { }


            try
            {
                ConsoleOutput.OutputWarning($"Error decoding {filePathGameMaster}. Attempting v2.4.1");
                using (var stream = File.OpenRead(filePathGameMaster))
                    return Serializer.ChangeType<GameMaster_v2_4_1, GameMaster>(Serializer.Deserialize<GameMaster_v2_4_1>(stream));
            }
            catch (Exception)
            { }

            try
            {
                ConsoleOutput.OutputWarning($"Error decoding {filePathGameMaster}. Attempting v2.4.0");
                using (var stream = File.OpenRead(filePathGameMaster))
                    return Serializer.ChangeType<GameMaster_v2_4_0, GameMaster>(Serializer.Deserialize<GameMaster_v2_4_0>(stream));
            }
            catch (Exception)
            { }

            try
            {
                ConsoleOutput.OutputWarning($"Error decoding {filePathGameMaster}. Attempting v2.3.0");
                using (var stream = File.OpenRead(filePathGameMaster))
                    return Serializer.ChangeType<GameMaster_v2_3_0, GameMaster>(Serializer.Deserialize<GameMaster_v2_3_0>(stream));
            }
            catch (Exception)
            { }

            try
            {
                ConsoleOutput.OutputWarning($"Error decoding {filePathGameMaster}. Attempting v2.3.0");
                using (var stream = File.OpenRead(filePathGameMaster))
                    return Serializer.ChangeType<GameMaster_v2_3_0, GameMaster>(Serializer.Deserialize<GameMaster_v2_3_0>(stream));
            }
            catch (Exception)
            { }

            try
            {
                ConsoleOutput.OutputWarning($"Error decoding {filePathGameMaster}. Attempting v2.2.0");
                using (var stream = File.OpenRead(filePathGameMaster))
                    return Serializer.ChangeType<GameMaster_v2_2_0, GameMaster>(Serializer.Deserialize<GameMaster_v2_2_0>(stream));
            }
            catch (Exception)
            { }

            try
            {
                ConsoleOutput.OutputWarning($"Error decoding {filePathGameMaster}. Attempting v2.1.0");
                using (var stream = File.OpenRead(filePathGameMaster))
                    return Serializer.ChangeType<GameMaster_v2_1_0, GameMaster>(Serializer.Deserialize<GameMaster_v2_1_0>(stream));
            }
            catch (Exception)
            { }

            try
            {
                ConsoleOutput.OutputWarning($"Error decoding {filePathGameMaster}. Attempting v2.0.2");
                using (var stream = File.OpenRead(filePathGameMaster))
                    return Serializer.ChangeType<GameMaster_v2_0_2, GameMaster>(Serializer.Deserialize<GameMaster_v2_0_2>(stream));
            }
            catch (Exception)
            { }

            try
            {
                ConsoleOutput.OutputWarning($"Error decoding {filePathGameMaster}. Attempting v2.0.1");
                using (var stream = File.OpenRead(filePathGameMaster))
                    return Serializer.ChangeType<GameMaster_v2_0_1, GameMaster>(Serializer.Deserialize<GameMaster_v2_0_1>(stream));
            }
            catch (Exception)
            { }

            try
            {
                ConsoleOutput.OutputWarning($"Error decoding {filePathGameMaster}. Attempting v2.0.0");
                using (var stream = File.OpenRead(filePathGameMaster))
                    return Serializer.ChangeType<GameMaster_v2_0_0, GameMaster>(Serializer.Deserialize<GameMaster_v2_0_0>(stream));
            }
#endif
            catch (Exception ex)
            {
                ConsoleOutput.OutputException(ex, $"Error decoding {filePathGameMaster}:");
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
                GameMaster gameMaster;
                using (var reader = new StreamReader(filePathJson))
                    gameMaster = JsonConvert.DeserializeObject<GameMaster>(reader.ReadToEnd());

                if (gameMaster.item_templates.Count == 0)
                    gameMaster = FixGameMaster(filePathJson);

                return gameMaster;
            }
            catch (Exception ex)
            {
                ConsoleOutput.OutputException(ex, "Error reading {0}:", filePathJson);
            }

            return null;
        }

        /// <summary>
        /// Writes the the GameMaster object into a .json file.
        /// </summary>
        /// <param name="filePathJson"></param>
        internal static void WriteGameMasterJson(GameMaster gameMaster, string filePathJson)
        {
            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                Formatting = Formatting.Indented,
            };

            using (StreamWriter writer = new StreamWriter(filePathJson))
                writer.Write(JsonConvert.SerializeObject(gameMaster, settings));

            GameMasterTimestampUtils.FixGameMasterFileTime(filePathJson);
        }

        /// <summary>
        /// Writes the the current GameMaster object into a .json file.
        /// </summary>
        /// <param name="filePathJson"></param>
        internal static void WriteGameMasterJson(string filePathGameMaster)
        {
            GameMaster gameMaster = GameMasterDecoder.ReadGameMaster(filePathGameMaster);
            WriteGameMasterJson(gameMaster, filePathGameMaster + ".json");
        }

        private static GameMaster FixGameMaster(string filePathJson)
        {
            string holdFolder = Path.Combine(Path.GetDirectoryName(filePathJson), "_Hold");
            if (!Directory.Exists(holdFolder))
                Directory.CreateDirectory(holdFolder);

            string rename = Path.Combine(holdFolder, Path.GetFileName(filePathJson));
            File.Move(filePathJson, rename);

            GameMaster gameMaster;
            using (StreamReader reader = new StreamReader(rename))
                gameMaster = JsonConvert.DeserializeObject<GameMaster_Old>(reader.ReadToEnd()).Convert();

            WriteGameMasterJson(gameMaster, filePathJson);

            return gameMaster;
        }
    }
}
