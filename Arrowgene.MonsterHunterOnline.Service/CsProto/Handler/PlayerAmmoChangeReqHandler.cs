using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Enums;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Structures;
using Arrowgene.MonsterHunterOnline.Service.System.ItemSystem;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class PlayerAmmoChangeReqHandler : CsProtoStructureHandler<CSPlayerAmmoChangeReq>
{
    private static readonly ServiceLogger Logger =
        LogProvider.Logger<ServiceLogger>(typeof(PlayerAmmoChangeReqHandler));

    public override CS_CMD_ID Cmd => CS_CMD_ID.CS_CMD_PLAYER_AMMO_CHANGE_REQ;

    public override void Handle(Client client, CSPlayerAmmoChangeReq req)
    {
        CsCsProtoStructurePacket<CSPlayerAmmoChangeRsp> rsp = CsProtoResponse.CSPlayerAmmoChangeRsp;
        //CsCsProtoStructurePacket<CSPlayerAmmoChangeReq> reqq = CsProtoResponse.CSPlayerAmmoChangeReq;

        //TODO: get ammo using
        //req.NextAmmoID
        //req.SubAmmoID

        rsp.Structure.Reserve = 100;

        //reqq.Structure.NextAmmoID = req.NextAmmoID;
        //reqq.Structure.SubAmmoID = req.SubAmmoID;

        //CsCsProtoStructurePacket<CSChangeAmmoRsp> rsp2 = CsProtoResponse.CSChangeAmmoRsp;
        //rsp2.Structure.NetID = (int)client.Character.Id;
        //rsp2.Structure.TypeID = req.NextAmmoID;
        //client.SendCsProtoStructurePacket(rsp2);

        client.SendCsProtoStructurePacket(rsp);
        //client.SendCsProtoStructurePacket(reqq);
    }
}