// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: DownloadItemTemplatesResponse.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Networking.Responses
{

    [global::ProtoBuf.ProtoContract()]
    public partial class DownloadItemTemplatesResponse : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public Result result { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public global::System.Collections.Generic.List<ItemTemplate> item_templates { get; } = new global::System.Collections.Generic.List<ItemTemplate>();

        [global::ProtoBuf.ProtoMember(3)]
        public ulong timestamp_ms { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public int page_offset { get; set; }

        [global::ProtoBuf.ProtoContract()]
        public partial class ItemTemplate : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1)]
            [global::System.ComponentModel.DefaultValue("")]
            public string template_id { get; set; } = "";

            [global::ProtoBuf.ProtoMember(2)]
            public global::POGOProtos.Settings.Master.PokemonSettings pokemon_settings { get; set; }

            [global::ProtoBuf.ProtoMember(3)]
            public global::POGOProtos.Settings.Master.ItemSettings item_settings { get; set; }

            [global::ProtoBuf.ProtoMember(4)]
            public global::POGOProtos.Settings.Master.MoveSettings move_settings { get; set; }

            [global::ProtoBuf.ProtoMember(5)]
            public global::POGOProtos.Settings.Master.MoveSequenceSettings move_sequence_settings { get; set; }

            [global::ProtoBuf.ProtoMember(8)]
            public global::POGOProtos.Settings.Master.TypeEffectiveSettings type_effective { get; set; }

            [global::ProtoBuf.ProtoMember(10)]
            public global::POGOProtos.Settings.Master.BadgeSettings badge_settings { get; set; }

            [global::ProtoBuf.ProtoMember(11)]
            public global::POGOProtos.Settings.Master.CameraSettings camera { get; set; }

            [global::ProtoBuf.ProtoMember(12)]
            public global::POGOProtos.Settings.Master.PlayerLevelSettings player_level { get; set; }

            [global::ProtoBuf.ProtoMember(13)]
            public global::POGOProtos.Settings.Master.GymLevelSettings gym_level { get; set; }

            [global::ProtoBuf.ProtoMember(14)]
            public global::POGOProtos.Settings.Master.GymBattleSettings battle_settings { get; set; }

            [global::ProtoBuf.ProtoMember(15)]
            public global::POGOProtos.Settings.Master.EncounterSettings encounter_settings { get; set; }

            [global::ProtoBuf.ProtoMember(16)]
            public global::POGOProtos.Settings.Master.IapItemDisplay iap_item_display { get; set; }

            [global::ProtoBuf.ProtoMember(17)]
            public global::POGOProtos.Settings.Master.IapSettings iap_settings { get; set; }

            [global::ProtoBuf.ProtoMember(18)]
            public global::POGOProtos.Settings.Master.PokemonUpgradeSettings pokemon_upgrades { get; set; }

            [global::ProtoBuf.ProtoMember(19)]
            public global::POGOProtos.Settings.Master.EquippedBadgeSettings equipped_badges { get; set; }

            [global::ProtoBuf.ProtoMember(20)]
            public global::POGOProtos.Settings.Master.QuestSettings quest_settings { get; set; }

            [global::ProtoBuf.ProtoMember(21)]
            public global::POGOProtos.Settings.Master.AvatarCustomizationSettings avatar_customization { get; set; }

            [global::ProtoBuf.ProtoMember(22)]
            public global::POGOProtos.Settings.Master.FormSettings form_settings { get; set; }

            [global::ProtoBuf.ProtoMember(23)]
            public global::POGOProtos.Settings.Master.GenderSettings gender_settings { get; set; }

            [global::ProtoBuf.ProtoMember(24)]
            public global::POGOProtos.Settings.Master.GymBadgeGmtSettings gym_badge_settings { get; set; }

            [global::ProtoBuf.ProtoMember(25)]
            public global::POGOProtos.Settings.Master.WeatherAffinity weather_affinities { get; set; }

            [global::ProtoBuf.ProtoMember(26)]
            public global::POGOProtos.Settings.Master.WeatherBonus weather_bonus_settings { get; set; }

            [global::ProtoBuf.ProtoMember(27)]
            public global::POGOProtos.Settings.Master.PokemonScaleSetting pokemon_scale_settings { get; set; }

            [global::ProtoBuf.ProtoMember(28)]
            public global::POGOProtos.Settings.Master.IapItemCategoryDisplay iap_category_display { get; set; }

            [global::ProtoBuf.ProtoMember(30)]
            public global::POGOProtos.Settings.Master.OnboardingSettings onboarding_settings { get; set; }

        }

        [global::ProtoBuf.ProtoContract()]
        public enum Result
        {
            UNSET = 0,
            SUCCESS = 1,
            PAGE = 2,
            RETRY = 3,
        }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
