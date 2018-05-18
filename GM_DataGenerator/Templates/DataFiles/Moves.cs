using System;
using System.Xml.Serialization;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.DataFiles
{
    [Serializable]
    public class Moves
    {
        [XmlAttribute(DataType = "date")]
        public DateTime last_updated { get; set; }

        [XmlAttribute]
        public string category { get; set; }

        [XmlElement]
        public _Move[] Move { get; set; }

        #region Internal classes

        [Serializable]
        public class _Move
        {

            [XmlAttribute]
            public string name { get; set; }

            [XmlAttribute]
            public string type { get; set; }

            [XmlAttribute]
            public int energy { get; set; }

            [XmlAttribute]
            public int power { get; set; }

            [XmlAttribute]
            public double duration { get; set; }

            [XmlAttribute]
            public int damage_window_start { get; set; }

            [XmlAttribute]
            public int damage_window_end { get; set; }

            public _Move() { }

            public _Move(string _name, string _type, int _energy, int _power, double _duration, int _damage_window_start, int _damage_window_end)
            {
                name = _name;
                type = _type;
                energy = _energy;
                power = _power;
                duration = _duration;
                damage_window_start = _damage_window_start;
                damage_window_end = _damage_window_end;
            }
        }

        #endregion Internal classes
    }
}