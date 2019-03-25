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

        public void Init(Dictionary<int, PokemonTranslator> pokemonTranslators)
        {
            foreach (var category in Category)
                category.Init(pokemonTranslators);
        }

        #region Internal classes


        [Serializable]
        public class _Category
        {
            [XmlAttribute]
            public string type { get; set; }

            [XmlElement]
            public Research[] Research { get; set; }

            public void Init(Dictionary<int, PokemonTranslator> pokemonTranslators)
            {
                if (Research != null)
                    foreach (var research in Research)
                        research.Init(pokemonTranslators);
            }
        }

        #endregion Internal classes

        [Serializable]
        public class Research
        {
            [XmlAttribute]
            public string task { get; set; }

            [XmlElement]
            public RewardEncounter[] Pokemon { get; set; }

            [XmlElement]
            public RewardItem[] Item { get; set; }

            public void Init(Dictionary<int, PokemonTranslator> pokemonTranslators)
            {
                if (Pokemon != null)
                    foreach (var pokemon in Pokemon)
                        pokemon.PokemonTranslator = pokemonTranslators[pokemon.id];
            }
        }

        [Serializable]
        public class RewardEncounter : Pokemon
        {
            [XmlIgnore]
            public PokemonTranslator PokemonTranslator { get; set; }
        }

        [Serializable]
        public class RewardItem
        {
            [XmlAttribute]
            public string id { get; set; }

            [XmlAttribute]
            public string amount { get; set; }
        }

    }
}