using System;
using System.ComponentModel;
using System.Xml.Serialization;
using VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.DataFiles
{
    [Serializable]
    public class Encounter
    {
        #region Properties

        [XmlAttribute(DataType = "date")]
        public DateTime last_updated { get; set; }

        [XmlElement]
        public Pokemon Pokemon { get; set; }

        [XmlElement]
        public Common.PossibilitySet Regular { get; set; }

        #endregion Properties

        #region ctor

        public Encounter() { }

        public Encounter(Pokemon pokemon, Common.PossibilitySet.Possibility[] possibilities)
        {
            last_updated = DateTime.Today;
            Pokemon = new Pokemon(pokemon);
            Regular = new Common.PossibilitySet(possibilities);
        }

        #endregion ctor
    }
}