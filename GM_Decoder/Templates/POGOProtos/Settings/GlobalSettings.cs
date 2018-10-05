// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: GlobalSettings.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Settings
{

    [global::ProtoBuf.ProtoContract()]
    public partial class GlobalSettings : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(2)]
        public FortSettings fort_settings { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public MapSettings map_settings { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public LevelSettings level_settings { get; set; }

        [global::ProtoBuf.ProtoMember(5)]
        public InventorySettings inventory_settings { get; set; }

        [global::ProtoBuf.ProtoMember(6)]
        [global::System.ComponentModel.DefaultValue("")]
        public string minimum_client_version { get; set; } = "";

        [global::ProtoBuf.ProtoMember(7)]
        public GpsSettings gps_settings { get; set; }

        [global::ProtoBuf.ProtoMember(8)]
        public FestivalSettings festival_settings { get; set; }

        [global::ProtoBuf.ProtoMember(9)]
        public EventSettings event_settings { get; set; }

        [global::ProtoBuf.ProtoMember(10)]
        public int max_pokemon_types { get; set; }

        [global::ProtoBuf.ProtoMember(11)]
        public SfidaSettings sfida_settings { get; set; }

        [global::ProtoBuf.ProtoMember(12)]
        public NewsSettings news_settings { get; set; }

        [global::ProtoBuf.ProtoMember(13)]
        public TranslationSettings translation_settings { get; set; }

        [global::ProtoBuf.ProtoMember(14)]
        public PasscodeSettings passcode_settings { get; set; }

        [global::ProtoBuf.ProtoMember(15)]
        public NotificationSettings notification_settings { get; set; }

        [global::ProtoBuf.ProtoMember(16)]
        public global::System.Collections.Generic.List<string> client_app_blacklist { get; } = new global::System.Collections.Generic.List<string>();

        [global::ProtoBuf.ProtoMember(17)]
        public ClientPerformanceSettings client_perf_settings { get; set; }

        [global::ProtoBuf.ProtoMember(18)]
        public NewsGlobalSettings news_global_settings { get; set; }

        [global::ProtoBuf.ProtoMember(19)]
        public QuestGlobalSettings quest_global_settings { get; set; }

        [global::ProtoBuf.ProtoMember(21)]
        public TelemetryGlobalSettings telemetry_global_settings { get; set; }

        [global::ProtoBuf.ProtoMember(22)]
        public LoginSettings login_settings { get; set; }

        [global::ProtoBuf.ProtoMember(23)]
        public SocialClientSettings social_settings { get; set; }

        [global::ProtoBuf.ProtoMember(24)]
        public TradingGlobalSettings trading_global_settings { get; set; }

        [global::ProtoBuf.ProtoMember(25, IsPacked = true)]
        public global::System.Collections.Generic.List<global::POGOProtos.Enums.PokemonId> additional_allowed_pokemon_ids { get; } = new global::System.Collections.Generic.List<global::POGOProtos.Enums.PokemonId>();

        [global::ProtoBuf.ProtoMember(26)]
        public UpsightLoggingSettings upsight_logging_settings { get; set; }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
