// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: AttackRaidBattleMessage.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Networking.Requests.Messages
{

    [global::ProtoBuf.ProtoContract()]
    public partial class AttackRaidBattleMessage : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string gym_id { get; set; } = "";

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string battle_id { get; set; } = "";

        [global::ProtoBuf.ProtoMember(3)]
        public global::System.Collections.Generic.List<global::POGOProtos.Data.Battle.BattleAction> attacker_actions { get; } = new global::System.Collections.Generic.List<global::POGOProtos.Data.Battle.BattleAction>();

        [global::ProtoBuf.ProtoMember(4)]
        public global::POGOProtos.Data.Battle.BattleAction last_retrieved_action { get; set; }

        [global::ProtoBuf.ProtoMember(5)]
        public long timestamp_ms { get; set; }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
