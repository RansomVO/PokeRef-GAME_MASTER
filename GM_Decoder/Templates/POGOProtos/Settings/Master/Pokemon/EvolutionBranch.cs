// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: EvolutionBranch.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Settings.Master.Pokemon
{

    [global::ProtoBuf.ProtoContract()]
    public partial class EvolutionBranch : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::POGOProtos.Enums.PokemonId evolution { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public global::POGOProtos.Inventory.Item.ItemId evolution_item_requirement { get; set; }

        [global::ProtoBuf.ProtoMember(3)]
        public int candy_cost { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public float km_buddy_distance_requirement { get; set; }

        [global::ProtoBuf.ProtoMember(5)]
        public global::POGOProtos.Enums.Form form { get; set; }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
