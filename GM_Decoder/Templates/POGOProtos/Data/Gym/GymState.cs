// ***** This is for GAME_MASTER files. *****
// This file was generated by a tool; you should avoid making direct changes.
// Consider using 'partial classes' to extend these types
// Input: GymState.proto

#pragma warning disable CS1591, CS0612, CS3021, IDE1006
namespace POGOProtos.Data.Gym
{

    [global::ProtoBuf.ProtoContract()]
    public partial class GymState : global::ProtoBuf.IExtensible
    {
        private global::ProtoBuf.IExtension __pbn__extensionData;
        global::ProtoBuf.IExtension global::ProtoBuf.IExtensible.GetExtensionObject(bool createIfMissing)
            => global::ProtoBuf.Extensible.GetExtensionObject(ref __pbn__extensionData, createIfMissing);

        [global::ProtoBuf.ProtoMember(1)]
        public global::POGOProtos.Map.Fort.FortData fort_data { get; set; }

        [global::ProtoBuf.ProtoMember(2)]
        public global::System.Collections.Generic.List<GymMembership> memberships { get; } = new global::System.Collections.Generic.List<GymMembership>();

        [global::ProtoBuf.ProtoMember(3)]
        public bool deploy_lockout { get; set; }

    }

}

#pragma warning restore CS1591, CS0612, CS3021, IDE1006
