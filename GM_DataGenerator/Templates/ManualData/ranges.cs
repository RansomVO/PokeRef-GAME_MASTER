using System;
using System.Xml.Serialization;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData
{
    [Serializable]
    public class Ranges
    {
        [XmlAttribute(DataType = "date")]
        public DateTime last_updated { get; set; }

        [XmlElement]
        public _Desirability Desirability { get; set; }

        [XmlElement]
        public _Benefit MaxCP { get; set; }

        [XmlElement]
        public _Benefit MaxHP { get; set; }

        [XmlElement]
        public _Benefit DPSPercent { get; set; }

        [XmlElement]
        public _Benefit DPS { get; set; }

        [XmlElement]
        public _Difficulty CaptureRate { get; set; }

        [XmlElement]
        public _Rate FleeRate { get; set; }

        [XmlElement]
        public _Rate AttackRate { get; set; }

        [XmlElement]
        public _Rate DodgeRate { get; set; }

        #region Internal classes

        [Serializable]
        public class _Desirability
        {
            [XmlAttribute]
            public int iv { get; set; }

            [XmlAttribute]
            public int attack { get; set; }

            [XmlAttribute]
            public int max_cp { get; set; }

            [XmlAttribute]
            public int base_dps { get; set; }
        }

        [Serializable]
        public class _Benefit
        {
            [XmlAttribute]
            public int great { get; set; }

            [XmlAttribute]
            public int good { get; set; }

            [XmlAttribute]
            public int okay { get; set; }
        }

        [Serializable]
        public class _Difficulty
        {
            [XmlAttribute]
            public int easy { get; set; }

            [XmlAttribute]
            public int moderate { get; set; }

            [XmlAttribute]
            public int difficult { get; set; }
        }

        [Serializable]
        public class _Rate
        {
            [XmlAttribute]
            public int nice { get; set; }

            [XmlAttribute]
            public int okay { get; set; }

            [XmlAttribute]
            public int bad { get; set; }
        }

        #endregion Internal classes
    }
}