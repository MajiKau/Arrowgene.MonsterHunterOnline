using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Enums;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Structures;
using Arrowgene.MonsterHunterOnline.Service.System.ItemSystem;
using System.Collections.Generic;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class ReloadAmmoReqHandler : CsProtoStructureHandler<CSReloadAmmoReq>
{
    private static readonly ServiceLogger Logger =
        LogProvider.Logger<ServiceLogger>(typeof(ReloadAmmoReqHandler));

    public override CS_CMD_ID Cmd => CS_CMD_ID.CS_CMD_RELOAD_AMMO_REQ;

    public override void Handle(Client client, CSReloadAmmoReq req)
    {
        CsCsProtoStructurePacket<CSReloadAmmoRsp> rsp = CsProtoResponse.CSReloadAmmoRsp;

        //TODO: get ammo using
        //req.TypeID
        //req.Reserved;

        rsp.Structure.Ammos = new List<CSAmmoInfo>() {
            new CSAmmoInfo()
            {
                NetID = 1,
                TypeID = req.TypeID
            }
        };
        rsp.Structure.NetID = (int)client.Character.Id;

        client.SendCsProtoStructurePacket(rsp);
    }
}