using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Enums;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Structures;
using Arrowgene.MonsterHunterOnline.Service.System.ItemSystem;

namespace Arrowgene.MonsterHunterOnline.Service.CsProto.Handler;

public class ManufactureProduceReqHandler : CsProtoStructureHandler<CSManufactureProduceReq>
{
    private static readonly ServiceLogger Logger =
        LogProvider.Logger<ServiceLogger>(typeof(ManufactureProduceReqHandler));

    private ItemManager _itemManager;

    public ManufactureProduceReqHandler(ItemManager itemManager)
    {
        _itemManager = itemManager;
    }

    public override CS_CMD_ID Cmd => CS_CMD_ID.CS_CMD_MANUFACTURE_PRODUCE_REQ;

    public override void Handle(Client client, CSManufactureProduceReq req)
    {


        int itemId = _itemManager.GetManufacturableItemId(req.ManufactureId);

        _itemManager.AddItem(client, (uint)itemId);

        CsCsProtoStructurePacket<CSManufactureProduceRsp> rsp = CsProtoResponse.CSManufactureProduceRsp;

        rsp.Structure.BindFlag = req.BindFlag;
        rsp.Structure.ItemID = itemId;
        rsp.Structure.Ret = 0;

        Logger.Debug($"BindFlag:{req.BindFlag} ManufactureId:{req.ManufactureId} itemId:{itemId} ItemPlaceMent:{req.ItemPlaceMent}");

        client.SendCsProtoStructurePacket(rsp);
    }
}