// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: DownloadRemoteConfigVersionMessage.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Networking.Requests.Messages
{

    [global::ProtoBuf.ProtoContract()]
    public partial class DownloadRemoteConfigVersionMessage : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(1)]
        public global::POGOProtos.Enums.Platform platform { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string device_manufacturer { get; set; } = "";

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue("")]
        public string device_model { get; set; } = "";

        [global::ProtoBuf.ProtoMember(4)]
        [global::System.ComponentModel.DefaultValue("")]
        public string locale { get; set; } = "";

        [global::ProtoBuf.ProtoMember(5)]
        public uint app_version { get; set; }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
