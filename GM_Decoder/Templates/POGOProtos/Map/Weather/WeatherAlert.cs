// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: WeatherAlert.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Map.Weather
{

    [global::ProtoBuf.ProtoContract()]
    public partial class WeatherAlert : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(1)]
        public Severity severity { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public bool warn_weather { get; set; }

        [global::ProtoBuf.ProtoContract()]
        public enum Severity
        {
            NONE = 0,
            MODERATE = 1,
            EXTREME = 2,
        }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
