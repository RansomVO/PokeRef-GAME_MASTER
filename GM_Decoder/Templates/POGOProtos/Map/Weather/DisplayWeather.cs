// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: DisplayWeather.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Map.Weather
{

    [global::ProtoBuf.ProtoContract()]
    public partial class DisplayWeather : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public DisplayLevel cloud_level { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public DisplayLevel rain_level { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public DisplayLevel wind_level { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public DisplayLevel snow_level { get; set; }

        [global::ProtoBuf.ProtoMember(5)]
        public DisplayLevel fog_level { get; set; }

        [global::ProtoBuf.ProtoMember(6)]
        public int wind_direction { get; set; }

        [global::ProtoBuf.ProtoMember(7)]
        public DisplayLevel special_effect_level { get; set; }

        [global::ProtoBuf.ProtoContract()]
        public enum DisplayLevel
        {
            LEVEL_0 = 0,
            LEVEL_1 = 1,
            LEVEL_2 = 2,
            LEVEL_3 = 3,
        }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
