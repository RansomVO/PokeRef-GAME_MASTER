﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

using VanOrman.Utils;

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
            #region Properties

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
            [DefaultValue("")]
            public string rarity { get; set; }

            [XmlAttribute]
            [DefaultValue("")]
            public string shiny { get; set; }

            [XmlElement]
            public _Form[] Form { get; set; }

            #endregion Properties

            #region ctor

            public _Pokemon() { }

            public _Pokemon(_Pokemon pokemon, _Form form)
            {
                id = pokemon.id;
                name = pokemon.name;
                date = form.date;
                availability = form.availability;
                shiny = form.shiny;
            }

            #endregion ctor

            #region Internal classes

            [Serializable]
            public class _Form
            {
                [XmlAttribute]
                public string name { get; set; }

                [XmlAttribute]
                public string date { get; set; }

                [XmlAttribute]
                public string availability { get; set; }

                [XmlAttribute]
                [DefaultValue("")]
                public string shiny { get; set; }
            }

            #endregion Internal classes
        }

        #endregion Internal classes

        private void GetPokemonLookup()
        {
            _pokemonLookup = new Dictionary<string, _Pokemon>();

            foreach (var pokemon in Pokemon)
            {
                if (_pokemonLookup.ContainsKey(pokemon.name))
                {
                    ConsoleOutput.OutputError($"_datafiles.manual\\infrequent\\pokemon.availability.xml contains duplicate: \"{pokemon.name}\"");
                    continue;
                }

                _pokemonLookup.Add(pokemon.name, pokemon);
                if (pokemon.Form != null)
                {
                    foreach (var form in pokemon.Form)
                    {
                        string key = GetPokemonLookupKey(pokemon.name, form.name);
                        if (_pokemonLookup.ContainsKey(key))
                        {
                            ConsoleOutput.OutputError($"_datafiles.manual\\infrequent\\pokemon.availability.xml contains duplicate Form: \"{pokemon.name}: {form.name}\"");
                            continue;
                        }

                        _pokemonLookup.Add(key, new _Pokemon(pokemon, form));
                    }
                }
            }
        }

        public static string GetPokemonLookupKey(string pokemonName, string form)
        {
            string result = pokemonName;

            if (!string.IsNullOrEmpty(form))
            {
                result += " (" + form + ")";
            }

            return result;
        }

        public _Pokemon GetPokemon(string name, string form)
        {
            string key = GetPokemonLookupKey(name, form);

            if (!_pokemonLookup.ContainsKey(key))
            {
                ConsoleOutput.OutputError($"_datafiles.manual\\infrequent\\pokemon.availability.xml missing: \"{key}\"");
                return null;
            }

            return _pokemonLookup[key];
        }
    }
}
