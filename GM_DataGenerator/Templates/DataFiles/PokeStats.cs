using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

using VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData;

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
        public class _Pokemon : PokemonForm
        {
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

            [XmlAttribute]
            [DefaultValue(false)]
            public bool ditto { get; set; }

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

            public _Pokemon(PokemonUnreleased._Pokemon pokemon, string _rarity) :
                base(pokemon)
            {
                family = pokemon.family;
                rarity = _rarity;
                availability = PokeConstants.Availability.Unreleased;

                Type = pokemon.Type;
                EvolvesFrom = pokemon.EvolvesFrom;
            }

            public _Pokemon(PokemonTranslator pokemonTranslator, PokemonAvailability._Pokemon _availability, _Stats._MaxStats maxStats) :
                base(pokemonTranslator.Id, pokemonTranslator.Name, pokemonTranslator.Form)
            {
                family = pokemonTranslator.CandyType;
                buddy_km = (int)pokemonTranslator.PokemonSettings.km_buddy_distance;
                gender_ratio = pokemonTranslator.GenderRatio;
                rarity = pokemonTranslator.Rarity;
                shiny = _availability.shiny;
                ditto = _availability.ditto;
                availability = _availability.availability;

                Type = new PokeTypes(pokemonTranslator.Type1, pokemonTranslator.Type2);

                if (pokemonTranslator.EvolvesFromId > 0)
                    EvolvesFrom = new EvolvesFrom(pokemonTranslator.EvolvesFromId, pokemonTranslator.EvolvesFrom, pokemonTranslator.CandiesToEvolve, pokemonTranslator.EvolveSpecialItem);

                Stats = new _Stats(
                    new _Stats.IVScore(pokemonTranslator.PokemonSettings.stats.base_attack, pokemonTranslator.PokemonSettings.stats.base_defense, pokemonTranslator.PokemonSettings.stats.base_stamina),
                    new _Stats.Variation(pokemonTranslator.PokemonSettings.pokedex_height_m, pokemonTranslator.PokemonSettings.pokedex_height_m),
                    new _Stats.Variation(pokemonTranslator.PokemonSettings.pokedex_weight_kg, pokemonTranslator.PokemonSettings.pokedex_weight_kg),
                    new _Stats._Rates(pokemonTranslator.PokemonSettings.encounter.base_capture_rate, pokemonTranslator.PokemonSettings.encounter.base_flee_rate, pokemonTranslator.PokemonSettings.encounter.attack_probability, pokemonTranslator.PokemonSettings.encounter.dodge_probability),
                    maxStats);
            }
        }

        #endregion Internal classes

        #region ctor

        public PokeStats() { }

        public PokeStats(int _gen, _Pokemon[] pokemon, DateTime updateDateTime)
        {
            last_updated = updateDateTime;
            gen = _gen;
            region = PokeConstants.Regions[_gen];
            Pokemon = pokemon;
        }

        #endregion ctor

        #region Writers

        /// <summary>
        /// Write out the stats for each generation of Pokemon
        /// </summary>
        public static void Write(IEnumerable<PokemonTranslator> pokemonTranslators, PokemonAvailability pokemonAvailability, PokemonUnreleased pokemonUnreleased, GameMasterStatsCalculator gameMasterStatsCalculator)
        {
            DateTime updateDateTime = new DateTime(Math.Max(Math.Max(
                gameMasterStatsCalculator.GameMasterStats.last_updated.Date.Ticks,
                pokemonAvailability.last_updated.Date.Ticks),
                pokemonAvailability.last_updated.Date.Ticks));

            // Create an array of lists to hold each generation.
            bool update = false;
            List<PokeStats._Pokemon>[] pokemonList = new List<PokeStats._Pokemon>[PokeConstants.Regions.Length + 1];
            for (int i = 1; i < PokeConstants.Regions.Length; i++)
            {
                string filePath = Path.Combine(Utils.OutputDataFileFolder, "pokestats.gen" + i + ".xml");
                DateTime lastUpdated = Utils.GetLastUpdated(filePath);
                if (!File.Exists(filePath) || lastUpdated < updateDateTime)
                {
                    update = true;
                    pokemonList[i] = new List<PokeStats._Pokemon>();
                }
            }

            if (update)
            {
                // Need to provide basic info for Unreleased Pokemon.
                foreach (var pokemon in pokemonUnreleased.Pokemon)
                {
                    if (pokemonList[PokeFormulas.GetGeneration(pokemon)] != null)
                        pokemonList[PokeFormulas.GetGeneration(pokemon)].Add(new PokeStats._Pokemon(pokemon,
                            pokemonAvailability.Pokemon[pokemon.id].rarity));

                    gameMasterStatsCalculator.Update(pokemon);
                }

                // Now gather the data for the Pokemon in the GAME_MASTER.
                foreach (var pokemonTranslator in pokemonTranslators)
                {
                    PokemonAvailability._Pokemon availability = pokemonAvailability.GetPokemon(pokemonTranslator.Name);
                    PokeStats._Pokemon pokemon = new PokeStats._Pokemon(pokemonTranslator, availability, GetMaxStats(pokemonTranslator));
                    if (pokemonList[PokeFormulas.GetGeneration(pokemonTranslator.Id)] != null)
                        pokemonList[PokeFormulas.GetGeneration(pokemonTranslator.Id)].Add(pokemon);

                    gameMasterStatsCalculator.Update(pokemon);
                }

                for (int i = 1; i < PokeConstants.Regions.Length; i++)
                    if (pokemonList[i] != null)
                        Utils.WriteXML(new PokeStats(i, pokemonList[i].ToArray(), updateDateTime), Path.Combine(Utils.OutputDataFileFolder, "pokestats.gen" + i + ".xml"));
            }
        }

        private static PokeStats._Pokemon._Stats._MaxStats GetMaxStats(PokemonTranslator pokemonTranslator)
        {
            return new PokeStats._Pokemon._Stats._MaxStats(PokeFormulas.GetMaxCP(pokemonTranslator), PokeFormulas.GetMaxHP(pokemonTranslator));
        }


        #endregion Writers
    }
}