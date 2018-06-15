using POGOProtos.Enums;
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
        public Form formIdRaw { get; set; }

        [XmlAttribute]
        [DefaultValue("")]
        public string form { get { return GetFormName(formIdRaw, name); } set { } }

        [XmlAttribute]
        [DefaultValue(0)]
        public int formId { get { return (int)formIdRaw; } set { formIdRaw = (Form)value; } }

        #endregion Properties

        #region ctor

        public PokemonForm() { }

        public PokemonForm(int _id, string _name, Form formId)
        {
            id = _id;
            name = _name;
            formIdRaw = formId;
        }

        /// <summary>
        /// This is for use when creating a PokemonForm from a class derived from PokemonForm.
        /// </summary>
        /// <param name="pokemon"></param>
        public PokemonForm(PokemonForm pokemonForm) :
            this(pokemonForm.id, pokemonForm.name, pokemonForm.formIdRaw)
        { }

        #endregion ctor

        public static string GetFormName(Form formId, string name)
        {
            if (formId == Form.FORM_UNSET)
                return "";

            int index = name.IndexOf(" (");
            if (index > 0)
                name = name.Substring(0, index);

            return CultureInfo.CurrentCulture.TextInfo.ToTitleCase(
                    formId.ToString().Substring(name.Length + 1)
                    .Replace('_', ' ')
                    .ToLower());
        }
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