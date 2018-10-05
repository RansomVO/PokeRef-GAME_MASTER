// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: PlayerPublicProfile.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Data.Player
{

    [global::ProtoBuf.ProtoContract()]
    public partial class PlayerPublicProfile : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string name { get; set; } = "";

        [global::ProtoBuf.ProtoMember(2)]
        public int level { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public PlayerAvatar avatar { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(4)]
        public global::POGOProtos.Enums.TeamColor team_color { get; set; }

        [global::ProtoBuf.ProtoMember(5)]
        public int battles_won { get; set; }

        [global::ProtoBuf.ProtoMember(6)]
        public float km_walked { get; set; }

        [global::ProtoBuf.ProtoMember(7)]
        public int caught_pokemon { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(8)]
        public global::POGOProtos.Enums.GymBadgeType gym_badge_type { get; set; }

        [global::ProtoBuf.ProtoMember(9)]
        public global::System.Collections.Generic.List<PlayerBadge> badges { get; } = new global::System.Collections.Generic.List<PlayerBadge>();

        [global::ProtoBuf.ProtoMember(10)]
        public long experience { get; set; }

        [global::ProtoBuf.ProtoMember(11)]
        public bool has_shared_ex_pass { get; set; }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
