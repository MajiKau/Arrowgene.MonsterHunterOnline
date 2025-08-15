using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Enums;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Structures;
using Arrowgene.MonsterHunterOnline.Service.System.ItemSystem;
using System;
using System.Collections.Generic;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class BattleActorFifoSyncHandler : CsProtoStructureHandler<FifoSyncInfo>
{
    private static readonly ServiceLogger Logger =
        LogProvider.Logger<ServiceLogger>(typeof(BattleActorFifoSyncHandler));
    
    public override CS_CMD_ID Cmd => CS_CMD_ID.CS_CMD_BATTLE_ACTOR_FIFO_SYNC;


    public override void Handle(Client client, FifoSyncInfo req)
    {
        // i think this is request for syncing stuff
        // client sends -> CS_CMD_BATTLE_ACTOR_FIFO_SYNC
        // looks like the best answer should be -> CS_CMD_BATTLE_ACTOR_FIFO_SYNC_NTF
        // question is do we send it to the player back as well
        
        // i think this flow is for forced updates by server, where client acknowledged
        // if server responds -> CS_CMD_SERVER_ACTOR_FIFO_SYNC_NTF
        // then client sends -> CS_CMD_SERVER_ACTOR_FIFO_SYNC_ACK
        
        CsCsProtoStructurePacket<ServerSyncInfoNtf> serverSyncInfoNtf = CsProtoResponse.ServerSyncInfoNtf;
        serverSyncInfoNtf.Structure.EntityId = client.Character.Id;
        serverSyncInfoNtf.Structure.SyncInfo = req;
        //client.SendCsProtoStructurePacket(serverSyncInfoNtf);
        
        CsCsProtoStructurePacket<FifoSyncInfoNtf> fifoSyncInfoNtf = CsProtoResponse.FifoSyncInfoNtf;
        fifoSyncInfoNtf.Structure.EntityId = client.Character.Id;
        fifoSyncInfoNtf.Structure.SyncInfo = req;
        //client.SendCsProtoStructurePacket(fifoSyncInfoNtf);

        //Logger.Debug(req.Print());

        if (req.State2 == 2)
        {
            //req.Type = 2;
            //req.State1 = 0;
            req.State2 = 3;
            //req.Extension = 1;
            //client.SendCsProtoStructurePacket(serverSyncInfoNtf);

            CsCsProtoStructurePacket<CSProjectileLaunchNtf> projNtf = CsProtoResponse.CSProjectileLaunchNtf;
            CsCsProtoStructurePacket<CSProjectileLaunchNtfList> projListNtf = CsProtoResponse.CSProjectileLaunchNtfList;

            CSProjectileLaunchNtf proj1 = new CSProjectileLaunchNtf()
            {
                SyncTime = req.SyncTime,
                NetID = 95,
                LauncherID = (int)client.Character.Id,
                VehicleID = 0,
                TypeID = 1,
                pos = client.State.Position,
                dir = new CSVec3(1f, 0f, 0f),
                additiveVel = new CSVec3(),
                skillId = 0,
                itemId = 95,
                delay = 0.0f,
                speedScale = 1.0f,
                damageScale = 0.0f,
                overrideTrail = 0,
                acc = new CSVec3(0f, 0f, -20f),
                vel = new CSVec3(0f, 20f, 2f),
                radius = 0.0f,
                gravityChangeTime = 1.0f,
                additiveGravity = 10.0f,
                launchType = 1,
                additiveAccXYZMode = 0,
                additiveAccXYZ = new List<CSVec3>(),
                additiveAccTime = new List<float>()
            };

            CSProjectileLaunchNtf proj2 = new CSProjectileLaunchNtf()
            {
                SyncTime = req.SyncTime,
                NetID = 95,
                LauncherID = (int)client.Character.Id,
                VehicleID = 0,
                TypeID = 0,
                pos = client.State.Position,
                dir = new CSVec3(1f, 0f, 0f),
                additiveVel = new CSVec3(),
                skillId = 0,
                itemId = 95,
                delay = 1.0f,
                speedScale = 1.0f,
                damageScale = 0.0f,
                overrideTrail = 0,
                acc = new CSVec3(0f, 0f, -20f),
                vel = new CSVec3(0f, 20f, 2f),
                radius = 0.0f,
                gravityChangeTime = 1.0f,
                additiveGravity = 10.0f,
                launchType = 0,
                additiveAccXYZMode = 0,
                additiveAccXYZ = new List<CSVec3>(),
                additiveAccTime = new List<float>()
            };

            projListNtf.Structure.Appear.Add(proj1);
            projListNtf.Structure.Appear.Add(proj2);

            projNtf.Structure.SyncTime = req.SyncTime;
            projNtf.Structure.NetID = 95;
            projNtf.Structure.LauncherID = (int)client.Character.Id;
            projNtf.Structure.VehicleID = 0;
            projNtf.Structure.TypeID = 0;
            projNtf.Structure.pos = client.State.Position;
            projNtf.Structure.dir = new CSVec3(1f, 0f, 0f);
            projNtf.Structure.additiveVel = new CSVec3();
            projNtf.Structure.skillId = 0;
            projNtf.Structure.itemId = 95;
            projNtf.Structure.delay = 1.0f;
            projNtf.Structure.speedScale = 1.0f;
            projNtf.Structure.damageScale = 0.0f;
            projNtf.Structure.overrideTrail = 0;
            projNtf.Structure.acc = new CSVec3(0f, 0f, -20f);
            projNtf.Structure.vel = new CSVec3(0f, 20f, 2f);
            projNtf.Structure.radius = 0.0f;
            projNtf.Structure.gravityChangeTime = 1.0f;
            projNtf.Structure.additiveGravity = 10.0f;
            projNtf.Structure.launchType = 0;
            projNtf.Structure.additiveAccXYZMode = 0;
            projNtf.Structure.additiveAccXYZ = new List<CSVec3>();
            projNtf.Structure.additiveAccTime = new List<float>();

            //client.SendCsProtoStructurePacket(projNtf);
            //client.SendCsProtoStructurePacket(projListNtf);
        }

    }
}