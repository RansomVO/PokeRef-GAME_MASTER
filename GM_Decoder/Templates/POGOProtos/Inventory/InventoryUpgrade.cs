// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: InventoryUpgrade.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Inventory
{

    [global::ProtoBuf.ProtoContract()]
    public partial class InventoryUpgrade : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(1)]
        public global::POGOProtos.Inventory.Item.ItemId item_id { get; set; }

        [Newtonsoft.Json.JsonConverter(typeof(Newtonsoft.Json.Converters.StringEnumConverter))]
        [global::ProtoBuf.ProtoMember(2)]
        public InventoryUpgradeType upgrade_type { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public int additional_storage { get; set; }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
