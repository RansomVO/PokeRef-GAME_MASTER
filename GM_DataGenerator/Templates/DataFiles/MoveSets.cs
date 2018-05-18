using System;
using System.Xml.Serialization;

using VanOrman.PokemonGO.GAME_MASTER.Parser.Templates;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.DataFiles
{
    [Serializable]
    public class MoveSets
    {
        #region Properties

        [XmlAttribute(DataType = "date")]
        public DateTime last_updated { get; set; }

        [XmlAttribute]
        public int gen { get; set; }

        [XmlElement]
        public _MoveSet[] MoveSet { get; set; }

        #endregion Properties

        #region Internal classes

        [Serializable]
        public class _MoveSet
        {
            #region Properties

            [XmlAttribute]
            public double base_dps { get; set; }

            [XmlAttribute]
            public double true_dps { get; set; }

            [XmlAttribute]
            public int comparison { get; set; }

            [XmlElement]
            public Pokemon Pokemon { get; set; }

            [XmlElement]
            public Attack FastAttack { get; set; }

            [XmlElement]
            public Attack ChargedAttack { get; set; }

            #endregion Properties

            #region Internal classes

            [Serializable]
            public class Attack
            {
                #region Properties

                [XmlAttribute]
                public string name { get; set; }

                [XmlAttribute]
                public bool stab { get; set; }

                [XmlAttribute]
                public bool legacy { get; set; }

                #endregion Properties

                #region ctor

                public Attack() { }

                public Attack(string _name, bool _stab, bool _legacy)
                {
                    name = _name;
                    stab = _stab;
                    legacy = _legacy;
                }

                #endregion ctor
            }

            #endregion Internal classes

            #region ctor

            public _MoveSet() { }

            public _MoveSet(PokemonTranslator pokemonTranslator, MoveTranslator fastMove, bool fastMoveLegacy, MoveTranslator chargedMove, bool chargedMoveLegacy)
            {
                base_dps = PokeFormulas.GetMoveSetDPS(pokemonTranslator, fastMove, chargedMove);
                true_dps = PokeFormulas.GetTrueDPS(pokemonTranslator, fastMove, chargedMove);
                Pokemon = new Pokemon(pokemonTranslator.Id, pokemonTranslator.Name);
                FastAttack = new Attack(fastMove.Name, PokeFormulas.HasStab(pokemonTranslator, fastMove), fastMoveLegacy);
                ChargedAttack = new Attack(chargedMove.Name, PokeFormulas.HasStab(pokemonTranslator, chargedMove), chargedMoveLegacy);
            }

            #endregion ctor
        }

        #endregion Internal classes

        #region ctor

        public MoveSets() { }

        public MoveSets(int _gen, _MoveSet[] moveSets)
        {
            last_updated = DateTime.Today;
            gen = _gen;
            MoveSet = moveSets;
        }

        #endregion ctor
    }
}