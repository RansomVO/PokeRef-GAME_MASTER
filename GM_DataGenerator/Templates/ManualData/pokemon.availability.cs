using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData
{
    [Serializable]
    public class PokemonAvailability
    {
        #region Helper Data

        [XmlIgnore]
        private Dictionary<string, _Pokemon> _pokemonLookup;

        #endregion Helper Data

        #region Properties

        [XmlAttribute(DataType = "date")]
        public DateTime last_updated { get; set; }


        [XmlElement]
        public _Pokemon[] Pokemon
        {
            get { return _pokemon; }
            set
            {
                _pokemon = value;
                GetPokemonLookup();
            }
        }

        [XmlIgnore]
        private _Pokemon[] _pokemon;

        #endregion Properties

        #region Internal classes

        [Serializable]
        public class _Pokemon : Pokemon
        {
            [XmlAttribute]
            public string form { get; set; }

            [XmlIgnore]
            public DateTime? Date
            {
                get
                {
                    if (string.IsNullOrWhiteSpace(date))
                        return null;

                    return DateTime.Parse(date);
                }
                set { date = value == null ? "" : ((DateTime)value).ToString(PokeConstants.DateFormat); }
            }

            [XmlAttribute]
            public string date { get; set; }

            [XmlAttribute]
            public string availability { get; set; }

            [XmlAttribute]
            public string rarity { get; set; }

            [XmlAttribute]
            [DefaultValue(false)]
            public bool shiny { get; set; }

            [XmlAttribute]
            [DefaultValue(false)]
            public bool ditto { get; set; }
        }

        #endregion Internal classes

        private void GetPokemonLookup()
        {
            _pokemonLookup = new Dictionary<string, _Pokemon>();

            foreach (var pokemon in Pokemon)
            {
                string key = pokemon.name + (pokemon.form == null ? string.Empty : (" (" + pokemon.form + ")"));
                if (_pokemonLookup.ContainsKey(key))
                    Console.Error.WriteLine("_datafiles.manual\\pokemon.availability.xml contains duplicate: " + key);
                else
                    _pokemonLookup.Add(key, pokemon);
            }
        }

        public _Pokemon GetPokemon(string name)
        {
            if (!_pokemonLookup.ContainsKey(name))
            {
                Console.Error.WriteLine("_datafiles.manual\\pokemon.availability.xml missing: " + name);
                return null;
            }

            return _pokemonLookup[name];
        }
    }
}
