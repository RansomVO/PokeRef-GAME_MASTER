// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: GymStartSessionResponse.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Networking.Responses
{

    [global::ProtoBuf.ProtoContract()]
    public partial class GymStartSessionResponse : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public Result result { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public global::POGOProtos.Data.Battle.Battle battle { get; set; }

        [global::ProtoBuf.ProtoContract()]
        public enum Result
        {
            UNSET = 0,
            SUCCESS = 1,
            ERROR_GYM_NOT_FOUND = 2,
            ERROR_GYM_NEUTRAL = 3,
            ERROR_GYM_WRONG_TEAM = 4,
            ERROR_GYM_EMPTY = 5,
            ERROR_INVALID_DEFENDER = 6,
            ERROR_TRAINING_INVALID_ATTACKER_COUNT = 7,
            ERROR_ALL_POKEMON_FAINTED = 8,
            ERROR_TOO_MANY_BATTLES = 9,
            ERROR_TOO_MANY_PLAYERS = 10,
            ERROR_GYM_BATTLE_LOCKOUT = 11,
            ERROR_PLAYER_BELOW_MINIMUM_LEVEL = 12,
            ERROR_NOT_IN_RANGE = 13,
            ERROR_POI_INACCESSIBLE = 14,
            ERROR_RAID_ACTIVE = 15,
        }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
