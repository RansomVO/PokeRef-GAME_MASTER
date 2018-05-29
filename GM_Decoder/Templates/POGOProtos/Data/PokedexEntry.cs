// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: PokedexEntry.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Data
{

    [global::ProtoBuf.ProtoContract()]
    public partial class PokedexEntry : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::POGOProtos.Enums.PokemonId pokemon_id { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public int times_encountered { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public int times_captured { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public int evolution_stone_pieces { get; set; }

        [global::ProtoBuf.ProtoMember(5)]
        public int evolution_stones { get; set; }

        [global::ProtoBuf.ProtoMember(6, IsPacked = true)]
        public global::System.Collections.Generic.List<global::POGOProtos.Enums.Costume> captured_costumes { get; } = new global::System.Collections.Generic.List<global::POGOProtos.Enums.Costume>();

        [global::ProtoBuf.ProtoMember(7, IsPacked = true)]
        public global::System.Collections.Generic.List<global::POGOProtos.Enums.Form> captured_forms { get; } = new global::System.Collections.Generic.List<global::POGOProtos.Enums.Form>();

        [global::ProtoBuf.ProtoMember(8, IsPacked = true)]
        public global::System.Collections.Generic.List<global::POGOProtos.Enums.Gender> captured_genders { get; } = new global::System.Collections.Generic.List<global::POGOProtos.Enums.Gender>();

        [global::ProtoBuf.ProtoMember(9)]
        public bool captured_shiny { get; set; }

        [global::ProtoBuf.ProtoMember(10, IsPacked = true)]
        public global::System.Collections.Generic.List<global::POGOProtos.Enums.Costume> encountered_costumes { get; } = new global::System.Collections.Generic.List<global::POGOProtos.Enums.Costume>();

        [global::ProtoBuf.ProtoMember(11, IsPacked = true)]
        public global::System.Collections.Generic.List<global::POGOProtos.Enums.Form> encountered_forms { get; } = new global::System.Collections.Generic.List<global::POGOProtos.Enums.Form>();

        [global::ProtoBuf.ProtoMember(12, IsPacked = true)]
        public global::System.Collections.Generic.List<global::POGOProtos.Enums.Gender> encountered_genders { get; } = new global::System.Collections.Generic.List<global::POGOProtos.Enums.Gender>();

        [global::ProtoBuf.ProtoMember(13)]
        public bool encountered_shiny { get; set; }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
