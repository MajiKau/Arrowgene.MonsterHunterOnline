using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Enums;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Structures;
using Arrowgene.MonsterHunterOnline.Service.System.ItemSystem;
using Microsoft.SqlServer.Server;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class CSBattleDMGHandler : CsProtoStructureHandler<CSBattleDMG>
{
    private static readonly ServiceLogger Logger =
        LogProvider.Logger<ServiceLogger>(typeof(CSBattleDMGHandler));

    public override CS_CMD_ID Cmd => CS_CMD_ID.CS_CMD_BATTLE_DMG_VERIFY;

    public override void Handle(Client client, CSBattleDMG req)
    {
        CsCsProtoStructurePacket<CSBattleDMG> dmgInfo = CsProtoResponse.CSBattleDMG;
        //CsCsProtoStructurePacket<CSDMGResult> dmgInfo2 = CsProtoResponse.CSDMGResult;
        //CsCsProtoStructurePacket<CSBattlePVPDMG> dmgInfo3 = CsProtoResponse.CSBattlePVPDMG;
        //CsCsProtoStructurePacket<CSBattlePVPDMGNtf> dmgInfo4 = CsProtoResponse.CSBattlePVPDMGNtf;


        //dmgInfo2.Structure.DamageResult = req.damageMin;
        //dmgInfo2.Structure.Dir = req.dir;
        //dmgInfo2.Structure.Pos = req.pos;
        //dmgInfo2.Structure.Normal = req.normal;
        //dmgInfo2.Structure.AttackLogicEntityId = (int)req.shooterId;
        //dmgInfo2.Structure.HitLogicEntityId = (int)req.targetId;

        //client.SendCsProtoStructurePacket(dmgInfo2);



        dmgInfo.Structure.shooterId = req.shooterId;
        dmgInfo.Structure.targetId = req.targetId;
        dmgInfo.Structure.weaponId = req.weaponId;
        dmgInfo.Structure.projectileId = req.projectileId;
        dmgInfo.Structure.material = req.material;
        dmgInfo.Structure.type = req.type;
        dmgInfo.Structure.bulletType = req.bulletType;
        dmgInfo.Structure.damageMin = req.damageMin;
        dmgInfo.Structure.pierce = req.pierce;
        dmgInfo.Structure.partId = req.partId;
        dmgInfo.Structure.pos = req.pos;
        dmgInfo.Structure.lpos = req.lpos;
        dmgInfo.Structure.dir = req.dir;
        dmgInfo.Structure.normal = req.normal;
        dmgInfo.Structure.lnorm = req.lnorm;
        dmgInfo.Structure.attackDir = req.attackDir;
        dmgInfo.Structure.tScarDir = req.tScarDir;
        dmgInfo.Structure.tPos = req.tPos;
        dmgInfo.Structure.tUp = req.tUp;
        dmgInfo.Structure.tNormal = req.tNormal;
        dmgInfo.Structure.localnormangle = req.localnormangle;
        dmgInfo.Structure.shakeStrength = req.shakeStrength;
        dmgInfo.Structure.shakeDurationTime = req.shakeDurationTime;
        dmgInfo.Structure.shakeStillTime = req.shakeStillTime;
        dmgInfo.Structure.projectileClassId = req.projectileClassId;
        dmgInfo.Structure.weaponClassId = req.weaponClassId;
        dmgInfo.Structure.remote = req.remote;
        dmgInfo.Structure.damageLevel = req.damageLevel;
        dmgInfo.Structure.attackType = req.attackType;
        dmgInfo.Structure.hitType = req.hitType;
        dmgInfo.Structure.defenseResult = req.defenseResult;
        dmgInfo.Structure.HitIndex = req.HitIndex;
        dmgInfo.Structure.shooterSrvId = req.shooterSrvId;
        dmgInfo.Structure.targetSrvId = req.targetSrvId;
        dmgInfo.Structure.weaponSrvId = req.weaponSrvId;
        dmgInfo.Structure.projectileSrvId = req.projectileSrvId;
        dmgInfo.Structure.hashWeaponClass = req.hashWeaponClass;
        dmgInfo.Structure.hashFireMode = req.hashFireMode;
        dmgInfo.Structure.hashAttacker = req.hashAttacker;
        dmgInfo.Structure.hashMeleeParams = req.hashMeleeParams;
        dmgInfo.Structure.hashCurEvent = req.hashCurEvent;
        dmgInfo.Structure.skillResID = req.skillResID;
        dmgInfo.Structure.skillSeq = req.skillSeq;
        dmgInfo.Structure.curStamina = req.curStamina;

        client.SendCsProtoStructurePacket(dmgInfo);


        //dmgInfo3.Structure.SyncTime = 0;
        //dmgInfo3.Structure.Sequence = 0;
        //dmgInfo3.Structure.DamageResult = dmgInfo2.Structure;
        //dmgInfo3.Structure.HitInfo = dmgInfo.Structure;

        //client.SendCsProtoStructurePacket(dmgInfo3);


        //dmgInfo4.Structure.DamageResult = dmgInfo2.Structure;
        //client.SendCsProtoStructurePacket(dmgInfo4);

    }
}