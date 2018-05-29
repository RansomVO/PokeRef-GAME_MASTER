// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: GetInboxResponse.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Networking.Responses
{

    [global::ProtoBuf.ProtoContract()]
    public partial class GetInboxResponse : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public Result result { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public ClientInbox inbox { get; set; }

        [global::ProtoBuf.ProtoContract()]
        public partial class ClientInbox : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1)]
            public global::System.Collections.Generic.List<Notification> notifications { get; } = new global::System.Collections.Generic.List<Notification>();

            [global::ProtoBuf.ProtoMember(2)]
            public global::System.Collections.Generic.List<TemplateVariable> builtin_variables { get; } = new global::System.Collections.Generic.List<TemplateVariable>();

            [global::ProtoBuf.ProtoContract()]
            public partial class Notification : global::ProtoBuf.IExtensible
            {
                private global::ProtoBuf.IExtension __pbn__extensionData;
                global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                    => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

                [global::ProtoBuf.ProtoMember(1)]
                [global::System.ComponentModel.DefaultValue("")]
                public string notification_id { get; set; } = "";

                [global::ProtoBuf.ProtoMember(2)]
                [global::System.ComponentModel.DefaultValue("")]
                public string title_key { get; set; } = "";

                [global::ProtoBuf.ProtoMember(3)]
                [global::System.ComponentModel.DefaultValue("")]
                public string category { get; set; } = "";

                [global::ProtoBuf.ProtoMember(4)]
                public long create_timestamp_ms { get; set; }

                [global::ProtoBuf.ProtoMember(5)]
                public global::System.Collections.Generic.List<GetInboxResponse.ClientInbox.TemplateVariable> variables { get; } = new global::System.Collections.Generic.List<GetInboxResponse.ClientInbox.TemplateVariable>();

                [global::ProtoBuf.ProtoMember(6, IsPacked = true)]
                public global::System.Collections.Generic.List<Label> labels { get; } = new global::System.Collections.Generic.List<Label>();

                [global::ProtoBuf.ProtoMember(7)]
                public long expire_time_ms { get; set; }

                [global::ProtoBuf.ProtoContract()]
                public enum Label
                {
                    UNSET_LABEL = 0,
                    UNREAD = 1,
                    NEW = 2,
                    IMMEDIATE = 3,
                }

            }

            [global::ProtoBuf.ProtoContract()]
            public partial class TemplateVariable : global::ProtoBuf.IExtensible
            {
                private global::ProtoBuf.IExtension __pbn__extensionData;
                global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                    => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

                [global::ProtoBuf.ProtoMember(1)]
                [global::System.ComponentModel.DefaultValue("")]
                public string name { get; set; } = "";

                [global::ProtoBuf.ProtoMember(2)]
                [global::System.ComponentModel.DefaultValue("")]
                public string literal { get; set; } = "";

                [global::ProtoBuf.ProtoMember(3)]
                [global::System.ComponentModel.DefaultValue("")]
                public string key { get; set; } = "";

                [global::ProtoBuf.ProtoMember(4)]
                [global::System.ComponentModel.DefaultValue("")]
                public string lookup_table { get; set; } = "";

                [global::ProtoBuf.ProtoMember(5)]
                public byte[] byte_value { get; set; }

            }

        }

        [global::ProtoBuf.ProtoContract()]
        public enum Result
        {
            UNSET = 0,
            SUCCESS = 1,
            FAILURE = 2,
        }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
