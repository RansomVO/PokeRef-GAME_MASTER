using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using POGOProtos.Settings.Master;
using static POGOProtos.Networking.Responses.DownloadItemTemplatesResponse;
using POGOProtos.Enums;

namespace VanOrman.PokemonGO.GAME_MASTER.DataGenerator.Templates
{
	public class FriendshipTranslator : TemplateTranslator
	{
		public FriendshipLevelMilestoneSettings FriendshipLevelMilestoneSettings { get; private set; }

		#region Computed Properties

		public int Level { get { return GetLevel(TemplateId); } }

		public int PointsToReach { get { return FriendshipLevelMilestoneSettings.min_points_to_reach; } }

		public int RewardXP { get { return FriendshipLevelMilestoneSettings.milestone_xp_reward; } }

		public int AttackBonus { get { return (int)(FriendshipLevelMilestoneSettings.attack_bonus_percentage * 100); } }

		public int RaidBallBonus { get { return FriendshipLevelMilestoneSettings.raid_ball_bonus; } }

		public PokemonTradingType[] UnlockedTrading { get { return FriendshipLevelMilestoneSettings.unlocked_trading.ToArray(); } }

		public int TradingDiscount { get { return (int)(FriendshipLevelMilestoneSettings.trading_discount * 100); } }

		#endregion Properties

		#region ctor

		public FriendshipTranslator(ItemTemplate itemTemplate)
			: base(itemTemplate.template_id)
		{
			FriendshipLevelMilestoneSettings = itemTemplate.friendship_milestone_settings;
		}

		#endregion ctor

		public static int GetLevel(string templateId)
		{
			return int.Parse(templateId.Split(new[] { '_' }, StringSplitOptions.RemoveEmptyEntries)[2]);
		}

	}
}
