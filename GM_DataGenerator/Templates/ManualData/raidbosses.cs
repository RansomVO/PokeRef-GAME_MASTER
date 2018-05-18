using System;
using System.ComponentModel;
using System.Xml.Serialization;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData
{
    [Serializable]
    public class RaidBosses
    {
        [XmlAttribute(DataType = "date")]
        public DateTime last_updated { get; set; }

        [XmlElement]
        public _RaidBoss[] RaidBoss { get; set; }

        #region Internal classes

        [Serializable]
        public class _RaidBoss : Pokemon
        {
            [XmlAttribute]
            [DefaultValue("")]
            public string tier { get; set; }

            [XmlAttribute]
            public int raid_cp { get; set; }

            [XmlAttribute]
            [DefaultValue(false)]
            public bool current { get; set; }
        }

        #endregion Internal classes
    }
}