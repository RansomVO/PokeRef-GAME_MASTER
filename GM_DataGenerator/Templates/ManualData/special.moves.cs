using POGOProtos.Enums;
using System;
using System.Xml.Serialization;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData
{
    [Serializable]
    public class SpecialMoves
    {
        [XmlAttribute(DataType = "date")]
        public DateTime last_updated { get; set; }

        [XmlElement]
        public _Move[] Move { get; set; }

        #region Internal classes

        [Serializable]
        public class _Move
        {
            [XmlAttribute]
            public string pokemonTemplateId { get; set; }

            [XmlAttribute]
            public PokemonMove movementId { get; set; }

            [XmlAttribute(DataType = "date")]
            public DateTime date { get; set; }

            [XmlAttribute]
            public string reason { get; set; }

            [XmlIgnore]
            public bool IsFast {  get { return movementId.ToString().EndsWith("_FAST", StringComparison.OrdinalIgnoreCase); } }
        }

        #endregion Internal classes
    }
}