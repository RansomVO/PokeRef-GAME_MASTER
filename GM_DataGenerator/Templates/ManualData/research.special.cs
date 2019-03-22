using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData
{
    [Serializable]
    public class SpecialResearch
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
            public string encounter { get; set; }

            [XmlElement]
            public _Stage[] Stage { get; set; }

            public void Init(Dictionary<int, PokemonTranslator> pokemonTranslators)
            {
                foreach (var stage in Stage)
                    stage.Init(pokemonTranslators);
            }


            #region Internal classes

            [Serializable]
            public class _Stage
            {
                [XmlElement]
                public FieldResearch.Research[] Research { get; set; }

                [XmlElement]
                public _Rewards Rewards { get; set; }

                public void Init(Dictionary<int, PokemonTranslator> pokemonTranslators)
                {
                    foreach (var research in Research)
                        research.Init(pokemonTranslators);
                }

                #region Internal classes

                [Serializable]
                public class _Rewards
                {
                    [XmlElement]
                    public FieldResearch.RewardItem[] Item { get; set; }

                    [XmlElement]
                    public FieldResearch.RewardEncounter[] Pokemon { get; set; }

                    public void Init(Dictionary<int, PokemonTranslator> pokemonTranslators)
                    {
                        if (Pokemon != null)
                            foreach (var encounter in Pokemon)
                                encounter.PokemonTranslator = pokemonTranslators[encounter.id];

                        if (Pokemon != null)
                            foreach (var encounter in Pokemon)
                                encounter.PokemonTranslator = pokemonTranslators[encounter.id];
                    }
                }

                #endregion Internal classes
            }

            #endregion Internal classes
        }

        #endregion Internal classes
    }
}