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
        public class _Pokemon : Pokemon
        {
            #region Properties

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
            [DefaultValue("")]
            public string shiny { get; set; }

            [XmlAttribute]
            [DefaultValue(false)]
            public bool ditto { get; set; }

            [XmlAttribute]
            [DefaultValue("")]
            public string sprite { get; set; }

            [XmlAttribute]
            [DefaultValue("")]
            public string sprite_shiny { get; set; }

            [XmlElement]
            public PokeTypes Type { get; set; }

            [XmlElement]
            public EvolvesFrom EvolvesFrom { get; set; }

            [XmlElement]
            public _Stats Stats { get; set; }

            #endregion Properties

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

            public _Pokemon(PokemonUnreleased._Pokemon pokemon, PokemonAvailability._Pokemon _availability, string _rarity) :
                base(pokemon)
            {
                family = pokemon.family;
                rarity = _rarity;
                availability = _availability.availability;

                Type = pokemon.Type;
                EvolvesFrom = pokemon.EvolvesFrom;
            }

            public _Pokemon(PokemonTranslator pokemonTranslator, PokemonAvailability._Pokemon _availability, Traits traits, _Stats._MaxStats maxStats) :
                base(pokemonTranslator.Id, pokemonTranslator.Name, pokemonTranslator.Form)
            {
                family = pokemonTranslator.CandyType;
                buddy_km = (int)pokemonTranslator.PokemonSettings.km_buddy_distance;
                gender_ratio = pokemonTranslator.GenderRatio;
                rarity = pokemonTranslator.Rarity;
                shiny = _availability.shiny;
                ditto = traits.CanBeDitto(pokemonTranslator);
                var egg = traits.GetEgg(pokemonTranslator);
                availability = (egg != null && string.Equals(_availability.availability, PokeConstants.Availability.HatchOnly)) ?
                    string.Format(PokeConstants.Availability.HatchOnlyFormat, egg.type) :
                    _availability.availability;

                Type = new PokeTypes(pokemonTranslator.Type1, pokemonTranslator.Type2);

                if (pokemonTranslator.EvolvesFromId > 0)
                    EvolvesFrom = new EvolvesFrom(pokemonTranslator);

                Stats = new _Stats(
                    new _Stats.IVScore(pokemonTranslator.PokemonSettings.stats.base_attack, pokemonTranslator.PokemonSettings.stats.base_defense, pokemonTranslator.PokemonSettings.stats.base_stamina),
                    new _Stats.Variation(pokemonTranslator.PokemonSettings.pokedex_height_m, pokemonTranslator.PokemonSettings.pokedex_height_m),
                    new _Stats.Variation(pokemonTranslator.PokemonSettings.pokedex_weight_kg, pokemonTranslator.PokemonSettings.pokedex_weight_kg),
                    new _Stats._Rates(Math.Min(pokemonTranslator.PokemonSettings.encounter.base_capture_rate, 1.0f), pokemonTranslator.PokemonSettings.encounter.base_flee_rate, pokemonTranslator.PokemonSettings.encounter.attack_probability, pokemonTranslator.PokemonSettings.encounter.dodge_probability),
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
        public static void Write(IEnumerable<PokemonTranslator> pokemonTranslators, ManualDataSettings manualDataSettings, GameMasterStatsCalculator gameMasterStatsCalculator)
        {
            DateTime updateDateTime = new DateTime(Math.Max(Math.Max(
                gameMasterStatsCalculator.GameMasterStats.last_updated.Date.Ticks,
                manualDataSettings.PokemonAvailability.last_updated.Date.Ticks),
                manualDataSettings.PokemonAvailability.last_updated.Date.Ticks));

            // Create an array of lists to hold each generation.
            List<PokeStats._Pokemon>[] pokemonList = new List<PokeStats._Pokemon>[PokeConstants.Regions.Length + 1];
            for (int i = 1; i < PokeConstants.Regions.Length; i++)
                pokemonList[i] = new List<PokeStats._Pokemon>();

            // Need to provide basic info for Unreleased Pokemon.
            foreach (var pokemon in manualDataSettings.PokemonUnreleased.Pokemon)
            {
                if (pokemonList[PokeFormulas.GetGeneration(pokemon)] != null)
                    pokemonList[PokeFormulas.GetGeneration(pokemon)].Add(
                        new _Pokemon(pokemon, manualDataSettings.PokemonAvailability.GetPokemon(pokemon.name, pokemon.form), manualDataSettings.PokemonAvailability.Pokemon[pokemon.id].rarity));

                gameMasterStatsCalculator.Update(pokemon);
            }

            // Now gather the data for the Pokemon in the GAME_MASTER.
            foreach (var pokemonTranslator in pokemonTranslators)
            {
                int gen = PokeFormulas.GetGeneration(pokemonTranslator.Id);

                _Pokemon pokemon = new _Pokemon(pokemonTranslator,
                    manualDataSettings.PokemonAvailability.GetPokemon(pokemonTranslator.Name, pokemonTranslator.FormName),
                    manualDataSettings.Traits,
                    GetMaxStats(pokemonTranslator));
                gameMasterStatsCalculator.Update(pokemon);

                if (pokemonList[gen] != null)
                {
                    pokemonList[gen].Add(pokemon);

                    // Handle cases where there are multiple forms, but only one record in the GAME_MASTER
                    if (string.Equals(pokemonTranslator.Name, "Unown", StringComparison.OrdinalIgnoreCase)
                        || string.Equals(pokemonTranslator.Name, "Spinda", StringComparison.OrdinalIgnoreCase))
                    {
                        foreach (var form in manualDataSettings.PokemonAvailability.Pokemon[pokemonTranslator.Id].Form)
                        {
                            POGOProtos.Enums.Form formId;
                            Enum.TryParse(pokemonTranslator.Name.ToUpper() + "_" + form.name.ToUpper().Replace(' ', '_'), out formId);
                            pokemonTranslator.PokemonSettings.form = formId;
                            pokemonList[gen].Add(new _Pokemon(pokemonTranslator,
                                manualDataSettings.PokemonAvailability.GetPokemon(pokemonTranslator.Name, pokemonTranslator.FormName),
                                manualDataSettings.Traits,
                                GetMaxStats(pokemonTranslator)));
                        }
                    }
                }
            }

            for (int i = 1; i < PokeConstants.Regions.Length; i++)
            {
                string filePath = Path.Combine(Utils.OutputDataFileFolder, "pokestats.gen" + i + ".xml");
                DateTime lastUpdated = Utils.GetLastUpdated(filePath);
                if (!File.Exists(filePath) || lastUpdated < updateDateTime)
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
