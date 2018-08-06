// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: DiskEncounterResponse.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Networking.Responses
{

    [global::ProtoBuf.ProtoContract()]
    public partial class DiskEncounterResponse : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(1)]
        public Result result { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public global::POGOProtos.Data.PokemonData pokemon_data { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public global::POGOProtos.Data.Capture.CaptureProbability capture_probability { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(4)]
        public global::POGOProtos.Inventory.Item.ItemId active_item { get; set; }

        [global::ProtoBuf.ProtoMember(5)]
        public int arplus_attempts_until_flee { get; set; }

        [global::ProtoBuf.ProtoContract()]
        public enum Result
        {
            UNKNOWN = 0,
            SUCCESS = 1,
            NOT_AVAILABLE = 2,
            NOT_IN_RANGE = 3,
            ENCOUNTER_ALREADY_FINISHED = 4,
            POKEMON_INVENTORY_FULL = 5,
        }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
