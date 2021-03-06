// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: AddFortModifierResponse.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Networking.Responses
{

    [global::ProtoBuf.ProtoContract()]
    public partial class AddFortModifierResponse : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(1)]
        public Result result { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public FortDetailsResponse fort_details { get; set; }

        [global::ProtoBuf.ProtoContract()]
        public enum Result
        {
            NO_RESULT_SET = 0,
            SUCCESS = 1,
            FORT_ALREADY_HAS_MODIFIER = 2,
            TOO_FAR_AWAY = 3,
            NO_ITEM_IN_INVENTORY = 4,
            POI_INACCESSIBLE = 5,
        }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
