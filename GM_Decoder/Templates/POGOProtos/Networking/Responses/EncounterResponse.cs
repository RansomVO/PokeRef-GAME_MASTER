// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: EncounterResponse.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Networking.Responses
{

    [global::ProtoBuf.ProtoContract()]
    public partial class EncounterResponse : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::POGOProtos.Map.Pokemon.WildPokemon wild_pokemon { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(2)]
        public Background background { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(3)]
        public Status status { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public global::POGOProtos.Data.Capture.CaptureProbability capture_probability { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(5)]
        public global::POGOProtos.Inventory.Item.ItemId active_item { get; set; }

        [global::ProtoBuf.ProtoMember(6)]
        public int arplus_attempts_until_flee { get; set; }

        [global::ProtoBuf.ProtoContract()]
        public enum Background
        {
            PARK = 0,
            DESERT = 1,
        }

        [global::ProtoBuf.ProtoContract()]
        public enum Status
        {
            ENCOUNTER_ERROR = 0,
            ENCOUNTER_SUCCESS = 1,
            ENCOUNTER_NOT_FOUND = 2,
            ENCOUNTER_CLOSED = 3,
            ENCOUNTER_POKEMON_FLED = 4,
            ENCOUNTER_NOT_IN_RANGE = 5,
            ENCOUNTER_ALREADY_HAPPENED = 6,
            POKEMON_INVENTORY_FULL = 7,
        }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
