// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: SetPlayerTeamResponse.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Networking.Responses
{

    [global::ProtoBuf.ProtoContract()]
    public partial class SetPlayerTeamResponse : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public Status status { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public global::POGOProtos.Data.PlayerData player_data { get; set; }

        [global::ProtoBuf.ProtoContract()]
        public enum Status
        {
            UNSET = 0,
            SUCCESS = 1,
            TEAM_ALREADY_SET = 2,
            FAILURE = 3,
        }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
