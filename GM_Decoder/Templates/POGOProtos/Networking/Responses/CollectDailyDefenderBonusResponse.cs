// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: CollectDailyDefenderBonusResponse.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Networking.Responses
{

    [global::ProtoBuf.ProtoContract()]
    public partial class CollectDailyDefenderBonusResponse : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public Result result { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public global::System.Collections.Generic.List<string> currency_type { get; } = new global::System.Collections.Generic.List<string>();

        [global::ProtoBuf.ProtoMember(3, IsPacked = true)]
        public int[] currency_awarded { get; set; }

        [global::ProtoBuf.ProtoMember(4)]
        public int defenders_count { get; set; }

        [global::ProtoBuf.ProtoContract()]
        public enum Result
        {
            UNSET = 0,
            SUCCESS = 1,
            FAILURE = 2,
            TOO_SOON = 3,
            NO_DEFENDERS = 4,
        }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
