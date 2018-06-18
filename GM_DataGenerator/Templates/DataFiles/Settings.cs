using System;
using System.IO;
using System.Xml.Serialization;
using VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.DataFiles
{
    [Serializable]
    public class Settings
    {
        #region Properties

        [XmlAttribute(DataType = "date")]
        public DateTime last_updated { get; set; }

        [XmlElement]
        public _GameMasterStats GameMasterStats { get; set; }

        [XmlElement]
        public _Desirable Desirable { get; set; }

        [XmlElement]
        public Range MaxCP { get; set; }

        [XmlElement]
        public Range MaxHP { get; set; }

        [XmlElement]
        public Range DPSPercent { get; set; }

        [XmlElement]
        public Range DPS { get; set; }

        [XmlElement]
        public Difficulty Capture { get; set; }

        [XmlElement]
        public Rate Flee { get; set; }

        [XmlElement]
        public Rate Attack { get; set; }

        [XmlElement]
        public Rate Dodge { get; set; }

        #endregion Properties

        #region Internal classes

        [Serializable]
        public class _GameMasterStats
        {
            #region Properties

            [XmlAttribute(DataType = "date")]
            public DateTime last_updated { get; set; }

            [XmlAttribute]
            public int gens_total { get; set; }

            [XmlAttribute]
            public int gens_released { get; set; }

            [XmlElement]
            public _PokemonCount[] PokemonCount { get; set; }

            [XmlElement]
            public _MoveSets[] MoveSets { get; set; }

            #endregion Properties

            #region Internal classes

            [Serializable]
            public class _PokemonCount
            {
                [XmlAttribute]
                public int gen { get; set; }

                [XmlAttribute]
                public int total { get; set; }

                [XmlAttribute]
                public int available { get; set; }

                [XmlAttribute]
                public int available_wa { get; set; }
            }

            [Serializable]
            public class _MoveSets
            {
                [XmlAttribute]
                public int gen { get; set; }

                [XmlAttribute]
                public double dps_max { get; set; }

                [XmlAttribute]
                public double dps_avg { get; set; }

                [XmlAttribute]
                public double true_dps_max { get; set; }

                [XmlAttribute]
                public double true_dps_avg { get; set; }
            }

            #endregion Internal classes

            public _GameMasterStats() { }

            public _GameMasterStats(DateTime _last_updated, int _gens_total)
            {
                last_updated = _last_updated;
                gens_total = _gens_total;

                PokemonCount = new _PokemonCount[gens_total + 1];
                MoveSets = new _MoveSets[gens_total + 1];
                for (int i = 0; i <= gens_total; i++)
                {
                    PokemonCount[i] = new _PokemonCount() { gen = i };
                    MoveSets[i] = new _MoveSets() { gen = i };
                }
            }
        }

        [Serializable]
        public class _Desirable
        {
            [XmlAttribute]
            public int iv { get; set; }

            [XmlAttribute]
            public int attack { get; set; }

            [XmlAttribute]
            public int max_cp { get; set; }

            [XmlAttribute]
            public int base_dps { get; set; }

            public _Desirable() { }

            public _Desirable(Ranges._Desirability desirability)
            {
                iv = desirability.iv;
                attack = desirability.attack;
                max_cp = desirability.max_cp;
                base_dps = desirability.base_dps;
            }
        }

        [Serializable]
        public class Range
        {
            [XmlAttribute]
            public int great { get; set; }

            [XmlAttribute]
            public int good { get; set; }

            [XmlAttribute]
            public int okay { get; set; }

            public Range() { }

            public Range(Ranges._Benefit range)
            {
                great = range.great;
                good = range.good;
                okay = range.okay;
            }
        }

        [Serializable]
        public class Difficulty
        {
            [XmlAttribute]
            public int easy { get; set; }

            [XmlAttribute]
            public int moderate { get; set; }

            [XmlAttribute]
            public int difficult { get; set; }

            public Difficulty() { }

            public Difficulty(Ranges._Difficulty range)
            {
                easy = range.easy;
                moderate = range.moderate;
                difficult = range.difficult;
            }

            //public Difficulty(int _easy, int _moderate, int _difficult)
            //{
            //    easy = _easy;
            //    moderate = _moderate;
            //    difficult = _difficult;
            //}
        }

        [Serializable]
        public class Rate
        {
            [XmlAttribute]
            public int bad { get; set; }

            [XmlAttribute]
            public int okay { get; set; }

            [XmlAttribute]
            public int nice { get; set; }

            public Rate() { }

            public Rate(Ranges._Rate range)
            {
                bad = range.bad;
                okay = range.okay;
                nice = range.nice;
            }
        }

		#endregion Internal classes

		#region Writers

		private static string XmlFilePath { get { return Path.Combine(Utils.OutputDataFileFolder, "settings.xml"); } }

		/// <summary>
		/// Write the rarely-changed files that are referenced globally.
		/// </summary>
		public static void Write(Ranges ranges, GameMasterStatsCalculator gameMasterStatsCalculator)
		{
            DateTime updateDateTime = new DateTime(Math.Max(
                gameMasterStatsCalculator.GameMasterStats.last_updated.Date.Ticks,
                ranges.last_updated.Ticks));

            DateTime lastUpdated = Utils.GetLastUpdated(XmlFilePath);
			if (!File.Exists(XmlFilePath) || lastUpdated < updateDateTime)
			{
				Settings settings = new Settings()
				{
					last_updated = updateDateTime,
					GameMasterStats = gameMasterStatsCalculator.GameMasterStats,
					Desirable = new Settings._Desirable(ranges.Desirability),
					MaxCP = new Settings.Range(ranges.MaxCP),
					MaxHP = new Settings.Range(ranges.MaxHP),
					DPSPercent = new Settings.Range(ranges.DPSPercent),
					DPS = new Settings.Range(ranges.DPS),
					Capture = new Settings.Difficulty(ranges.CaptureRate),
					Flee = new Settings.Rate(ranges.FleeRate),
					Attack = new Settings.Rate(ranges.AttackRate),
					Dodge = new Settings.Rate(ranges.DodgeRate),
				};

				Utils.WriteXML(settings, XmlFilePath);
			}
		}

		#endregion Writers
	}
}

