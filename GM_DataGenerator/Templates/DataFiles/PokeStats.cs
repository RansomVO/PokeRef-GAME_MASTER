using System;
using System.ComponentModel;
using System.Xml.Serialization;
using VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData;
using VanOrman.PokemonGO.GAME_MASTER.Parser.Templates;
using static VanOrman.PokemonGO.GAME_MASTER.DataGenerator.PokeConstants;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.DataFiles
{
    [Serializable]
    public class PokeStats
    {
        #region Properties 

        [XmlAttribute(DataType = "date")]
        public DateTime last_updated { get; set; }

        [XmlAttribute]
        public int gen { get; set; }

        [XmlAttribute]
        public string region { get; set; }

        [XmlElement]
        public _Pokemon[] Pokemon { get; set; }

        #endregion Properties 

        #region Internal classes

        [Serializable]
        public class _Pokemon
        {
            [XmlAttribute]
            public int id { get; set; }

            [XmlAttribute]
            public string name { get; set; }

            [XmlAttribute]
            public string form { get; set; }

            [XmlAttribute]
            public string family { get; set; }

            [XmlAttribute]
            [DefaultValue(0)]
            public int buddy_km { get; set; }

            [XmlAttribute]
            public string gender_ratio { get; set; }

            [XmlAttribute]
            [DefaultValue("")]
            public string rarity { get; set; }

            [XmlAttribute]
            public string availability { get; set; }

            [XmlAttribute]
            [DefaultValue(false)]
            public bool shiny { get; set; }

            [XmlElement]
            public PokeTypes Type { get; set; }

            [XmlElement]
            public EvolvesFrom EvolvesFrom { get; set; }

            [XmlElement]
            public _Stats Stats { get; set; }

            #region Internal classes

            [Serializable]
            public class _Stats
            {
                [XmlElement]
                public IVScore BaseIV { get; set; }

                [XmlElement]
                public Variation Height { get; set; }

                [XmlElement]
                public Variation Weight { get; set; }

                [XmlElement]
                public _Rates Rates { get; set; }

                [XmlElement]
                public _MaxStats Max { get; set; }

                #region Internal classes

                [Serializable]
                public class IVScore
                {
                    [XmlAttribute]
                    public int attack { get; set; }

                    [XmlAttribute]
                    public int defense { get; set; }

                    [XmlAttribute]
                    public int stamina { get; set; }

                    public IVScore() { }

                    public IVScore(int _attack, int _defense, int _stamina)
                    {
                        attack = _attack;
                        defense = _defense;
                        stamina = _stamina;
                    }
                }

                [Serializable]
                public class Variation
                {
                    [XmlAttribute]
                    public double standard { get; set; }

                    [XmlAttribute]
                    public double deviation { get; set; }

                    public Variation() { }

                    public Variation(double _standard, double _deviation)
                    {
                        standard = _standard;
                        deviation = _deviation;
                    }
                }

                [Serializable]
                public class _Rates
                {
                    [XmlAttribute]
                    public double capture { get; set; }

                    [XmlAttribute]
                    public double flee { get; set; }

                    [XmlAttribute]
                    public double attack { get; set; }

                    [XmlAttribute]
                    public double dodge { get; set; }

                    public _Rates() { }

                    public _Rates(double _capture, double _flee, double _attack, double _dodge)
                    {
                        capture = _capture;
                        flee = _flee;
                        attack = _attack;
                        dodge = _dodge;
                    }
                }

                [Serializable]
                public class _MaxStats
                {
                    [XmlAttribute]
                    public int cp { get; set; }

                    [XmlAttribute]
                    public int hp { get; set; }

                    public _MaxStats() { }

                    public _MaxStats(int _cp, int _hp)
                    {
                        cp = _cp;
                        hp = _hp;
                    }
                }

                #endregion Internal classes

                public _Stats() { }

                public _Stats(IVScore baseIV, Variation height, Variation weight, _Rates rates, _MaxStats max)
                {
                    BaseIV = baseIV;
                    Height = height;
                    Weight = weight;
                    Rates = rates;
                    Max = max;
                }
            }

            #endregion Internal classes

            public _Pokemon() { }

            public _Pokemon(PokemonUnreleased._Pokemon pokemon, string _rarity)
            {
                id = pokemon.id;
                name = pokemon.name;
                form = pokemon.form;
                family = pokemon.family;
                rarity = _rarity;
                availability = PokeConstants.Availability.Unreleased;

                Type = pokemon.Type;
                EvolvesFrom = pokemon.EvolvesFrom;
            }

            public _Pokemon(PokemonTranslator pokemonTranslator, string _availability, string _rarity, bool _shiny, _Stats._MaxStats maxStats)
            {
                id = pokemonTranslator.Id;
                name = pokemonTranslator.Name;
                form = pokemonTranslator.Form;
                family = pokemonTranslator.CandyType;
                buddy_km = (int)pokemonTranslator.PokemonSettings.kmBuddyDistance;
                gender_ratio = pokemonTranslator.GenderRatio;
                rarity = pokemonTranslator.Rarity;
                shiny = _shiny;
                availability = _availability;

                Type = new PokeTypes(pokemonTranslator.Type1, pokemonTranslator.Type2);

                if (pokemonTranslator.EvolvesFromId > 0)
                    EvolvesFrom = new EvolvesFrom(pokemonTranslator.EvolvesFromId, pokemonTranslator.EvolvesFrom, pokemonTranslator.CandiesToEvolve, pokemonTranslator.EvolveSpecialItem);

                Stats = new _Stats(
                    new _Stats.IVScore(pokemonTranslator.PokemonSettings.stats.baseAttack, pokemonTranslator.PokemonSettings.stats.baseDefense, pokemonTranslator.PokemonSettings.stats.baseStamina),
                    new _Stats.Variation(pokemonTranslator.PokemonSettings.pokedexHeightM, pokemonTranslator.PokemonSettings.heightStdDev),
                    new _Stats.Variation(pokemonTranslator.PokemonSettings.pokedexWeightKg, pokemonTranslator.PokemonSettings.weightStdDev),
                    new _Stats._Rates(pokemonTranslator.PokemonSettings.encounter.baseCaptureRate, pokemonTranslator.PokemonSettings.encounter.baseFleeRate, pokemonTranslator.PokemonSettings.encounter.attackProbability, pokemonTranslator.PokemonSettings.encounter.dodgeProbability),
                    maxStats);
            }
        }

        #endregion Internal classes

        #region ctor

        public PokeStats() { }

        public PokeStats(int _gen, _Pokemon[] pokemon)
        {
            last_updated = DateTime.Today;
            gen = _gen;
            region = PokeConstants.Regions[_gen];
            Pokemon = pokemon;
        }

        #endregion ctor
    }
}