using System;
using System.Collections.Generic;
using System.IO;

using VanOrman.PokemonGO.GAME_MASTER.Parser.Templates;
using VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData;
using VanOrman.Utils;
using System.Xml.Serialization;
using System.Xml;
using System.Text;
using VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.DataFiles;
using VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator
{
    class GameMasterDataWriter
    {
        #region Properties

        private string RootFolder { get; set; }

        private string DataFileFolder { get { return Path.Combine(RootFolder, @"_datafiles\"); } }
        private string ManualDataFileFolder { get { return Path.Combine(RootFolder, @"_datafiles.manual\"); } }
        private string RaidBossFolder { get { return Path.Combine(RootFolder, @"charts\raidboss\"); } }
        private string RaidBossXmlFolder { get { return Path.Combine(DataFileFolder, @"raidboss\"); } }
        private string EncounterFolder { get { return Path.Combine(RootFolder, @"charts\fieldresearch\"); } }
        private string EncounterXmlFolder { get { return Path.Combine(DataFileFolder, @"encounter\"); } }

        #endregion Properties

        #region Collected Data

        ManualDataSettings ManualDataSettings { get; set; }

        GameMasterTemplate CurrentGameMaster { get; set; }

        Dictionary<string, bool> GameMasters
        {
            get
            {
                if (_gameMasters == null)
                    _gameMasters = new Dictionary<string, bool>();

                return _gameMasters;
            }
        }
        Dictionary<string, bool> _gameMasters;

        Dictionary<string, PokemonTranslator> Pokemon
        {
            get
            {
                if (_pokemon == null)
                    _pokemon = new Dictionary<string, PokemonTranslator>();

                return _pokemon;
            }
        }
        private Dictionary<string, PokemonTranslator> _pokemon;

        Dictionary<string, FormSettingsTranslator> Forms
        {
            get
            {
                if (_forms == null)
                    _forms = new Dictionary<string, FormSettingsTranslator>();

                return _forms;
            }
        }
        private Dictionary<string, FormSettingsTranslator> _forms;

        Dictionary<string, MoveTranslator> Moves
        {
            get
            {
                if (_moves == null)
                    _moves = new Dictionary<string, MoveTranslator>();

                return _moves;
            }
        }
        private Dictionary<string, MoveTranslator> _moves;

        Dictionary<string, GenderRatioTranslator> GenderRatios
        {
            get
            {
                if (_genderRatios == null)
                    _genderRatios = new Dictionary<string, GenderRatioTranslator>();

                return _genderRatios;
            }
        }
        private Dictionary<string, GenderRatioTranslator> _genderRatios;

        PlayerLevelTranslator PlayerLevel { get; set; }

        GameMasterStatsCalculator GameMasterStatsCalculator { get; set; }

        #endregion Collected Data

        #region ctor

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="gameMaster"></param>
        /// <param name="legacyGameMasters"></param>
        /// <param name="rootFolder"></param>
        public GameMasterDataWriter(GameMasterTemplate gameMasterTemplate, IEnumerable<GameMasterTemplate> legacyGameMasterTemplates, string rootFolder)
        {
            RootFolder = rootFolder;

            // Read the config.
            ManualDataSettings = new ManualDataSettings(ManualDataFileFolder);

            GameMasterStatsCalculator = new GameMasterStatsCalculator(gameMasterTemplate.FileName);

            CollectData(gameMasterTemplate, legacyGameMasterTemplates);

            PokeFormulas.Init(PlayerLevel);
        }

        #endregion ctor

        /// <summary>
        /// Writes out all of the .XML files.
        /// </summary>
        public void Write()
        {
            if (!Directory.Exists(DataFileFolder))
                Directory.CreateDirectory(DataFileFolder);

            WriteGameMaster();
            WriteConstants();
            WriteMoves();
            WriteMoveSets();
            WritePokeStats();
            WriteRaidBosses();
            WriteEncounters();
            WriteSettings();
        }

        #region Output methods

        /// <summary>
        /// Write out the list of GAME_MASTER files we are using.
        /// </summary>
        private void WriteGameMaster()
        {
            string filePath = Path.Combine(DataFileFolder, "GAME_MASTER.xml");
            if (!File.Exists(filePath) || GetLastUpdated(filePath) < GameMasterStatsCalculator.GameMasterStats.last_updated.Date)
            {
                List<GAME_MASTERS._GAME_MASTER> gameMasterList = new List<GAME_MASTERS._GAME_MASTER>();
                using (TextWriter writer = new StreamWriter(Path.Combine(RootFolder, "GAME_MASTER.proj")))
                {
                    writer.WriteLine("<Project xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">");
                    writer.WriteLine("  <!-- ======================================================================= -->");
                    writer.WriteLine("  <!-- ============= This file is generated by GM_DataGenerator. ============= -->");
                    writer.WriteLine("  <!-- ======================================================================= -->");
                    writer.WriteLine("  <ItemGroup>");

                    foreach (var gameMaster in GameMasters)
                    {
                        gameMasterList.Add(new GAME_MASTERS._GAME_MASTER(gameMaster.Key, gameMaster.Value));

                        if (gameMaster.Value)
                        {
                            writer.WriteLine("    <WebContent Include=\"tech\\GAME_MASTER\\archive\\" + gameMaster.Key + "\"> ");
                            writer.WriteLine("      <Visible>true</Visible>");
                            writer.WriteLine("      <DependentUpon>index.html.xml</DependentUpon>");
                            writer.WriteLine("    </WebContent>");
                        }
                        writer.WriteLine("    <WebContent Include=\"tech\\GAME_MASTER\\archive\\" + gameMaster.Key + ".json\"> ");
                        writer.WriteLine("      <Visible>true</Visible>");
                        writer.WriteLine("      <DependentUpon>index.html.xml</DependentUpon>");
                        writer.WriteLine("    </WebContent>");
                    }

                    writer.WriteLine("  </ItemGroup>");
                    writer.WriteLine("</Project>");
                }

                WriteXML(new GAME_MASTERS(gameMasterList.ToArray()), filePath);
            }
        }

        /// <summary>
        /// Write the files that don't usually change.
        /// </summary>
        private void WriteConstants()
        {
            string filePath = Path.Combine(DataFileFolder, "constants.xml");
            if (!File.Exists(filePath) || GetLastUpdated(filePath) < PokeConstants.LastModified)
                WriteXML(new Constants(), filePath);

            filePath = Path.Combine(DataFileFolder, "effectiveness.xml");
            if (!File.Exists(filePath) || GetLastUpdated(filePath) < MoveEffectiveness.LastModified)
                WriteXML(new MoveEffectiveness(), filePath);
        }

        /// <summary>
        /// Write out the Moves that are available in the game.
        /// </summary>
        private void WriteMoves()
        {
            string filePathFast = Path.Combine(DataFileFolder, "moves.fast.xml");
            string filePathCharged = Path.Combine(DataFileFolder, "moves.charged.xml");
            if (!File.Exists(filePathFast) || GetLastUpdated(filePathFast) < GameMasterStatsCalculator.GameMasterStats.last_updated.Date ||
                !File.Exists(filePathCharged) || GetLastUpdated(filePathCharged) < GameMasterStatsCalculator.GameMasterStats.last_updated.Date)
            {
                List<Moves._Move> movesFast = new List<Moves._Move>();
                List<Moves._Move> movesCharged = new List<Moves._Move>();
                foreach (var move in Moves.Values)
                {
                    (move.IsFast ? movesFast : movesCharged).Add(
                        new Moves._Move(move.Name, move.Type, move.Energy, move.Power, move.Duration, move.DamageWindowStart, move.DamageWindowEnd));
                }

                if (!File.Exists(filePathFast) || GetLastUpdated(filePathFast) < GameMasterStatsCalculator.GameMasterStats.last_updated.Date)
                    WriteXML(new Moves()
                    {
                        last_updated = DateTime.Today,
                        category = "Fast",
                        Move = movesFast.ToArray(),
                    }, filePathFast);

                if (!File.Exists(filePathCharged) || GetLastUpdated(filePathCharged) < GameMasterStatsCalculator.GameMasterStats.last_updated.Date)
                    WriteXML(new Moves()
                    {
                        last_updated = DateTime.Today,
                        category = "Charged",
                        Move = movesCharged.ToArray(),
                    }, filePathCharged);
            }
        }

        /// <summary>
        /// Write out the Move Sets for each generation of Pokemon;
        /// </summary>
        private void WriteMoveSets()
        {
            bool update = false;
            List<MoveSets._MoveSet>[] moveSetList = new List<MoveSets._MoveSet>[PokeConstants.Regions.Length + 1];
            for (int gen = 1; gen < PokeConstants.Regions.Length; gen++)
            {
                string filePath = Path.Combine(DataFileFolder, "movesets.gen" + gen + ".xml");
                if (!File.Exists(filePath) || GetLastUpdated(filePath) < GameMasterStatsCalculator.GameMasterStats.last_updated.Date)
                {
                    update = true;
                    moveSetList[gen] = new List<MoveSets._MoveSet>();
                }
            }

            if (update)
            {
                foreach (var pokemonTranslator in Pokemon.Values)
                {
                    // Need to deal with the following cases:
                    //  - Unown has multiple forms, but only a single record.
                    //  - Castform has multiple forms and multiple records, but each record has unique movesets.
                    //  - Deoxys has multiple forms and multiple records, but all records have the same movesets.
                    if (Forms.ContainsKey(pokemonTranslator.Id.ToString("0000")))
                    {
                        PokemonTranslator baseRecord = null;
                        List<PokemonTranslator> records = new List<PokemonTranslator>();
                        foreach (var pokemon in Pokemon.Values)
                            if (pokemon.Id == pokemonTranslator.Id)
                            {
                                records.Add(pokemon);
                                if (pokemonTranslator.Key.StartsWith(pokemon.Key) && !string.Equals(pokemonTranslator.Key, pokemon.Key))
                                    baseRecord = pokemon;
                            }

                        // If there are no matches, then there is only 1 record, so proceed. (E.G. Unown)
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

                        foreach (var fastMove in pokemonTranslator.PokemonSettings.quickMoves)
                            foreach (var chargedMove in pokemonTranslator.PokemonSettings.cinematicMoves)
                                pokemonMoveSets.Add(GetMoveSet(pokemonTranslator, fastMove, false, chargedMove, false));

                        foreach (var fastMove in pokemonTranslator.LegacyFastMoves)
                            foreach (var chargedMove in pokemonTranslator.PokemonSettings.cinematicMoves)
                                pokemonMoveSets.Add(GetMoveSet(pokemonTranslator, fastMove, true, chargedMove, false));

                        foreach (var fastMove in pokemonTranslator.PokemonSettings.quickMoves)
                            foreach (var chargedMove in pokemonTranslator.LegacyChargedMoves)
                                pokemonMoveSets.Add(GetMoveSet(pokemonTranslator, fastMove, false, chargedMove, true));

                        foreach (var fastMove in pokemonTranslator.LegacyFastMoves)
                            foreach (var chargedMove in pokemonTranslator.LegacyChargedMoves)
                                pokemonMoveSets.Add(GetMoveSet(pokemonTranslator, fastMove, true, chargedMove, true));

                        double maxDPS = 0;
                        foreach (var moveSet in pokemonMoveSets)
                            maxDPS = Math.Max(maxDPS, moveSet.true_dps);

                        foreach (var moveSet in pokemonMoveSets)
                        {
                            moveSet.comparison = (int)Math.Ceiling(moveSet.true_dps * 100 / maxDPS);
                            GameMasterStatsCalculator.Update(moveSet);
                            moveSetList[gen].Add(moveSet);
                        }
                    }
                }

                for (int gen = 1; gen < PokeConstants.Regions.Length; gen++)
                    if (moveSetList[gen] != null && moveSetList[gen].Count > 1)
                        WriteXML(new MoveSets(gen, moveSetList[gen].ToArray()), Path.Combine(DataFileFolder, "movesets.gen" + gen + ".xml"));
            }
        }

        /// <summary>
        /// Write out the stats for each generation of Pokemon
        /// </summary>
        private void WritePokeStats()
        {
            // Create an array of lists to hold each generation.
            bool update = false;
            List<PokeStats._Pokemon>[] pokemonList = new List<PokeStats._Pokemon>[PokeConstants.Regions.Length + 1];
            for (int i = 1; i < PokeConstants.Regions.Length; i++)
            {
                string filePath = Path.Combine(DataFileFolder, "pokestats.gen" + i + ".xml");
                if (!File.Exists(filePath) || GetLastUpdated(filePath) < GameMasterStatsCalculator.GameMasterStats.last_updated.Date)
                {
                    update = true;
                    pokemonList[i] = new List<PokeStats._Pokemon>();
                }
            }

            if (update)
            {
                // Need to provide basic info for Unreleased Pokemon.
                foreach (var pokemon in ManualDataSettings.PokemonUnreleased.Pokemon)
                {
                    if (pokemonList[PokeFormulas.GetGeneration(pokemon)] != null)
                        pokemonList[PokeFormulas.GetGeneration(pokemon)].Add(new PokeStats._Pokemon(pokemon,
                            ManualDataSettings.PokemonAvailability.Pokemon[pokemon.id].rarity));

                    GameMasterStatsCalculator.Update(pokemon);
                }

                // Now gather the data for the Pokemon in the GAME_MASTER.
                foreach (var pokemonTranslator in Pokemon.Values)
                {
                    PokemonAvailability._Pokemon availability = ManualDataSettings.PokemonAvailability.GetPokemon(pokemonTranslator.Name);
                    PokeStats._Pokemon pokemon = new PokeStats._Pokemon(pokemonTranslator,
                        availability.availability, availability.rarity, availability.shiny,
                        GetMaxStats(pokemonTranslator));
                    if (pokemonList[PokeFormulas.GetGeneration(pokemonTranslator.Id)] != null)
                        pokemonList[PokeFormulas.GetGeneration(pokemonTranslator.Id)].Add(pokemon);

                    GameMasterStatsCalculator.Update(pokemon);
                }

                for (int i = 1; i < PokeConstants.Regions.Length; i++)
                    if (pokemonList[i] != null)
                        WriteXML(new PokeStats(i, pokemonList[i].ToArray()), Path.Combine(DataFileFolder, "pokestats.gen" + i + ".xml"));
            }
        }

        /// <summary>
        /// Write out the Raid Boss data.
        /// </summary>
        private void WriteRaidBosses()
        {
            if (!Directory.Exists(RaidBossXmlFolder))
                Directory.CreateDirectory(RaidBossXmlFolder);

            using (TextWriter projWriter = new StreamWriter(Path.Combine(RootFolder, "raidbosses.proj")))
            {
                // TODO QZX: Check all raidboss files to see if they are already up-to-date.

                projWriter.WriteLine("<Project xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">");
                projWriter.WriteLine("  <!-- ======================================================================= -->");
                projWriter.WriteLine("  <!-- ============= This file is generated by GM_DataGenerator. ============= -->");
                projWriter.WriteLine("  <!-- ======================================================================= -->");
                projWriter.WriteLine("  <ItemGroup>");

                foreach (var raidboss in ManualDataSettings.RaidBosses.RaidBoss)
                    if (raidboss.id > 0)
                        projWriter.Write(WriteRaidBoss(raidboss));

                projWriter.WriteLine("  </ItemGroup>");
                projWriter.WriteLine("</Project>");
            }
        }

        /// <summary>
        /// Write out a single RaidBoss XML file if necessary, then return the text that should be included in the .proj file.
        /// </summary>
        /// <param name="raidboss"></param>
        /// <returns>The text that should be included in the .proj file</returns>
        private string WriteRaidBoss(RaidBosses._RaidBoss raidboss)
        {
            string raidbossFileName = "raidboss." + raidboss.name.ToLower();
            string filePath = Path.Combine(RaidBossXmlFolder, raidbossFileName + ".xml");
            DateTime lastUpdated = GetLastUpdated(filePath);
            if (!File.Exists(filePath) ||
                lastUpdated < ManualDataSettings.RaidBosses.last_updated ||
                lastUpdated < GameMasterStatsCalculator.GameMasterStats.last_updated.Date)
            {
                WriteXML(new RaidBoss(raidboss, GetEncounterPossibilities(raidboss.id, 20), GetEncounterPossibilities(raidboss.id, 25)), filePath);
            }

            string htmlFilePath = Path.Combine(RaidBossFolder, raidbossFileName + ".html.xml");
            if (!File.Exists(htmlFilePath))
                using (TextWriter htmlWriter = new StreamWriter(htmlFilePath))
                {
                    htmlWriter.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    htmlWriter.WriteLine("<!DOCTYPE Root [");
                    htmlWriter.WriteLine("  <!ENTITY Constants SYSTEM \"/_datafiles/constants.xml\">");
					htmlWriter.WriteLine("  <!ENTITY Settings SYSTEM \"/_datafiles/settings.xml\">");
					htmlWriter.WriteLine("  <!ENTITY Settings SYSTEM \"/_datafiles.manual/images.xml\">");
					htmlWriter.WriteLine("  <!ENTITY RaidBoss SYSTEM \"/_datafiles/raidboss/" + raidbossFileName + ".xml\">");
                    htmlWriter.WriteLine("]>");
                    htmlWriter.WriteLine("<?xml-stylesheet type=\"text/xsl\" href=\"raidboss.xsl\" output=\"" + raidbossFileName + ".html\"?>");
                    htmlWriter.WriteLine("<Root xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
                    htmlWriter.WriteLine("  &Constants; ");
					htmlWriter.WriteLine("  &Settings;");
					htmlWriter.WriteLine("  &Images;");
					htmlWriter.WriteLine("  &RaidBoss;");
                    htmlWriter.WriteLine("</Root> ");
                }


            // Create the output to be included in the .proj file.
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("    <!-- #region " + raidboss.name + " -->");

            // Add .xml as part of _datafiles
            stringBuilder.AppendLine("    <FixIntermediateFile Include=\"" + @"_datafiles\raidboss\" + raidbossFileName + ".xml\">");
            stringBuilder.AppendLine(@"      <Visible>true</Visible>");
            stringBuilder.AppendLine(@"    </FixIntermediateFile>");

            // Add .html.xml as DependentUpon .xsl
            stringBuilder.AppendLine("    <XslTransform  Include=\"" + @"charts\raidboss\" + raidbossFileName + ".html.xml\">");
            stringBuilder.AppendLine(@"     <Visible>true</Visible>");
            stringBuilder.AppendLine(@"     <DependentUpon>raidboss.xsl</DependentUpon>");
            stringBuilder.AppendLine(@"     <Dependencies>");
            stringBuilder.AppendLine(@"       charts\raidboss\raidboss.js;");
            stringBuilder.AppendLine(@"       js\global.js;");
            stringBuilder.AppendLine(@"       _datafiles\raidboss\" + raidbossFileName + ".xml;");
            stringBuilder.AppendLine(@"       charts\raidboss\index.css;");
            stringBuilder.AppendLine(@"       charts\index.css;");
            stringBuilder.AppendLine(@"       index.css;");
            stringBuilder.AppendLine(@"     </Dependencies>");
            stringBuilder.AppendLine(@"     <OutputFileName>" + raidbossFileName + ".html</OutputFileName>");
            stringBuilder.AppendLine(@"    </XslTransform>");

            stringBuilder.AppendLine("    <!-- #endregion " + raidboss.name + " -->");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Write out data for Encounters
        /// </summary>
        private void WriteEncounters()
        {
            if (!Directory.Exists(EncounterXmlFolder))
                Directory.CreateDirectory(EncounterXmlFolder);

            using (TextWriter projWriter = new StreamWriter(Path.Combine(RootFolder, "encounters.proj")))
            {
                // TODO QZX: Check all raidboss files to see if they are already up-to-date.

                projWriter.WriteLine("<Project xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">");
                projWriter.WriteLine("  <!-- ======================================================================= -->");
                projWriter.WriteLine("  <!-- ============= This file is generated by GM_DataGenerator. ============= -->");
                projWriter.WriteLine("  <!-- ======================================================================= -->");
                projWriter.WriteLine("  <ItemGroup>");

                foreach (var category in ManualDataSettings.FieldResearch.Category)
                    foreach (var research in category.Research)
                        if (research.Encounter != null)
                            foreach (var encounter in research.Encounter)
                                projWriter.Write(WriteEncounter(encounter));

                projWriter.WriteLine("  </ItemGroup>");
                projWriter.WriteLine("</Project>");
            }
        }

        /// <summary>
        /// Write out a single RaidBoss XML file if necessary, then return the text that should be included in the .proj file.
        /// </summary>
        /// <param name="raidboss"></param>
        /// <returns>The text that should be included in the .proj file</returns>
        private string WriteEncounter(Pokemon encounter)
        {
            string encounterFileName = "encounter." + encounter.name.ToLower();
            string filePath = Path.Combine(EncounterXmlFolder, encounterFileName + ".xml");
            DateTime lastUpdated = GetLastUpdated(filePath);
            if (!File.Exists(filePath) ||
                lastUpdated < ManualDataSettings.FieldResearch.last_updated ||
                lastUpdated < GameMasterStatsCalculator.GameMasterStats.last_updated.Date)
            {
                WriteXML(new Templates.DataFiles.Encounter(encounter, GetEncounterPossibilities(encounter.id, 15)), filePath);
            }

            string htmlFilePath = Path.Combine(EncounterFolder, encounterFileName + ".html.xml");
            if (!File.Exists(htmlFilePath))
                using (TextWriter htmlWriter = new StreamWriter(htmlFilePath))
                {
                    htmlWriter.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    htmlWriter.WriteLine("<!DOCTYPE Root [");
                    htmlWriter.WriteLine("  <!ENTITY Constants SYSTEM \"/_datafiles/constants.xml\">");
                    htmlWriter.WriteLine("  <!ENTITY Settings SYSTEM \"/_datafiles/settings.xml\">");
					htmlWriter.WriteLine("  <!ENTITY Settings SYSTEM \"/_datafiles.manual/images.xml\">");
					htmlWriter.WriteLine("  <!ENTITY Encounter SYSTEM \"/_datafiles/encounter/" + encounterFileName + ".xml\">");
                    htmlWriter.WriteLine("]>");
                    htmlWriter.WriteLine("<?xml-stylesheet type=\"text/xsl\" href=\"encounter.xsl\" output=\"" + encounterFileName + ".html\"?>");
                    htmlWriter.WriteLine("<Root xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
                    htmlWriter.WriteLine("  &Constants; ");
                    htmlWriter.WriteLine("  &Settings;");
					htmlWriter.WriteLine("  &Images;");
					htmlWriter.WriteLine("  &Encounter;");
                    htmlWriter.WriteLine("</Root> ");
                }


            // Create the output to be included in the .proj file.
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("    <!-- #region " + encounter.name + " -->");

            // Add .xml as part of _datafiles
            stringBuilder.AppendLine("    <FixIntermediateFile Include=\"" + @"_datafiles\encounter\" + encounterFileName + ".xml\">");
            stringBuilder.AppendLine(@"      <Visible>true</Visible>");
            stringBuilder.AppendLine(@"    </FixIntermediateFile>");

            // Add .html.xml as DependentUpon .xsl
            stringBuilder.AppendLine("    <XslTransform  Include=\"" + @"charts\fieldresearch\" + encounterFileName + ".html.xml\">");
            stringBuilder.AppendLine(@"     <Visible>true</Visible>");
            stringBuilder.AppendLine(@"     <DependentUpon>encounter.xsl</DependentUpon>");
            stringBuilder.AppendLine(@"     <Dependencies>");
            stringBuilder.AppendLine(@"       js\global.js;");
            stringBuilder.AppendLine(@"       _datafiles\encounter\" + encounterFileName + ".xml;");
            stringBuilder.AppendLine(@"       charts\fieldresearch\index.css;");
            stringBuilder.AppendLine(@"       charts\index.css;");
            stringBuilder.AppendLine(@"       index.css;");
            stringBuilder.AppendLine(@"     </Dependencies>");
            stringBuilder.AppendLine(@"     <OutputFileName>" + encounterFileName + ".html</OutputFileName>");
            stringBuilder.AppendLine(@"    </XslTransform>");

            stringBuilder.AppendLine("    <!-- #endregion " + encounter.name + " -->");

            return stringBuilder.ToString();
        }

        /// <summary>
        /// Write the rarely-changed files that are referenced globally.
        /// </summary>
        private void WriteSettings()
        {
            string filePath = Path.Combine(DataFileFolder, "settings.xml");
            DateTime lastUpdated = GetLastUpdated(filePath);

            if (!File.Exists(filePath) ||
                lastUpdated < GameMasterStatsCalculator.GameMasterStats.last_updated.Date ||
                lastUpdated < ManualDataSettings.Ranges.last_updated)
            {
                Settings settings = new Settings()
                {
                    last_updated = DateTime.Today,
                    GameMasterStats = GameMasterStatsCalculator.GameMasterStats,
                    Desirable = new Settings._Desirable(ManualDataSettings.Ranges.Desirability),
                    MaxCP = new Settings.Range(ManualDataSettings.Ranges.MaxCP),
                    MaxHP = new Settings.Range(ManualDataSettings.Ranges.MaxHP),
                    DPSPercent = new Settings.Range(ManualDataSettings.Ranges.DPSPercent),
                    DPS = new Settings.Range(ManualDataSettings.Ranges.DPS),
                    Capture = new Settings.Difficulty(ManualDataSettings.Ranges.CaptureRate),
                    Flee = new Settings.Rate(ManualDataSettings.Ranges.FleeRate),
                    Attack = new Settings.Rate(ManualDataSettings.Ranges.AttackRate),
                    Dodge = new Settings.Rate(ManualDataSettings.Ranges.DodgeRate),
                };

                WriteXML(settings, filePath);
            }
        }

        #endregion Output methods

        #region Calculations

        private PokeStats._Pokemon._Stats._MaxStats GetMaxStats(PokemonTranslator pokemonTranslator)
        {
            return new PokeStats._Pokemon._Stats._MaxStats(PokeFormulas.GetMaxCP(pokemonTranslator), PokeFormulas.GetMaxHP(pokemonTranslator));
        }

        private Common.PossibilitySet.Possibility[] GetEncounterPossibilities(int pokemonId, int level)
        {
            Dictionary<int, List<Common.PossibilitySet.Possibility.IV>> IVs = new Dictionary<int, List<Common.PossibilitySet.Possibility.IV>>();
            for (int attack = PokeConstants.MinRaidBossIV; attack <= PokeConstants.Evaluation.Attribute.Max; attack++)
            {
                for (int defense = PokeConstants.MinRaidBossIV; defense <= PokeConstants.Evaluation.Attribute.Max; defense++)
                {
                    for (int stamina = PokeConstants.MinRaidBossIV; stamina <= PokeConstants.Evaluation.Attribute.Max; stamina++)
                    {
                        Common.IVScore pokemonIV = new Common.IVScore(attack, defense, stamina);
                        Common.IVScore baseIV = GetBaseIV(pokemonId);

                        int cp = PokeFormulas.GetPokemonCP(baseIV, pokemonIV, level);
                        int hp = PokeFormulas.GetPokemonHP(baseIV.stamina, stamina, level);
                        if (!IVs.ContainsKey(cp))
                            IVs.Add(cp, new List<Common.PossibilitySet.Possibility.IV>());
                        IVs[cp].Add(new Common.PossibilitySet.Possibility.IV(pokemonIV, hp));
                    }
                }
            }

            List<Common.PossibilitySet.Possibility> possibilities = new List<Common.PossibilitySet.Possibility>();
            foreach (var ivs in IVs)
                possibilities.Add(new Common.PossibilitySet.Possibility(ivs.Key, ivs.Value.ToArray()));

            return possibilities.ToArray();
        }

        private MoveSets._MoveSet GetMoveSet(PokemonTranslator pokemonTranslator, string fastMove, bool fastMoveLegacy, string chargedMove, bool chargedMoveLegacy)
        {
            // Check to see if this is one of those cases where the GAME_MASTER specifies a number instead of the name.
            int i;
            if (int.TryParse(fastMove, out i))
                fastMove = GetMoveByID(i);
            if (int.TryParse(chargedMove, out i))
                chargedMove = GetMoveByID(i);

            // TODO QZX: Fix it so Moves[] keys use MOVE_NAME instead of a number, like 292
            return new MoveSets._MoveSet(pokemonTranslator, Moves[fastMove], fastMoveLegacy, Moves[chargedMove], chargedMoveLegacy);
        }

        private string GetMoveByID(int id)
        {
            foreach (var move in Moves.Values)
                if (move.TemplateId.StartsWith("V" + id.ToString("0000")))
                    return move.Key;

            return null;
        }

        #endregion Calculations

        #region Support Methods

        /// <summary>
        /// Export the specified value to an .xml file.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="filePath"></param>
        private static void WriteXML(object value, string filePath)
        {
            Console.Out.WriteLine("Writing " + filePath);

            XmlSerializer xmlSerializer = new XmlSerializer(value.GetType());
            using (XmlWriter writer = XmlWriter.Create(filePath,
                new XmlWriterSettings() { Encoding = Encoding.UTF8, NamespaceHandling = NamespaceHandling.OmitDuplicates, Indent = true, NewLineHandling = NewLineHandling.Entitize, }))
            {
                xmlSerializer.Serialize(writer, value);
            }
        }

        private Common.IVScore GetBaseIV(int pokemonId)
        {
            // Get the PokemonTemplate corresponding to the specified pokemon, and return the base IV stats for it.
            foreach (var pokemonTranslator in Pokemon.Values)
            {
                if (pokemonTranslator.Id == pokemonId)
                    return new Common.IVScore(pokemonTranslator.PokemonSettings.stats);
            }

            return new Common.IVScore();
        }

        private static DateTime GetLastUpdated(string filePath)
        {
            if (File.Exists(filePath))
            {
                const string marker = "last_updated=\"";

                using (StreamReader reader = new StreamReader(filePath))
                {
                    string line;
                    while ((line = reader.ReadLine()) != null)
                    {
                        int pos = line.IndexOf(marker);
                        if (pos != -1)
                        {
                            pos += marker.Length;
                            return DateTime.Parse(line.Substring(pos, line.IndexOf("\"", pos) - pos));
                        }
                    }
                }
            }

            return DateTime.MinValue;
        }

        private static bool IsMoveSetMatch(PokemonTranslator pokemonTranslator1, PokemonTranslator pokemonTranslator2)
        {
            return
                IsMoveSetMatch(pokemonTranslator1.PokemonSettings.quickMoves, pokemonTranslator2.PokemonSettings.quickMoves) &&
                IsMoveSetMatch(pokemonTranslator1.PokemonSettings.cinematicMoves, pokemonTranslator2.PokemonSettings.cinematicMoves);
        }

        private static bool IsMoveSetMatch(IEnumerable<string> moves1, IEnumerable<string> moves2)
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

        #endregion Support Methods

        /// <summary>
        /// Goes through the GAME_MASTERs and collects the data we want to leverage for the PokeRef site.
        /// </summary>
        /// <param name="gameMaster"></param>
        /// <param name="legacyGameMasters"></param>
        private void CollectData(GameMasterTemplate gameMasterTemplate, IEnumerable<GameMasterTemplate> legacyGameMasterTemplates)
        {
            // Get a list of all of the GAME_MASTER files.
            CurrentGameMaster = gameMasterTemplate;
            GameMasters.Add(gameMasterTemplate.FileName, gameMasterTemplate.HaveRawGameMaster);
            foreach (var legacyGameMasterTemplate in legacyGameMasterTemplates)
                GameMasters.Add(legacyGameMasterTemplate.FileName, legacyGameMasterTemplate.HaveRawGameMaster);

            // Process the current GameMaster
            foreach (var itemTemplate in gameMasterTemplate.GameMaster.itemTemplates)
            {
                try
                {
                    if (itemTemplate.pokemonSettings != null)
                    {
                        PokemonTranslator pokemon = new PokemonTranslator(itemTemplate);
                        Pokemon.Add(pokemon.Key, pokemon);
                    }
                    else if (itemTemplate.moveSettings != null)
                    {
                        MoveTranslator move = new MoveTranslator(itemTemplate);
                        Moves.Add(move.Key, move);
                    }
                    else if (itemTemplate.genderSettings != null)
                    {
                        GenderRatioTranslator genderRatio = new GenderRatioTranslator(itemTemplate);

                        // Some Pokemon are duplicated and should be ignored. (E.G. Castform for each of the weathers.) 
                        if (GenderRatios.ContainsKey(genderRatio.Key))
                            continue;

                        GenderRatios.Add(genderRatio.Key, genderRatio);
                    }
                    else if (itemTemplate.playerLevel != null)
                    {
                        PlayerLevel = new PlayerLevelTranslator(itemTemplate);
                    }
                    else if (itemTemplate.formSettings != null)
                    {
                        if (itemTemplate.formSettings.forms != null)
                        {
                            FormSettingsTranslator formSettings = new FormSettingsTranslator(itemTemplate);
                            Forms.Add(formSettings.Key, formSettings);
                        }
                    }

                    #region Data I am currently not using.

                    //else if (itemTemplate.avatarCustomization != null)
                    //{
                    //}
                    //else if (itemTemplate.badgeSettings != null)
                    //{
                    //}
                    //else if (itemTemplate.battleSettings != null)
                    //{
                    //}
                    //else if (itemTemplate.camera != null)
                    //{
                    //}
                    //else if (itemTemplate.encounterSettings != null)
                    //{
                    //}
                    //else if (itemTemplate.gymBadgeSettings != null)
                    //{
                    //}
                    //else if (itemTemplate.gymLevel != null)
                    //{
                    //}
                    //else if (itemTemplate.iapItemDisplay != null)
                    //{
                    //}
                    //else if (itemTemplate.iapSettings != null)
                    //{
                    //}
                    //else if (itemTemplate.itemSettings != null)
                    //{
                    //}
                    //else if (itemTemplate.moveSequenceSettings != null)
                    //{
                    //}
                    //else if (itemTemplate.pokemonUpgrades != null)
                    //{
                    //}
                    //else if (itemTemplate.questSettings != null)
                    //{
                    //}
                    //else if (itemTemplate.typeEffective != null)
                    //{
                    //}

                    #endregion Data I am currently not using.
                }
                catch (Exception ex)
                {
                    ConsoleOutput.OutputException(ex, "Error processing {0}", itemTemplate.templateId);
                }
            }

            Legacy.Initialize(gameMasterTemplate, legacyGameMasterTemplates, ManualDataSettings.PokemonAvailability, ManualDataSettings.SpecialMoves);
            foreach (var pokemon in Pokemon.Values)
                pokemon.AssignProperties(Pokemon,
                    GenderRatios.ContainsKey(pokemon.Key) ? GenderRatios[pokemon.Key] : null,
                    Legacy.FastMoves.ContainsKey(pokemon.TemplateId) ? Legacy.FastMoves[pokemon.TemplateId] : null,
                    Legacy.ChargedMoves.ContainsKey(pokemon.TemplateId) ? Legacy.ChargedMoves[pokemon.TemplateId] : null);
        }
    }
}