﻿using System;
using System.Xml.Serialization;

using VanOrman.Utils;
using System.IO;
using System.Collections.Generic;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.DataFiles
{
    [Serializable]
    public class GAME_MASTERS
    {
        #region Properties

        [XmlAttribute(DataType = "date")]
        public DateTime last_updated { get; set; }

        [XmlElement]
        public _GAME_MASTER[] GAME_MASTER { get; set; }

        #endregion Properties

        #region Internal classes

        [Serializable]
        public class _GAME_MASTER
        {
            #region Properties

            [XmlAttribute]
            public string name { get; set; }

            [XmlAttribute]
            public bool have_original { get; set; }

            [XmlAttribute]
            public ulong timestamp_dec { get; set; }

            [XmlAttribute]
            public string timestamp_hex { get; set; }

            [XmlAttribute]
            public string timestamp { get; set; }

            #endregion Properties

            #region ctor

            public _GAME_MASTER() { }

            public _GAME_MASTER(string _name, bool _have_original)
            {
                name = _name;
                timestamp_hex = TimeStampUtils.FileNameToHexTimeStamp(name);
                timestamp_dec = TimeStampUtils.HexTimeStampToTimeStamp(timestamp_hex);
                timestamp = TimeStampUtils.TimestampToDateTime(timestamp_dec).ToString(PokeConstants.DateTimeFormat);
                have_original = _have_original;
            }

            #endregion ctor
        }

        #endregion Internal classes

        #region ctor

        public GAME_MASTERS() { }

        public GAME_MASTERS(_GAME_MASTER[] gameMasters)
        {
            last_updated = DateTime.Parse(gameMasters[0].timestamp);
            GAME_MASTER = gameMasters;
        }

        #endregion ctor

        #region Writers

        public static string XmlFilePath { get { return Path.Combine(Utils.OutputDataFileFolder, "GAME_MASTER.xml"); } }
        public static string ProjFilePath { get { return Path.Combine(Utils.RootFolder, "GAME_MASTER.proj"); } }
        
        /// <summary>
        /// Write out the list of GAME_MASTER files we are using.
        /// </summary>
        public static void Write(Dictionary<string, bool> gameMasters, GameMasterStatsCalculator gameMasterStatsCalculator)
        {
            if (!File.Exists(XmlFilePath) || Utils.GetLastUpdated(XmlFilePath) < gameMasterStatsCalculator.GameMasterStats.last_updated.Date)
            {
                List<GAME_MASTERS._GAME_MASTER> gameMasterList = new List<GAME_MASTERS._GAME_MASTER>();
                using (TextWriter writer = new StreamWriter(ProjFilePath))
                {
                    writer.WriteLine("<Project xmlns=\"http://schemas.microsoft.com/developer/msbuild/2003\">");
                    writer.WriteLine("  <!-- ======================================================================= -->");
                    writer.WriteLine("  <!-- ============= This file is generated by GM_DataGenerator. ============= -->");
                    writer.WriteLine("  <!-- ======================================================================= -->");
                    writer.WriteLine("  <ItemGroup>");

                    foreach (var gameMaster in gameMasters)
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

                Utils.WriteXML(new GAME_MASTERS(gameMasterList.ToArray()), XmlFilePath);
            }
        }

        #endregion Writers
    }
}
