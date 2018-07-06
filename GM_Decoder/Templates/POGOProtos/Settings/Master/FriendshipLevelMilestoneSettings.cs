// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: FriendshipLevelMilestoneSettings.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Settings.Master
{

    [global::ProtoBuf.ProtoContract()]
    public partial class FriendshipLevelMilestoneSettings : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public int min_points_to_reach { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public int milestone_xp_reward { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public float attack_bonus_percentage { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public int raid_ball_bonus { get; set; }

        [global::ProtoBuf.ProtoMember(5, IsPacked = true)]
        public global::System.Collections.Generic.List<global::POGOProtos.Enums.PokemonTradingType> unlocked_trading { get; } = new global::System.Collections.Generic.List<global::POGOProtos.Enums.PokemonTradingType>();

        [global::ProtoBuf.ProtoMember(6)]
        public float trading_discount { get; set; }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006