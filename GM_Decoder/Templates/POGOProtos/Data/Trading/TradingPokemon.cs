// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: TradingPokemon.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Data.Trading
{

    [global::ProtoBuf.ProtoContract()]
    public partial class TradingPokemon : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
        public ulong pokemon_id { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public int pokedex_entry_number { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public int original_cp { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public int adjusted_cp_min { get; set; }

        [global::ProtoBuf.ProtoMember(5)]
        public int adjusted_cp_max { get; set; }

        [global::ProtoBuf.ProtoMember(6)]
        public int original_stamina { get; set; }

        [global::ProtoBuf.ProtoMember(7)]
        public int adjusted_stamina_min { get; set; }

        [global::ProtoBuf.ProtoMember(8)]
        public int adjusted_stamina_max { get; set; }

        [global::ProtoBuf.ProtoMember(9)]
        public bool friend_level_cap { get; set; }

        [global::ProtoBuf.ProtoMember(10)]
        public int move1 { get; set; }

        [global::ProtoBuf.ProtoMember(11)]
        public int move2 { get; set; }

        [global::ProtoBuf.ProtoMember(12)]
        public global::POGOProtos.Data.PokemonDisplay pokemon_display { get; set; }

        [global::ProtoBuf.ProtoMember(13)]
        public long captured_s2_cell_id { get; set; }

        [global::ProtoBuf.ProtoMember(14)]
        public global::POGOProtos.Data.PokemonData traded_pokemon { get; set; }

        [global::ProtoBuf.ProtoMember(15)]
        public global::POGOProtos.Inventory.Item.ItemData pokeball { get; set; }

        [global::ProtoBuf.ProtoMember(16)]
        public int individual_attack { get; set; }

        [global::ProtoBuf.ProtoMember(17)]
        public int individual_defense { get; set; }

        [global::ProtoBuf.ProtoMember(18)]
        public int individual_stamina { get; set; }

        [global::ProtoBuf.ProtoMember(19)]
        [global::System.ComponentModel.DefaultValue("")]
        public string nickname { get; set; } = "";

        [global::ProtoBuf.ProtoMember(20)]
        public bool favorite { get; set; }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
