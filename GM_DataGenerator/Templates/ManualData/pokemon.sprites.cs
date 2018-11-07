using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Xml.Serialization;

using VanOrman.Utils;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData
{
    [Serializable]
    public class PokemonSprites
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

            [XmlAttribute]
            [DefaultValue("")]
            public string sprite { get; set; }

            [XmlAttribute]
            [DefaultValue("")]
            public string sprite_shiny { get; set; }

            [XmlElement]
            public _Form[] Form { get; set; }

            #endregion Properties

            #region ctor

            public _Pokemon() { }

            public _Pokemon(_Pokemon pokemon, _Form form)
            {
                id = pokemon.id;
                name = pokemon.name;
                sprite = form.sprite;
                sprite_shiny = form.sprite_shiny;
            }

            #endregion ctor

            #region Internal classes

            [Serializable]
            public class _Form
            {
                [XmlAttribute]
                public string name { get; set; }

                [XmlAttribute]
                [DefaultValue("")]
                public string sprite { get; set; }

                [XmlAttribute]
                [DefaultValue("")]
                public string sprite_shiny { get; set; }
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
                    ConsoleOutput.OutputError($"_datafiles.manual\\infrequent\\pokemon.sprites.xml contains duplicate Pokemon: \"{pokemon.name}\"");
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
                            ConsoleOutput.OutputError($"_datafiles.manual\\infrequent\\pokemon.sprites.xml contains duplicate Form: \"{pokemon.name}: {form.name}\"");
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
				result += " (" + form + ")";

			return result;
		}

        public _Pokemon GetPokemon(string name, string form)
        {
			string key = GetPokemonLookupKey(name, form);

			if (!_pokemonLookup.ContainsKey(key))
            {
                ConsoleOutput.OutputError($"_datafiles.manual\\infrequent\\pokemon.sprites.xml missing: \"{key}\"");
                return null;
            }

            return _pokemonLookup[key];
        }
    }
}
