using System.Collections.Generic;
using Arrowgene.MonsterHunterOnline.Service.CsProto;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Structures;
using Arrowgene.MonsterHunterOnline.Service.System.CharacterSystem;

namespace Arrowgene.MonsterHunterOnline.Service.System.ChatSystem.Command.Commands
{
    /// <summary>
    /// test random stuff
    /// </summary>
    public class TestCommand : ChatCommand
    {
        public override AccountType Account => AccountType.Admin;
        public override string Key => "test";
        public override string HelpText => "usage: `/test` - test random stuff";

        public override void Execute(string[] command, Client client, ChatMessage message, List<ChatMessage> responses)
        {
            //client.SendCsPacket(NewCsPacket.SelectHuntingBagRsp(new CSSelectHuntingBagRsp()));
           
        //   client.SendCsPacket(NewCsPacket.HunterStarInitNtf(new CSHunterStarInitNtf()
        //   {
        //       Entry = 0
        //   }));

            CsCsProtoStructurePacket<EnterInstanceCountDown>
                enterInstanceCountDown = CsProtoResponse.EnterInstanceCountDown;

            enterInstanceCountDown.Structure.Second = 5;
            enterInstanceCountDown.Structure.LevelId = client.State.MainInstanceLevelId;
            client.SendCsProtoStructurePacket(enterInstanceCountDown);
            

            // client.SendCsPacket(NewCsPacket.CSWeaponBreakEffect(new CSWeaponBreakEffect()
            // {
            //     ShowEffect = 0
            // }));


            // client.SendCsPacket(NewCsPacket.HealthSyncNtf(new CSHealthSyncNtf()
            // {
            //     Health = 200,
            //     HealthRecover = 10,
            //     NetID = (int)client.Character.Id
            // }));


            // client.SendCsPacket(NewCsPacket.AttrInfo(new CSAttrInitInfo()
            // {
            //     Attr = new List<byte>() { 16, 20 }
            // }));


            //CSAttrSync sync = new CSAttrSync(null);
            //sync.EntityID = (uint)client.Character.Id;
            //sync.AttrID = 16;
            //sync.BonusID = 1;
            //client.SendCsPacket(NewCsPacket.AttrSync(sync));

            //sync.EntityID = (uint)client.Character.Id;
            //sync.AttrID = 20;
            //sync.BonusID = 0;
            //client.SendCsPacket(NewCsPacket.AttrSyncList(new CSAttrSyncList()
            //{
            //    Attr = new List<CSAttrSync>() { sync }
            //}));

            //SyncAllAttr

        }
    }
}