// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: CheckAwardedBadgesResponse.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Networking.Responses
{

    [global::ProtoBuf.ProtoContract()]
    public partial class CheckAwardedBadgesResponse : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public bool success { get; set; }

        [global::ProtoBuf.ProtoMember(2, IsPacked = true)]
        public global::System.Collections.Generic.List<global::POGOProtos.Enums.BadgeType> awarded_badges { get; } = new global::System.Collections.Generic.List<global::POGOProtos.Enums.BadgeType>();

        [global::ProtoBuf.ProtoMember(3, IsPacked = true)]
        public int[] awarded_badge_levels { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public global::System.Collections.Generic.List<string> avatar_template_ids { get; } = new global::System.Collections.Generic.List<string>();

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
