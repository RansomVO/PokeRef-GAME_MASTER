// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: NewsSettings.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Settings
{

    [global::ProtoBuf.ProtoContract()]
    public partial class NewsSettings : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::System.Collections.Generic.List<News> news { get; } = new global::System.Collections.Generic.List<News>();

        [global::ProtoBuf.ProtoContract()]
        public partial class News : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1)]
            [global::System.ComponentModel.DefaultValue("")]
            public string news_bundle_id { get; set; } = "";

            [global::ProtoBuf.ProtoMember(2)]
            public global::System.Collections.Generic.List<string> exclusive_countries { get; } = new global::System.Collections.Generic.List<string>();

        }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
