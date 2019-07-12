using System;
using System.Collections.Generic;
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

        public void Init(Dictionary<int, PokemonTranslator> pokemonTranslators)
        {
            foreach (var raidboss in RaidBoss)
                raidboss.PokemonTranslator = pokemonTranslators[raidboss.Key];
        }

        #region Internal classes

        [Serializable]
        public class _RaidBoss : Pokemon
        {
            [XmlAttribute]
            public int tier { get; set; }

            [XmlAttribute]
            [DefaultValue(false)]
            public bool current { get; set; }

            [XmlIgnore]
            public PokemonTranslator PokemonTranslator { get; set; }
        }

        #endregion Internal classes
    }
}