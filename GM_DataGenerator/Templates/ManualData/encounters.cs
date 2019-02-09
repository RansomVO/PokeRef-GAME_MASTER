using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData
{
    [Serializable]
    public class FieldResearch
    {
        [XmlAttribute(DataType = "date")]
        public DateTime last_updated { get; set; }

        [XmlElement]
        public _Category[] Category { get; set; }

        [XmlElement]
        public _Event[] Event { get; set; }

        public void Init(Dictionary<int, PokemonTranslator> pokemonTranslators)
        {
            foreach (var category in Category)
                if (category.Research != null)
                    Init(category.Research, pokemonTranslators);
            foreach (var _event in Event)
                if (_event.Research != null)
                    Init(_event.Research, pokemonTranslators);
        }

        private void Init(IEnumerable<_Research> _research, Dictionary<int, PokemonTranslator> pokemonTranslators)
        {
            foreach (var research in _research)
                if (research.Pokemon != null)
                    foreach (var pokemon in research.Pokemon)
                        pokemon.PokemonTranslator = pokemonTranslators[pokemon.id];
        }

        #region Internal classes

        [Serializable]
        public class _Research
        {
            [XmlAttribute]
            public string task { get; set; }

            [XmlElement]
            public _Pokemon[] Pokemon { get; set; }

            [XmlElement]
            public _Item[] Item { get; set; }

            #region Internal classes

            [Serializable]
            public class _Pokemon : Pokemon
            {
                [XmlIgnore]
                public PokemonTranslator PokemonTranslator { get; set; }
            }

            [Serializable]
            public class _Item
            {
                [XmlAttribute]
                public string id { get; set; }

                [XmlAttribute]
                public string amount { get; set; }
            }

            #endregion Internal classes
        }

        [Serializable]
        public class _Category
        {
            [XmlAttribute]
            public string type { get; set; }

            [XmlElement]
            public _Research[] Research { get; set; }
        }

        [Serializable]
        public class _Event
        {
            [XmlAttribute]
            public string name { get; set; }

            public DateTime startdate { get; set; }

            public DateTime enddate { get; set; }

            [XmlElement]
            public _Research[] Research { get; set; }
        }

        #endregion Internal classes
    }
}