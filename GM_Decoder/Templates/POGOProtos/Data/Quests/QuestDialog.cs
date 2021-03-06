// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: QuestDialog.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Data.Quests
{

    [global::ProtoBuf.ProtoContract()]
    public partial class QuestDialog : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        [global::System.ComponentModel.DefaultValue("")]
        public string text { get; set; } = "";

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(2)]
        public CharacterExpression expression { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue("")]
        public string image_uri { get; set; } = "";

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(4)]
        public Character character { get; set; }

        [global::ProtoBuf.ProtoContract()]
        public enum CharacterExpression
        {
            EXPRESSION_UNSET = 0,
            HAPPY = 1,
            SYMPATHETIC = 2,
            ENERGETIC = 3,
            PUSHY = 4,
            IMPATIENT = 5,
            ADMIRATION = 6,
        }

        [global::ProtoBuf.ProtoContract()]
        public enum Character
        {
            CHARACTER_UNSET = 0,
            PROFESSOR_WILLOW = 1,
        }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
