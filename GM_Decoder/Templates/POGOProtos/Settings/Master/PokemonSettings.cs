// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: PokemonSettings.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Settings.Master
{

    [global::ProtoBuf.ProtoContract()]
    public partial class PokemonSettings : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(1)]
        public global::POGOProtos.Enums.PokemonId pokemon_id { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public float model_scale { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(4)]
        public global::POGOProtos.Enums.PokemonType type { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(5)]
        public global::POGOProtos.Enums.PokemonType type_2 { get; set; }

        [global::ProtoBuf.ProtoMember(6)]
        public global::POGOProtos.Settings.Master.Pokemon.CameraAttributes camera { get; set; }

        [global::ProtoBuf.ProtoMember(7)]
        public global::POGOProtos.Settings.Master.Pokemon.EncounterAttributes encounter { get; set; }

        [global::ProtoBuf.ProtoMember(8)]
        public global::POGOProtos.Settings.Master.Pokemon.StatsAttributes stats { get; set; }

        [global::ProtoBuf.ProtoMember(9, IsPacked = true)]
        public global::System.Collections.Generic.List<global::POGOProtos.Enums.PokemonMove> quick_moves { get; } = new global::System.Collections.Generic.List<global::POGOProtos.Enums.PokemonMove>();

        [global::ProtoBuf.ProtoMember(10, IsPacked = true)]
        public global::System.Collections.Generic.List<global::POGOProtos.Enums.PokemonMove> cinematic_moves { get; } = new global::System.Collections.Generic.List<global::POGOProtos.Enums.PokemonMove>();

        [global::ProtoBuf.ProtoMember(11, IsPacked = true)]
        public float[] animation_time { get; set; }

        [global::ProtoBuf.ProtoMember(12, IsPacked = true)]
        public global::System.Collections.Generic.List<global::POGOProtos.Enums.PokemonId> evolution_ids { get; } = new global::System.Collections.Generic.List<global::POGOProtos.Enums.PokemonId>();

        [global::ProtoBuf.ProtoMember(13)]
        public int evolution_pips { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(14)]
        public global::POGOProtos.Enums.PokemonRarity rarity { get; set; }

        [global::ProtoBuf.ProtoMember(15)]
        public float pokedex_height_m { get; set; }

        [global::ProtoBuf.ProtoMember(16)]
        public float pokedex_weight_kg { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(17)]
        public global::POGOProtos.Enums.PokemonId parent_pokemon_id { get; set; }

        [global::ProtoBuf.ProtoMember(18)]
        public float height_std_dev { get; set; }

        [global::ProtoBuf.ProtoMember(19)]
        public float weight_std_dev { get; set; }

        [global::ProtoBuf.ProtoMember(20)]
        public float km_distance_to_hatch { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(21)]
        public global::POGOProtos.Enums.PokemonFamilyId family_id { get; set; }

        [global::ProtoBuf.ProtoMember(22)]
        public int candy_to_evolve { get; set; }

        [global::ProtoBuf.ProtoMember(23)]
        public float km_buddy_distance { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(24)]
        public global::POGOProtos.Enums.BuddySize buddy_size { get; set; }

        [global::ProtoBuf.ProtoMember(25)]
        public float model_height { get; set; }

        [global::ProtoBuf.ProtoMember(26)]
        public global::System.Collections.Generic.List<global::POGOProtos.Settings.Master.Pokemon.EvolutionBranch> evolution_branch { get; } = new global::System.Collections.Generic.List<global::POGOProtos.Settings.Master.Pokemon.EvolutionBranch>();

        [global::ProtoBuf.ProtoMember(27)]
        public float model_scale_v2 { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(28)]
        public global::POGOProtos.Enums.Form form { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(29)]
        public global::POGOProtos.Enums.PokemonMove event_quick_move { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(30)]
        public global::POGOProtos.Enums.PokemonMove event_cinematic_move { get; set; }

        [global::ProtoBuf.ProtoMember(31, IsPacked = true)]
        public float[] buddy_offset_male { get; set; }

        [global::ProtoBuf.ProtoMember(32, IsPacked = true)]
        public float[] buddy_offset_female { get; set; }

        [global::ProtoBuf.ProtoMember(33)]
        public float buddy_scale { get; set; }

        [global::ProtoBuf.ProtoMember(34, IsPacked = true)]
        public float[] buddy_portrait_offset { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(35)]
        public global::POGOProtos.Enums.Form parent_form { get; set; }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
