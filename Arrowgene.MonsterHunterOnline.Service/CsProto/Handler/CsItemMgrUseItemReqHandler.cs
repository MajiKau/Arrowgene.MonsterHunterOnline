using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Enums;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Structures;
using Arrowgene.MonsterHunterOnline.Service.System.ItemSystem;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class CsItemMgrUseItemReqHandler : CsProtoStructureHandler<CSItemMgrUseItemReq>
{
    private static readonly ServiceLogger Logger =
        LogProvider.Logger<ServiceLogger>(typeof(CsItemMgrUseItemReqHandler));

    public override CS_CMD_ID Cmd => CS_CMD_ID.CS_CMD_ITEMMGR_USE_ITEM_REQ;

    public override void Handle(Client client, CSItemMgrUseItemReq req)
    {
        Inventory inventory = client.Inventory;
        if (inventory == null)
        {
            Logger.Error(client, "inventory null");
            return;
        }

        CsCsProtoStructurePacket<CSItemMgrUseItemReq> itemMgrUseItemNtf = CsProtoResponse.CSItemMgrUseItemReq;

        itemMgrUseItemNtf.Structure.ItemID = req.ItemID;
        itemMgrUseItemNtf.Structure.ItemColumn = req.ItemColumn;
        itemMgrUseItemNtf.Structure.ItemGrid = req.ItemGrid;
        itemMgrUseItemNtf.Structure.Param1 = req.Param1;
        itemMgrUseItemNtf.Structure.Param2 = req.Param2;
        itemMgrUseItemNtf.Structure.Param3 = req.Param3;
        itemMgrUseItemNtf.Structure.ExtParam = req.ExtParam;

        Logger.Debug($"ItemID:{req.ItemID} ItemColumn:{req.ItemColumn} ItemGrid:{req.ItemGrid} Param1:{req.Param1} Param2:{req.Param2} Param3:{req.Param3} ExtParam:{req.ExtParam} ");

        client.SendCsProtoStructurePacket(itemMgrUseItemNtf);
    }
}