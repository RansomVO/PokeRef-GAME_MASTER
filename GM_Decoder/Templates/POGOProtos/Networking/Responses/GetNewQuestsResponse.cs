// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: GetNewQuestsResponse.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Networking.Responses
{

    [global::ProtoBuf.ProtoContract()]
    public partial class GetNewQuestsResponse : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public Status status { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public global::System.Collections.Generic.List<global::POGOProtos.Data.Quests.ClientQuest> quests { get; } = new global::System.Collections.Generic.List<global::POGOProtos.Data.Quests.ClientQuest>();

        [global::ProtoBuf.ProtoMember(3)]
        public global::System.Collections.Generic.List<global::POGOProtos.Data.Quests.ClientQuest> version_changed_quests { get; } = new global::System.Collections.Generic.List<global::POGOProtos.Data.Quests.ClientQuest>();

        [global::ProtoBuf.ProtoContract()]
        public enum Status
        {
            UNSET = 0,
            SUCCESS = 1,
            ERROR_INVALID_DISPLAY = 2,
        }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
