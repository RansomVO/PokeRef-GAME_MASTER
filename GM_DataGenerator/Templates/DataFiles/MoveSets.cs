using POGOProtos.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData;

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

        #region Writers

        /// <summary>
        /// Write out the Move Sets for each generation of Pokemon.
        /// </summary>
        public static void Write(IEnumerable<PokemonTranslator> pokemonTranslators, Dictionary<PokemonId, FormSettingsTranslator> forms, Dictionary<PokemonMove, MoveTranslator> moves, GameMasterStatsCalculator gameMasterStatsCalculator, SpecialMoves specialMoves)
        {
            bool update = false;
            List<MoveSets._MoveSet>[] moveSetList = new List<MoveSets._MoveSet>[PokeConstants.Regions.Length + 1];
            for (int gen = 1; gen < PokeConstants.Regions.Length; gen++)
            {
                string filePath = Path.Combine(Utils.OutputDataFileFolder, "movesets.gen" + gen + ".xml");
                DateTime lastUpdated = Utils.GetLastUpdated(filePath);
                if (!File.Exists(filePath) ||
                    lastUpdated  < gameMasterStatsCalculator.GameMasterStats.last_updated.Date ||
                    lastUpdated < specialMoves.last_updated.Date)
                {
                    update = true;
                    moveSetList[gen] = new List<MoveSets._MoveSet>();
                }
            }

            if (update)
            {
                foreach (var pokemonTranslator in pokemonTranslators)
                {
                    // Need to deal with the following cases:
                    //  - Unown has multiple forms, but only a single record.
                    //  - Castform has multiple forms and multiple records, but each record has unique movesets.
                    //  - Deoxys has multiple forms and multiple records, but all records have the same movesets.
                    if (forms.ContainsKey(pokemonTranslator.PokemonSettings.pokemon_id))
                    {
                        PokemonTranslator baseRecord = null;
                        List<PokemonTranslator> records = new List<PokemonTranslator>();
                        foreach (var pokemon in pokemonTranslators)
                            if (pokemon.Id == pokemonTranslator.Id)
                            {
                                records.Add(pokemon);
                                if (pokemon.Form == Form.FORM_UNSET)
                                    baseRecord = pokemon;
                            }

                        // If there are more that 1 match, then we need to deal with pokemon with multiple forms. (E.G. Unown)
                        if (records.Count > 1)
                        {
                            // There are multiple records. We need to compare the movesets for the records.
                            int matches = 0;
                            foreach (var record in records)
                                if (IsMoveSetMatch(pokemonTranslator, record))
                                    matches++;

                            // If every record matches the moveset, skip all but the baseRecord.
                            if (matches == records.Count)
                            {
                                if (baseRecord != null)
                                    continue;
                            }
                            // If only a sub-set of records match the moveset, skip the baseRecord.
                            else if (baseRecord == null)
                                continue;
                        }
                    }

                    int gen = PokeFormulas.GetGeneration(pokemonTranslator);
                    if (moveSetList[gen] != null)
                    {
                        List<MoveSets._MoveSet> pokemonMoveSets = new List<MoveSets._MoveSet>();

                        foreach (var fastMove in pokemonTranslator.PokemonSettings.quick_moves)
                            foreach (var chargedMove in pokemonTranslator.PokemonSettings.cinematic_moves)
                                pokemonMoveSets.Add(new MoveSets._MoveSet(pokemonTranslator, moves[fastMove], false, moves[chargedMove], false));

                        foreach (var fastMove in pokemonTranslator.LegacyFastMoves)
                            foreach (var chargedMove in pokemonTranslator.PokemonSettings.cinematic_moves)
                                pokemonMoveSets.Add(new MoveSets._MoveSet(pokemonTranslator, moves[fastMove], true, moves[chargedMove], false));

                        foreach (var fastMove in pokemonTranslator.PokemonSettings.quick_moves)
                            foreach (var chargedMove in pokemonTranslator.LegacyChargedMoves)
                                pokemonMoveSets.Add(new MoveSets._MoveSet(pokemonTranslator, moves[fastMove], false, moves[chargedMove], true));

                        foreach (var fastMove in pokemonTranslator.LegacyFastMoves)
                            foreach (var chargedMove in pokemonTranslator.LegacyChargedMoves)
                                pokemonMoveSets.Add(new MoveSets._MoveSet(pokemonTranslator, moves[fastMove], true, moves[chargedMove], true));

                        double maxDPS = 0;
                        foreach (var moveSet in pokemonMoveSets)
                            if (!moveSet.FastAttack.legacy && !moveSet.ChargedAttack.legacy)
                                maxDPS = Math.Max(maxDPS, moveSet.true_dps);

                        foreach (var moveSet in pokemonMoveSets)
                        {
                            moveSet.comparison = (int)Math.Ceiling(moveSet.true_dps / maxDPS * 100);
                            gameMasterStatsCalculator.Update(moveSet);
                            moveSetList[gen].Add(moveSet);
                        }
                    }
                }

                for (int gen = 1; gen < PokeConstants.Regions.Length; gen++)
                    if (moveSetList[gen] != null && moveSetList[gen].Count > 1)
                        Utils.WriteXML(new MoveSets(gen, moveSetList[gen].ToArray()), Path.Combine(Utils.OutputDataFileFolder, "movesets.gen" + gen + ".xml"));
            }
        }

        private static bool IsMoveSetMatch(PokemonTranslator pokemonTranslator1, PokemonTranslator pokemonTranslator2)
        {
            return
                IsMoveSetMatch(pokemonTranslator1.PokemonSettings.quick_moves, pokemonTranslator2.PokemonSettings.quick_moves) &&
                IsMoveSetMatch(pokemonTranslator1.PokemonSettings.cinematic_moves, pokemonTranslator2.PokemonSettings.cinematic_moves);
        }

        private static bool IsMoveSetMatch(IEnumerable<PokemonMove> moves1, IEnumerable<PokemonMove> moves2)
        {
            bool match = true;
            foreach (var move1 in moves1)
            {
                bool found = false;
                foreach (var move2 in moves2)
                    if (string.Equals(move1, move2))
                    {
                        found = true;
                        break;
                    }
                if (!found)
                {
                    match = false;
                    break;
                }
            }

            return match;
        }

        #endregion Writers
    }
}