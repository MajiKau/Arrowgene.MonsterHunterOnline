using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Enums;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Structures;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class PlayerExtRequestHandler : CsProtoStructureHandler<CSPlayerExtRequest>
{
    private static readonly ServiceLogger Logger =
        LogProvider.Logger<ServiceLogger>(typeof(PlayerExtRequestHandler));

    public override CS_CMD_ID Cmd => CS_CMD_ID.CS_CMD_PLAYER_EXT_REQUEST;

    public override void Handle(Client client, CSPlayerExtRequest req)
    {

        client.SendCsPacket(NewCsPacket.PlayerExtResult(new CSPlayerExtResult(req.Request, new CSActionPointData())));

        //client.SendCsPacket(NewCsPacket.PlayerExtNotify(new CSPlayerExtNotify());



    }
}