using System.Collections.Generic;
using System.Net.NetworkInformation;
using System.Security.Claims;
using System.Security.Policy;
using System.Xml.Linq;
using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Structures;

namespace Arrowgene.MonsterHunterOnline.Service.System.ChatSystem.Command.Commands
{
    /// <summary>
    /// Spawns enteties
    /// </summary>
    public class SpawnCommand : ChatCommand
    {
        private static readonly ServiceLogger Logger =
            LogProvider.Logger<ServiceLogger>(typeof(SpawnCommand));

        public override AccountType Account => AccountType.Admin;
        public override string Key => "spawn";
        public override string HelpText => "usage: `/spawn` - spawns entity";

        public override void Execute(string[] command, Client client, ChatMessage message, List<ChatMessage> responses)
        {
            
            
            
            CsCsProtoStructurePacket<EntityAppearNtfIdList> entityAppearNtfIdList = CsProtoResponse.EntityAppearNtfIdList;
            LogicEntityId leId = new LogicEntityId();
            leId.Type = LogicEntityType.MH_LETYPE_UNK_3;
            leId.Id = 39002;
            entityAppearNtfIdList.Structure.InitType = 0;
            entityAppearNtfIdList.Structure.LogicEntityId.Add(leId.Id);
            entityAppearNtfIdList.Structure.LogicEntityType.Add((uint)leId.Type);
            client.SendCsProtoStructurePacket(entityAppearNtfIdList);
            
            
            
            



            // SceneObjAppearNtf sceneObjAppearNtf = new SceneObjAppearNtf()
            // {
            //     NetId = client.Character.Id,
            //     EntityName = "Barrel Bomb S",
            //     ClassName = "Bomb.SmallBucketBomb",
            //     Pose = new CSQuatT() { t = client.State.Position },
            //     SubTypeId = 0,
            //     Sync2CE = 1,
            //     SpawnType = 1,
            //     Bone = 0,
            //     Holder = client.Character.Id,
            //     Owner = client.Character.Id,
            //     Faction = 0,
            //     RegionId = 0,
            //     EntGuid = 0,
            //     PropertityFile = "",
            //     MHSpawnType = 0,
            //     BTState = "",
            //     BBVars = new CSBBVarList(),
            //     ParentId = 0,
            //     ParentGuid = 0
            // };


            // CsCsProtoStructurePacket<SceneObjAppearNtfList> sceneObjAppearNtfList = CsProtoResponse.SceneObjAppearNtfList;
            // sceneObjAppearNtfList.Structure.Appear.Add(sceneObjAppearNtf);
            // client.SendCsProtoStructurePacket(sceneObjAppearNtfList);



            //CsCsProtoStructurePacket<MonsterAppearNtf> monsterAppearNtfId = CsProtoResponse.MonsterAppearNtf;
            //LogicEntityId leId = new LogicEntityId();
            //leId.Type = LogicEntityType.MH_LETYPE_MONSTER;
            //leId.Id = 50080;
            //monsterAppearNtfId.Structure.NetId = client.Character.Id;
            //monsterAppearNtfId.Structure.SpawnType = 0;
            //monsterAppearNtfId.Structure.MonsterInfoId = 0;
            //monsterAppearNtfId.Structure.EntGuid = 0;
            //monsterAppearNtfId.Structure.Name = "";
            //monsterAppearNtfId.Structure.Class = "";
            //monsterAppearNtfId.Structure.Pose = new CSQuatT();
            //monsterAppearNtfId.Structure.Faction = 0;
            //monsterAppearNtfId.Structure.BTState = "";
            //monsterAppearNtfId.Structure.BBVars = new CSBBVarList();
            //monsterAppearNtfId.Structure.Dead = 0;
            //monsterAppearNtfId.Structure.LcmState = new CSMonsterLocomotion();
            //monsterAppearNtfId.Structure.AttrInit = new List<CSAttrData>();
            //monsterAppearNtfId.Structure.ProjIds = new List<CSAmmoInfo>();
            //monsterAppearNtfId.Structure.Buff = new List<byte>();
            //monsterAppearNtfId.Structure.ParentGuid = 0;
            //monsterAppearNtfId.Structure.LastChildId = 0;
            //client.SendCsProtoStructurePacket(entityAppearNtfIdList);

        }
    }
}