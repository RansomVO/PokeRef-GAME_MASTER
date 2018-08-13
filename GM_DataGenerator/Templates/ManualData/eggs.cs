using System;
using System.Xml.Serialization;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData
{
    [Serializable]
    public class Eggs
    {
        [XmlAttribute(DataType = "date")]
        public DateTime last_updated { get; set; }

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

        public _Egg GetEgg(PokemonTranslator pokemonTranslator)
        {
            foreach (var egg in Egg)
                foreach (var pokemon in egg.Pokemon)
                    if (pokemon.id == pokemonTranslator.Id && pokemon.FormId == pokemonTranslator.Form)
                        return egg;

            return null;
        }
    }
}