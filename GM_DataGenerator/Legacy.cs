using System;
using System.Collections.Generic;

using PokemonGO.GAME_MASTER.Templates;
using VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData;
using VanOrman.Utils;
using VanOrman.PokemonGO.GAME_MASTER.Parser.Templates;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator
{
    static class Legacy
    {
        public static Dictionary<string, List<string>> FastMoves { get; private set; }

        public static Dictionary<string, List<string>> ChargedMoves { get; private set; }

        private static PokemonAvailability PokemonReleases { get; set; }

        static Legacy()
        {
            FastMoves = new Dictionary<string, List<string>>();
            ChargedMoves = new Dictionary<string, List<string>>();
        }

        /// <summary>
        /// Process the Legacy GameMasters
        /// </summary>
        /// <param name="legacyGameMasters"></param>
        public static void Initialize(GameMasterTemplate currentGameMasterTemplate, IEnumerable<GameMasterTemplate> legacyGameMasterTemplates, PokemonAvailability pokemonReleases, SpecialMoves specialMoves)
        {
            PokemonReleases = pokemonReleases;

            foreach (var legacyGameMasterTemplate in legacyGameMasterTemplates)
            {
                foreach (var itemTemplate in legacyGameMasterTemplate.GameMaster.itemTemplates)
                {
                    if (IsValidItemTemplate(itemTemplate, legacyGameMasterTemplate.GameMaster.timestampMs))
                    {
                        // Get the Legacy Moves.
                        if (!FastMoves.ContainsKey(itemTemplate.templateId))
                            FastMoves.Add(itemTemplate.templateId, new List<string>());
                        List<string> fastMoves = FastMoves[itemTemplate.templateId];

                        foreach (var move in itemTemplate.pokemonSettings.quickMoves)
                        {
                            if (!fastMoves.Contains(move))
                                fastMoves.Add(move);
                        }

                        if (!ChargedMoves.ContainsKey(itemTemplate.templateId))
                            ChargedMoves.Add(itemTemplate.templateId, new List<string>());
                        List<string> chargedMoves = ChargedMoves[itemTemplate.templateId];

                        foreach (var move in itemTemplate.pokemonSettings.cinematicMoves)
                        {
                            if (!chargedMoves.Contains(move))
                                chargedMoves.Add(move);
                        }
                    }
                }
            }

            foreach (var specialMove in specialMoves.Move)
            {
                if (specialMove.movementId.EndsWith("_FAST"))
                {
                    if (!FastMoves.ContainsKey(specialMove.pokemonTemplateId))
                        FastMoves.Add(specialMove.pokemonTemplateId, new List<string>());
                    List<string> fastMoves = FastMoves[specialMove.pokemonTemplateId];
                    if (!fastMoves.Contains(specialMove.movementId))
                        fastMoves.Add(specialMove.movementId);
                }
                else
                {
                    if (!ChargedMoves.ContainsKey(specialMove.pokemonTemplateId))
                        ChargedMoves.Add(specialMove.pokemonTemplateId, new List<string>());
                    List<string> chargedMoves = ChargedMoves[specialMove.pokemonTemplateId];
                    if (!chargedMoves.Contains(specialMove.movementId))
                        chargedMoves.Add(specialMove.movementId);
                }
            }
        }

        /// <summary>
        /// Determines whether the specified itemTemplate, from the GAME_MASTER with the specified timestamp, 
        /// contains movesets that should included in the legacy lists.
        /// </summary>
        /// <param name="itemTemplate"></param>
        /// <param name="timestamp"></param>
        /// <returns></returns>
        private static bool IsValidItemTemplate(ItemTemplate itemTemplate, string timestamp)
        {
            // If there is no pokemonSettings, the it doesn't contain MoveSets.
            if (itemTemplate.pokemonSettings == null)
                return false;

            // If baseCaptureRate isn't positive, then it cannot be caught. (Not released)
            if (itemTemplate.pokemonSettings.encounter.baseCaptureRate <= 0)
                return false;

            // If is a GAME_MASTER before the one used during the Pokemon's release, just ignore it.
            if (GetReleaseDate(int.Parse(itemTemplate.templateId.Substring(1, 4))) > TimeStampUtils.TimestampToDateTime(timestamp))
                return false;

            // If we made it here, then it should be used.
            return true;
        }

        /// <summary>
        /// Looks up the timestamp for the GAME_MASTER used when the Pokemon with the specified id was released.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        /// <remarks>This counts on the hard-coded POKEMON_RELEASE_DATES from the Data region above.</remarks>
        private static DateTime? GetReleaseDate(int id)
        {
            // Find when the Pokemon with the specified id was released.
            foreach (var pokemonRelease in PokemonReleases.Pokemon)
            {
                if (pokemonRelease.id == id)
                    return pokemonRelease.Date;
            }

            return DateTime.MaxValue;
        }
    }
}
