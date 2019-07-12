using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

using VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates;
using VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.DataFiles;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator
{
    static class Utils
    {
        public static void Init(string rootFolder)
        {
            RootFolder = rootFolder;
            GameMasterTimestampUtils.Init(GAME_MASTER_Folder);
        }

        #region Properties

        public static string RootFolder { get; private set; }

		public static string DataFileFolder { get { return @"_datafiles\"; } }

		public static string OutputDataFileFolder { get { return Path.Combine(RootFolder, DataFileFolder); } }

        public static string InputManualDataFileFolder { get { return Path.Combine(RootFolder, @"_datafiles.manual\"); } }

        public static string GAME_MASTER_Folder { get { return Path.Combine(RootFolder, @"_GAME_MASTER\"); } }

        #endregion Properties

        /// <summary>
        /// Export the specified value to an .xml file.
        /// </summary>
        /// <param name="value"></param>
        /// <param name="filePath"></param>
        public static void WriteXML(object value, string filePath)
        {
            Console.Out.WriteLine("Writing " + filePath);

            XmlSerializer xmlSerializer = new XmlSerializer(value.GetType());
            using (XmlWriter writer = XmlWriter.Create(filePath,
                new XmlWriterSettings() { Encoding = Encoding.UTF8, NamespaceHandling = NamespaceHandling.OmitDuplicates, Indent = true, NewLineHandling = NewLineHandling.Entitize, }))
            {
                xmlSerializer.Serialize(writer, value);
            }
        }

        public static DateTime GetLastUpdated(string filePath)
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

        public static Common.PossibilitySet.Possibility[] GetEncounterPossibilities(PokemonTranslator pokemonTranslator, int level)
        {
            Dictionary<int, List<Common.PossibilitySet.Possibility.IV>> IVs = new Dictionary<int, List<Common.PossibilitySet.Possibility.IV>>();
            for (int attack = PokeConstants.MinRaidBossIV; attack <= PokeConstants.Evaluation.Attribute.Max; attack++)
            {
                for (int defense = PokeConstants.MinRaidBossIV; defense <= PokeConstants.Evaluation.Attribute.Max; defense++)
                {
                    for (int stamina = PokeConstants.MinRaidBossIV; stamina <= PokeConstants.Evaluation.Attribute.Max; stamina++)
                    {
                        Common.IVScore pokemonIV = new Common.IVScore(attack, defense, stamina);
                        Common.IVScore baseIV = new Common.IVScore(pokemonTranslator.PokemonSettings.stats);

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
    }
}
