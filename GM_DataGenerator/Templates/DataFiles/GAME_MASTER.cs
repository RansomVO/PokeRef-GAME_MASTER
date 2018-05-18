using System;
using System.Xml.Serialization;

using PokemonGO.GAME_MASTER.Templates;
using VanOrman.Utils;
using System.IO;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.DataFiles
{
    [Serializable]
    public class GAME_MASTERS
    {
        #region Properties

        [XmlAttribute(DataType = "date")]
        public DateTime last_updated { get; set; }

        [XmlElement]
        public _GAME_MASTER[] GAME_MASTER { get; set; }

        #endregion Properties

        #region Internal classes

        [Serializable]
        public class _GAME_MASTER
        {
            #region Properties

            [XmlAttribute]
            public string name { get; set; }

            [XmlAttribute]
            public bool have_original { get; set; }

            [XmlAttribute]
            public long timestamp_dec { get; set; }

            [XmlAttribute]
            public string timestamp_hex { get; set; }

            [XmlAttribute]
            public string timestamp { get; set; }

            #endregion Properties

            #region ctor

            public _GAME_MASTER() { }

            public _GAME_MASTER(string _name, bool _have_original)
            {
                name = _name;
                timestamp_hex = TimeStampUtils.FileNameToHexTimeStamp(name);
                timestamp_dec = TimeStampUtils.HexTimeStampToTimeStamp(timestamp_hex);
                timestamp = TimeStampUtils.TimestampToDateTime(timestamp_dec).ToString(PokeConstants.DateTimeFormat);
                have_original = _have_original;
            }

            #endregion ctor
        }

        #endregion Internal classes

        #region ctor

        public GAME_MASTERS() { }

        public GAME_MASTERS(_GAME_MASTER[] gameMasters)
        {
            last_updated = DateTime.Parse(gameMasters[0].timestamp);
            GAME_MASTER = gameMasters;
        }

        #endregion ctor
    }
}
