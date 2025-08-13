using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Enums;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Structures;
using Arrowgene.MonsterHunterOnline.Service.System.ItemSystem;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class ChangeAmmoReqHandler : CsProtoStructureHandler<CSChangeAmmoReq>
{
    private static readonly ServiceLogger Logger =
        LogProvider.Logger<ServiceLogger>(typeof(ChangeAmmoReqHandler));

    public override CS_CMD_ID Cmd => CS_CMD_ID.CS_CMD_CHANGE_AMMO_REQ;

    public override void Handle(Client client, CSChangeAmmoReq req)
    {
        //CsCsProtoStructurePacket<CSChangeAmmoReq> reqq = CsProtoResponse.CSChangeAmmoReq;
        CsCsProtoStructurePacket<CSChangeAmmoRsp> rsp = CsProtoResponse.CSChangeAmmoRsp;

        rsp.Structure.NetID = (int)client.Character.Id;
        rsp.Structure.TypeID = req.TypeID;

        //reqq.Structure.TypeID = req.TypeID;


        //client.SendCsProtoStructurePacket(reqq);
        client.SendCsProtoStructurePacket(rsp);
    }
}