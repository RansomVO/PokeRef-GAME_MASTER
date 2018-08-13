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

        /// <summary>
        /// Originally the TimeStamp on the GAME_MASTER files was correct.
        ///     Later they started setting them to times in the future, and the amount frequently changed.
        ///     This table is data I can use to "unmangle" the TimeStamps to get the correct date.
        /// </summary>
        private static readonly Mangle[] Mangles =
        {
            // MUST be sorted descending by Hex TimeStamp.
			new Mangle("0000016571B7C311", new DateTime(2018, 08, 12, 14, 44, 0, DateTimeKind.Local)),
            new Mangle("0000016531856AC9", new DateTime(2018, 08, 04, 04, 34, 0, DateTimeKind.Local)),
            new Mangle("000001650A8B5966", new DateTime(2018, 07, 14, 11, 25, 0, DateTimeKind.Local)),
			new Mangle("00000164D105FCB9", new DateTime(2018, 07, 05, 18, 01, 0, DateTimeKind.Local)),
			new Mangle("0000016486ED5920", new DateTime(2018, 07, 08, 16, 32, 0, DateTimeKind.Local)),
			new Mangle("0000016477739A1E", new DateTime(2018, 06, 15, 13, 13, 0, DateTimeKind.Local)),
            new Mangle("0000016470644D97", new DateTime(2018, 06, 19, 14, 37, 0, DateTimeKind.Local)),
            new Mangle("0000016447AC2253", new DateTime(2018, 06, 21, 11, 32, 0, DateTimeKind.Local)),
            new Mangle("00000164159FEF31", new DateTime(2018, 06, 08, 15, 19, 0, DateTimeKind.Local)),
            new Mangle("0000016322DEEA14", new DateTime(2018, 04, 22, 05, 00, 0, DateTimeKind.Local)),
        };

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
            return Path.GetFileName(fileName).Split('_')[0].Substring(5);
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
                ConsoleOutput.OutputException(ex, $"Error setting time stamp on {filePath}");
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
