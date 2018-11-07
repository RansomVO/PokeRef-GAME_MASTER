using System;
using System.Xml.Serialization;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData
{
    [Serializable]
    public class Traits
    {
        [XmlAttribute(DataType = "date")]
        public DateTime last_updated { get; set; }

        [XmlElement]
        public _Eggs Eggs { get; set; }

        [XmlElement]
        public _Ditto Ditto { get; set; }

        #region Internal classes

        [Serializable]
        public class _Eggs
        {
                [XmlElement]
                public _Egg[] Egg { get; set; }

                #region Internal classes

                [Serializable]
                public class _Egg
                {
                    [XmlAttribute]
                    public string type { get; set; }

                    [XmlElement]
                    public PokemonForm[] Pokemon { get; set; }
                }

                #endregion Internal classes
        }

        [Serializable]
        public class _Ditto
        {
            [XmlElement]
            public PokemonForm[] Pokemon { get; set; }
        }

        #endregion Internal classes

        public _Eggs._Egg GetEgg(PokemonTranslator pokemonTranslator)
        {
            foreach (var egg in Eggs.Egg)
                foreach (var pokemon in egg.Pokemon)
                    if (pokemon.id == pokemonTranslator.Id && pokemon.FormId == pokemonTranslator.Form)
                        return egg;

            return null;
        }

        public bool CanBeDitto(PokemonTranslator pokemonTranslator)
        {
                foreach (var pokemon in Ditto.Pokemon)
                    if (pokemon.id == pokemonTranslator.Id && pokemon.FormId == pokemonTranslator.Form)
                        return true;

            return false;
        }
    }
}