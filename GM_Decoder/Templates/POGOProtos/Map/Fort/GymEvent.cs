// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: GymEvent.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Map.Fort
{

    [global::ProtoBuf.ProtoContract()]
    public partial class GymEvent : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string trainer { get; set; } = "";

        [global::ProtoBuf.ProtoMember(2)]
        public long timestamp_ms { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public Event @event { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public int pokedex_id { get; set; }

        [global::ProtoBuf.ProtoMember(5, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong pokemon_id { get; set; }

        [global::ProtoBuf.ProtoContract()]
        public enum Event
        {
            UNKNOWN = 0,
            POKEMON_FED = 1,
            POKEMON_DEPLOYED = 2,
            POKEMON_RETURNED = 3,
            BATTLE_WON = 4,
            BATTLE_LOSS = 5,
            RAID_STARTED = 6,
            RAID_ENDED = 7,
            GYM_NEUTRALIZED = 8,
        }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
