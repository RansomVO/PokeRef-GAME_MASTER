// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: GpsSettings.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Settings
{

    [global::ProtoBuf.ProtoContract()]
    public partial class GpsSettings : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public float driving_warning_speed_meters_per_second { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public float driving_warning_cooldown_minutes { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public float driving_speed_sample_interval_seconds { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public int driving_speed_sample_count { get; set; }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
