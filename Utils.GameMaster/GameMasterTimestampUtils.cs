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
        /// <remarks>
        /// All values should be in UTC.
        ///     UTC = PDT+7 : Daylight Savings Time (2nd Sunday in March    02:00 A.M. - 1st Sunday in November 02:00 A.M.)
        ///     UTC = PST+8 : Standard Time         (1st Sunday in November 02:00 A.M. - 2nd Sunday in March    02:00 A.M.)
        /// </remarks>
        private static readonly Mangle[] Mangles =
        {
            // MUST be sorted descending by Hex TimeStamp.
            new Mangle("00000168C0989967", new DateTime(2019, 01, 30, 14, 20, 0, DateTimeKind.Utc)),    // 2019-02-06 02:17:33.000 - 06 days, 11 hours, 57 minutes
            new Mangle("00000168C0989967", new DateTime(2018, 11, 15, 02, 54, 0, DateTimeKind.Utc)),    // 2018-12-09 16:51:54.000 - 24 days, 13 hours, 57 minutes
            new Mangle("0000016793E1E421", new DateTime(2018, 11, 15, 02, 54, 0, DateTimeKind.Utc)),    // 2018-12-09 16:51:54.000 - 24 days, 13 hours, 57 minutes
            new Mangle("00000167141C2339", new DateTime(2018, 11, 14, 19, 39, 0, DateTimeKind.Utc)),    // 2018-11-14 21:24:08.000 - 00 days, 01 hours, 45 minutes
            new Mangle("00000166BD9C0CEF", new DateTime(2018, 10, 16, 17, 26, 0, DateTimeKind.Utc)),    // 2018-10-29 02:16:53.000 - 12 days, 08 hours, 50 minutes
            new Mangle("000001669C9BE63D", new DateTime(2018, 10, 21, 21, 05, 0, DateTimeKind.Utc)),    // 2018-10-22 16:29:15.000 - 00 days, 19 hours, 24 minutes
            new Mangle("0000016652E7D261", new DateTime(2018, 09, 13, 22, 29, 0, DateTimeKind.Utc)),    // 2018-10-08 09:00:16.000 - 24 days, 19 hours, 38 minutes
            new Mangle("0000016630E4F72A", new DateTime(2018, 09, 26, 21, 43, 0, DateTimeKind.Utc)),    // 2018-10-01 18:30:04.000 - 04 days, 16 hours, 53 minutes
            new Mangle("00000166210182D7", new DateTime(2018, 09, 15, 22, 38, 0, DateTimeKind.Utc)),    // 2018-09-28 16:27:19.000 - 12 days, 17 hours, 49 minutes
            new Mangle("0000016571B7C311", new DateTime(2018, 08, 12, 21, 44, 0, DateTimeKind.Utc)),    // 2018-08-25 15:33:13.000 - 12 days, 17 hours, 49 minutes
            new Mangle("0000016531856AC9", new DateTime(2018, 08, 04, 11, 34, 0, DateTimeKind.Utc)),    // 2018-08-13 04:22:32.000 - 08 days, 16 hours, 48 minutes
            new Mangle("000001650A8B5966", new DateTime(2018, 07, 14, 18, 25, 0, DateTimeKind.Utc)),    // 2018-08-05 14:43:49.000 - 21 days, 20 hours, 18 minutes
			new Mangle("00000164D105FCB9", new DateTime(2018, 07, 06, 01, 01, 0, DateTimeKind.Utc)),    // 2018-07-25 10:39:48.000 - 19 days, 09 hours, 38 minutes
			new Mangle("0000016486ED5920", new DateTime(2018, 07, 08, 23, 32, 0, DateTimeKind.Utc)),    // 2018-07-11 01:20:59.000 - 02 days, 01 hours, 48 minutes
			new Mangle("0000016477739A1E", new DateTime(2018, 06, 15, 20, 13, 0, DateTimeKind.Utc)),    // 2018-07-08 01:13:42.000 - 22 days, 05 hours, 00 minutes
            new Mangle("0000016470644D97", new DateTime(2018, 06, 19, 21, 37, 0, DateTimeKind.Utc)),    // 2018-07-06 16:19:39.000 - 16 days, 18 hours, 42 minutes
            new Mangle("0000016447AC2253", new DateTime(2018, 06, 21, 18, 32, 0, DateTimeKind.Utc)),    // 2018-06-28 18:33:41.000 - 07 days, 00 hours, 01 minutes
            new Mangle("00000164159FEF31", new DateTime(2018, 06, 08, 22, 19, 0, DateTimeKind.Utc)),    // 2018-06-19 01:19:20.000 - 10 days, 03 hours, 00 minutes
            new Mangle("0000016322DEEA14", new DateTime(2018, 04, 22, 12, 00, 0, DateTimeKind.Utc)),    // 2018-05-02 22:00:24.000 - 10 days, 10 hours, 00 minutes
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
