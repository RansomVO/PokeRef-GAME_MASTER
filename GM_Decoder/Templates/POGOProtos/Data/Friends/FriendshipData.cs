// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: FriendshipData.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Data.Friends
{

    [global::ProtoBuf.ProtoContract()]
    public partial class FriendshipData : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public FriendshipLevelData friendship_level_data { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public global::System.Collections.Generic.List<global::POGOProtos.Data.Gift.GiftBoxDetails> giftbox_details { get; } = new global::System.Collections.Generic.List<global::POGOProtos.Data.Gift.GiftBoxDetails>();

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue("")]
        public string codename { get; set; } = "";

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
