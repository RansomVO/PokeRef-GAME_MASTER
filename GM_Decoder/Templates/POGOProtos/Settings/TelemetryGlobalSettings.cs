// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: TelemetryGlobalSettings.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Settings
{

    [global::ProtoBuf.ProtoContract()]
    public partial class TelemetryGlobalSettings : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public bool enabled { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public double session_sampling_fraction { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public int max_buffer_size_kb { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public int batch_size { get; set; }

        [global::ProtoBuf.ProtoMember(5)]
        public long update_interval_ms { get; set; }

        [global::ProtoBuf.ProtoMember(6)]
        public long frame_rate_sample_interval_ms { get; set; }

        [global::ProtoBuf.ProtoMember(7)]
        public long frame_rate_sample_period_ms { get; set; }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
