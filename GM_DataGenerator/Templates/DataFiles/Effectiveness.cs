using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.DataFiles
{
    [Serializable]
    public class MoveEffectiveness
    {
        [XmlIgnore]
        public readonly static DateTime LastModified = DateTime.Parse("2018-04-25");

        #region Properties

        [XmlAttribute(DataType = "date")]
        public DateTime last_updated { get; set; }

        [XmlElement]
        public _Key Key { get; set; }

        [XmlElement]
        public _Move[] Moves { get; set; }

        #endregion Properties

        #region Internal classes

        [Serializable]
        public class _Key
        {
            [XmlElement]
            public KeyEntry SuperEffective { get; set; }

            [XmlElement]
            public KeyEntry Neutral { get; set; }

            [XmlElement]
            public KeyEntry NotVeryEffective { get; set; }

            [XmlElement]
            public KeyEntry Immune { get; set; }

            #region Internal classes

            [Serializable]
            public class KeyEntry
            {
                [XmlAttribute]
                public string symbol { get; set; }

                [XmlAttribute]
                public double multiplier { get; set; }

                public KeyEntry() { }

                public KeyEntry(string _symbol, double _multiplier)
                {
                    symbol = _symbol.ToString();
                    multiplier = _multiplier;
                }

                public KeyEntry(char _symbol, double _multiplier)
                    : this(_symbol.ToString(), _multiplier)
                { }
            }

            #endregion Internal classes
        }

        [Serializable]
        public class _Move
        {
            [XmlAttribute]
            public string type { get; set; }

            [XmlElement]
            public _Pokemon[] Pokemon { get; set; }

            #region Internal classes

            [Serializable]
            public class _Pokemon
            {
                [XmlAttribute]
                public string type { get; set; }

                [XmlAttribute]
                public string value { get; set; }

                public _Pokemon() { }

                public _Pokemon(string _type, string _value)
                {
                    type = _type;
                    value = _value;
                }

                public _Pokemon(string _type, char _value)
                  : this(_type, _value.ToString())
                { }
            }

            #endregion Internal classes
        }

        #endregion Internal classes

        public MoveEffectiveness()
        {
            last_updated = LastModified;

            #region Key

            Key = new MoveEffectiveness._Key();
            foreach (var key in PokeConstants.EffectivnessKey)
            {
                switch (key.Key)
                {
                    case PokeConstants.Effectiveness.SuperEffective:
                        Key.SuperEffective = new MoveEffectiveness._Key.KeyEntry(key.Symbol, key.Multiplier);
                        break;

                    case PokeConstants.Effectiveness.NotVeryEffective:
                        Key.NotVeryEffective = new MoveEffectiveness._Key.KeyEntry(key.Symbol, key.Multiplier);
                        break;

                    case PokeConstants.Effectiveness.Immune:
                        Key.Immune = new MoveEffectiveness._Key.KeyEntry(key.Symbol, key.Multiplier);
                        break;

                    default:    // case Constants.Effectiveness.Neutral:
                        Key.Neutral = new MoveEffectiveness._Key.KeyEntry(key.Symbol, key.Multiplier);
                        break;
                }
            }

            #endregion Key

            #region Content

            List<MoveEffectiveness._Move> moves = new List<MoveEffectiveness._Move>();
            foreach (var moveType in Enum.GetValues(typeof(PokeConstants.PokeType)))
            {
                MoveEffectiveness._Move move = new MoveEffectiveness._Move();
                move.type = moveType.ToString();

                List<MoveEffectiveness._Move._Pokemon> pokemon = new List<MoveEffectiveness._Move._Pokemon>();
                foreach (var pokeType in Enum.GetValues(typeof(PokeConstants.PokeType)))
                    pokemon.Add(new MoveEffectiveness._Move._Pokemon(pokeType.ToString(), PokeConstants.Effectivness[(int)moveType][(int)pokeType]));

                move.Pokemon = pokemon.ToArray();

                moves.Add(move);
            }

            Moves = moves.ToArray();

            #endregion Content
        }
    }
}