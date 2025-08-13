using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Enums;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Structures;
using Arrowgene.MonsterHunterOnline.Service.System.ItemSystem;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class CsSpeakExecHandler : CsProtoStructureHandler<C2SSpeakExec>
{
    private static readonly ServiceLogger Logger =
        LogProvider.Logger<ServiceLogger>(typeof(ItemMgrMoveItemReqHandler));

    public override CS_CMD_ID Cmd => CS_CMD_ID.C2S_CMD_SPEAK_EXEC;

    public override void Handle(Client client, C2SSpeakExec req)
    {
        CsCsProtoStructurePacket<C2SSpeakExec> speakExec = CsProtoResponse.C2SSpeakExec;

        speakExec.Structure.Exec = req.Exec;

        client.SendCsProtoStructurePacket(speakExec);

        CsCsProtoStructurePacket<S2CSpeakExec> speakExec2 = CsProtoResponse.S2CSpeakExec;

        speakExec2.Structure.Exec = req.Exec;
        speakExec2.Structure.netId = (int)client.Character.Id;

        client.SendCsProtoStructurePacket(speakExec2);


    }
}