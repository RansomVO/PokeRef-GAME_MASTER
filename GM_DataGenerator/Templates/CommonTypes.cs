﻿using POGOProtos.Enums;
using System;
using System.ComponentModel;
using System.Globalization;
using System.Xml.Serialization;

/// <summary>
/// Common classes leveraged Templates.Info classes.
/// </summary>
namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates
{
    [Serializable]
    public class Pokemon
    {
        #region Properties

        [XmlAttribute]
        [DefaultValue(0)]
        public int id { get; set; }

        [XmlAttribute]
        public string name { get; set; }

        #endregion Properties

        #region ctor

        public Pokemon() { }

        public Pokemon(int _id, string _name)
        {
            id = _id;
            name = _name;
        }

        /// <summary>
        /// This is for use when creating a Pokmeon from a class derived from Pokemon.
        /// </summary>
        /// <param name="pokemon"></param>
        public Pokemon(Pokemon pokemon)
        {
            id = pokemon.id;
            name = pokemon.name;
        }

        #endregion ctor
    }

    [Serializable]
    public class PokemonForm : Pokemon
    {
        #region Properties

        [XmlIgnore]
        [DefaultValue(Form.FORM_UNSET)]
        public Form FormId { get; private set; }

        [XmlAttribute]
        [DefaultValue(null)]
        public string form
        {
            get { return _form; }
            set
            {
                _form = value;

                if (value != null)
                {
                    if (string.IsNullOrEmpty(name))
                        throw new InvalidOperationException("Attribute \"name\" must be set before the attribute \"form\".");

                    string formIdText = name.ToUpper() + "_" + value.ToUpper();
                    foreach (Form formId in Enum.GetValues(typeof(Form)))
                    {
                        if (string.Equals(formId.ToString().ToUpper(), formIdText))
                        {
                            FormId = formId;
                            break;
                        }
                    }
                }
            }
        }
        private string _form;

        public int PokemonTranslatorKey { get { return id + (1000 * (int)FormId); } }

        #endregion Properties

        #region ctor

        public PokemonForm() { }

        public PokemonForm(int id, string name, Form formId) :
            base(id, name)
        {
            form = PokemonTranslator.GetFormName(formId);
        }

        /// <summary>
        /// This is for use when creating a PokemonForm from a class derived from PokemonForm.
        /// </summary>
        /// <param name="pokemon"></param>
        public PokemonForm(PokemonForm pokemonForm) :
            base(pokemonForm.id, pokemonForm.name)
        {
            form = pokemonForm.FormId == Form.FORM_UNSET && !string.IsNullOrWhiteSpace(pokemonForm.form) ?
                pokemonForm.form : 
                PokemonTranslator.GetFormName(pokemonForm.FormId);
        }

        #endregion ctor
    }


    [Serializable]
    public class EvolvesFrom : Pokemon
    {
        [XmlAttribute]
        [DefaultValue(0)]
        public int candies { get; set; }

        [XmlAttribute]
        public string special { get; set; }

        public EvolvesFrom() { }

        public EvolvesFrom(int _id, string _name, int _candies, string _special)
        {
            id = _id;
            name = _name;
            candies = _candies;
            special = _special;
        }
    }

    [Serializable]
    public class PokeTypes
    {
        [XmlAttribute]
        public string primary { get; set; }

        [XmlAttribute]
        [DefaultValue("")]
        public string secondary { get; set; }

        public PokeTypes() { }

        public PokeTypes(string _primary, string _secondary)
        {
            primary = _primary;
            secondary = _secondary;
        }
    }

    [Serializable]
    public class Research
    {
        #region Properties 

        [XmlAttribute]
        public string type { get; set; }

        [XmlAttribute]
        public string task { get; set; }

        #endregion Properties 
    }
}