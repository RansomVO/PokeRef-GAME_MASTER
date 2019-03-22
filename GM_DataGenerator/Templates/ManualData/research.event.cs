using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData
{
    [Serializable]
    public class EventResearch
    {
        [XmlAttribute(DataType = "date")]
        public DateTime last_updated { get; set; }

        [XmlElement]
        public _Event[] Event { get; set; }

        public void Init(Dictionary<int, PokemonTranslator> pokemonTranslators)
        {
            foreach (var specialEvent in Event)
                specialEvent.Init(pokemonTranslators);
        }

        #region Internal classes

        [Serializable]
        public class _Event
        {
            [XmlAttribute]
            public string name { get; set; }

            [XmlAttribute]
            public DateTime startdate { get; set; }

            [XmlAttribute]
            public DateTime enddate { get; set; }

            [XmlElement]
            public FieldResearch.Research[] Research { get; set; }

            public void Init(Dictionary<int, PokemonTranslator> pokemonTranslators)
            {
                foreach (var research in Research)
                    research.Init(pokemonTranslators);
            }
        }

        #endregion Internal classes
    }
} 