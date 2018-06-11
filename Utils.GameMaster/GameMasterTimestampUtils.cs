using System;
using System.Globalization;
using System.IO;

using VanOrman.Utils;

namespace VanOrman.PokemonGO.GAME_MASTER
{
    public static class GameMasterTimestampUtils
    {
        #region data

        private static readonly DateTime DateOfMangle = new DateTime(2018, 04, 30);
        private static readonly TimeSpan MangleAmount = new TimeSpan(250, 0, 0);

        #endregion data

        public static ulong DateTimeToTimestamp(DateTime dateTime)
        {
            return TimestampUtils.DateTimeToTimestamp(dateTime);
        }

        public static ulong HexTimeStampToTimeStamp(string hexTimeStamp)
        {
            return TimestampUtils.HexTimeStampToTimeStamp(hexTimeStamp);
        }

        public static DateTime TimestampToDateTime(ulong timeStamp)
        {
            return AdjustForMangle(TimestampUtils.TimestampToDateTime(timeStamp));
        }

        public static DateTime TimestampToDateTime(string timeStamp)
        {
            return AdjustForMangle(TimestampUtils.TimestampToDateTime(timeStamp));
        }

        public static string FileNameToHexTimeStamp(string fileName)
        {
            return fileName.Split('_')[0].Substring(5);
        }

        public static DateTime FileNameToDateTime(string fileName)
        {
            return TimestampToDateTime(HexTimeStampToTimeStamp(FileNameToHexTimeStamp(fileName)));
        }

        public static void FixGameMasterFileTime(string filePath)
        {
            try
            {
                // Decode hex time stamp in the name of the file.
                string timeStampCode = Path.GetFileName(filePath).Split('_', '.')[0];
                ulong timeStampValue = ulong.Parse(timeStampCode, System.Globalization.NumberStyles.HexNumber);
                DateTime timeStamp = TimestampToDateTime(timeStampValue);

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

        #region private Methods

        private static DateTime AdjustForMangle(DateTime value)
        {
            if (value > DateOfMangle)
                return value.Subtract(MangleAmount);

            return value;
        }

        #endregion private Methods
    }
}
