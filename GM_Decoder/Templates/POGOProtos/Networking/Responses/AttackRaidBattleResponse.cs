// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: AttackRaidBattleResponse.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Networking.Responses
{

    [global::ProtoBuf.ProtoContract()]
    public partial class AttackRaidBattleResponse : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(1)]
        public Result result { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public global::POGOProtos.Data.Battle.BattleUpdate battle_update { get; set; }

        [global::ProtoBuf.ProtoContract()]
        public enum Result
        {
            UNSET = 0,
            SUCCESS = 1,
            ERROR_GYM_NOT_FOUND = 2,
            ERROR_BATTLE_NOT_FOUND = 3,
            ERROR_INVALID_ATTACK_ACTIONS = 4,
            ERROR_NOT_PART_OF_BATTLE = 5,
            ERROR_BATTLE_ID_NOT_RAID = 6,
        }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
