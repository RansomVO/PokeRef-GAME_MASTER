// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: UseItemCaptureResponse.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Networking.Responses
{

    [global::ProtoBuf.ProtoContract()]
    public partial class UseItemCaptureResponse : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public bool success { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public double item_capture_mult { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public double item_flee_mult { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public bool stop_movement { get; set; }

        [global::ProtoBuf.ProtoMember(5)]
        public bool stop_attack { get; set; }

        [global::ProtoBuf.ProtoMember(6)]
        public bool target_max { get; set; }

        [global::ProtoBuf.ProtoMember(7)]
        public bool target_slow { get; set; }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
