using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.DataFiles
{
    [Serializable]
    public class Constants
    {
        #region Properties

        [XmlAttribute(DataType = "date")]
        public DateTime last_updated { get; set; }

        [XmlElement]
        public _Gender Gender { get; set; }

        [XmlElement]
        public _Types Types { get; set; }

        [XmlElement]
        public _Weathers Weathers { get; set; }

        [XmlElement]
        public _Regions Regions { get; set; }

        [XmlElement]
        public _Availability Availability { get; set; }

        [XmlElement]
        public _Rarity Rarity { get; set; }

        [XmlElement]
        public _IV_Evaluation IV_Evaluation { get; set; }

        [XmlElement]
        public _Mappings Mappings { get; set; }

        [XmlElement]
        public _NumericChars NumericChars { get; set; }

        [XmlElement]
        public _CPMultipliers CPMultipliers { get; set; }

        #endregion Properties

        #region Internal classes

        [Serializable]
        public class _Gender
        {
            [XmlAttribute]
            public string male { get { return PokeConstants.Gender.Male; } set { } }

            [XmlAttribute]
            public string female { get { return PokeConstants.Gender.Female; } set { } }

            [XmlAttribute]
            public string neutral { get { return PokeConstants.Gender.Neutral; } set { } }
        }

        [Serializable]
        public class _Types
        {
            [XmlElement]
            public string[] Type { get; set; }

            public _Types()
            {
                Type = PokeConstants.GetValues(typeof(PokeConstants.PokeType));
            }
        }

        [Serializable]
        public class _Weathers
        {
            [XmlElement]
            public string[] Weather { get; set; }

            public _Weathers()
            {
                Weather = new[]
                {
                    PokeConstants.Weather.Unknown,
                    PokeConstants.Weather.Rainy,
                    PokeConstants.Weather.Fog,
                    PokeConstants.Weather.Windy,
                    PokeConstants.Weather.Cloudy,
                    PokeConstants.Weather.Sunny,
                    PokeConstants.Weather.Snow,
                    PokeConstants.Weather.PartlyCloudy,
                };
            }
        }

        [Serializable]
        public class _Regions
        {
            [XmlElement]
            public _Region[] Region { get; set; }

            #region Internal classes

            [Serializable]
            public class _Region
            {
                [XmlAttribute]
                public int gen { get; set; }

                [XmlAttribute]
                public string name { get; set; }

                [XmlAttribute]
                public int rangeMin { get; set; }

                [XmlAttribute]
                public int rangeMax { get; set; }

                public _Region()
                { }

                public _Region(int _gen)
                {
                    gen = _gen;
                    name = PokeConstants.Regions[gen];
                    rangeMin = PokeConstants.GenerationRanges[gen][0];
                    rangeMax = PokeConstants.GenerationRanges[gen][1];
                }
            }

            public _Regions()
            {
                Region = new[]
                {
                    null,
                    new _Region(1),
                    new _Region(2),
                    new _Region(3),
                    new _Region(4),
                    new _Region(5),
                    new _Region(6),
                    new _Region(7),
                    new _Region(8),
                };
            }

            #endregion Internal classes
        }

        [Serializable]
        public class _Availability
        {
            [XmlAttribute]
            public string Unreleased { get { return PokeConstants.Availability.Unreleased; } set { } }

            [XmlAttribute]
            public string General { get { return PokeConstants.Availability.General; } set { } }

            [XmlAttribute]
            public string Regional { get { return PokeConstants.Availability.Regional; } set { } }

            [XmlAttribute]
            public string RaidBossOnly { get { return PokeConstants.Availability.RaidBossOnly; } set { } }

            [XmlAttribute]
            public string RaidBossOnly_EX { get { return PokeConstants.Availability.EXRaidBossOnly; } set { } }

            [XmlAttribute]
            public string HatchOnly_2K { get { return string.Format(PokeConstants.Availability.HatchOnlyFormat, 2); } set { } }

            [XmlAttribute]
            public string HatchOnly_5K { get { return string.Format(PokeConstants.Availability.HatchOnlyFormat, 5); } set { } }

            [XmlAttribute]
            public string HatchOnly_7K { get { return string.Format(PokeConstants.Availability.HatchOnlyFormat, 7); } set { } }

            [XmlAttribute]
            public string HatchOnly_10K { get { return string.Format(PokeConstants.Availability.HatchOnlyFormat, 10); } set { } }

            [XmlAttribute]
            public string EvolveOnly { get { return PokeConstants.Availability.EvolveOnly; } set { } }
        }

        [Serializable]
        public class _Rarity
        {
            [XmlAttribute]
            public string Legendary { get { return PokeConstants.Rarity.Legendary; } set { } }

            [XmlAttribute]
            public string Mythic { get { return PokeConstants.Rarity.Mythic; } set { } }

            [XmlAttribute]
            public string UltraBeast { get { return PokeConstants.Rarity.UltraBeast; } set { } }
        }

        [Serializable]
        public class _IV_Evaluation
        {
            [XmlElement]
            public _Overall Overall { get; set; }

            [XmlElement]
            public _Attribute Attribute { get; set; }

            #region Internal classes

            public class IV_Range
            {
                #region Properties

                [XmlAttribute]
                public int min { get; set; }

                [XmlAttribute]
                public int max { get; set; }

                #endregion Properties

                #region ctor

                public IV_Range() { }

                public IV_Range(int _min, int _max)
                {
                    min = _min;
                    max = _max;
                }

                #endregion ctor
            }

            [Serializable]
            public class _Overall
            {
                [XmlAttribute]
                public int max { get; set; }

                [XmlElement]
                public IV_Range NotGreat { get; set; }

                [XmlElement]
                public IV_Range Decent { get; set; }

                [XmlElement]
                public IV_Range Strong { get; set; }

                [XmlElement]
                public IV_Range Amazes { get; set; }

                public _Overall()
                {
                    max = PokeConstants.Evaluation.IV.Max;
                    NotGreat = new IV_Range(PokeConstants.Evaluation.IV.NotGreat.Min, PokeConstants.Evaluation.IV.NotGreat.Max);
                    Decent = new IV_Range(PokeConstants.Evaluation.IV.Decent.Min, PokeConstants.Evaluation.IV.Decent.Max);
                    Strong = new IV_Range(PokeConstants.Evaluation.IV.Strong.Min, PokeConstants.Evaluation.IV.Strong.Max);
                    Amazes = new IV_Range(PokeConstants.Evaluation.IV.Amazes.Min, PokeConstants.Evaluation.IV.Amazes.Max);
                }
            }

            [Serializable]
            public class _Attribute
            {
                [XmlAttribute]
                public int max { get; set; }

                [XmlElement]
                public IV_Range NotGreatness { get; set; }

                [XmlElement]
                public IV_Range JobDone { get; set; }

                [XmlElement]
                public IV_Range Excellent { get; set; }

                [XmlElement]
                public IV_Range Wow { get; set; }

                public _Attribute()
                {
                    max = PokeConstants.Evaluation.Attribute.Max;
                    NotGreatness = new IV_Range(PokeConstants.Evaluation.Attribute.NotGreatness.Min, PokeConstants.Evaluation.Attribute.NotGreatness.Max);
                    JobDone = new IV_Range(PokeConstants.Evaluation.Attribute.JobDone.Min, PokeConstants.Evaluation.Attribute.JobDone.Max);
                    Excellent = new IV_Range(PokeConstants.Evaluation.Attribute.Excellent.Min, PokeConstants.Evaluation.Attribute.Excellent.Max);
                    Wow = new IV_Range(PokeConstants.Evaluation.Attribute.Wow.Min, PokeConstants.Evaluation.Attribute.Wow.Max);
                }
            }

            #endregion Internal classes

            public _IV_Evaluation()
            {
                Overall = new _Overall();
                Attribute = new _Attribute();
            }
        }

        [Serializable]
        public class _Mappings
        {
            [XmlElement]
            public _WeatherBoost[] WeatherBoost { get; set; }

            #region Internal classes

            [Serializable]
            public class _WeatherBoost
            {
                [XmlAttribute]
                public string type { get; set; }

                [XmlAttribute]
                public string boost { get; set; }

                public _WeatherBoost() { }

                public _WeatherBoost(string _type, string _boost)
                {
                    type = _type;
                    boost = _boost;
                }
            }

            #endregion Internal classes

            public _Mappings()
            {
                List<_WeatherBoost> weatherBoosts = new List<_WeatherBoost>();
                foreach (var weatherBoost in PokeConstants.WeatherBoosts)
                    weatherBoosts.Add(new _WeatherBoost(weatherBoost.Key.ToString(), weatherBoost.Value.ToString()));

                WeatherBoost = weatherBoosts.ToArray();
            }
        }

        [Serializable]
        public class _NumericChars
        {
            [XmlElement]
            public _NumericChar[] NumericChar = new _NumericChar[]
            {
                new _NumericChar(00, PokeConstants.NumericChars._00),
                new _NumericChar(01, PokeConstants.NumericChars._01),
                new _NumericChar(02, PokeConstants.NumericChars._02),
                new _NumericChar(03, PokeConstants.NumericChars._03),
                new _NumericChar(04, PokeConstants.NumericChars._04),
                new _NumericChar(05, PokeConstants.NumericChars._05),
                new _NumericChar(06, PokeConstants.NumericChars._06),
                new _NumericChar(07, PokeConstants.NumericChars._07),
                new _NumericChar(08, PokeConstants.NumericChars._08),
                new _NumericChar(09, PokeConstants.NumericChars._09),
                new _NumericChar(10, PokeConstants.NumericChars._10),
                new _NumericChar(11, PokeConstants.NumericChars._11),
                new _NumericChar(12, PokeConstants.NumericChars._12),
                new _NumericChar(13, PokeConstants.NumericChars._13),
                new _NumericChar(14, PokeConstants.NumericChars._14),
                new _NumericChar(15, PokeConstants.NumericChars._15),
                new _NumericChar(16, PokeConstants.NumericChars._16),
                new _NumericChar(17, PokeConstants.NumericChars._17),
                new _NumericChar(18, PokeConstants.NumericChars._18),
                new _NumericChar(19, PokeConstants.NumericChars._19),
                new _NumericChar(20, PokeConstants.NumericChars._20),
                new _NumericChar(21, PokeConstants.NumericChars._21),
                new _NumericChar(22, PokeConstants.NumericChars._22),
                new _NumericChar(23, PokeConstants.NumericChars._23),
                new _NumericChar(24, PokeConstants.NumericChars._24),
                new _NumericChar(25, PokeConstants.NumericChars._25),
                new _NumericChar(26, PokeConstants.NumericChars._26),
                new _NumericChar(27, PokeConstants.NumericChars._27),
                new _NumericChar(28, PokeConstants.NumericChars._28),
                new _NumericChar(29, PokeConstants.NumericChars._29),
                new _NumericChar(30, PokeConstants.NumericChars._30),
                new _NumericChar(31, PokeConstants.NumericChars._31),
                new _NumericChar(32, PokeConstants.NumericChars._32),
                new _NumericChar(33, PokeConstants.NumericChars._33),
                new _NumericChar(34, PokeConstants.NumericChars._34),
                new _NumericChar(35, PokeConstants.NumericChars._35),
                new _NumericChar(36, PokeConstants.NumericChars._36),
                new _NumericChar(37, PokeConstants.NumericChars._37),
                new _NumericChar(38, PokeConstants.NumericChars._38),
                new _NumericChar(39, PokeConstants.NumericChars._39),
                new _NumericChar(40, PokeConstants.NumericChars._40),
                new _NumericChar(41, PokeConstants.NumericChars._41),
                new _NumericChar(42, PokeConstants.NumericChars._42),
                new _NumericChar(43, PokeConstants.NumericChars._43),
                new _NumericChar(44, PokeConstants.NumericChars._44),
                new _NumericChar(45, PokeConstants.NumericChars._45),
                new _NumericChar(46, PokeConstants.NumericChars._46),
                new _NumericChar(47, PokeConstants.NumericChars._47),
                new _NumericChar(48, PokeConstants.NumericChars._48),
                new _NumericChar(49, PokeConstants.NumericChars._49),
                new _NumericChar(50, PokeConstants.NumericChars._50),
            };

            #region Internal classes
            public class _NumericChar
            {
                [XmlAttribute]
                public int number { get; set; }

                [XmlAttribute]
                public string character { get; set; }

                public _NumericChar() { }
                public _NumericChar(int _number, char _character)
                {
                    number = _number;
                    character = _character.ToString();
                }
            }
            #endregion Internal classes
        }

        [Serializable]
        public class _CPMultipliers
        {
            [XmlElement]
            public _CPM[] CPM { get; set; }

            public _CPMultipliers()
            {
                List<_CPM> cpms = new List<_CPM>();

                for (int level = 1; level <= 40; level++)
                    cpms.Add(new _CPM(level, PokeFormulas.GetCMP(level)));

                for (int level = 1; level < 40; level++)
                    cpms.Add(new _CPM(level + 0.5, PokeFormulas.GetCMPHalfStep(level)));

                CPM = cpms.ToArray();
            }

            #region Internal classes

            [Serializable]
            public class _CPM
            {
                [XmlAttribute]
                public double level { get; set; }

                [XmlAttribute]
                public double value { get; set; }

                public _CPM() { }

                public _CPM(double _level, double _value)
                {
                    level = _level;
                    value = _value;
                }
            }

            #endregion Internal classes
        }

        #endregion Internal classes

        public Constants()
        {
            last_updated = PokeConstants.LastModified;
            Gender = new _Gender();
            Types = new _Types();
            Weathers = new _Weathers();
            Regions = new _Regions();
            Availability = new _Availability();
            Rarity = new _Rarity();
            IV_Evaluation = new _IV_Evaluation();
            Mappings = new _Mappings();
            NumericChars = new _NumericChars();
            CPMultipliers = new _CPMultipliers();
        }

        #region Writers

        private static string ConstantsXmlFilePath { get { return Path.Combine(Utils.OutputDataFileFolder, "constants.xml"); } }
        private static string EffectivenessXmlFilePath { get { return Path.Combine(Utils.OutputDataFileFolder, "effectiveness.xml"); } }

        /// <summary>
        /// Write the files that don't usually change.
        /// </summary>
        public static void Write()
        {
            if (!File.Exists(ConstantsXmlFilePath) || Utils.GetLastUpdated(ConstantsXmlFilePath) < PokeConstants.LastModified)
                Utils.WriteXML(new Constants(), ConstantsXmlFilePath);

            if (!File.Exists(EffectivenessXmlFilePath) || Utils.GetLastUpdated(EffectivenessXmlFilePath) < MoveEffectiveness.LastModified)
                Utils.WriteXML(new MoveEffectiveness(), EffectivenessXmlFilePath);
        }

        #endregion Writers
    }
}