// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: SetFavoritePokemonResponse.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Networking.Responses
{

    [global::ProtoBuf.ProtoContract()]
    public partial class SetFavoritePokemonResponse : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public Result result { get; set; }

        [global::ProtoBuf.ProtoContract()]
        public enum Result
        {
            UNSET = 0,
            SUCCESS = 1,
            ERROR_POKEMON_NOT_FOUND = 2,
            ERROR_POKEMON_IS_EGG = 3,
        }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
