// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: DiskEncounterMessage.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Networking.Requests.Messages
{

    [global::ProtoBuf.ProtoContract()]
    public partial class DiskEncounterMessage : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public ulong encounter_id { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        [global::System.ComponentModel.DefaultValue("")]
        public string fort_id { get; set; } = "";

        [global::ProtoBuf.ProtoMember(3)]
        public double player_latitude { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public double player_longitude { get; set; }

        [global::ProtoBuf.ProtoMember(5)]
        public double gym_lat_degrees { get; set; }

        [global::ProtoBuf.ProtoMember(6)]
        public double gym_lng_degrees { get; set; }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
