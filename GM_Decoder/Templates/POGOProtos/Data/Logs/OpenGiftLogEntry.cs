// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: OpenGiftLogEntry.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Data.Logs
{

    [global::ProtoBuf.ProtoContract()]
    public partial class OpenGiftLogEntry : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(1)]
        public Result result { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string friend_codename { get; set; } = "";

        [global::ProtoBuf.ProtoMember(3)]
        public global::POGOProtos.Inventory.Loot items { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public global::System.Collections.Generic.List<global::POGOProtos.Data.PokemonData> pokemon_eggs { get; } = new global::System.Collections.Generic.List<global::POGOProtos.Data.PokemonData>();

        [global::ProtoBuf.ProtoContract()]
        public enum Result
        {
            UNSET = 0,
            SUCCESS = 1,
        }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
