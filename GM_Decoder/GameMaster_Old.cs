using System;
using System.Collections.Generic;
using System.Text;
using GameMaster = POGOProtos.Networking.Responses.DownloadItemTemplatesResponse;

namespace VanOrman.PokemonGO.GAME_MASTER.Old
{
    #region Classes for Old GAME_MASTER.json

    public class GameMaster_Old
    {
        public GameMaster Convert()
        {
            GameMaster gameMaster = new GameMaster()
            {
                timestamp_ms = ulong.Parse(timestampMs)
            };

            foreach (var itemTemplate in itemTemplates)
                gameMaster.item_templates.Add(itemTemplate.Convert());

            return gameMaster;
        }

        public List<ItemTemplate> itemTemplates { get; set; }
        public string timestampMs { get; set; }
    }

    public class ItemTemplate
    {
        public GameMaster.ItemTemplate Convert()
        {
            GameMaster.ItemTemplate itemTemplate = new GameMaster.ItemTemplate() { template_id = templateId };

            if (avatarCustomization != null)
                itemTemplate.avatar_customization = avatarCustomization.Convert();
            else if (badgeSettings != null)
                itemTemplate.badge_settings = badgeSettings.Convert();
            else if (battleSettings != null)
                itemTemplate.battle_settings = battleSettings.Convert();
            else if (encounterSettings != null)
                itemTemplate.encounter_settings = encounterSettings.Convert();
            else if (formSettings != null)
                itemTemplate.form_settings = formSettings.Convert();
            else if (gymBadgeSettings != null)
                itemTemplate.gym_badge_settings = gymBadgeSettings.Convert();
            else if (gymLevel != null)
                itemTemplate.gym_level = gymLevel.Convert();
            else if (iapSettings != null)
                itemTemplate.iap_settings = iapSettings.Convert();
            else if (itemSettings != null)
                itemTemplate.item_settings = itemSettings.Convert();
            else if (playerLevel != null)
                itemTemplate.player_level = playerLevel.Convert();
            else if (typeEffective != null)
                itemTemplate.type_effective = typeEffective.Convert();
            else if (pokemonUpgrades != null)
                itemTemplate.pokemon_upgrades = pokemonUpgrades.Convert();
            else if (questSettings != null)
                itemTemplate.quest_settings = questSettings.Convert();
            else if (genderSettings != null)
                itemTemplate.gender_settings = genderSettings.Convert();
            else if (pokemonSettings != null)
                itemTemplate.pokemon_settings = pokemonSettings.Convert();
            else if (moveSettings != null)
                itemTemplate.move_settings = moveSettings.Convert();
            else if (iapItemDisplay != null)
                itemTemplate.iap_item_display = iapItemDisplay.Convert();
            else if (camera != null)
                itemTemplate.camera = camera.Convert();
            else if (moveSequenceSettings != null)
                itemTemplate.move_sequence_settings = moveSequenceSettings.Convert();
            return itemTemplate;
        }

        public string templateId { get; set; }
        public AvatarCustomization avatarCustomization { get; set; }
        public BadgeSettings badgeSettings { get; set; }
        public BattleSettings battleSettings { get; set; }
        public EncounterSettings encounterSettings { get; set; }
        public FormSettings formSettings { get; set; }
        public GymBadgeSettings gymBadgeSettings { get; set; }
        public GymLevel gymLevel { get; set; }
        public IapSettings iapSettings { get; set; }
        public ItemSettings itemSettings { get; set; }
        public PlayerLevel playerLevel { get; set; }
        public TypeEffective typeEffective { get; set; }
        public PokemonUpgrades pokemonUpgrades { get; set; }
        public QuestSettings questSettings { get; set; }
        public GenderSettings genderSettings { get; set; }
        public PokemonSettings pokemonSettings { get; set; }
        public MoveSettings moveSettings { get; set; }
        public IapItemDisplay iapItemDisplay { get; set; }
        public Camera2 camera { get; set; }
        public MoveSequenceSettings moveSequenceSettings { get; set; }
    }

    public class AvatarCustomization
    {
        public POGOProtos.Settings.Master.AvatarCustomizationSettings Convert()
        {
            POGOProtos.Settings.Master.AvatarCustomizationSettings avatarCustomizationSettings = new POGOProtos.Settings.Master.AvatarCustomizationSettings()
            {
                enabled = enabled,
                avatar_type = TypeConverter.Convert<POGOProtos.Data.Player.PlayerAvatarType>(avatarType),
                bundle_name = bundleName,
                asset_name = assetName,
                group_name = groupName,
                sort_order = sortOrder,
                unlock_type = TypeConverter.Convert<POGOProtos.Settings.Master.AvatarCustomizationSettings.AvatarCustomizationUnlockType>(unlockType),
                iap_sku = iapSku,
                icon_name = iconName,
                unlock_player_level = unlockPlayerLevel == null ? 0 : (int)unlockPlayerLevel,
            };
            foreach (var slotEntry in slot)
                avatarCustomizationSettings.slot.Add(TypeConverter.Convert<POGOProtos.Enums.Slot>(slotEntry));

            return avatarCustomizationSettings;
        }

        public bool enabled { get; set; }
        public string avatarType { get; set; }
        public List<string> slot { get; set; }
        public string bundleName { get; set; }
        public string assetName { get; set; }
        public string groupName { get; set; }
        public int sortOrder { get; set; }
        public string unlockType { get; set; }
        public string iapSku { get; set; }
        public string iconName { get; set; }
        public int? unlockPlayerLevel { get; set; }
    }

    public class BadgeSettings
    {
        public POGOProtos.Settings.Master.BadgeSettings Convert()
        {
            POGOProtos.Settings.Master.BadgeSettings badgeSettings = new POGOProtos.Settings.Master.BadgeSettings()
            {
                badge_type = TypeConverter.Convert<POGOProtos.Enums.BadgeType>(badgeType),
                badge_rank = badgeRank,
            };

            List<int> list = new List<int>();
            foreach (var target in targets)
                list.Add(target);
            badgeSettings.targets = list.ToArray();

            return badgeSettings;
        }

        public string badgeType { get; set; }
        public int badgeRank { get; set; }
        public List<int> targets { get; set; }
    }

    public class BattleSettings
    {
        public POGOProtos.Settings.Master.GymBattleSettings Convert()
        {
            POGOProtos.Settings.Master.GymBattleSettings gymBattleSettings = new POGOProtos.Settings.Master.GymBattleSettings()
            {
                retarget_seconds = (float)retargetSeconds,
                enemy_attack_interval = (float)enemyAttackInterval,
                attack_server_interval = (float)attackServerInterval,
                round_duration_seconds = (float)roundDurationSeconds,
                bonus_time_per_ally_seconds = (float)bonusTimePerAllySeconds,
                maximum_attackers_per_battle = maximumAttackersPerBattle,
                same_type_attack_bonus_multiplier = (float)sameTypeAttackBonusMultiplier,
                maximum_energy = maximumEnergy,
                energy_delta_per_health_lost = (float)energyDeltaPerHealthLost,
                dodge_duration_ms = dodgeDurationMs,
                minimum_player_level = minimumPlayerLevel,
                swap_duration_ms = swapDurationMs,
                dodge_damage_reduction_percent = (float)dodgeDamageReductionPercent,
                minimum_raid_player_level = minimumRaidPlayerLevel,
            };

            return gymBattleSettings;
        }

        public double retargetSeconds { get; set; }
        public double enemyAttackInterval { get; set; }
        public double attackServerInterval { get; set; }
        public double roundDurationSeconds { get; set; }
        public double bonusTimePerAllySeconds { get; set; }
        public int maximumAttackersPerBattle { get; set; }
        public double sameTypeAttackBonusMultiplier { get; set; }
        public int maximumEnergy { get; set; }
        public double energyDeltaPerHealthLost { get; set; }
        public int dodgeDurationMs { get; set; }
        public int minimumPlayerLevel { get; set; }
        public int swapDurationMs { get; set; }
        public double dodgeDamageReductionPercent { get; set; }
        public int minimumRaidPlayerLevel { get; set; }
    }

    public class EncounterSettings
    {
        public POGOProtos.Settings.Master.EncounterSettings Convert()
        {
            POGOProtos.Settings.Master.EncounterSettings encounterSettings = new POGOProtos.Settings.Master.EncounterSettings()
            {
                spin_bonus_threshold = (float)spinBonusThreshold,
                excellent_throw_threshold = (float)excellentThrowThreshold,
                great_throw_threshold = (float)greatThrowThreshold,
                nice_throw_threshold = (float)niceThrowThreshold,
                milestone_threshold = milestoneThreshold,
            };

            return encounterSettings;
        }

        public double spinBonusThreshold { get; set; }
        public double excellentThrowThreshold { get; set; }
        public double greatThrowThreshold { get; set; }
        public double niceThrowThreshold { get; set; }
        public int milestoneThreshold { get; set; }
    }

    public class Form
    {
        public POGOProtos.Settings.Master.FormSettings.Form Convert()
        {
            POGOProtos.Settings.Master.FormSettings.Form formSettingsForm = new POGOProtos.Settings.Master.FormSettings.Form()
            {
                form = TypeConverter.Convert<POGOProtos.Enums.Form>(form),
                asset_bundle_value = assetBundleValue,
            };

            return formSettingsForm;
        }

        public string form { get; set; }
        public int assetBundleValue { get; set; }
    }

    public class FormSettings
    {
        public POGOProtos.Settings.Master.FormSettings Convert()
        {
            POGOProtos.Settings.Master.FormSettings formSettings = new POGOProtos.Settings.Master.FormSettings()
            {
                pokemon = TypeConverter.Convert<POGOProtos.Enums.PokemonId>(pokemon),
            };

            if (forms != null)
                foreach (var form in forms)
                    formSettings.forms.Add(form.Convert());

            return formSettings;
        }

        public string pokemon { get; set; }
        public List<Form> forms { get; set; }
    }

    public class GymBadgeSettings
    {
        public POGOProtos.Settings.Master.GymBadgeGmtSettings Convert()
        {
            POGOProtos.Settings.Master.GymBadgeGmtSettings gymBadgeGmtSettings = new POGOProtos.Settings.Master.GymBadgeGmtSettings()
            {
                battle_winning_score_per_defender_cp = (float)battleWinningScorePerDefenderCp,
                gym_defending_score_per_minute = (float)gymDefendingScorePerMinute,
                berry_feeding_score = berryFeedingScore,
                pokemon_deploy_score = pokemonDeployScore,
                raid_battle_winning_score = raidBattleWinningScore,
                lose_all_battles_score = loseAllBattlesScore,
            };
            List<int> list = new List<int>();
            foreach (var value in target)
                list.Add(value);
            gymBadgeGmtSettings.target = list.ToArray();

            return gymBadgeGmtSettings;
        }

        public List<int> target { get; set; }
        public double battleWinningScorePerDefenderCp { get; set; }
        public double gymDefendingScorePerMinute { get; set; }
        public int berryFeedingScore { get; set; }
        public int pokemonDeployScore { get; set; }
        public int raidBattleWinningScore { get; set; }
        public int loseAllBattlesScore { get; set; }
    }

    public class GymLevel
    {
        public POGOProtos.Settings.Master.GymLevelSettings Convert()
        {
            POGOProtos.Settings.Master.GymLevelSettings gymLevelSettings = new POGOProtos.Settings.Master.GymLevelSettings();

            List<int> list = new List<int>();
            foreach (var value in requiredExperience)
                list.Add(value);
            gymLevelSettings.required_experience = list.ToArray();

            list = new List<int>();
            foreach (var value in leaderSlots)
                list.Add(value);
            gymLevelSettings.leader_slots = list.ToArray();

            list = new List<int>();
            foreach (var value in trainerSlots)
                list.Add(value);
            gymLevelSettings.trainer_slots = list.ToArray();

            return gymLevelSettings;
        }

        public List<int> requiredExperience { get; set; }
        public List<int> leaderSlots { get; set; }
        public List<int> trainerSlots { get; set; }
    }

    public class IapSettings
    {
        public POGOProtos.Settings.Master.IapSettings Convert()
        {
            POGOProtos.Settings.Master.IapSettings iapSettings = new POGOProtos.Settings.Master.IapSettings()
            {
                daily_defender_bonus_max_defenders = dailyDefenderBonusMaxDefenders,
                min_time_between_claims_ms = long.Parse(minTimeBetweenClaimsMs),
            };

            List<int> listInt = new List<int>();
            foreach (var value in dailyDefenderBonusPerPokemon)
                listInt.Add(value);
            iapSettings.daily_defender_bonus_per_pokemon = listInt.ToArray();

            iapSettings.daily_defender_bonus_currency.AddRange(dailyDefenderBonusCurrency);

            return iapSettings;
        }

        public List<int> dailyDefenderBonusPerPokemon { get; set; }
        public int dailyDefenderBonusMaxDefenders { get; set; }
        public List<string> dailyDefenderBonusCurrency { get; set; }
        public string minTimeBetweenClaimsMs { get; set; }
    }

    public class Food
    {
        public POGOProtos.Settings.Master.Item.FoodAttributes Convert()
        {
            POGOProtos.Settings.Master.Item.FoodAttributes foodAttributes = new POGOProtos.Settings.Master.Item.FoodAttributes()
            {
                growth_percent = (float)growthPercent,
                berry_multiplier = berryMultiplier == null ? 0 : (float)berryMultiplier,
            };

            foreach (var value in itemEffect)
                foodAttributes.item_effect.Add(TypeConverter.Convert<POGOProtos.Enums.ItemEffect>(value));

            List<float> listFloat = new List<float>();
            foreach (var value in itemEffectPercent)
                listFloat.Add((float)value);
            foodAttributes.item_effect_percent = listFloat.ToArray();

            return foodAttributes;
        }

        public List<string> itemEffect { get; set; }
        public List<double> itemEffectPercent { get; set; }
        public double growthPercent { get; set; }
        public double? berryMultiplier { get; set; }
    }

    public class Potion
    {
        public POGOProtos.Settings.Master.Item.PotionAttributes Convert()
        {
            POGOProtos.Settings.Master.Item.PotionAttributes potionAttributes = new POGOProtos.Settings.Master.Item.PotionAttributes()
            {
                sta_amount = staAmount,
                sta_percent = staPercent == null ? 0 : (float)staPercent,
            };

            return potionAttributes;
        }

        public int staAmount { get; set; }
        public double? staPercent { get; set; }
    }

    public class Incense
    {
        public POGOProtos.Settings.Master.Item.IncenseAttributes Convert()
        {
            POGOProtos.Settings.Master.Item.IncenseAttributes incenseAttributes = new POGOProtos.Settings.Master.Item.IncenseAttributes()
            {
                incense_lifetime_seconds = incenseLifetimeSeconds,
                standing_time_between_encounters_seconds = standingTimeBetweenEncountersSeconds,
                moving_time_between_encounter_seconds = movingTimeBetweenEncounterSeconds,
                distance_required_for_shorter_interval_meters = distanceRequiredForShorterIntervalMeters,
            };

            return incenseAttributes;
        }

        public int incenseLifetimeSeconds { get; set; }
        public int standingTimeBetweenEncountersSeconds { get; set; }
        public int movingTimeBetweenEncounterSeconds { get; set; }
        public int distanceRequiredForShorterIntervalMeters { get; set; }
    }

    public class EggIncubator
    {
        public POGOProtos.Settings.Master.Item.EggIncubatorAttributes Convert()
        {
            POGOProtos.Settings.Master.Item.EggIncubatorAttributes eggIncubatorAttributes = new POGOProtos.Settings.Master.Item.EggIncubatorAttributes()
            {
                incubator_type = TypeConverter.Convert<POGOProtos.Inventory.EggIncubatorType>(incubatorType),
                uses = uses,
                distance_multiplier = (float)distanceMultiplier,
            };

            return eggIncubatorAttributes;
        }

        public string incubatorType { get; set; }
        public int uses { get; set; }
        public double distanceMultiplier { get; set; }
    }

    public class InventoryUpgrade
    {
        public POGOProtos.Settings.Master.Item.InventoryUpgradeAttributes Convert()
        {
            POGOProtos.Settings.Master.Item.InventoryUpgradeAttributes inventoryUpgradeAttributes = new POGOProtos.Settings.Master.Item.InventoryUpgradeAttributes()
            {
                additional_storage = additionalStorage,
                upgrade_type = TypeConverter.Convert<POGOProtos.Inventory.InventoryUpgradeType>(upgradeType),
            };

            return inventoryUpgradeAttributes;
        }

        public int additionalStorage { get; set; }
        public string upgradeType { get; set; }
    }

    public class XpBoost
    {
        public POGOProtos.Settings.Master.Item.ExperienceBoostAttributes Convert()
        {
            POGOProtos.Settings.Master.Item.ExperienceBoostAttributes experienceBoostAttributes = new POGOProtos.Settings.Master.Item.ExperienceBoostAttributes()
            {
                xp_multiplier = (float)xpMultiplier,
                boost_duration_ms = boostDurationMs,
            };

            return experienceBoostAttributes;
        }

        public double xpMultiplier { get; set; }
        public int boostDurationMs { get; set; }
    }

    public class Revive
    {
        public POGOProtos.Settings.Master.Item.ReviveAttributes Convert()
        {
            POGOProtos.Settings.Master.Item.ReviveAttributes reviveAttributes = new POGOProtos.Settings.Master.Item.ReviveAttributes()
            {
                sta_percent = (float)staPercent,
            };

            return reviveAttributes;
        }

        public double staPercent { get; set; }
    }

    public class ItemSettings
    {
        public POGOProtos.Settings.Master.ItemSettings Convert()
        {
            POGOProtos.Settings.Master.ItemSettings itemSettings = new POGOProtos.Settings.Master.ItemSettings()
            {
                item_id = itemId == null ? default(POGOProtos.Inventory.Item.ItemId) : TypeConverter.Convert<POGOProtos.Inventory.Item.ItemId>(itemId),
                item_type = itemId == null ? default(POGOProtos.Inventory.Item.ItemType) : TypeConverter.Convert<POGOProtos.Inventory.Item.ItemType>(itemType),
                category = itemId == null ? default(POGOProtos.Enums.ItemCategory) : TypeConverter.Convert<POGOProtos.Enums.ItemCategory>(category),
                drop_trainer_level = dropTrainerLevel,
                food = food == null ? null : food.Convert(),
                potion = potion == null ? null : potion.Convert(),
                incense = incense == null ? null : incense.Convert(),
                egg_incubator = eggIncubator == null ? null : eggIncubator.Convert(),
                inventory_upgrade = inventoryUpgrade == null ? null : inventoryUpgrade.Convert(),
                xp_boost = xpBoost == null ? null : xpBoost.Convert(),
                revive = revive == null ? null : revive.Convert(),
            };

            return itemSettings;
        }

        public string itemId { get; set; }
        public string itemType { get; set; }
        public string category { get; set; }
        public int dropTrainerLevel { get; set; }
        public Food food { get; set; }
        public Potion potion { get; set; }
        public Incense incense { get; set; }
        public EggIncubator eggIncubator { get; set; }
        public InventoryUpgrade inventoryUpgrade { get; set; }
        public XpBoost xpBoost { get; set; }
        public Revive revive { get; set; }
    }

    public class PlayerLevel
    {
        public POGOProtos.Settings.Master.PlayerLevelSettings Convert()
        {
            POGOProtos.Settings.Master.PlayerLevelSettings playerLevelSettings = new POGOProtos.Settings.Master.PlayerLevelSettings()
            {

                max_egg_player_level = maxEggPlayerLevel,
                max_encounter_player_level = maxEncounterPlayerLevel,
            };

            List<int> listInt = new List<int>();
            foreach (var value in rankNum)
                listInt.Add(value);
            playerLevelSettings.rank_num = listInt.ToArray();

            listInt = new List<int>();
            foreach (var value in requiredExperience)
                listInt.Add(value);
            playerLevelSettings.required_experience = listInt.ToArray();

            List<float> listFloat = new List<float>();
            foreach (var value in cpMultiplier)
                listFloat.Add((float)value);
            playerLevelSettings.cp_multiplier = listFloat.ToArray();


            return playerLevelSettings;
        }

        public List<int> rankNum { get; set; }
        public List<int> requiredExperience { get; set; }
        public List<double> cpMultiplier { get; set; }
        public int maxEggPlayerLevel { get; set; }
        public int maxEncounterPlayerLevel { get; set; }
    }

    public class TypeEffective
    {
        public POGOProtos.Settings.Master.TypeEffectiveSettings Convert()
        {
            POGOProtos.Settings.Master.TypeEffectiveSettings typeEffectiveSettings = new POGOProtos.Settings.Master.TypeEffectiveSettings()
            {
                attack_type = TypeConverter.Convert<POGOProtos.Enums.PokemonType>(attackType),
            };
            List<float> listFloat = new List<float>();
            foreach (var value in attackScalar)
                listFloat.Add((float)value);
            typeEffectiveSettings.attack_scalar = listFloat.ToArray();

            return typeEffectiveSettings;
        }

        public List<double> attackScalar { get; set; }
        public string attackType { get; set; }
    }

    public class PokemonUpgrades
    {
        public POGOProtos.Settings.Master.PokemonUpgradeSettings Convert()
        {
            POGOProtos.Settings.Master.PokemonUpgradeSettings pokemonUpgradeSettings = new POGOProtos.Settings.Master.PokemonUpgradeSettings()
            {
                upgrades_per_level = upgradesPerLevel,
                allowed_levels_above_player = allowedLevelsAbovePlayer
            };

            List<int> listInt = new List<int>();
            foreach (var value in candyCost)
                listInt.Add(value);
            pokemonUpgradeSettings.candy_cost = listInt.ToArray();

            listInt = new List<int>();
            foreach (var value in stardustCost)
                listInt.Add(value);
            pokemonUpgradeSettings.stardust_cost = listInt.ToArray();

            return pokemonUpgradeSettings;
        }

        public int upgradesPerLevel { get; set; }
        public int allowedLevelsAbovePlayer { get; set; }
        public List<int> candyCost { get; set; }
        public List<int> stardustCost { get; set; }
    }

    public class DailyQuest
    {
        public POGOProtos.Settings.Master.Quest.DailyQuestSettings Convert()
        {
            POGOProtos.Settings.Master.Quest.DailyQuestSettings dailyQuestSettings = new POGOProtos.Settings.Master.Quest.DailyQuestSettings()
            {
                buckets_per_day = bucketsPerDay,
                streak_length = streakLength,
                bonus_multiplier = bonusMultiplier == null ? 0 : (float)bonusMultiplier,
                streak_bonus_multiplier = streakBonusMultiplier == null ? 0 : (float)streakBonusMultiplier,
            };

            return dailyQuestSettings;
        }

        public int bucketsPerDay { get; set; }
        public int streakLength { get; set; }
        public double? bonusMultiplier { get; set; }
        public double? streakBonusMultiplier { get; set; }
    }

    public class QuestSettings
    {
        public POGOProtos.Settings.Master.QuestSettings Convert()
        {
            POGOProtos.Settings.Master.QuestSettings questSettings = new POGOProtos.Settings.Master.QuestSettings()
            {
                quest_type = TypeConverter.Convert<POGOProtos.Enums.QuestType>(questType),
                daily_quest = dailyQuest.Convert(),
            };

            return questSettings;
        }

        public string questType { get; set; }
        public DailyQuest dailyQuest { get; set; }
    }

    public class Gender
    {
        public POGOProtos.Settings.Master.Pokemon.PokemonGenderSettings Convert()
        {
            POGOProtos.Settings.Master.Pokemon.PokemonGenderSettings pokemonGenderSettings = new POGOProtos.Settings.Master.Pokemon.PokemonGenderSettings()
            {
                male_percent = (float)malePercent,
                female_percent = (float)femalePercent,
                genderless_percent = genderlessPercent == null ? 0 : (float)genderlessPercent,
            };

            return pokemonGenderSettings;
        }

        public double malePercent { get; set; }
        public double femalePercent { get; set; }
        public double? genderlessPercent { get; set; }
    }

    public class GenderSettings
    {
        public POGOProtos.Settings.Master.GenderSettings Convert()
        {
            POGOProtos.Settings.Master.GenderSettings genderSettings = new POGOProtos.Settings.Master.GenderSettings()
            {
                pokemon = TypeConverter.Convert<POGOProtos.Enums.PokemonId>(pokemon),
                gender = gender.Convert(),
            };

            return genderSettings;
        }

        public string pokemon { get; set; }
        public Gender gender { get; set; }
    }

    public class Camera
    {
        public POGOProtos.Settings.Master.Pokemon.CameraAttributes Convert()
        {
            POGOProtos.Settings.Master.Pokemon.CameraAttributes cameraAttributes = new POGOProtos.Settings.Master.Pokemon.CameraAttributes()
            {
                disk_radius_m = (float)diskRadiusM,
                cylinder_radius_m = (float)cylinderRadiusM,
                cylinder_height_m = (float)cylinderHeightM,
                shoulder_mode_scale = (float)shoulderModeScale,
                cylinder_ground_m = cylinderGroundM == null ? 0 : (float)cylinderGroundM,
            };

            return cameraAttributes;
        }

        public double diskRadiusM { get; set; }
        public double cylinderRadiusM { get; set; }
        public double cylinderHeightM { get; set; }
        public double shoulderModeScale { get; set; }
        public double? cylinderGroundM { get; set; }
    }

    public class Encounter
    {
        public POGOProtos.Settings.Master.Pokemon.EncounterAttributes Convert()
        {
            POGOProtos.Settings.Master.Pokemon.EncounterAttributes encounterAttributes = new POGOProtos.Settings.Master.Pokemon.EncounterAttributes()
            {
                base_capture_rate = (float)baseCaptureRate,
                base_flee_rate = (float)baseFleeRate,
                collision_radius_m = (float)collisionRadiusM,
                collision_height_m = (float)collisionHeightM,
                collision_head_radius_m = (float)collisionHeadRadiusM,
                movement_type = TypeConverter.Convert<POGOProtos.Enums.PokemonMovementType>(movementType),
                movement_timer_s = (float)movementTimerS,
                jump_time_s = (float)jumpTimeS,
                attack_timer_s = (float)attackTimerS,
                attack_probability = (float)attackProbability,
                dodge_probability = (float)dodgeProbability,
                dodge_duration_s = (float)dodgeDurationS,
                dodge_distance = (float)dodgeDistance,
                camera_distance = (float)cameraDistance,
                min_pokemon_action_frequency_s = (float)minPokemonActionFrequencyS,
                max_pokemon_action_frequency_s = (float)maxPokemonActionFrequencyS,
                bonus_candy_capture_reward = bonusCandyCaptureReward == null ? 0 : (int)bonusCandyCaptureReward,
                bonus_stardust_capture_reward = bonusStardustCaptureReward == null ? 0 : (int)bonusStardustCaptureReward,
            };

            return encounterAttributes;
        }

        public double baseCaptureRate { get; set; }
        public double baseFleeRate { get; set; }
        public double collisionRadiusM { get; set; }
        public double collisionHeightM { get; set; }
        public double collisionHeadRadiusM { get; set; }
        public string movementType { get; set; }
        public double movementTimerS { get; set; }
        public double jumpTimeS { get; set; }
        public double attackTimerS { get; set; }
        public double attackProbability { get; set; }
        public double dodgeProbability { get; set; }
        public double dodgeDurationS { get; set; }
        public double dodgeDistance { get; set; }
        public double cameraDistance { get; set; }
        public double minPokemonActionFrequencyS { get; set; }
        public double maxPokemonActionFrequencyS { get; set; }
        public int? bonusCandyCaptureReward { get; set; }
        public int? bonusStardustCaptureReward { get; set; }
    }

    public class Stats
    {
        public POGOProtos.Settings.Master.Pokemon.StatsAttributes Convert()
        {
            POGOProtos.Settings.Master.Pokemon.StatsAttributes statsAttributes = new POGOProtos.Settings.Master.Pokemon.StatsAttributes()
            {
                base_stamina = baseStamina,
                base_attack = baseAttack,
                base_defense = baseDefense,
            };

            return statsAttributes;
        }

        public int baseStamina { get; set; }
        public int baseAttack { get; set; }
        public int baseDefense { get; set; }
    }

    public class EvolutionBranch
    {
        public POGOProtos.Settings.Master.Pokemon.EvolutionBranch Convert()
        {
            POGOProtos.Settings.Master.Pokemon.EvolutionBranch evolutionBranch = new POGOProtos.Settings.Master.Pokemon.EvolutionBranch()
            {
                evolution = TypeConverter.Convert<POGOProtos.Enums.PokemonId>(evolution),
                candy_cost = candyCost,
                evolution_item_requirement = TypeConverter.Convert<POGOProtos.Inventory.Item.ItemId>(evolutionItemRequirement),
            };

            return evolutionBranch;
        }

        public string evolution { get; set; }
        public int candyCost { get; set; }
        public string evolutionItemRequirement { get; set; }
    }

    public class PokemonSettings
    {
        public POGOProtos.Settings.Master.PokemonSettings Convert()
        {
            POGOProtos.Settings.Master.PokemonSettings pokemonSettings = new POGOProtos.Settings.Master.PokemonSettings()
            {
                pokemon_id = TypeConverter.Convert<POGOProtos.Enums.PokemonId>(pokemonId),
                model_scale = (float)modelScale,
                type = TypeConverter.Convert<POGOProtos.Enums.PokemonType>(type),
                type_2 = TypeConverter.Convert<POGOProtos.Enums.PokemonType>(type2),
                camera = camera.Convert(),
                encounter = encounter.Convert(),
                stats = stats.Convert(),
                evolution_pips = evolutionPips,
                pokedex_height_m = (float)pokedexHeightM,
                pokedex_weight_kg = (float)pokedexWeightKg,
                height_std_dev = (float)heightStdDev,
                weight_std_dev = (float)weightStdDev,
                family_id = TypeConverter.Convert<POGOProtos.Enums.PokemonFamilyId>(familyId),
                candy_to_evolve = candyToEvolve,
                km_buddy_distance = (float)kmBuddyDistance,
                model_height = (float)modelHeight,
                parent_pokemon_id = TypeConverter.Convert<POGOProtos.Enums.PokemonId>(parentPokemonId),
                buddy_size = TypeConverter.Convert<POGOProtos.Enums.BuddySize>(buddySize),
                rarity = TypeConverter.Convert<POGOProtos.Enums.PokemonRarity>(rarity),
            };

            foreach (var value in quickMoves)
                pokemonSettings.quick_moves.Add(TypeConverter.Convert<POGOProtos.Enums.PokemonMove>(value));

            foreach (var value in cinematicMoves)
                pokemonSettings.cinematic_moves.Add(TypeConverter.Convert<POGOProtos.Enums.PokemonMove>(value));

            List<float> listFloat = new List<float>();
            foreach (var value in animationTime)
                listFloat.Add((float)value);
            pokemonSettings.animation_time = listFloat.ToArray();

            if (evolutionIds != null)
                foreach (var value in evolutionIds)
                    pokemonSettings.evolution_ids.Add(TypeConverter.Convert<POGOProtos.Enums.PokemonId>(value));

            if (evolutionBranch != null)
                foreach (var value in evolutionBranch)
                    pokemonSettings.evolution_branch.Add(value.Convert());

            return pokemonSettings;
        }

        public string pokemonId { get; set; }
        public double modelScale { get; set; }
        public string type { get; set; }
        public string type2 { get; set; }
        public Camera camera { get; set; }
        public Encounter encounter { get; set; }
        public Stats stats { get; set; }
        public List<string> quickMoves { get; set; }
        public List<string> cinematicMoves { get; set; }
        public List<double> animationTime { get; set; }
        public List<string> evolutionIds { get; set; }
        public int evolutionPips { get; set; }
        public double pokedexHeightM { get; set; }
        public double pokedexWeightKg { get; set; }
        public double heightStdDev { get; set; }
        public double weightStdDev { get; set; }
        public string familyId { get; set; }
        public int candyToEvolve { get; set; }
        public double kmBuddyDistance { get; set; }
        public double modelHeight { get; set; }
        public List<EvolutionBranch> evolutionBranch { get; set; }
        public string parentPokemonId { get; set; }
        public string buddySize { get; set; }
        public string rarity { get; set; }
    }

    public class MoveSettings
    {
        public POGOProtos.Settings.Master.MoveSettings Convert()
        {
            POGOProtos.Settings.Master.MoveSettings moveSettings = new POGOProtos.Settings.Master.MoveSettings()
            {
                movement_id = TypeConverter.Convert<POGOProtos.Enums.PokemonMove>(movementId),
                animation_id = animationId,
                pokemon_type = TypeConverter.Convert<POGOProtos.Enums.PokemonType>(pokemonType),
                power = (float)power,
                accuracy_chance = (float)accuracyChance,
                critical_chance = (float)criticalChance,
                stamina_loss_scalar = (float)staminaLossScalar,
                trainer_level_min = trainerLevelMin,
                trainer_level_max = trainerLevelMax,
                vfx_name = vfxName,
                duration_ms = durationMs,
                damage_window_start_ms = damageWindowStartMs,
                damage_window_end_ms = damageWindowEndMs,
                energy_delta = energyDelta,
                heal_scalar = healScalar == null ? 0 : (float)healScalar,
            };

            return moveSettings;
        }

        public string movementId { get; set; }
        public int animationId { get; set; }
        public string pokemonType { get; set; }
        public double power { get; set; }
        public double accuracyChance { get; set; }
        public double criticalChance { get; set; }
        public double staminaLossScalar { get; set; }
        public int trainerLevelMin { get; set; }
        public int trainerLevelMax { get; set; }
        public string vfxName { get; set; }
        public int durationMs { get; set; }
        public int damageWindowStartMs { get; set; }
        public int damageWindowEndMs { get; set; }
        public int energyDelta { get; set; }
        public double? healScalar { get; set; }
    }

    public class IapItemDisplay
    {
        public POGOProtos.Settings.Master.IapItemDisplay Convert()
        {
            POGOProtos.Settings.Master.IapItemDisplay iapItemDisplay = new POGOProtos.Settings.Master.IapItemDisplay()
            {
                sku = sku,
                category = TypeConverter.Convert<POGOProtos.Enums.HoloIapItemCategory>(category),
                sort_order = sortOrder,
            };

            // Not sure why these are in this class, but not the new stuff.
            //iapItemDisplay.item_ids.AddRange(itemIds);

            //List<int> listInt = new List<int>();
            //foreach (var value in counts)
            //	listInt.Add(value == null ? 0 : (int)value);
            //iapItemDisplay.counts.AddRange(listInt);

            return iapItemDisplay;
        }

        public string sku { get; set; }
        public string category { get; set; }
        public int sortOrder { get; set; }
        public List<string> itemIds { get; set; }
        public List<int?> counts { get; set; }
    }

    public class Camera2
    {
        public POGOProtos.Settings.Master.CameraSettings Convert()
        {
            POGOProtos.Settings.Master.CameraSettings cameraSettings = new POGOProtos.Settings.Master.CameraSettings()
            {
                next_camera = nextCamera,
            };

            if (interpolation != null)
                foreach (var value in interpolation)
                    cameraSettings.interpolation.Add(TypeConverter.Convert<POGOProtos.Enums.CameraInterpolation>(value));

            if (targetType != null)
                foreach (var value in targetType)
                    cameraSettings.target_type.Add(TypeConverter.Convert<POGOProtos.Enums.CameraTarget>(value));

            List<float> listFloat;
            if (easeInSpeed != null)
            {
                listFloat = new List<float>();
                foreach (var value in easeInSpeed)
                    listFloat.Add((float)value);
                cameraSettings.ease_in_speed = listFloat.ToArray();
            }

            if (easeOutSpeed != null)
            {
                listFloat = new List<float>();
                foreach (var value in easeOutSpeed)
                    listFloat.Add((float)value);
                cameraSettings.ease_out_speed = listFloat.ToArray();
            }

            if (durationSeconds != null)
            {
                listFloat = new List<float>();
                foreach (var value in durationSeconds)
                    listFloat.Add((float)value);
                cameraSettings.duration_seconds = listFloat.ToArray();
            }

            if (waitSeconds != null)
            {
                listFloat = new List<float>();
                foreach (var value in waitSeconds)
                    listFloat.Add((float)value);
                cameraSettings.wait_seconds = listFloat.ToArray();
            }

            if (transitionSeconds != null)
            {
                listFloat = new List<float>();
                foreach (var value in transitionSeconds)
                    listFloat.Add((float)value);
                cameraSettings.transition_seconds = listFloat.ToArray();
            }

            if (angleDegree != null)
            {
                listFloat = new List<float>();
                foreach (var value in angleDegree)
                    listFloat.Add((float)value);
                cameraSettings.angle_degree = listFloat.ToArray();
            }

            if (angleOffsetDegree != null)
            {
                listFloat = new List<float>();
                foreach (var value in angleOffsetDegree)
                    listFloat.Add((float)value);
                cameraSettings.angle_offset_degree = listFloat.ToArray();
            }

            if (pitchDegree != null)
            {
                listFloat = new List<float>();
                foreach (var value in pitchDegree)
                    listFloat.Add((float)value);
                cameraSettings.pitch_degree = listFloat.ToArray();
            }

            if (pitchOffsetDegree != null)
            {
                listFloat = new List<float>();
                foreach (var value in pitchOffsetDegree)
                    listFloat.Add((float)value);
                cameraSettings.pitch_offset_degree = listFloat.ToArray();
            }

            if (rollDegree != null)
            {
                listFloat = new List<float>();
                foreach (var value in rollDegree)
                    listFloat.Add((float)value);
                cameraSettings.roll_degree = listFloat.ToArray();
            }

            if (distanceMeters != null)
            {
                listFloat = new List<float>();
                foreach (var value in distanceMeters)
                    listFloat.Add((float)value);
                cameraSettings.distance_meters = listFloat.ToArray();
            }

            if (heightPercent != null)
            {
                listFloat = new List<float>();
                foreach (var value in heightPercent)
                    listFloat.Add((float)value);
                cameraSettings.height_percent = listFloat.ToArray();
            }

            if (vertCtrRatio != null)
            {
                listFloat = new List<float>();
                foreach (var value in vertCtrRatio)
                    listFloat.Add((float)value);
                cameraSettings.vert_ctr_ratio = listFloat.ToArray();
            }

            return cameraSettings;
        }

        public List<string> interpolation { get; set; }
        public List<string> targetType { get; set; }
        public List<double> easeInSpeed { get; set; }
        public List<double> easeOutSpeed { get; set; }
        public List<double> durationSeconds { get; set; }
        public List<double> waitSeconds { get; set; }
        public List<double> transitionSeconds { get; set; }
        public List<double> angleDegree { get; set; }
        public List<double> angleOffsetDegree { get; set; }
        public List<double> pitchDegree { get; set; }
        public List<double> pitchOffsetDegree { get; set; }
        public List<double> rollDegree { get; set; }
        public List<double> distanceMeters { get; set; }
        public List<double> heightPercent { get; set; }
        public List<double> vertCtrRatio { get; set; }
        public string nextCamera { get; set; }
    }

    public class MoveSequenceSettings
    {
        public POGOProtos.Settings.Master.MoveSequenceSettings Convert()
        {
            POGOProtos.Settings.Master.MoveSequenceSettings moveSequenceSettings = new POGOProtos.Settings.Master.MoveSequenceSettings();

            moveSequenceSettings.sequence.AddRange(sequence);

            return moveSequenceSettings;
        }

        public List<string> sequence { get; set; }
    }

    #endregion Classes for Old GAME_MASTER.json

    public static class TypeConverter
    {
        public static T Convert<T>(string text)
        {
            if (text == null)
                return default(T);

            StringBuilder stringBuilder = new StringBuilder();
            foreach (var c in typeof(T).Name)
            {
                if (char.IsUpper(c) && stringBuilder.Length > 0)
                    stringBuilder.Append('_');
                stringBuilder.Append(char.ToUpper(c));
            }
            string prepend = stringBuilder.ToString() + '_';

            foreach (T value in Enum.GetValues(typeof(T)))
            {
                if (string.Equals(text, value.ToString()) ||
                    string.Equals(prepend + text, value.ToString()))
                    return value;

                int number;
                if (int.TryParse(text, out number) && (int)((object)value) == number)
                    return value;
            }

            return default(T);
        }
    }
}
