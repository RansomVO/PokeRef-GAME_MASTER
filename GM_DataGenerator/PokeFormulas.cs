using System;

using VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates;
using VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.DataFiles;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator
{
    public static class PokeFormulas
    {
        #region Properties

        private static float[] CPMultiplier { get; set; }

        #endregion Properties

        #region Initializer

        public static void Init(PlayerLevelTranslator playerLevel)
        {
            CPMultiplier = playerLevel.CPMultiplier;
        }

        #endregion Initializer

        #region CPM

        /// <summary>
        /// Method to calculate the CPM for a half-level.
        /// </summary>
        /// <param name="below"></param>
        /// <param name="above"></param>
        /// <returns></returns>
        private static double GetCMPHalfStep(double below, double above)
        {
            return Math.Sqrt((Math.Pow(below, 2) + Math.Pow(above, 2)) / 2);
        }

        public static double GetCMPHalfStep(int levelBelow)
        {
            return GetCMPHalfStep(CPMultiplier[levelBelow - 1], CPMultiplier[levelBelow]);
        }

        public static double GetCMP(float level)
        {
            return level == (int)level ? CPMultiplier[(int)level - 1] : GetCMPHalfStep((int)level);
        }

        #endregion CMP

        #region CP and HP

        public static int GetMaxCP(PokemonTranslator pokemonTranslator)
        {
            return GetPokemonCP(new Common.IVScore(pokemonTranslator.PokemonSettings.stats), new Common.IVScore(PokeConstants.Evaluation.Attribute.Max, PokeConstants.Evaluation.Attribute.Max, PokeConstants.Evaluation.Attribute.Max), PokeConstants.MaxPokemonLevel);
        }

        public static int GetMaxHP(PokemonTranslator pokemonTranslator)
        {
            return GetPokemonHP(pokemonTranslator.PokemonSettings.stats.base_stamina, PokeConstants.Evaluation.Attribute.Max, PokeConstants.MaxPokemonLevel);
        }

        public static int GetPokemonCP(Common.IVScore baseIV, Common.IVScore pokemonIV, float level)
        {
            return GetPokemonCP(baseIV.attack, baseIV.defense, baseIV.stamina, pokemonIV, level);
        }

        public static int GetPokemonCP(int baseAttack, int baseDefense, int baseStamina, Common.IVScore pokemonIV, float level)
        {
            return (int)((baseAttack + pokemonIV.attack) * Math.Sqrt((baseDefense + pokemonIV.defense) * (baseStamina + pokemonIV.stamina)) * Math.Pow(GetCMP(level), 2) / 10);
        }

        public static int GetPokemonHP(int baseStamina, int pokemonStamina, float level)
        {
            return (int)((baseStamina + pokemonStamina) * GetCMP(level));
        }

        #endregion CP and HP

        #region DPS

        private const double STAB_BONUS = 1.25;

        public static bool HasStab(PokemonTranslator pokemonTranslator, MoveTranslator move)
        {
            return string.Equals(pokemonTranslator.Type1, move.Type, StringComparison.OrdinalIgnoreCase) ||
                string.Equals(pokemonTranslator.Type2, move.Type, StringComparison.OrdinalIgnoreCase);
        }

        public static double GetMoveSetDPS(PokemonTranslator pokemonTranslator, MoveTranslator fastMove, MoveTranslator chargedMove)
        {
            if (fastMove.Energy == 0)
                return 0;

            double fastMovesToCharge = Math.Ceiling((double)-chargedMove.Energy / (double)fastMove.Energy);
            double damage = (fastMove.Power * fastMovesToCharge) + chargedMove.Power;
            double time = (fastMove.Duration * fastMovesToCharge) + chargedMove.Duration;

            return damage / time;
        }

        public static double GetTrueDPS(PokemonTranslator pokemonTranslator, MoveTranslator fastMove, MoveTranslator chargedMove)
        {
            return GetTrueDPS(pokemonTranslator, fastMove, HasStab(pokemonTranslator, fastMove), chargedMove, HasStab(pokemonTranslator, chargedMove));
        }

        public static double GetTrueDPS(PokemonTranslator pokemonTranslator, MoveTranslator fastMove, bool fastMoveStab, MoveTranslator chargedMove, bool chargedMoveStab)
        {
            if (fastMove.Energy == 0)
                return 0;

            double fastSTAB = fastMoveStab ? STAB_BONUS : 1;
            double chargedSTAB = chargedMoveStab ? STAB_BONUS : 1;
            double fastMovesToCharge = Math.Ceiling((double)-chargedMove.Energy / (double)fastMove.Energy);
            double fastPower = (Math.Floor((pokemonTranslator.PokemonSettings.stats.base_attack + PokeConstants.Evaluation.Attribute.Max) * fastMove.Power * fastSTAB / 200) + 1) * fastMovesToCharge;
            double chargedPower = (Math.Floor((pokemonTranslator.PokemonSettings.stats.base_attack + PokeConstants.Evaluation.Attribute.Max) * chargedMove.Power * chargedSTAB / 200) + 1);
            double time = (fastMove.Duration * fastMovesToCharge) + chargedMove.Duration;

            return (fastPower + chargedPower) / time;
        }


        #endregion DPS

        #region Get Generation

        public static int GetGeneration(int pokemonId)
        {
            for (int i = 1; i < PokeConstants.GenerationRanges.Length; i++)
                if (PokeConstants.GenerationRanges[i][0] <= pokemonId && pokemonId <= PokeConstants.GenerationRanges[i][1])
                    return i;

            throw new ArgumentOutOfRangeException("pokemon");
        }

        public static int GetGeneration(Pokemon pokemon)
        {
            return GetGeneration(pokemon.id);
        }

        public static int GetGeneration(PokemonTranslator pokemon)
        {
            return GetGeneration(pokemon.Id);
        }

        #endregion Get Generation
    }
}
