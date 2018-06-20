using System;
using System.Collections.Generic;

using static POGOProtos.Networking.Responses.DownloadItemTemplatesResponse;
using POGOProtos.Enums;

using VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates;
using VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator
{
    static class Legacy
    {
        public static Dictionary<string, List<PokemonMove>> FastMoves { get; private set; }

        public static Dictionary<string, List<PokemonMove>> ChargedMoves { get; private set; }

        private static PokemonAvailability PokemonReleases { get; set; }

        static Legacy()
        {
            FastMoves = new Dictionary<string, List<PokemonMove>>();
            ChargedMoves = new Dictionary<string, List<PokemonMove>>();
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
                foreach (var itemTemplate in legacyGameMasterTemplate.GameMaster.item_templates)
                {
                    if (IsValidItemTemplate(itemTemplate, legacyGameMasterTemplate.GameMaster.timestamp_ms))
                    {
                        // Get the Legacy Moves.
                        if (!FastMoves.ContainsKey(itemTemplate.template_id))
                            FastMoves.Add(itemTemplate.template_id, new List<PokemonMove>());
                        List<PokemonMove> fastMoves = FastMoves[itemTemplate.template_id];

                        foreach (var move in itemTemplate.pokemon_settings.quick_moves)
                            if (!fastMoves.Contains(move))
                                fastMoves.Add(move);

                        if (!ChargedMoves.ContainsKey(itemTemplate.template_id))
                            ChargedMoves.Add(itemTemplate.template_id, new List<PokemonMove>());
                        List<PokemonMove> chargedMoves = ChargedMoves[itemTemplate.template_id];

                        foreach (var move in itemTemplate.pokemon_settings.cinematic_moves)
                            if (!chargedMoves.Contains(move))
                                chargedMoves.Add(move);
                    }
                }
            }

            foreach (var specialMove in specialMoves.Move)
            {
                if (specialMove.IsFast)
                {
                    if (!FastMoves.ContainsKey(specialMove.pokemonTemplateId))
                        FastMoves.Add(specialMove.pokemonTemplateId, new List<PokemonMove>());
                    List<PokemonMove> fastMoves = FastMoves[specialMove.pokemonTemplateId];
                    if (!fastMoves.Contains(specialMove.movementId))
                        fastMoves.Add(specialMove.movementId);
                }
                else
                {
                    if (!ChargedMoves.ContainsKey(specialMove.pokemonTemplateId))
                        ChargedMoves.Add(specialMove.pokemonTemplateId, new List<PokemonMove>());
                    List<PokemonMove> chargedMoves = ChargedMoves[specialMove.pokemonTemplateId];
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
        private static bool IsValidItemTemplate(ItemTemplate itemTemplate, ulong timestamp)
        {
            // If there is no pokemonSettings, the it doesn't contain MoveSets.
            if (itemTemplate.pokemon_settings == null)
                return false;

            // If baseCaptureRate isn't positive, then it cannot be caught. (Not released)
            if (itemTemplate.pokemon_settings.encounter.base_capture_rate <= 0)
                return false;

            // If is a GAME_MASTER before the one used during the Pokemon's release, just ignore it.
            if (GetReleaseDate(int.Parse(itemTemplate.template_id.Substring(1, 4))) > GameMasterTimestampUtils.TicksToDateTime(timestamp))
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
