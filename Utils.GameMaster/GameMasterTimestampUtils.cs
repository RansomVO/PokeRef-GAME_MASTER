using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;

using VanOrman.Utils;

namespace VanOrman.PokemonGO.GAME_MASTER
{
    public static class GameMasterTimestampUtils
    {
        public static void Init(string folder)
        {
            using (StreamReader reader = new StreamReader(Path.Combine(folder, "_mangles.csv")))
            {
                while (!reader.EndOfStream)
                {
                    string[] entry = reader.ReadLine().Split(',');
                    Mangles.Add(new Mangle(entry[0], entry[1]));
                }
            }
        }

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
        private static readonly List<Mangle> Mangles = new List<Mangle>();

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

            public Mangle(string gameMaster, string corrected) :
                this(gameMaster, DateTime.Parse(corrected))
            { }
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
