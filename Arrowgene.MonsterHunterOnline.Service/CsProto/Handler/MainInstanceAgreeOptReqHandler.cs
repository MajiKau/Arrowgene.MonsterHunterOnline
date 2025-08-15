using System.Globalization;
using System.IO;
using System.Threading;
using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Constant;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Enums;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Structures;
using Arrowgene.MonsterHunterOnline.Service.System;
using Microsoft.VisualBasic.FileIO;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class MainInstanceAgreeOptReqHandler : CsProtoStructureHandler<MainInstanceAgreeOptReq>
{
    private static readonly ServiceLogger Logger =
        LogProvider.Logger<ServiceLogger>(typeof(MainInstanceAgreeOptReqHandler));

    public override CS_CMD_ID Cmd => CS_CMD_ID.CS_CMD_MAIN_INSTANCE_AGREE_OPT_REQ;

    private readonly Setting _setting;
    
    public MainInstanceAgreeOptReqHandler(Setting setting)
    {
        _setting = setting;
    }

    public override void Handle(Client client, MainInstanceAgreeOptReq req)
    {
        CsCsProtoStructurePacket<MainInstanceAgreeOptRsp> rsp = CsProtoResponse.MainInstanceAgreeOptRsp;
        rsp.Structure.Reason = 0;
        rsp.Structure.LevelId = client.State.MainInstanceLevelId;
        rsp.Structure.NetId = (int)client.Character.Id;
        rsp.Structure.Agree = req.Agree;
        rsp.Structure.RoleName = client.Character.Name;
        client.SendCsProtoStructurePacket(rsp);

        CsCsProtoStructurePacket<EnterInstanceCountDown>
            enterInstanceCountDown = CsProtoResponse.EnterInstanceCountDown;

        enterInstanceCountDown.Structure.Second = 5;
        enterInstanceCountDown.Structure.LevelId = client.State.MainInstanceLevelId;
        client.SendCsProtoStructurePacket(enterInstanceCountDown);

        // TODO dirty hack
        Thread.Sleep(5000);


        //client.SendCsPacket(NewCsPacket.MainInstanceClose(new CSMainInstanceClose()
        //    {
        //        LevelID = client.State.levelId,
        //        RoomID = 0,
        //        Reason = 0,
        //        TriggerNetID = client.Character.Id,
        //        RoleName = client.Character.Name,
        //    }
        //));


        CsCsProtoStructurePacket<InstanceInitInfo>
            instanceInitInfo = CsProtoResponse.InstanceInitInfo;

        instanceInitInfo.Structure.BattleGroundId = 0;
        instanceInitInfo.Structure.LevelId = client.State.MainInstanceLevelId;
        instanceInitInfo.Structure.CreateMaxPlayerCount = 4;
        instanceInitInfo.Structure.GameMode = GameMode.Casual;
        instanceInitInfo.Structure.TimeType = TimeType.Noon;
        instanceInitInfo.Structure.WeatherType = WeatherType.Sunny;
        instanceInitInfo.Structure.Time = 1;
        instanceInitInfo.Structure.LevelRandSeed = 1;
        instanceInitInfo.Structure.WarningFlag = 0;
        instanceInitInfo.Structure.CreatePlayerMaxLv = 99;


        // client.SendCsProtoStructurePacket(instanceInitInfo);


        CsCsProtoStructurePacket<TownInstanceVerifyRsp> townServerInitNtf = CsProtoResponse.TownServerInitNtf;
        townServerInitNtf.Structure.ErrNo = 0;
        townServerInitNtf.Structure.LineId = 0;
        townServerInitNtf.Structure.LevelEnterType = 0;
        townServerInitNtf.Structure.InstanceInitInfo = instanceInitInfo.Structure;
        // client.SendCsProtoStructurePacket(townServerInitNtf);
        //client.State.prevLevelId = client.State.levelId;
        //client.State.levelId = client.State.MainInstanceLevelId;
        client.State.prevLevelId = client.State.MainInstanceLevelId;
        client.State.levelId = client.State.InitLevelId;


        CsCsProtoStructurePacket<EnterInstanceRsp> enterInstanceRsp = CsProtoResponse.EnterInstanceRsp;
        enterInstanceRsp.Structure.ErrNo = 0;
        enterInstanceRsp.Structure.RoleId = (int)client.Character.Id;
        enterInstanceRsp.Structure.InstanceId = 1;
        enterInstanceRsp.Structure.BattleSvr = $"127.0.0.1:{_setting.BattleServerPort}";
        //enterInstanceRsp.Structure.ServiceId = 1;
        enterInstanceRsp.Structure.ServiceId = client.State.MainInstanceLevelId; //Hack to pass the level ID to InstanceVerifyReqHandler
        enterInstanceRsp.Structure.Key = "BtlSvr01";
        enterInstanceRsp.Structure.InstanceInfo = instanceInitInfo.Structure;
        enterInstanceRsp.Structure.SameBS = 1;
        enterInstanceRsp.Structure.CrossRegion = 0;
        enterInstanceRsp.Structure.MatchRoom = 0;


        //string staticFolder = Path.Combine(Util.ExecutingDirectory(), "Files\\Static");
        //string csvSpawnPointsPath = Path.Combine(staticFolder, "SpawnPoints.csv");
        //int level = client.State.MainInstanceLevelId;
        //using (TextFieldParser parser = new TextFieldParser(csvSpawnPointsPath))
        //{
        //    string level_comp = level.ToString();
        //    parser.TextFieldType = FieldType.Delimited;
        //    parser.SetDelimiters(",");

        //    // Skip the header line
        //    parser.ReadLine();
        //    while (!parser.EndOfData)
        //    {
        //        string[] fields = parser.ReadFields();
        //        string levelId = fields[0];
        //        bool isMatch = (level_comp.Contains(levelId) || levelId.Contains(level_comp));
        //        if (isMatch)
        //        {
        //            string filename = fields[1];
        //            string areaName = fields[2];
        //            string pos = fields[3];
        //            string rotate = fields[4];

        //            //Logger.Info($"warp point match found: ({levelId})({filename})({areaName})({name})");
        //            // Process the position (Pos) and rotation (Rotate) values
        //            string[] posValues = pos.Split(',');
        //            string[] rotateValues = rotate.Split(',');

        //            float posX = float.Parse(posValues[0], CultureInfo.InvariantCulture);
        //            float posY = float.Parse(posValues[1], CultureInfo.InvariantCulture);
        //            float posZ = float.Parse(posValues[2], CultureInfo.InvariantCulture);

        //            float rotateX = float.Parse(rotateValues[0], CultureInfo.InvariantCulture);
        //            float rotateY = float.Parse(rotateValues[1], CultureInfo.InvariantCulture);
        //            float rotateZ = float.Parse(rotateValues[2], CultureInfo.InvariantCulture);
        //            float rotateW = float.Parse(rotateValues[3], CultureInfo.InvariantCulture);

        //            CSQuatT TargetPosition = new CSQuatT()
        //            {

        //                q = new CSQuat()
        //                {
        //                    v = new CSVec3() { x = rotateX, y = rotateY, z = rotateZ },
        //                    w = rotateW
        //                },
        //                t = new CSVec3() { x = (float)posX, y = (float)posY, z = (float)posZ }
        //            };

        //            CsCsProtoStructurePacket<PlayerTeleport> PlayerTeleport = CsProtoResponse.PlayerTeleport;
        //            PlayerTeleport.Structure.SyncTime = 1;
        //            //One day netobjid would not be the character id ?
        //            PlayerTeleport.Structure.NetObjId = client.Character.Id;
        //            PlayerTeleport.Structure.Region = level;
        //            PlayerTeleport.Structure.TargetPos = TargetPosition;
        //            PlayerTeleport.Structure.ParentGuid = 1;
        //            PlayerTeleport.Structure.InitState = 1;
        //            client.SendCsProtoStructurePacket(PlayerTeleport);
        //            Logger.Debug($"Warp point found at {posX} {posY} {posZ} for level {level}");
        //            Thread.Sleep(1000);
        //            break;
        //        }
        //    }
        //}


        client.SendCsProtoStructurePacket(enterInstanceRsp);

    
    }
}