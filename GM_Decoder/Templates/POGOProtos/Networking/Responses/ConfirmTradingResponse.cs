// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: ConfirmTradingResponse.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Networking.Responses
{

    [global::ProtoBuf.ProtoContract()]
    public partial class ConfirmTradingResponse : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public Result result { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public global::POGOProtos.Data.Trading.Trading trading { get; set; }

        [global::ProtoBuf.ProtoContract()]
        public enum Result
        {
            UNSET = 0,
            SUCCESS = 1,
            ERROR_UNKNOWN = 2,
            ERROR_FRIEND_NOT_FOUND = 3,
            ERROR_INVALID_PLAYER_ID = 4,
            ERROR_INVALID_STATE = 5,
            ERROR_STATE_HANDLER = 6,
            ERROR_INVALID_POKEMON = 7,
            ERROR_INSUFFICIENT_PAYMENT = 8,
            ERROR_NO_PLAYER_POKEMON = 9,
            ERROR_NO_FRIEND_POKEMON = 10,
            ERROR_PLAYER_ALREADY_CONFIRMED = 11,
            ERROR_TRANSACTION_LOG_NOT_MATCH = 12,
            ERROR_TRADING_EXPIRED = 13,
            ERROR_TRANSACTION = 14,
            ERROR_DAILY_LIMIT_REACHED = 15,
        }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006