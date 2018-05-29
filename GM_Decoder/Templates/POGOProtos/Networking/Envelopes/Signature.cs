// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: Signature.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Networking.Envelopes
{

    [global::ProtoBuf.ProtoContract()]
    public partial class Signature : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::System.Collections.Generic.List<UnknownMessage> field1 { get; } = new global::System.Collections.Generic.List<UnknownMessage>();

        [global::ProtoBuf.ProtoMember(2)]
        public ulong timestamp_since_start { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        [global::System.ComponentModel.DefaultValue("")]
        public string field3 { get; set; } = "";

        [global::ProtoBuf.ProtoMember(4)]
        public global::System.Collections.Generic.List<LocationFix> location_fix { get; } = new global::System.Collections.Generic.List<LocationFix>();

        [global::ProtoBuf.ProtoMember(5)]
        public global::System.Collections.Generic.List<AndroidGpsInfo> gps_info { get; } = new global::System.Collections.Generic.List<AndroidGpsInfo>();

        [global::ProtoBuf.ProtoMember(6)]
        public global::System.Collections.Generic.List<UnknownMessage> field6 { get; } = new global::System.Collections.Generic.List<UnknownMessage>();

        [global::ProtoBuf.ProtoMember(7)]
        public global::System.Collections.Generic.List<SensorInfo> sensor_info { get; } = new global::System.Collections.Generic.List<SensorInfo>();

        [global::ProtoBuf.ProtoMember(8)]
        public DeviceInfo device_info { get; set; }

        [global::ProtoBuf.ProtoMember(9)]
        public ActivityStatus activity_status { get; set; }

        [global::ProtoBuf.ProtoMember(10)]
        public int location_hash1 { get; set; }

        [global::ProtoBuf.ProtoMember(11)]
        public bool field11 { get; set; }

        [global::ProtoBuf.ProtoMember(12)]
        public bool field12 { get; set; }

        [global::ProtoBuf.ProtoMember(13)]
        public int field13 { get; set; }

        [global::ProtoBuf.ProtoMember(14)]
        public int field14 { get; set; }

        [global::ProtoBuf.ProtoMember(15)]
        [global::System.ComponentModel.DefaultValue("")]
        public string field15 { get; set; } = "";

        [global::ProtoBuf.ProtoMember(16)]
        public int field16 { get; set; }

        [global::ProtoBuf.ProtoMember(17)]
        [global::System.ComponentModel.DefaultValue("")]
        public string field17 { get; set; } = "";

        [global::ProtoBuf.ProtoMember(18)]
        [global::System.ComponentModel.DefaultValue("")]
        public string field18 { get; set; } = "";

        [global::ProtoBuf.ProtoMember(19)]
        public bool field19 { get; set; }

        [global::ProtoBuf.ProtoMember(20)]
        public int location_hash2 { get; set; }

        [global::ProtoBuf.ProtoMember(21)]
        public bool field21 { get; set; }

        [global::ProtoBuf.ProtoMember(22)]
        public byte[] session_hash { get; set; }

        [global::ProtoBuf.ProtoMember(23)]
        public ulong timestamp { get; set; }

        [global::ProtoBuf.ProtoMember(24, IsPacked = true)]
        public ulong[] request_hash { get; set; }

        [global::ProtoBuf.ProtoMember(25)]
        public long unknown25 { get; set; }

        [global::ProtoBuf.ProtoMember(27)]
        public int unknown27 { get; set; }

        [global::ProtoBuf.ProtoContract()]
        public partial class LocationFix : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1)]
            [global::System.ComponentModel.DefaultValue("")]
            public string provider { get; set; } = "";

            [global::ProtoBuf.ProtoMember(2)]
            public ulong timestamp_snapshot { get; set; }

            [global::ProtoBuf.ProtoMember(4)]
            public float altitude { get; set; }

            [global::ProtoBuf.ProtoMember(13)]
            public float latitude { get; set; }

            [global::ProtoBuf.ProtoMember(14)]
            public float longitude { get; set; }

            [global::ProtoBuf.ProtoMember(18)]
            public float speed { get; set; }

            [global::ProtoBuf.ProtoMember(20)]
            public float course { get; set; }

            [global::ProtoBuf.ProtoMember(21)]
            public float horizontal_accuracy { get; set; }

            [global::ProtoBuf.ProtoMember(22)]
            public float vertical_accuracy { get; set; }

            [global::ProtoBuf.ProtoMember(26)]
            public ulong provider_status { get; set; }

            [global::ProtoBuf.ProtoMember(27)]
            public uint floor { get; set; }

            [global::ProtoBuf.ProtoMember(28)]
            public ulong location_type { get; set; }

        }

        [global::ProtoBuf.ProtoContract()]
        public partial class AndroidGpsInfo : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1)]
            public ulong time_to_fix { get; set; }

            [global::ProtoBuf.ProtoMember(2, IsPacked = true)]
            public int[] satellites_prn { get; set; }

            [global::ProtoBuf.ProtoMember(3, IsPacked = true)]
            public float[] azimuth { get; set; }

            [global::ProtoBuf.ProtoMember(4, IsPacked = true)]
            public float[] elevation { get; set; }

            [global::ProtoBuf.ProtoMember(5, IsPacked = true)]
            public float[] snr { get; set; }

            [global::ProtoBuf.ProtoMember(6, IsPacked = true)]
            public bool[] has_almanac { get; set; }

            [global::ProtoBuf.ProtoMember(7, IsPacked = true)]
            public bool[] has_ephemeris { get; set; }

            [global::ProtoBuf.ProtoMember(8, IsPacked = true)]
            public bool[] used_in_fix { get; set; }

        }

        [global::ProtoBuf.ProtoContract()]
        public partial class SensorInfo : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1)]
            public ulong timestamp_snapshot { get; set; }

            [global::ProtoBuf.ProtoMember(3)]
            public double linear_acceleration_x { get; set; }

            [global::ProtoBuf.ProtoMember(4)]
            public double linear_acceleration_y { get; set; }

            [global::ProtoBuf.ProtoMember(5)]
            public double linear_acceleration_z { get; set; }

            [global::ProtoBuf.ProtoMember(6)]
            public double magnetic_field_x { get; set; }

            [global::ProtoBuf.ProtoMember(7)]
            public double magnetic_field_y { get; set; }

            [global::ProtoBuf.ProtoMember(8)]
            public double magnetic_field_z { get; set; }

            [global::ProtoBuf.ProtoMember(9)]
            public int magnetic_field_accuracy { get; set; }

            [global::ProtoBuf.ProtoMember(10)]
            public double attitude_pitch { get; set; }

            [global::ProtoBuf.ProtoMember(11)]
            public double attitude_yaw { get; set; }

            [global::ProtoBuf.ProtoMember(12)]
            public double attitude_roll { get; set; }

            [global::ProtoBuf.ProtoMember(13)]
            public double rotation_rate_x { get; set; }

            [global::ProtoBuf.ProtoMember(14)]
            public double rotation_rate_y { get; set; }

            [global::ProtoBuf.ProtoMember(15)]
            public double rotation_rate_z { get; set; }

            [global::ProtoBuf.ProtoMember(16)]
            public double gravity_x { get; set; }

            [global::ProtoBuf.ProtoMember(17)]
            public double gravity_y { get; set; }

            [global::ProtoBuf.ProtoMember(18)]
            public double gravity_z { get; set; }

            [global::ProtoBuf.ProtoMember(19)]
            public int status { get; set; }

        }

        [global::ProtoBuf.ProtoContract()]
        public partial class DeviceInfo : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1)]
            [global::System.ComponentModel.DefaultValue("")]
            public string device_id { get; set; } = "";

            [global::ProtoBuf.ProtoMember(2)]
            [global::System.ComponentModel.DefaultValue("")]
            public string android_board_name { get; set; } = "";

            [global::ProtoBuf.ProtoMember(3)]
            [global::System.ComponentModel.DefaultValue("")]
            public string android_bootloader { get; set; } = "";

            [global::ProtoBuf.ProtoMember(4)]
            [global::System.ComponentModel.DefaultValue("")]
            public string device_brand { get; set; } = "";

            [global::ProtoBuf.ProtoMember(5)]
            [global::System.ComponentModel.DefaultValue("")]
            public string device_model { get; set; } = "";

            [global::ProtoBuf.ProtoMember(6)]
            [global::System.ComponentModel.DefaultValue("")]
            public string device_model_identifier { get; set; } = "";

            [global::ProtoBuf.ProtoMember(7)]
            [global::System.ComponentModel.DefaultValue("")]
            public string device_model_boot { get; set; } = "";

            [global::ProtoBuf.ProtoMember(8)]
            [global::System.ComponentModel.DefaultValue("")]
            public string hardware_manufacturer { get; set; } = "";

            [global::ProtoBuf.ProtoMember(9)]
            [global::System.ComponentModel.DefaultValue("")]
            public string hardware_model { get; set; } = "";

            [global::ProtoBuf.ProtoMember(10)]
            [global::System.ComponentModel.DefaultValue("")]
            public string firmware_brand { get; set; } = "";

            [global::ProtoBuf.ProtoMember(12)]
            [global::System.ComponentModel.DefaultValue("")]
            public string firmware_tags { get; set; } = "";

            [global::ProtoBuf.ProtoMember(13)]
            [global::System.ComponentModel.DefaultValue("")]
            public string firmware_type { get; set; } = "";

            [global::ProtoBuf.ProtoMember(14)]
            [global::System.ComponentModel.DefaultValue("")]
            public string firmware_fingerprint { get; set; } = "";

        }

        [global::ProtoBuf.ProtoContract()]
        public partial class ActivityStatus : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1)]
            public ulong start_time_ms { get; set; }

            [global::ProtoBuf.ProtoMember(2)]
            public bool unknown_status { get; set; }

            [global::ProtoBuf.ProtoMember(3)]
            public bool walking { get; set; }

            [global::ProtoBuf.ProtoMember(4)]
            public bool running { get; set; }

            [global::ProtoBuf.ProtoMember(5)]
            public bool stationary { get; set; }

            [global::ProtoBuf.ProtoMember(6)]
            public bool automotive { get; set; }

            [global::ProtoBuf.ProtoMember(7)]
            public bool tilting { get; set; }

            [global::ProtoBuf.ProtoMember(8)]
            public bool cycling { get; set; }

            [global::ProtoBuf.ProtoMember(9)]
            public byte[] status { get; set; }

        }

    }

    [global::ProtoBuf.ProtoContract()]
    public partial class UnknownMessage : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
