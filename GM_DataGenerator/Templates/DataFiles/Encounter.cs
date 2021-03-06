﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.DataFiles
{
    [Serializable]
    public class Encounter
    {
        #region Properties

        [XmlAttribute(DataType = "date")]
        public DateTime last_updated { get; set; }

        [XmlElement]
        public Pokemon Pokemon { get; set; }

        [XmlElement]
        public Common.PossibilitySet Regular { get; set; }

        [XmlIgnore]
        public PokemonTranslator PokemonTranslator { get; set; }

        #endregion Properties

        #region ctor

        public Encounter() { }

        public Encounter(Pokemon pokemon, Common.PossibilitySet.Possibility[] possibilities, DateTime updateDateTime)
        {
            last_updated = updateDateTime;
            Pokemon = new Pokemon(pokemon);
            Regular = new Common.PossibilitySet(possibilities);
        }

        #endregion ctor

        #region Writers

        private const string srcFolder = @"charts\research\encounter\";
        private const string xmlFolder = @"encounter\";
        public static string HtmlFileFolder { get { return Path.Combine(Utils.RootFolder, srcFolder); } }
        private static string OutputXmlFolder { get { return Path.Combine(Utils.OutputDataFileFolder, xmlFolder); } }
        private static string ProjXmlFolder { get { return Path.Combine(Utils.DataFileFolder, xmlFolder); } }
        private static string ProjFilePath { get { return Path.Combine(Utils.RootFolder, "encounters.proj"); } }

        /// <summary>
        /// Write out data for Encounters
        /// </summary>
        public static void Write(ManualDataSettings manualDataSettings, GameMasterStatsCalculator gameMasterStatsCalculator)
        {
            DateTime updateDateTime = new DateTime(Math.Max(
                gameMasterStatsCalculator.GameMasterStats.last_updated.Date.Ticks,
                manualDataSettings.Encounters.last_updated.Ticks));

            if (!Directory.Exists(OutputXmlFolder))
                Directory.CreateDirectory(OutputXmlFolder);

            // Write all encounter files. Keeping track if all are up-to-date.
            bool upToDate = true;
            List<int> written = new List<int>();
            foreach (var category in manualDataSettings.Encounters.Category)
                upToDate = Write(category.Research, updateDateTime, written) && upToDate;
            foreach (var specialResearch in manualDataSettings.SpecialResearch.Event)
                foreach (var stage in specialResearch.Stage)
                {
                    upToDate = Write(stage.Research, updateDateTime, written) && upToDate;
                    if (stage.Rewards.Pokemon != null)
                        foreach (var pokemon in stage.Rewards.Pokemon)
                            upToDate = Write(pokemon, updateDateTime, written) && upToDate;
                }
            foreach (var eventResearch in manualDataSettings.EventResearch.Event)
                upToDate = Write(eventResearch.Research, updateDateTime, written) && upToDate;

            if (!upToDate)
            {
                using (TextWriter projWriter = new StreamWriter(ProjFilePath))
                {
                    projWriter.WriteLine("<Project xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">");
                    projWriter.WriteLine("  <!-- ======================================================================= -->");
                    projWriter.WriteLine("  <!-- ============= This file is generated by GM_DataGenerator. ============= -->");
                    projWriter.WriteLine("  <!-- ===================== (last_updated=\"" + updateDateTime.ToString(PokeConstants.DateFormat) + "\") ===================== --> ");
                    projWriter.WriteLine("  <!-- ======================================================================= -->");
                    projWriter.WriteLine("  <ItemGroup>");

                    foreach (var category in manualDataSettings.Encounters.Category)
                        Write(projWriter, category.Research, written);
                    foreach (var specialResearch in manualDataSettings.SpecialResearch.Event)
                        foreach (var stage in specialResearch.Stage)
                        {
                            Write(projWriter, stage.Research, written);
                            if (stage.Rewards.Pokemon != null)
                                foreach (var pokemon in stage.Rewards.Pokemon)
                                    Write(projWriter, pokemon, written);
                        }
                    foreach (var eventResearch in manualDataSettings.EventResearch.Event)
                        Write(projWriter, eventResearch.Research, written);

                    projWriter.WriteLine("  </ItemGroup>");
                    projWriter.WriteLine("</Project>");
                }
            }
        }

        /// <summary>
        /// Write out all of the Encounters for a set of Research Tasks.
        /// </summary>
        /// <param name="_research"></param>
        /// <param name="updateDateTime"></param>
        /// <param name="written"></param>
        /// <returns></returns>
        public static bool Write(IEnumerable<FieldResearch.Research> _research, DateTime updateDateTime, List<int> written)
        {
            bool upToDate = true;
            foreach (var research in _research)
                if (research.Pokemon != null)
                    foreach (var pokemon in research.Pokemon)
                        upToDate = Write(pokemon, updateDateTime, written) && upToDate;

            return upToDate;
        }

        /// <summary>
        /// Write out an Encounter chart for the specified Pokemon, if it hasn't already been written.
        /// </summary>
        /// <param name="_research"></param>
        /// <param name="updateDateTime"></param>
        /// <param name="written"></param>
        /// <returns></returns>
        public static bool Write(FieldResearch.RewardEncounter pokemon, DateTime updateDateTime, List<int> written)
        {
            bool upToDate = true;
            if (!written.Contains(pokemon.Key))
            {
                upToDate = WriteEncounter(pokemon, updateDateTime) && upToDate;
                written.Add(pokemon.Key);
            }

            return upToDate;
        }

        public static void Write(TextWriter projWriter, IEnumerable<FieldResearch.Research> _research, List<int> written)
        {
            foreach (var research in _research)
                if (research.Pokemon != null)
                    foreach (var pokemon in research.Pokemon)
                        Write(projWriter, pokemon, written);
        }

        public static void Write(TextWriter projWriter, FieldResearch.RewardEncounter pokemon, List<int> written)
        {
            if (written.Contains(pokemon.Key))
            {
                written.Remove(pokemon.Key);

                string encounterFileName = GetFileNameBase(pokemon);
                projWriter.WriteLine("    <!-- #region " + pokemon.name + " -->");

                // Add .xml as part of _datafiles
                projWriter.WriteLine("    <FixIntermediateFile Include=\"" + ProjXmlFolder + encounterFileName + ".xml\">");
                projWriter.WriteLine(@"      <Visible>true</Visible>");
                projWriter.WriteLine(@"    </FixIntermediateFile>");

                // Add .html.xml as DependentUpon .xsl
                projWriter.WriteLine("    <XslTransform  Include=\"" + srcFolder + encounterFileName + ".html.xml\">");
                projWriter.WriteLine(@"     <Visible>true</Visible>");
                projWriter.WriteLine(@"     <DependentUpon>encounter.xsl</DependentUpon>");
                projWriter.WriteLine(@"     <Dependencies>");
                projWriter.WriteLine(@"       js\global.js;");
                projWriter.WriteLine(@"       " + ProjXmlFolder + encounterFileName + ".xml;");
                projWriter.WriteLine(@"       " + srcFolder + @"index.css;");
                projWriter.WriteLine(@"       charts\research\index.css;");
                projWriter.WriteLine(@"       charts\index.css;");
                projWriter.WriteLine(@"       index.css;");
                projWriter.WriteLine(@"     </Dependencies>");
                projWriter.WriteLine(@"     <OutputFileName>" + encounterFileName + ".html</OutputFileName>");
                projWriter.WriteLine(@"    </XslTransform>");

                projWriter.WriteLine("    <!-- #endregion " + pokemon.name + " -->");
            }
        }
        /// <summary>
        /// Write out a single RaidBoss XML file if necessary, then return the text that should be included in the .proj file.
        /// </summary>
        /// <param name="raidboss"></param>
        /// <returns>The text that should be included in the .proj file</returns>
        private static bool WriteEncounter(FieldResearch.RewardEncounter pokemon, DateTime updateDateTime)
        {
            bool upToDate = true;
            string encounterFileName = GetFileNameBase(pokemon);
            string filePath = Path.Combine(OutputXmlFolder, encounterFileName + ".xml");
            DateTime lastUpdated = Utils.GetLastUpdated(filePath);
            if (!File.Exists(filePath) || lastUpdated < updateDateTime)
            {
                Utils.WriteXML(new Encounter(pokemon, Utils.GetEncounterPossibilities(pokemon.PokemonTranslator, 15), updateDateTime), filePath);
                upToDate = false;
            }

            string htmlFilePath = Path.Combine(HtmlFileFolder, encounterFileName + ".html.xml");
            if (!File.Exists(htmlFilePath))
            {
                using (TextWriter htmlWriter = new StreamWriter(htmlFilePath))
                {
                    htmlWriter.WriteLine("<?xml version=\"1.0\" encoding=\"utf-8\"?>");
                    htmlWriter.WriteLine("<!DOCTYPE Root [");
                    htmlWriter.WriteLine("  <!ENTITY Constants SYSTEM \"/_datafiles/constants.xml\">");
                    htmlWriter.WriteLine("  <!ENTITY Settings SYSTEM \"/_datafiles/settings.xml\">");
                    htmlWriter.WriteLine("  <!ENTITY PokeSprites SYSTEM \"/_datafiles.manual/infrequent/pokemon.sprites.xml\">");
                    htmlWriter.WriteLine("  <!ENTITY Images SYSTEM \"/_datafiles.manual/infrequent/images.xml\">");
                    htmlWriter.WriteLine("  <!ENTITY PokeStats SYSTEM \"/_datafiles/pokestats.gen" + PokeFormulas.GetGeneration(pokemon.id) + ".xml\">");
                    htmlWriter.WriteLine("  <!ENTITY Encounter SYSTEM \"/_datafiles/encounter/" + encounterFileName + ".xml\">");
                    htmlWriter.WriteLine("]>");
                    htmlWriter.WriteLine("<?xml-stylesheet type=\"text/xsl\" href=\"encounter.xsl\" output=\"" + encounterFileName + ".html\"?>");
                    htmlWriter.WriteLine("<Root xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
                    htmlWriter.WriteLine("  &Constants; ");
                    htmlWriter.WriteLine("  &Settings;");
                    htmlWriter.WriteLine("  &PokeSprites;");
                    htmlWriter.WriteLine("  &Images;");
                    htmlWriter.WriteLine("  &PokeStats;");
                    htmlWriter.WriteLine("  &Encounter;");
                    htmlWriter.WriteLine("</Root> ");
                }

                upToDate = false;
            }

            return upToDate;
        }

        private static string GetFileNameBase(Pokemon encounter)
        {
            return "encounter." + encounter.FileNameBase;
        }

        #endregion Writers
    }
}