// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: AttackGymResponse.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Networking.Responses
{

    [global::ProtoBuf.ProtoContract()]
    public partial class AttackGymResponse : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public Result result { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public global::POGOProtos.Data.Battle.BattleLog battle_log { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue("")]
        public string battle_id { get; set; } = "";

        [global::ProtoBuf.ProtoMember(4)]
        public global::POGOProtos.Data.Battle.BattlePokemonInfo active_defender { get; set; }

        [global::ProtoBuf.ProtoMember(5)]
        public global::POGOProtos.Data.Battle.BattlePokemonInfo active_attacker { get; set; }

        [global::ProtoBuf.ProtoMember(6)]
        public global::POGOProtos.Data.Battle.BattleUpdate battle_update { get; set; }

        [global::ProtoBuf.ProtoContract()]
        public enum Result
        {
            UNSET = 0,
            SUCCESS = 1,
            ERROR_INVALID_ATTACK_ACTIONS = 2,
            ERROR_NOT_IN_RANGE = 3,
        }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
