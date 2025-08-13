using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Enums;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Structures;
using Arrowgene.MonsterHunterOnline.Service.System.ItemSystem;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class CsSkillEffectSyncHandler : CsProtoStructureHandler<CSSkillEffectInfo>
{
    private static readonly ServiceLogger Logger =
        LogProvider.Logger<ServiceLogger>(typeof(CsSkillEffectSyncHandler));

    public override CS_CMD_ID Cmd => CS_CMD_ID.CS_CMD_SKILL_EFFECT_SYNC;

    public override void Handle(Client client, CSSkillEffectInfo req)
    {
        Inventory inventory = client.Inventory;
        if (inventory == null)
        {
            Logger.Error(client, "inventory null");
            return;
        }

        CsCsProtoStructurePacket<CSSkillEffectInfo> skillEffectInfo = CsProtoResponse.CSSkillEffectInfo;

        skillEffectInfo.Structure.EntityId = req.EntityId;
        skillEffectInfo.Structure.SkillID = req.SkillID;
        skillEffectInfo.Structure.SkillLevel = req.SkillLevel;
        skillEffectInfo.Structure.Type = req.Type;
        skillEffectInfo.Structure.EventName = req.EventName;

        client.SendCsProtoStructurePacket(skillEffectInfo);


        Logger.Debug($"EntityId:{req.EntityId} SkillID:{req.SkillID} SkillLevel:{req.SkillLevel} Type:{req.Type} EventName:{req.EventName}");

        //CsCsProtoStructurePacket<CSHealthSyncNtf> healthSync = CsProtoResponse.CSHealthSyncNtf;

        //healthSync.Structure.Health = 1f;
        //healthSync.Structure.HealthRecover = 1f;
        //healthSync.Structure.NetID = (int)client.Character.Id;

        //CsCsProtoStructurePacket<SceneObjAppearNtfList> sceneObjectList = CsProtoResponse.SceneObjAppearNtfList;
        //sceneObjectList.Structure.Appear.Add(new SceneObjAppearNtf(){
        //    Pose = new CSQuatT() { t = client.State.Position },
        //    Bone = 1,
        //    ParentGuid = 1234,
        //    Sync2CE = 1,
        //    SpawnType = 0,
        //    ClassName = "Bomb.SmallBucketBomb",
        //    EntityName = "Bomb.SmallBucketBomb"
        //});


        //client.SendCsProtoStructurePacket(healthSync);
        //client.SendCsProtoStructurePacket(sceneObjectList);
    }
}