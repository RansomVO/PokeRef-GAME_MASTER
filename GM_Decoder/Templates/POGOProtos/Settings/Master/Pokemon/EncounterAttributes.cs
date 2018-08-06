// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: EncounterAttributes.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Settings.Master.Pokemon
{

    [global::ProtoBuf.ProtoContract()]
    public partial class EncounterAttributes : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public float base_capture_rate { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public float base_flee_rate { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public float collision_radius_m { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public float collision_height_m { get; set; }

        [global::ProtoBuf.ProtoMember(5)]
        public float collision_head_radius_m { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(6)]
        public global::POGOProtos.Enums.PokemonMovementType movement_type { get; set; }

        [global::ProtoBuf.ProtoMember(7)]
        public float movement_timer_s { get; set; }

        [global::ProtoBuf.ProtoMember(8)]
        public float jump_time_s { get; set; }

        [global::ProtoBuf.ProtoMember(9)]
        public float attack_timer_s { get; set; }

        [global::ProtoBuf.ProtoMember(10)]
        public int bonus_candy_capture_reward { get; set; }

        [global::ProtoBuf.ProtoMember(11)]
        public int bonus_stardust_capture_reward { get; set; }

        [global::ProtoBuf.ProtoMember(12)]
        public float attack_probability { get; set; }

        [global::ProtoBuf.ProtoMember(13)]
        public float dodge_probability { get; set; }

        [global::ProtoBuf.ProtoMember(14)]
        public float dodge_duration_s { get; set; }

        [global::ProtoBuf.ProtoMember(15)]
        public float dodge_distance { get; set; }

        [global::ProtoBuf.ProtoMember(16)]
        public float camera_distance { get; set; }

        [global::ProtoBuf.ProtoMember(17)]
        public float min_pokemon_action_frequency_s { get; set; }

        [global::ProtoBuf.ProtoMember(18)]
        public float max_pokemon_action_frequency_s { get; set; }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
