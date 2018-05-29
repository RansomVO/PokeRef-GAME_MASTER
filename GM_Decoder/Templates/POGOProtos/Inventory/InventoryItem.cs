// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: InventoryItem.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Inventory
{

    [global::ProtoBuf.ProtoContract()]
    public partial class InventoryItem : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public long modified_timestamp_ms { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public DeletedItem deleted_item { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public InventoryItemData inventory_item_data { get; set; }

        [global::ProtoBuf.ProtoContract()]
        public partial class DeletedItem : global::ProtoBuf.IExtensible
        {
            private global::ProtoBuf.IExtension __pbn__extensionData;
            global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
                => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

            [global::ProtoBuf.ProtoMember(1, DataFormat = global::ProtoBuf.DataFormat.FixedSize)]
            public ulong pokemon_id { get; set; }

        }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
