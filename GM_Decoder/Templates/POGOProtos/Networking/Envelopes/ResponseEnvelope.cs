// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: ResponseEnvelope.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Networking.Envelopes
{

    [global::ProtoBuf.ProtoContract()]
    public partial class ResponseEnvelope : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(1)]
        public StatusCode status_code { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public ulong request_id { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue("")]
        public string api_url { get; set; } = "";

        [global::ProtoBuf.ProtoMember(6)]
        public global::System.Collections.Generic.List<PlatformResponse> platform_returns { get; } = new global::System.Collections.Generic.List<PlatformResponse>();

        [global::ProtoBuf.ProtoMember(7)]
        public AuthTicket auth_ticket { get; set; }

        [global::ProtoBuf.ProtoMember(100)]
        public global::System.Collections.Generic.List<byte[]> returns { get; } = new global::System.Collections.Generic.List<byte[]>();

        [global::ProtoBuf.ProtoMember(101)]
        [global::System.ComponentModel.DefaultValue("")]
        public string error { get; set; } = "";

        [global::ProtoBuf.ProtoContract()]
        public partial class PlatformResponse : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
            [global::ProtoBuf.ProtoMember(1)]
            public global::POGOProtos.Networking.Platform.PlatformRequestType type { get; set; }

            [global::ProtoBuf.ProtoMember(2)]
            public byte[] response { get; set; }

        }

        [global::ProtoBuf.ProtoContract()]
        public enum StatusCode
        {
            UNKNOWN = 0,
            OK = 1,
            OK_RPC_URL_IN_RESPONSE = 2,
            BAD_REQUEST = 3,
            INVALID_REQUEST = 51,
            INVALID_PLATFORM_REQUEST = 52,
            REDIRECT = 53,
            SESSION_INVALIDATED = 100,
            INVALID_AUTH_TOKEN = 102,
        }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
