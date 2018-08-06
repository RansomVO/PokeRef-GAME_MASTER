using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates.DataFiles
{
	[Serializable]
	public class Friendship : IComparable<Friendship>
	{
		#region Properties

		[XmlAttribute]
		public int level { get; set; }

		[XmlAttribute]
		public string name { get; set; }

		[XmlAttribute]
		public string text { get; set; }

		[XmlAttribute]
		public int points_to_reach { get; set; }

		[XmlAttribute]
		public int xp_reward { get; set; }

		[XmlElement]
		public _Bonus Bonus { get; set; }

		#endregion Properties

		#region Internal classes

		[Serializable]
		public class _Bonus
		{
			#region Properties

			[XmlAttribute]
			public int raid_ball { get; set; }

			[XmlAttribute]
			public int attack_percentage { get; set; }

			[XmlAttribute]
			public int trading_discount { get; set; }

			[XmlElement]
			public _Trade[] Trade { get; set; }

			#endregion Properties

			#region Internal classes

			[Serializable]
			public class _Trade
			{
				#region Properties

				[XmlAttribute]
				public int type { get; set; }

				[XmlAttribute]
				public string category { get; set; }

				[XmlAttribute]
				public bool in_pokedex { get; set; }


				#endregion Properties

				#region ctor

				public _Trade() { }

				public _Trade(POGOProtos.Enums.PokemonTradingType unlockedTrade)
				{
					type = (int)unlockedTrade;
					category = GetCategory(unlockedTrade);
					in_pokedex = GetInPokedex(unlockedTrade);
				}

				private string GetCategory(POGOProtos.Enums.PokemonTradingType unlockedTrade)
				{
					return TemplateTranslator.FixName(unlockedTrade.ToString().Split(new[] { "_IN_", "_NON_" }, StringSplitOptions.RemoveEmptyEntries)[0]);
				}

				private bool GetInPokedex(POGOProtos.Enums.PokemonTradingType unlockedTrade)
				{
					if (unlockedTrade == POGOProtos.Enums.PokemonTradingType.REGULAR_IN_POKEDEX ||
						unlockedTrade == POGOProtos.Enums.PokemonTradingType.SPECIAL_IN_POKEDEX)
						return true;

					return false;
				}

				#endregion ctor
			}

			#endregion Internal classes

			#region ctor

			public _Bonus() { }

			public _Bonus(FriendshipTranslator friendshipTranslator)
			{
				raid_ball = friendshipTranslator.RaidBallBonus;
				attack_percentage = friendshipTranslator.AttackBonus;
				trading_discount = friendshipTranslator.TradingDiscount;
				List<_Trade> trade = new List<_Trade>();
				foreach (var unlockedTrade in friendshipTranslator.UnlockedTrading)
					trade.Add(new _Trade(unlockedTrade));
				Trade = trade.ToArray();
			}

			#endregion ctor
		}

		#endregion Internal classes

		#region ctor

		public Friendship() { }

		public Friendship(FriendshipTranslator friendshipTranslator)
		{
			level = friendshipTranslator.Level;
			points_to_reach = friendshipTranslator.PointsToReach;
			xp_reward = friendshipTranslator.RewardXP;
			Bonus = new _Bonus(friendshipTranslator);
		}

		#endregion ctor

		#region IComparable

		public int CompareTo(Friendship other)
		{
			return (level - other.level);
		}

		#endregion IComparable

		#region Writers

		private static string FilePath { get { return Path.Combine(Utils.OutputDataFileFolder, "friends.xml"); } }

		/// <summary>
		/// Write out the Moves that are available in the game.
		/// </summary>
		public static void Write(List<FriendshipTranslator> friendshipTranslators, GameMasterStatsCalculator gameMasterStatsCalculator)
		{
			DateTime updateDateTime = gameMasterStatsCalculator.GameMasterStats.last_updated.Date;

			if (!File.Exists(FilePath) || Utils.GetLastUpdated(FilePath) < updateDateTime)
				Utils.WriteXML(new Friendships(friendshipTranslators, updateDateTime), FilePath);
		}

		[Serializable]
		public class Friendships
		{
			[XmlAttribute(DataType = "date")]
			public DateTime last_updated { get; set; }

			[XmlElement]
			public Friendship[] Friendship { get; set; }

			#region ctor

			public Friendships() { }

			public Friendships(IEnumerable<FriendshipTranslator> friendshipTranslators, DateTime updateDateTime)
			{
				last_updated = updateDateTime;

				List<Friendship> friendships = new List<Friendship>();
				foreach (var friendshipTranslator in friendshipTranslators)
					friendships.Add(new Friendship(friendshipTranslator));
				friendships.Sort();
				Friendship = friendships.ToArray();
			}

			#endregion ctor
		}

		#endregion Writers
	}
}