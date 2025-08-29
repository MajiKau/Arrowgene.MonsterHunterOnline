using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Enums;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Structures;
using Arrowgene.MonsterHunterOnline.Service.System.ItemSystem;
using Microsoft.SqlServer.Server;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class GuideBookAutoFirstOpenReqHandler : CsProtoStructureHandler<CSGuideBookAutoFirstOpenReq>
{
    private static readonly ServiceLogger Logger =
        LogProvider.Logger<ServiceLogger>(typeof(GuideBookAutoFirstOpenReqHandler));

    public override CS_CMD_ID Cmd => CS_CMD_ID.C2S_CMD_GUIDE_BOOK_AUTO_FIRST_OPEN_REQ;

    public override void Handle(Client client, CSGuideBookAutoFirstOpenReq req)
    {
        CsCsProtoStructurePacket<SCGuideBookAutoFirstOpenRsp> rsp = CsProtoResponse.SCGuideBookAutoFirstOpenRsp;


        rsp.Structure.ErrCode = 0;

        client.SendCsProtoStructurePacket(rsp);

    }
}