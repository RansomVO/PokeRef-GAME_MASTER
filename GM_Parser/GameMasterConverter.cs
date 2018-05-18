using Newtonsoft.Json;
using System;
using System.IO;

using PokemonGO.GAME_MASTER.Templates;
using VanOrman.Utils;

namespace VanOrman.PokemonGO.GAME_MASTER.Parser
{
    static class GameMasterConverter
    {
        public static GameMaster Convert(string filePath)
        {
            GameMaster gameMaster = GetGameMaster(filePath);
            if (gameMaster == null)
                return null;

            FixFileTime(filePath);

            // Write out the .json file.
            using (TextWriter writer = new StreamWriter(filePath + ".json"))
                writer.WriteLine(JsonConvert.SerializeObject(gameMaster));

            FixFileTime(filePath + ".json");

            return gameMaster;
        }

        /// <summary>
        /// Read in the raw GAME_MASTER file and convert it.
        /// </summary>
        /// <param name="filePath"></param>
        /// <returns></returns>
        private static GameMaster GetGameMaster(string filePath)
        {
            Console.Out.WriteLine("TODO QZX: Write code to parse GAME_MASTER file and covert it to JSON.");

            return null;
        }

        #region Support Methods

        public static void FixFileTime(string filePath)
        {
            try
            {
                // Decode hex time stamp in the name of the file.
                string timeStampCode = Path.GetFileName(filePath).Split('_', '.')[0];
                long timeStampValue = long.Parse(timeStampCode, System.Globalization.NumberStyles.HexNumber);
                DateTime timeStamp = TimeStampUtils.TimestampToDateTime(timeStampValue);

                // Set the timestamps on the file.
                if (Directory.Exists(filePath))
                {
                    Directory.SetCreationTime(filePath, timeStamp);
                    Directory.SetLastWriteTime(filePath, timeStamp);
                    Directory.SetLastAccessTime(filePath, timeStamp);
                }
                else
                {
                    File.SetCreationTime(filePath, timeStamp);
                    File.SetLastWriteTime(filePath, timeStamp);
                    File.SetLastAccessTime(filePath, timeStamp);
                }
            }
            catch (Exception ex)
            {
                ConsoleOutput.OutputException(ex, "Error setting time stamp on {0}", filePath);
            }
        }


        #endregion Support Methods
    }
}