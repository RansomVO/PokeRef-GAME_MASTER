// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: IapItemCategoryDisplay.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Settings.Master
{

    [global::ProtoBuf.ProtoContract()]
    public partial class IapItemCategoryDisplay : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(1)]
        public global::POGOProtos.Enums.HoloIapItemCategory category { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string name { get; set; } = "";

        [global::ProtoBuf.ProtoMember(3)]
        public bool hidden { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public int sort_order { get; set; }

        [global::ProtoBuf.ProtoMember(5)]
        public bool banner_enabled { get; set; }

        [global::ProtoBuf.ProtoMember(6)]
        [global::System.ComponentModel.DefaultValue("")]
        public string banner_title { get; set; } = "";

        [global::ProtoBuf.ProtoMember(7)]
        [global::System.ComponentModel.DefaultValue("")]
        public string image_url { get; set; } = "";

        [global::ProtoBuf.ProtoMember(8)]
        [global::System.ComponentModel.DefaultValue("")]
        public string description { get; set; } = "";

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
