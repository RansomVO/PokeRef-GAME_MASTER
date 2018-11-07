using System;

using VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.DataFiles;
using VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData;

using VanOrman.Utils;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator
{
    public class GameMasterStatsCalculator
    {
        #region Stats Data

        private _MoveSetStats[] _moveSetsStats = new _MoveSetStats[PokeConstants.Regions.Length];

        #endregion Stats Data

        public Settings._GameMasterStats GameMasterStats { get; private set; }

        public GameMasterStatsCalculator(string currentGameMasterFileName)
        {
            GameMasterStats = new Settings._GameMasterStats(GameMasterTimestampUtils.FileNameToDateTime(currentGameMasterFileName), PokeConstants.Regions.Length - 1);

            for (int i = 0; i < PokeConstants.Regions.Length; i++)
                _moveSetsStats[i] = new _MoveSetStats();
        }

        public void Update(PokemonUnreleased._Pokemon pokemon)
        {
            int gen = PokeFormulas.GetGeneration(pokemon.id);

            GameMasterStats.PokemonCount[0].total++;
            GameMasterStats.PokemonCount[gen].total++;
        }

        public void Update(PokeStats._Pokemon pokemon)
        {
            int gen = PokeFormulas.GetGeneration(pokemon.id);
            GameMasterStats.gens_released = Math.Max(gen, GameMasterStats.gens_released);

            GameMasterStats.PokemonCount[0].total++;
            GameMasterStats.PokemonCount[gen].total++;

            if (!string.Equals(pokemon.availability, PokeConstants.Availability.Unreleased, StringComparison.OrdinalIgnoreCase))
            {
                GameMasterStats.PokemonCount[0].available++;
                GameMasterStats.PokemonCount[gen].available++;

                if (pokemon.availability != null && (
                    pokemon.availability.IndexOf(PokeConstants.Availability.Regional, StringComparison.OrdinalIgnoreCase) == -1 ||
                    pokemon.availability.IndexOf(PokeConstants.Availability.Region.UnitedStates, StringComparison.OrdinalIgnoreCase) != -1 ||
                    pokemon.availability.IndexOf(PokeConstants.Availability.Region.Americas, StringComparison.OrdinalIgnoreCase) != -1))
                {
                    GameMasterStats.PokemonCount[0].available_wa++;
                    GameMasterStats.PokemonCount[gen].available_wa++;
                }
            }
        }

        public void Update(MoveSets._Pokemon pokemonMoveSets)
        {
            try
            {
                int gen = PokeFormulas.GetGeneration(pokemonMoveSets.id);

				foreach (var moveSet in pokemonMoveSets.MoveSet)
				{
					_moveSetsStats[0].count++;
					_moveSetsStats[gen].count++;
					_moveSetsStats[0].totalDPS += moveSet.base_dps;
					_moveSetsStats[gen].totalDPS += moveSet.base_dps;
					_moveSetsStats[0].totalTrueDPS += moveSet.true_dps;
					_moveSetsStats[gen].totalTrueDPS += moveSet.true_dps;

					GameMasterStats.MoveSets[0].dps_max = Math.Max(GameMasterStats.MoveSets[0].dps_max, moveSet.base_dps);
					GameMasterStats.MoveSets[gen].dps_max = Math.Max(GameMasterStats.MoveSets[gen].dps_max, moveSet.base_dps);
					GameMasterStats.MoveSets[0].true_dps_max = Math.Max(GameMasterStats.MoveSets[0].true_dps_max, moveSet.true_dps);
					GameMasterStats.MoveSets[gen].true_dps_max = Math.Max(GameMasterStats.MoveSets[gen].true_dps_max, moveSet.true_dps);
					GameMasterStats.MoveSets[0].dps_avg = _moveSetsStats[0].totalDPS / _moveSetsStats[0].count;
					GameMasterStats.MoveSets[gen].dps_avg = _moveSetsStats[gen].totalDPS / _moveSetsStats[gen].count;
					GameMasterStats.MoveSets[0].true_dps_avg = _moveSetsStats[0].totalTrueDPS / _moveSetsStats[0].count;
					GameMasterStats.MoveSets[gen].true_dps_avg = _moveSetsStats[gen].totalTrueDPS / _moveSetsStats[gen].count;
				}
                //Console.Out.WriteLine("{0}\t{1}\t{2}\t{3}\t{4}", _moveSetsStats[gen].count, moveSet.base_dps, _moveSetsStats[gen].totalDPS, GameMasterStats.MoveSets[gen].dps_max, GameMasterStats.MoveSets[0].dps_avg);
            }
            catch (Exception ex)
            {
                ConsoleOutput.OutputException(ex, "Error calculating MoveSet Stats.");
            }
        }

        #region Internal Classes

        private class _MoveSetStats
        {
            public int count = 0;
            public double totalDPS = 0;
            public double totalTrueDPS = 0;
        }

        #endregion Internal Classes
    }
}
