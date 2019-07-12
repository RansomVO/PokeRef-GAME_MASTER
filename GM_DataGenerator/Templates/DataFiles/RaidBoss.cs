﻿using POGOProtos.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;

using VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.DataFiles
{
    [Serializable]
    public class RaidBoss
    {
        #region Data

        private static readonly int[] RaidHPBoost =
        {
            0,  // Dummy to make indexing work
            600,
            1800,
            3600,   // Changed from  3000
            9000,   // Changed from  7500
            15000,  // Changed from 12500
        };

        #endregion Data

        #region Properties

        [XmlAttribute(DataType = "date")]
        public DateTime last_updated { get; set; }

        [XmlAttribute]
        [DefaultValue(0)]
        public int raid_cp { get; set; }

        [XmlElement]
        public Pokemon Pokemon { get; set; }

        [XmlElement]
        public Common.PossibilitySet Regular { get; set; }

        [XmlElement]
        public Common.PossibilitySet Boosted { get; set; }

        #endregion Properties

        #region ctor

        public RaidBoss() { }

        public RaidBoss(RaidBosses._RaidBoss raidboss, Common.IVScore baseIV, Common.PossibilitySet.Possibility[] regular, Common.PossibilitySet.Possibility[] boosted, DateTime updateDateTime)
        {
            last_updated = updateDateTime;
            Pokemon = new Pokemon(raidboss.id, raidboss.name, raidboss.FormId);
            raid_cp = (int)Math.Floor((baseIV.attack + 15) * Math.Sqrt(baseIV.defense + 15) * Math.Sqrt(RaidHPBoost[raidboss.tier]) / 10);
            Regular = new Common.PossibilitySet(regular);
            Boosted = new Common.PossibilitySet(boosted);
        }

		#endregion ctor

		#region Writers

		private const string srcFolder = @"charts\raidboss\";
		private const string xmlFolder = @"raidboss\";
		private static string HtmlFileFolder { get { return Path.Combine(Utils.RootFolder, srcFolder); } }
        private static string OutputXmlFolder { get { return Path.Combine(Utils.OutputDataFileFolder, xmlFolder); } }
		private static string ProjXmlFolder { get { return Path.Combine(Utils.DataFileFolder, xmlFolder); } }
		private static string ProjFilePath { get { return Path.Combine(Utils.RootFolder, "raidbosses.proj"); } }

        /// <summary>
        /// Write out the Raid Boss data.
        /// </summary>
        public static void Write(ManualDataSettings manualDataSettings, GameMasterStatsCalculator gameMasterStatsCalculator)
        {
            DateTime updateDateTime = gameMasterStatsCalculator.GameMasterStats.last_updated.Date;

            if (!Directory.Exists(OutputXmlFolder))
                Directory.CreateDirectory(OutputXmlFolder);

            // Write all raidboss files. Keeping track if all are up-to-date.
            bool upToDate = true;
            foreach (var raidboss in manualDataSettings.RaidBosses.RaidBoss)
                if (raidboss.id > 0)
                    upToDate = WriteRaidBoss(raidboss, updateDateTime) && upToDate;

            DateTime projLastUpdated = Utils.GetLastUpdated(ProjFilePath);
            if (!upToDate || projLastUpdated < updateDateTime)
            {
                using (TextWriter projWriter = new StreamWriter(ProjFilePath))
                {
                    projWriter.WriteLine("<Project xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">");
                    projWriter.WriteLine("  <!-- ======================================================================= -->");
                    projWriter.WriteLine("  <!-- ============= This file is generated by GM_DataGenerator. ============= -->");
                    projWriter.WriteLine("  <!-- ===================== (last_updated=\"" + updateDateTime.ToString(PokeConstants.DateFormat) + "\") ===================== --> ");
                    projWriter.WriteLine("  <!-- ======================================================================= -->");
                    projWriter.WriteLine("  <ItemGroup>");

                    foreach (var raidboss in manualDataSettings.RaidBosses.RaidBoss)
                    {
                        string raidbossFileName = GetFileNameBase(raidboss);
                        projWriter.WriteLine("    <!-- #region " + raidboss.name + " -->");

                        // Add .xml as part of _datafiles
                        projWriter.WriteLine("    <FixIntermediateFile Include=\"" + ProjXmlFolder + raidbossFileName + ".xml\">");
                        projWriter.WriteLine(@"      <Visible>true</Visible>");
                        projWriter.WriteLine(@"    </FixIntermediateFile>");

                        // Add .html.xml as DependentUpon .xsl
                        projWriter.WriteLine("    <XslTransform  Include=\"" + srcFolder + raidbossFileName + ".html.xml\">");
                        projWriter.WriteLine(@"     <Visible>true</Visible>");
                        projWriter.WriteLine(@"     <DependentUpon>raidboss.xsl</DependentUpon>");
                        projWriter.WriteLine(@"     <Dependencies>");
                        projWriter.WriteLine(@"       charts\raidboss\raidboss.js;");
                        projWriter.WriteLine(@"       js\global.js;");
                        projWriter.WriteLine(@"       " + ProjXmlFolder + raidbossFileName + ".xml;");
                        projWriter.WriteLine(@"       charts\raidboss\index.css;");
                        projWriter.WriteLine(@"       charts\index.css;");
                        projWriter.WriteLine(@"       index.css;");
                        projWriter.WriteLine(@"     </Dependencies>");
                        projWriter.WriteLine(@"     <OutputFileName>" + raidbossFileName + ".html</OutputFileName>");
                        projWriter.WriteLine(@"    </XslTransform>");

                        projWriter.WriteLine("    <!-- #endregion " + raidboss.name + " -->");
                    }

                    projWriter.WriteLine("  </ItemGroup>");
                    projWriter.WriteLine("</Project>");
                }
            }
        }

        /// <summary>
        /// Write out a single RaidBoss XML file if necessary, then return the text that should be included in the .proj file.
        /// </summary>
        /// <param name="raidboss"></param>
        /// <returns>The text that should be included in the .proj file</returns>
        private static bool WriteRaidBoss(RaidBosses._RaidBoss raidboss, DateTime updateDateTime)
        {
            bool upToDate = true;
            string raidbossFileName = GetFileNameBase(raidboss);
            string filePath = Path.Combine(OutputXmlFolder, raidbossFileName + ".xml");
            DateTime lastUpdated = Utils.GetLastUpdated(filePath);
            if (!File.Exists(filePath) || lastUpdated < updateDateTime)
            {
                Common.IVScore baseIV = new Common.IVScore(
                    raidboss.PokemonTranslator.PokemonSettings.stats.base_attack,
                    raidboss.PokemonTranslator.PokemonSettings.stats.base_defense,
                    raidboss.PokemonTranslator.PokemonSettings.stats.base_stamina
                    );
                Utils.WriteXML(
                    new RaidBoss(raidboss, baseIV, 
                    Utils.GetEncounterPossibilities(raidboss.PokemonTranslator, 20), 
                    Utils.GetEncounterPossibilities(raidboss.PokemonTranslator, 25),
                    updateDateTime), 
                    filePath);
				upToDate = false;
            }

            string htmlFilePath = Path.Combine(HtmlFileFolder, raidbossFileName + ".html.xml");
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
                    htmlWriter.WriteLine("  <!ENTITY PokeStats SYSTEM \"/_datafiles/pokestats.gen" + PokeFormulas.GetGeneration(raidboss.id) + ".xml\">");
                    htmlWriter.WriteLine("  <!ENTITY RaidBoss SYSTEM \"/_datafiles/raidboss/" + raidbossFileName + ".xml\">");
                    htmlWriter.WriteLine("]>");
                    htmlWriter.WriteLine("<?xml-stylesheet type=\"text/xsl\" href=\"raidboss.xsl\" output=\"" + raidbossFileName + ".html\"?>");
                    htmlWriter.WriteLine("<Root xmlns:xsi=\"http://www.w3.org/2001/XMLSchema-instance\">");
                    htmlWriter.WriteLine("  &Constants; ");
                    htmlWriter.WriteLine("  &Settings;");
                    htmlWriter.WriteLine("  &PokeSprites;");
                    htmlWriter.WriteLine("  &Images;");
                    htmlWriter.WriteLine("  &PokeStats;");
                    htmlWriter.WriteLine("  &RaidBoss;");
                    htmlWriter.WriteLine("</Root> ");
                }

				upToDate = false;
			}

			return upToDate;
        }

        private static string GetFileNameBase(Pokemon raidboss)
        {
            return "raidboss." + raidboss.FileNameBase;
        }

        #endregion Writers
    }
}