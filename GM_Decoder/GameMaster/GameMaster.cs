using System;
using System.Collections.Generic;

namespace PokemonGO.GAME_MASTER.Templates
{
    public class GameMaster
    {
        public List<ItemTemplate> itemTemplates { get; set; }
        public string timestampMs { get; set; }
    }

    public class ItemTemplate
    {
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
        public object badgeType { get; set; }
        public int badgeRank { get; set; }
        public List<int> targets { get; set; }
    }

    public class BattleSettings
    {
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
        public double spinBonusThreshold { get; set; }
        public double excellentThrowThreshold { get; set; }
        public double greatThrowThreshold { get; set; }
        public double niceThrowThreshold { get; set; }
        public int milestoneThreshold { get; set; }
    }

    public class Form
    {
        public string form { get; set; }
        public int assetBundleValue { get; set; }
    }

    public class FormSettings
    {
        public string pokemon { get; set; }
        public List<Form> forms { get; set; }
    }

    public class GymBadgeSettings
    {
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
        public List<int> requiredExperience { get; set; }
        public List<int> leaderSlots { get; set; }
        public List<int> trainerSlots { get; set; }
    }

    public class IapSettings
    {
        public List<int> dailyDefenderBonusPerPokemon { get; set; }
        public int dailyDefenderBonusMaxDefenders { get; set; }
        public List<string> dailyDefenderBonusCurrency { get; set; }
        public string minTimeBetweenClaimsMs { get; set; }
    }

    public class Food
    {
        public List<string> itemEffect { get; set; }
        public List<double> itemEffectPercent { get; set; }
        public double growthPercent { get; set; }
        public double? berryMultiplier { get; set; }
    }

    public class Potion
    {
        public int staAmount { get; set; }
        public double? staPercent { get; set; }
    }

    public class Incense
    {
        public int incenseLifetimeSeconds { get; set; }
        public int standingTimeBetweenEncountersSeconds { get; set; }
        public int movingTimeBetweenEncounterSeconds { get; set; }
        public int distanceRequiredForShorterIntervalMeters { get; set; }
    }

    public class EggIncubator
    {
        public string incubatorType { get; set; }
        public int uses { get; set; }
        public double distanceMultiplier { get; set; }
    }

    public class InventoryUpgrade
    {
        public int additionalStorage { get; set; }
        public string upgradeType { get; set; }
    }

    public class XpBoost
    {
        public double xpMultiplier { get; set; }
        public int boostDurationMs { get; set; }
    }

    public class Revive
    {
        public double staPercent { get; set; }
    }

    public class ItemSettings
    {
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
        public List<int> rankNum { get; set; }
        public List<int> requiredExperience { get; set; }
        public List<double> cpMultiplier { get; set; }
        public int maxEggPlayerLevel { get; set; }
        public int maxEncounterPlayerLevel { get; set; }
    }

    public class TypeEffective
    {
        public List<double> attackScalar { get; set; }
        public string attackType { get; set; }
    }

    public class PokemonUpgrades
    {
        public int upgradesPerLevel { get; set; }
        public int allowedLevelsAbovePlayer { get; set; }
        public List<int> candyCost { get; set; }
        public List<int> stardustCost { get; set; }
    }

    public class DailyQuest
    {
        public int bucketsPerDay { get; set; }
        public int streakLength { get; set; }
        public double? bonusMultiplier { get; set; }
        public double? streakBonusMultiplier { get; set; }
    }

    public class QuestSettings
    {
        public string questType { get; set; }
        public DailyQuest dailyQuest { get; set; }
    }

    public class Gender
    {
        public double malePercent { get; set; }
        public double femalePercent { get; set; }
        public double? genderlessPercent { get; set; }
    }

    public class GenderSettings
    {
        public string pokemon { get; set; }
        public Gender gender { get; set; }
    }

    public class Camera
    {
        public double diskRadiusM { get; set; }
        public double cylinderRadiusM { get; set; }
        public double cylinderHeightM { get; set; }
        public double shoulderModeScale { get; set; }
        public double? cylinderGroundM { get; set; }
    }

    public class Encounter
    {
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
        public int baseStamina { get; set; }
        public int baseAttack { get; set; }
        public int baseDefense { get; set; }
    }

    public class EvolutionBranch
    {
        public string evolution { get; set; }
        public int candyCost { get; set; }
        public string evolutionItemRequirement { get; set; }
    }

    public class PokemonSettings
    {
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
        public string sku { get; set; }
        public string category { get; set; }
        public int sortOrder { get; set; }
        public List<string> itemIds { get; set; }
        public List<int?> counts { get; set; }
    }

    public class Camera2
    {
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
        public List<string> sequence { get; set; }
    }
}
