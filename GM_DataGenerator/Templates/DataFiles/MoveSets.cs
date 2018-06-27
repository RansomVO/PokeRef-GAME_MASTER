using POGOProtos.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Xml.Serialization;
using VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.ManualData;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.DataFiles
{
	[Serializable]
	public class MoveSets
	{
		#region Properties

		[XmlAttribute(DataType = "date")]
		public DateTime last_updated { get; set; }

		[XmlAttribute]
		public int gen { get; set; }

		[XmlElement]
		public _Pokemon[] Pokemon { get; set; }

		#endregion Properties

		#region Internal classes

		[Serializable]
		public class _Pokemon : PokemonForm
		{
			#region Properties

			[XmlElement]
			public _MoveSet[] MoveSet { get; set; }

			#endregion Properties

			#region Internal classes

			[Serializable]
			public class _MoveSet
			{
				#region Properties

				[XmlAttribute]
				public double base_dps { get; set; }

				[XmlAttribute]
				public double true_dps { get; set; }

				[XmlAttribute]
				public int comparison { get; set; }

				[XmlElement]
				public Attack FastAttack { get; set; }

				[XmlElement]
				public Attack ChargedAttack { get; set; }

				#endregion Properties

				#region Internal classes

				[Serializable]
				public class Attack
				{
					#region Properties

					[XmlAttribute]
					public string name { get; set; }

					[XmlAttribute]
					public bool stab { get; set; }

					[XmlAttribute]
					public bool legacy { get; set; }

					#endregion Properties

					#region ctor

					public Attack() { }

					public Attack(string _name, bool _stab, bool _legacy)
					{
						name = _name;
						stab = _stab;
						legacy = _legacy;
					}

					#endregion ctor
				}

				#endregion Internal classes

				#region ctor

				public _MoveSet() { }

				public _MoveSet(PokemonTranslator pokemonTranslator, MoveTranslator fastMove, bool fastMoveLegacy, MoveTranslator chargedMove, bool chargedMoveLegacy) :
					this(pokemonTranslator, fastMove, fastMoveLegacy, PokeFormulas.HasStab(pokemonTranslator, fastMove), chargedMove, chargedMoveLegacy, PokeFormulas.HasStab(pokemonTranslator, chargedMove))
				{ }

				public _MoveSet(PokemonTranslator pokemonTranslator, MoveTranslator fastMove, bool fastMoveLegacy, bool fastMoveStab, MoveTranslator chargedMove, bool chargedMoveLegacy) :
					this(pokemonTranslator, fastMove, fastMoveLegacy, fastMoveStab, chargedMove, chargedMoveLegacy, PokeFormulas.HasStab(pokemonTranslator, chargedMove))
				{ }

				public _MoveSet(PokemonTranslator pokemonTranslator, MoveTranslator fastMove, bool fastMoveLegacy, MoveTranslator chargedMove, bool chargedMoveLegacy, bool chargedMoveStab) :
					this(pokemonTranslator, fastMove, fastMoveLegacy, PokeFormulas.HasStab(pokemonTranslator, fastMove), chargedMove, chargedMoveLegacy, chargedMoveStab)
				{ }

				public _MoveSet(PokemonTranslator pokemonTranslator, MoveTranslator fastMove, bool fastMoveLegacy, bool fastMoveStab, MoveTranslator chargedMove, bool chargedMoveLegacy, bool chargedMoveStab)
				{
					base_dps = PokeFormulas.GetMoveSetDPS(pokemonTranslator, fastMove, chargedMove);
					true_dps = PokeFormulas.GetTrueDPS(pokemonTranslator, fastMove, fastMoveStab, chargedMove, chargedMoveStab);
					FastAttack = new Attack(fastMove.Name, fastMoveStab, fastMoveLegacy);
					ChargedAttack = new Attack(chargedMove.Name, chargedMoveStab, chargedMoveLegacy);
				}

				#endregion ctor
			}

			#endregion Internal classes

			#region ctor

			public _Pokemon() { }

			public _Pokemon(PokemonTranslator pokemonTranslator, _MoveSet[] moveSet) :
				base(pokemonTranslator.Id, pokemonTranslator.Name, pokemonTranslator.Form)
			{
				MoveSet = moveSet;
			}

			#endregion ctor
		}

		#endregion Internal classes

		#region ctor

		public MoveSets() { }

		public MoveSets(int _gen, _Pokemon[] pokemon, DateTime updateDateTime)
		{
			last_updated = updateDateTime;
			gen = _gen;

			Pokemon = pokemon;
		}

		#endregion ctor

		#region Writers

		/// <summary>
		/// Write out the Move Sets for each generation of Pokemon.
		/// </summary>
		public static void Write(IEnumerable<PokemonTranslator> pokemonTranslators, Dictionary<PokemonId, FormSettingsTranslator> forms, Dictionary<PokemonMove, MoveTranslator> moves, ManualDataSettings manualDataSettings, GameMasterStatsCalculator gameMasterStatsCalculator)
		{
			DateTime updateDateTime = new DateTime(Math.Max(
				gameMasterStatsCalculator.GameMasterStats.last_updated.Date.Ticks,
                manualDataSettings.SpecialMoves.last_updated.Date.Ticks));

			bool update = false;
			List<MoveSets._Pokemon>[] pokemonMoveSetList = new List<MoveSets._Pokemon>[PokeConstants.Regions.Length + 1];
			for (int gen = 1; gen < PokeConstants.Regions.Length; gen++)
			{
				string filePath = Path.Combine(Utils.OutputDataFileFolder, "movesets.gen" + gen + ".xml");
				DateTime lastUpdated = Utils.GetLastUpdated(filePath);
				if (!File.Exists(filePath) || lastUpdated < updateDateTime)
				{
					update = true;
					pokemonMoveSetList[gen] = new List<MoveSets._Pokemon>();
				}
			}

			if (update)
			{
				foreach (var pokemonTranslator in pokemonTranslators)
				{
					// Need to deal with the following cases:
					//  - Unown has multiple forms, but only a single record.
					//  - Castform has multiple forms and multiple records, but each record has unique movesets.
					//  - Deoxys has multiple forms and multiple records, but all records have the same movesets.
					if (forms.ContainsKey(pokemonTranslator.PokemonSettings.pokemon_id) &&
						forms[pokemonTranslator.PokemonSettings.pokemon_id].FormSettings.forms.Count > 0)
					{
						List<PokemonTranslator> records = new List<PokemonTranslator>();
						foreach (var pokemon in pokemonTranslators)
							if (pokemon.Id == pokemonTranslator.Id)
								records.Add(pokemon);

						// If there are more that 1 match, then we need to deal with pokemon with multiple forms. (E.G. Unown)
						if (records.Count > 1)
						{
							// There are multiple records. We need to compare the movesets for the records.
							int matches = 0;
							foreach (var record in records)
								if (IsMoveSetMatch(pokemonTranslator, record))
									matches++;

							// If every record matches the moveset, skip all but the base form.
							if (matches == records.Count)
							{
								if (pokemonTranslator.Form != Form.FORM_UNSET)
									continue;
							}
							// If only a sub-set of records match the moveset, skip the base form.
							else if (pokemonTranslator.Form == Form.FORM_UNSET)
								continue;
						}
					}

					int gen = PokeFormulas.GetGeneration(pokemonTranslator);
					if (pokemonMoveSetList[gen] != null)
					{
						List<_Pokemon._MoveSet> moveSets = new List<_Pokemon._MoveSet>();

						moveSets.AddRange(GetMoveSets(pokemonTranslator, moves, false, false));
						moveSets.AddRange(GetMoveSets(pokemonTranslator, moves, true, false));
						moveSets.AddRange(GetMoveSets(pokemonTranslator, moves, false, true));
						moveSets.AddRange(GetMoveSets(pokemonTranslator, moves, true, true));

						double maxDPS = 0;
						foreach (var moveSet in moveSets)
							if (!moveSet.FastAttack.legacy && !moveSet.ChargedAttack.legacy)
								maxDPS = Math.Max(maxDPS, moveSet.true_dps);

						foreach (var moveSet in moveSets)
							moveSet.comparison = (int)Math.Ceiling(moveSet.true_dps / maxDPS * 100);

						_Pokemon pokemon = new _Pokemon(pokemonTranslator, moveSets.ToArray());
						pokemonMoveSetList[gen].Add(pokemon);
						gameMasterStatsCalculator.Update(pokemon);
					}
				}

				for (int gen = 1; gen < PokeConstants.Regions.Length; gen++)
					if (pokemonMoveSetList[gen] != null && pokemonMoveSetList[gen].Count > 1)
						Utils.WriteXML(new MoveSets(gen, pokemonMoveSetList[gen].ToArray(), updateDateTime), Path.Combine(Utils.OutputDataFileFolder, "movesets.gen" + gen + ".xml"));
			}
		}

		private static List<_Pokemon._MoveSet> GetMoveSets(PokemonTranslator pokemonTranslator, Dictionary<PokemonMove, MoveTranslator> moves, bool fastMovesLegacy, bool chargedMovesLegacy)
		{
			List<_Pokemon._MoveSet> moveSets = new List<_Pokemon._MoveSet>();

			List<PokemonMove> fastMoves = fastMovesLegacy ? pokemonTranslator.LegacyFastMoves : pokemonTranslator.PokemonSettings.quick_moves;
			List<PokemonMove> chargedMoves = chargedMovesLegacy ? pokemonTranslator.LegacyChargedMoves : pokemonTranslator.PokemonSettings.cinematic_moves;

			foreach (var fastMove in fastMoves)
			{
				foreach (var chargedMove in chargedMoves)
					if (fastMove == PokemonMove.HIDDEN_POWER_FAST)
					{
						moveSets.Add(new _Pokemon._MoveSet(pokemonTranslator, moves[fastMove], fastMovesLegacy, true, moves[chargedMove], chargedMovesLegacy));
						moveSets.Add(new _Pokemon._MoveSet(pokemonTranslator, moves[fastMove], fastMovesLegacy, false, moves[chargedMove], chargedMovesLegacy));
					}
					else
						moveSets.Add(new _Pokemon._MoveSet(pokemonTranslator, moves[fastMove], fastMovesLegacy, moves[chargedMove], chargedMovesLegacy));
			}

			return moveSets;
		}

		private static bool IsMoveSetMatch(PokemonTranslator pokemonTranslator1, PokemonTranslator pokemonTranslator2)
		{
			return
				IsMoveSetMatch(pokemonTranslator1.PokemonSettings.quick_moves, pokemonTranslator2.PokemonSettings.quick_moves) &&
				IsMoveSetMatch(pokemonTranslator1.PokemonSettings.cinematic_moves, pokemonTranslator2.PokemonSettings.cinematic_moves);
		}

		private static bool IsMoveSetMatch(IEnumerable<PokemonMove> moves1, IEnumerable<PokemonMove> moves2)
		{
			bool match = true;
			foreach (var move1 in moves1)
			{
				bool found = false;
				foreach (var move2 in moves2)
					if (string.Equals(move1, move2))
					{
						found = true;
						break;
					}
				if (!found)
				{
					match = false;
					break;
				}
			}

			return match;
		}

		#endregion Writers
	}
}