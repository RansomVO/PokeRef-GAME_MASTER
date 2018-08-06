// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: NearbyPokemon.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Map.Pokemon
{

    [global::ProtoBuf.ProtoContract()]
    public partial class NearbyPokemon : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(1)]
        public global::POGOProtos.Enums.PokemonId pokemon_id { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public float distance_in_meters { get; set; }

        [global::ProtoBuf.ProtoMember(3, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong encounter_id { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        [global::System.ComponentModel.DefaultValue("")]
        public string fort_id { get; set; } = "";

        [global::ProtoBuf.ProtoMember(5)]
        [global::System.ComponentModel.DefaultValue("")]
        public string fort_image_url { get; set; } = "";

        [global::ProtoBuf.ProtoMember(6)]
        public global::POGOProtos.Data.PokemonDisplay pokemon_display { get; set; }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
