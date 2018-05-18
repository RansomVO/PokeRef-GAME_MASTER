using System;
using System.ComponentModel;
using System.Xml.Serialization;
using static VanOrman.PokemonGO.GAME_MASTER.DataGenerator.PokeConstants;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData
{
    [Serializable]
    public class PokemonUnreleased
    {
        [XmlAttribute(DataType = "date")]
        public DateTime last_updated { get; set; }

        [XmlElement]
        public _Pokemon[] Pokemon { get; set; }

        [Serializable]
        public class _Pokemon : Pokemon
        {
            [XmlAttribute]
            [DefaultValue("")]
            public string form { get; set; }

            [XmlAttribute]
            public string family { get; set; }

            [XmlElement]
            public PokeTypes Type { get; set; }

            [XmlElement]
            public EvolvesFrom EvolvesFrom { get; set; }
        }
    }
}
