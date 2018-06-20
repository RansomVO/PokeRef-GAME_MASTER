using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

using VanOrman.Utils;

namespace VanOrman.PokemonGO.GAME_MASTER
{
    public static class GameMasterTimestampUtils
    {
        #region data

        private class Mangle
        {
            public DateTime Start { get; set; }

            private DateTime Corrected { get; set; }

            public TimeSpan Amount { get { return Start.ToUniversalTime() - Corrected.ToUniversalTime(); } }

            public Mangle(string gameMaster, DateTime corrected)
            {
                Start = TimestampUtils.HexTimeStampToDateTime(gameMaster);
                Corrected = corrected;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private static readonly Mangle[] Mangles =
        {
            // MUST be sorted descending by Hex TimeStamp.
            new Mangle("0000016477739A1E", new DateTime(2018, 06, 15, 13, 13, 0, DateTimeKind.Local)),
            new Mangle("0000016470644D97", new DateTime(2018, 06, 19, 14, 37, 0, DateTimeKind.Local)),
            new Mangle("00000164159FEF31", new DateTime(2018, 06, 08, 15, 19, 0, DateTimeKind.Local)),
            new Mangle("0000016322DEEA14", new DateTime(2018, 04, 22, 05, 00, 0, DateTimeKind.Local)),
        };

        #endregion data


        public static ulong DateTimeToTicks(DateTime dateTime)
        {
            return TimestampUtils.DateTimeToTicks(dateTime);
        }

        public static ulong HexTimeStampToTicks(string hexTimeStamp)
        {
            return TimestampUtils.HexTimeStampToTicks(hexTimeStamp);
        }

        public static DateTime TicksToDateTime(ulong timeStamp)
        {
            return AdjustForMangle(TimestampUtils.TicksToDateTime(timeStamp));
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
            return TicksToDateTime(HexTimeStampToTicks(FileNameToHexTimeStamp(fileName)));
        }

        public static void FixGameMasterFileTime(string filePath)
        {
            try
            {
                // Decode hex time stamp in the name of the file.
                string timeStampCode = Path.GetFileName(filePath).Split('_', '.')[0];
                ulong timeStampValue = ulong.Parse(timeStampCode, System.Globalization.NumberStyles.HexNumber);
                DateTime timeStamp = TicksToDateTime(timeStampValue).ToLocalTime();

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
            foreach (var mangle in Mangles)
                if (value >= mangle.Start)
                    return value.Subtract(mangle.Amount);

            return value;
        }

        #endregion private Methods
    }
}
