// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: MarkTutorialCompleteMessage.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Networking.Requests.Messages
{

    [global::ProtoBuf.ProtoContract()]
    public partial class MarkTutorialCompleteMessage : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1, IsPacked = true)]
        public global::System.Collections.Generic.List<global::POGOProtos.Enums.TutorialState> tutorials_completed { get; } = new global::System.Collections.Generic.List<global::POGOProtos.Enums.TutorialState>();

        [global::ProtoBuf.ProtoMember(2)]
        public bool send_marketing_emails { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public bool send_push_notifications { get; set; }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
