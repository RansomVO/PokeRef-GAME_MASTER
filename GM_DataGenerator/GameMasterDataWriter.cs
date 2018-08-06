using System;
using System.Collections.Generic;
using System.IO;

using POGOProtos.Enums;

using VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates;
using VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData;
using VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.DataFiles;

using VanOrman.Utils;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator
{
    class GameMasterDataWriter
    {
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

        Dictionary<int, PokemonTranslator> Pokemon
        {
            get
            {
                if (_pokemon == null)
                    _pokemon = new Dictionary<int, PokemonTranslator>();

                return _pokemon;
            }
        }
        private Dictionary<int, PokemonTranslator> _pokemon;

        Dictionary<PokemonId, FormSettingsTranslator> Forms
        {
            get
            {
                if (_forms == null)
                    _forms = new Dictionary<PokemonId, FormSettingsTranslator>();

                return _forms;
            }
        }
        private Dictionary<PokemonId, FormSettingsTranslator> _forms;

        Dictionary<PokemonMove, MoveTranslator> PokeMoves
        {
            get
            {
                if (_moves == null)
                    _moves = new Dictionary<PokemonMove, MoveTranslator>();

                return _moves;
            }
        }
        private Dictionary<PokemonMove, MoveTranslator> _moves;

        Dictionary<PokemonId, GenderRatioTranslator> GenderRatios
        {
            get
            {
                if (_genderRatios == null)
                    _genderRatios = new Dictionary<PokemonId, GenderRatioTranslator>();

                return _genderRatios;
            }
        }
        private Dictionary<PokemonId, GenderRatioTranslator> _genderRatios;

        PlayerLevelTranslator PlayerLevel { get; set; }

        List<FriendshipTranslator> Friendships
        {
            get
            {
                if (_friendships == null)
                    _friendships = new List<FriendshipTranslator>();

                return _friendships;
            }
        }
        private List<FriendshipTranslator> _friendships;

        GameMasterStatsCalculator GameMasterStatsCalculator { get; set; }

        #endregion Collected Data

        #region ctor

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="gameMaster"></param>
        /// <param name="legacyGameMasters"></param>
        /// <param name="rootFolder"></param>
        public GameMasterDataWriter(GameMasterTemplate gameMasterTemplate, IEnumerable<GameMasterTemplate> legacyGameMasterTemplates)
        {
            // Read the config.
            ManualDataSettings = new ManualDataSettings(Utils.InputManualDataFileFolder);

            GameMasterStatsCalculator = new GameMasterStatsCalculator(gameMasterTemplate.FileName);

            CollectData(gameMasterTemplate, legacyGameMasterTemplates);

            PokeFormulas.Init(PlayerLevel);
            ManualDataSettings.RaidBosses.Init(Pokemon);
            ManualDataSettings.FieldResearch.Init(Pokemon);
        }

        #endregion ctor

        /// <summary>
        /// Writes out all of the .XML files.
        /// </summary>
        public void Write()
        {
            if (!Directory.Exists(Utils.OutputDataFileFolder))
                Directory.CreateDirectory(Utils.OutputDataFileFolder);

            // Must be written first so other Write() methods can leverage calculated values.
            Constants.Write();
            GAME_MASTERS.Write(GameMasters, GameMasterStatsCalculator);

            Moves.Write(PokeMoves.Values, GameMasterStatsCalculator);
            MoveSets.Write(Pokemon.Values, Forms, PokeMoves, ManualDataSettings, GameMasterStatsCalculator);
            PokeStats.Write(Pokemon.Values, ManualDataSettings, GameMasterStatsCalculator);
            RaidBoss.Write(ManualDataSettings, GameMasterStatsCalculator);
            Encounter.Write(ManualDataSettings, GameMasterStatsCalculator);
            Friendship.Write(Friendships, GameMasterStatsCalculator);

            // Must be written last so other Write() methods can update.
            Settings.Write(ManualDataSettings, GameMasterStatsCalculator);
        }

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
            foreach (var itemTemplate in gameMasterTemplate.GameMaster.item_templates)
            {
                try
                {
                    if (itemTemplate.pokemon_settings != null)
                    {
                        PokemonTranslator pokemon = new PokemonTranslator(itemTemplate);
                        Pokemon.Add(pokemon.Key, pokemon);
                    }
                    else if (itemTemplate.move_settings != null)
                    {
                        MoveTranslator move = new MoveTranslator(itemTemplate);
                        PokeMoves.Add(move.Key, move);
                    }
                    else if (itemTemplate.gender_settings != null)
                    {
                        GenderRatioTranslator genderRatio = new GenderRatioTranslator(itemTemplate);

                        // Some Pokemon are duplicated and should be ignored. (E.G. Castform for each of the weathers.) 
                        if (GenderRatios.ContainsKey(genderRatio.Key))
                            continue;

                        GenderRatios.Add(genderRatio.Key, genderRatio);
                    }
                    else if (itemTemplate.player_level != null)
                    {
                        PlayerLevel = new PlayerLevelTranslator(itemTemplate);
                    }
                    else if (itemTemplate.form_settings != null)
                    {
                        if (itemTemplate.form_settings.forms != null)
                        {
                            FormSettingsTranslator formSettings = new FormSettingsTranslator(itemTemplate);
                            Forms.Add(formSettings.Key, formSettings);
                        }
                    }
                    else if (itemTemplate.friendship_milestone_settings != null)
                    {
						Friendships.Add(new FriendshipTranslator(itemTemplate));
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
                    ConsoleOutput.OutputException(ex, $"Error processing {itemTemplate.template_id} ({gameMasterTemplate.FileName})");
                }
            }

            Legacy.Initialize(gameMasterTemplate, legacyGameMasterTemplates, ManualDataSettings.PokemonAvailability, ManualDataSettings.SpecialMoves);
            foreach (var pokemon in Pokemon.Values)
                pokemon.AssignProperties(Pokemon,
                    GenderRatios.ContainsKey(pokemon.PokemonSettings.pokemon_id) ? GenderRatios[pokemon.PokemonSettings.pokemon_id] : null,
                    Legacy.FastMoves.ContainsKey(pokemon.TemplateId) ? Legacy.FastMoves[pokemon.TemplateId] : null,
                    Legacy.ChargedMoves.ContainsKey(pokemon.TemplateId) ? Legacy.ChargedMoves[pokemon.TemplateId] : null);
        }
    }
}