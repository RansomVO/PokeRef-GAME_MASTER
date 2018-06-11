using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml;
using System.Xml.Serialization;

using VanOrman.Utils;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData

{
    /// <summary>
    /// Contains the data from the manually updated.xml files.
    /// </summary>
    class ManualDataSettings
    {
        #region Properties

        /// <summary>
        /// Basic information about Pokemon, including release dates, availability, etc.
        /// </summary>
        /// <remarks>
        /// Among other things, this is used so we don't treat MoveSets released before the Pokemon as Legacy.
        /// </remarks>
        public PokemonAvailability PokemonAvailability { get; set; }

        /// <summary>
        /// Basic information about unreleased Pokemon.
        /// </summary>
        public PokemonUnreleased PokemonUnreleased { get; set; }

        /// <summary>
        /// Pokemon can hatch from eggs.
        /// </summary>
        public Eggs Eggs { get; set; }

        /// <summary>
        /// RaidBosses
        /// </summary>
        public RaidBosses RaidBosses { get; set; }

        /// <summary>
        /// Encounters
        /// </summary>
        public FieldResearch FieldResearch { get; set; }

        /// <summary>
        /// Ranges used to determine how display output.
        /// </summary>
        /// <remarks>
        /// E.G. Great, Good, Okay, etc.
        /// </remarks>
        public Ranges Ranges { get; set; }

        /// <summary>
        /// Moves that were released for a special occasion, but do not appear in GAME_MASTER file.
        /// </summary>
        public SpecialMoves SpecialMoves { get; set; }

        #endregion Properties

        public ManualDataSettings(string inputFolder)
        {
            PokemonAvailability = (PokemonAvailability)ReadXmlConfig(Path.Combine(inputFolder, @"infrequent\pokemon.availability.xml"), typeof(PokemonAvailability));
            PokemonUnreleased = (PokemonUnreleased)ReadXmlConfig(Path.Combine(inputFolder, @"infrequent\pokemon.unreleased_gens.xml"), typeof(PokemonUnreleased));
            Eggs = (Eggs)ReadXmlConfig(Path.Combine(inputFolder, "eggs.xml"), typeof(Eggs));
            RaidBosses = (RaidBosses)ReadXmlConfig(Path.Combine(inputFolder, "raidbosses.xml"), typeof(RaidBosses));
            FieldResearch = (FieldResearch)ReadXmlConfig(Path.Combine(inputFolder, "fieldresearch.xml"), typeof(FieldResearch));
            Ranges = (Ranges)ReadXmlConfig(Path.Combine(inputFolder, @"infrequent\ranges.xml"), typeof(Ranges));
            SpecialMoves = (SpecialMoves)ReadXmlConfig(Path.Combine(inputFolder, @"infrequent\special.moves.xml"), typeof(SpecialMoves));

            // Perform checks.
            StringBuilder stringBuilder = new StringBuilder();
            foreach (var pokemon in PokemonAvailability.Pokemon)
                if (pokemon.id != 0 && pokemon.date == null &&
                    !pokemon.availability.Contains(PokeConstants.Availability.Unreleased))
                    stringBuilder.AppendLine("  • " + pokemon.id.ToString() + " - " + pokemon.name + (pokemon.form == null ? string.Empty : (" (" + pokemon.form + ")")));

            if (stringBuilder.Length > 0)
                ConsoleOutput.OutputError("Update the release date in _datafiles.manual\\pokemon.availability.xml for the following Pokemon:\r\n" + stringBuilder.ToString());
        }

        private static object ReadXmlConfig(string filePath, Type type)
        {
            try
            {
                XmlSerializer xmlSerializer = new XmlSerializer(type);
                using (XmlReader reader = XmlReader.Create(filePath))
                    return xmlSerializer.Deserialize(reader);
            }
            catch (Exception ex)
            {
                ConsoleOutput.OutputException(ex, "Loading {0} Failed!", filePath);
            }

            return null;
        }
    }
}