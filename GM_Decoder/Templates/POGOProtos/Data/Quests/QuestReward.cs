// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: QuestReward.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Data.Quests
{

    [global::ProtoBuf.ProtoContract()]
    public partial class QuestReward : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(1)]
        public Type type { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public int exp { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public ItemReward item { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public int stardust { get; set; }

        [global::ProtoBuf.ProtoMember(5)]
        public PokemonCandyReward candy { get; set; }

        [global::ProtoBuf.ProtoMember(6)]
        [global::System.ComponentModel.DefaultValue("")]
        public string avatar_template_id { get; set; } = "";

        [global::ProtoBuf.ProtoMember(7)]
        [global::System.ComponentModel.DefaultValue("")]
        public string quest_template_id { get; set; } = "";

        [global::ProtoBuf.ProtoMember(8)]
        public PokemonEncounterReward pokemon_encounter { get; set; }

        [global::ProtoBuf.ProtoContract()]
        public partial class ItemReward : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
            [global::ProtoBuf.ProtoMember(1)]
            public global::POGOProtos.Inventory.Item.ItemId item { get; set; }

            [global::ProtoBuf.ProtoMember(2)]
            public int amount { get; set; }

        }

        [global::ProtoBuf.ProtoContract()]
        public partial class PokemonCandyReward : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
            [global::ProtoBuf.ProtoMember(1)]
            public global::POGOProtos.Enums.PokemonId pokemon_id { get; set; }

            [global::ProtoBuf.ProtoMember(2)]
            public int amount { get; set; }

        }

        [global::ProtoBuf.ProtoContract()]
        public partial class PokemonEncounterReward : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
            [global::ProtoBuf.ProtoMember(1)]
            public global::POGOProtos.Enums.PokemonId pokemon_id { get; set; }

            [global::ProtoBuf.ProtoMember(2)]
            public bool use_quest_pokemon_encounter_distribuition { get; set; }

            [global::ProtoBuf.ProtoMember(3)]
            public global::POGOProtos.Data.PokemonDisplay pokemon_display { get; set; }

            [global::ProtoBuf.ProtoMember(4)]
            public bool is_hidden_ditto { get; set; }

            [global::ProtoBuf.ProtoMember(5)]
            public global::POGOProtos.Data.PokemonDisplay ditto_display { get; set; }

        }

        [global::ProtoBuf.ProtoContract()]
        public enum Type
        {
            UNSET = 0,
            EXPERIENCE = 1,
            ITEM = 2,
            STARDUST = 3,
            CANDY = 4,
            AVATAR_CLOTHING = 5,
            QUEST = 6,
            POKEMON_ENCOUNTER = 7,
        }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
