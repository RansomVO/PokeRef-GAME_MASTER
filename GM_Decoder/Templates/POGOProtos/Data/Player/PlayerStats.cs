// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: PlayerStats.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Data.Player
{

    [global::ProtoBuf.ProtoContract()]
    public partial class PlayerStats : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public int level { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public long experience { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public long prev_level_xp { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public long next_level_xp { get; set; }

        [global::ProtoBuf.ProtoMember(5)]
        public float km_walked { get; set; }

        [global::ProtoBuf.ProtoMember(6)]
        public int pokemons_encountered { get; set; }

        [global::ProtoBuf.ProtoMember(7)]
        public int unique_pokedex_entries { get; set; }

        [global::ProtoBuf.ProtoMember(8)]
        public int pokemons_captured { get; set; }

        [global::ProtoBuf.ProtoMember(9)]
        public int evolutions { get; set; }

        [global::ProtoBuf.ProtoMember(10)]
        public int poke_stop_visits { get; set; }

        [global::ProtoBuf.ProtoMember(11)]
        public int pokeballs_thrown { get; set; }

        [global::ProtoBuf.ProtoMember(12)]
        public int eggs_hatched { get; set; }

        [global::ProtoBuf.ProtoMember(13)]
        public int big_magikarp_caught { get; set; }

        [global::ProtoBuf.ProtoMember(14)]
        public int battle_attack_won { get; set; }

        [global::ProtoBuf.ProtoMember(15)]
        public int battle_attack_total { get; set; }

        [global::ProtoBuf.ProtoMember(16)]
        public int battle_defended_won { get; set; }

        [global::ProtoBuf.ProtoMember(17)]
        public int battle_training_won { get; set; }

        [global::ProtoBuf.ProtoMember(18)]
        public int battle_training_total { get; set; }

        [global::ProtoBuf.ProtoMember(19)]
        public int prestige_raised_total { get; set; }

        [global::ProtoBuf.ProtoMember(20)]
        public int prestige_dropped_total { get; set; }

        [global::ProtoBuf.ProtoMember(21)]
        public int pokemon_deployed { get; set; }

        [global::ProtoBuf.ProtoMember(22, IsPacked = true)]
        public int[] pokemon_caught_by_type { get; set; }

        [global::ProtoBuf.ProtoMember(23)]
        public int small_rattata_caught { get; set; }

        [global::ProtoBuf.ProtoMember(24)]
        public double used_km_pool { get; set; }

        [global::ProtoBuf.ProtoMember(25)]
        public long last_km_refill_ms { get; set; }

        [global::ProtoBuf.ProtoMember(26)]
        public int num_raid_battle_won { get; set; }

        [global::ProtoBuf.ProtoMember(27)]
        public int num_raid_battle_total { get; set; }

        [global::ProtoBuf.ProtoMember(28)]
        public int num_legendary_battle_won { get; set; }

        [global::ProtoBuf.ProtoMember(29)]
        public int num_legendary_battle_total { get; set; }

        [global::ProtoBuf.ProtoMember(30)]
        public int num_berries_fed { get; set; }

        [global::ProtoBuf.ProtoMember(31)]
        public long total_defended_ms { get; set; }

        [global::ProtoBuf.ProtoMember(32, IsPacked = true)]
        public global::System.Collections.Generic.List<global::POGOProtos.Enums.GymBadgeType> event_badges { get; } = new global::System.Collections.Generic.List<global::POGOProtos.Enums.GymBadgeType>();

        [global::ProtoBuf.ProtoMember(33)]
        public float km_walked_past_active_day { get; set; }

        [global::ProtoBuf.ProtoMember(34)]
        public int num_challenge_quests_completed { get; set; }

        [global::ProtoBuf.ProtoMember(35)]
        public int num_trades { get; set; }

        [global::ProtoBuf.ProtoMember(36)]
        public int num_max_level_friends { get; set; }

        [global::ProtoBuf.ProtoMember(37)]
        public long trade_accumulated_distance_km { get; set; }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
