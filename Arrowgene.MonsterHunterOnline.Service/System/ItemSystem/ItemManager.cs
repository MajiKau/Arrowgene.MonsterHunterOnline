using Arrowgene.Logging;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Core;
using Arrowgene.MonsterHunterOnline.Service.CsProto.Structures;
using Arrowgene.MonsterHunterOnline.Service.Database;
using Arrowgene.MonsterHunterOnline.Service.System.CharacterSystem;
using Arrowgene.MonsterHunterOnline.Service.System.ClientAssetSystem;
using Arrowgene.MonsterHunterOnline.Service.System.ItemSystem.Constant;
using Microsoft.VisualBasic.FileIO;
using System.IO;
using System.Reflection.Emit;

namespace Arrowgene.MonsterHunterOnline.Service.System.ItemSystem;

public class ItemManager
{
    private static readonly ServiceLogger Logger = LogProvider.Logger<ServiceLogger>(typeof(ItemManager));

    private readonly IDatabase _database;
    private readonly AssetRepository _assets;

    public ItemManager(IDatabase database, AssetRepository assets)
    {
        _database = database;
        _assets = assets;
    }

    /// <summary>
    /// Adds specified item to client
    /// </summary>
    public bool AddItem(Client client, uint itemId)
    {
        Inventory inventory = client.Inventory;
        if (inventory == null)
        {
            Logger.Error(client, "AddItem::inventory null");
            return false;
        }

        Character character = client.Character;
        if (character == null)
        {
            Logger.Error(client, "AddItem::character null");
            return false;
        }

        Item item = MakeItem(character.Id, itemId, inventory);
        if (item == null)
        {
            Logger.Error(client, "AddItem::failed to make item");
            return false;
        }

        CsCsProtoStructurePacket<ItemMgrAddItemNtf> itemMgrAddItemNtf = CsProtoResponse.ItemMgrAddItemNtf;
        // TODO check if replay with error code for client works
        itemMgrAddItemNtf.Structure.Reason = 0;
        itemMgrAddItemNtf.Structure.ItemList.Add(new ItemData(item));
        client.SendCsProtoStructurePacket(itemMgrAddItemNtf);

        return true;
    }

    public int GetManufacturableItemId(int manufactureId)
    {
        string staticFolder = Path.Combine(Util.ExecutingDirectory(), "Files\\Static");
        string csvPath = Path.Combine(staticFolder, "ManufactureDataInfo.csv");


        using (TextFieldParser parser = new TextFieldParser(csvPath))
        {
            parser.TextFieldType = FieldType.Delimited;
            parser.SetDelimiters(",");

            // Skip the header line
            parser.ReadLine();
            while (!parser.EndOfData)
            {
                string[] fields = parser.ReadFields();
                string manId = fields[0];
                //bool isMatch = !string.IsNullOrEmpty(levelId) &&
                //    !string.IsNullOrEmpty(level_comp) &&
                //    (level_comp.Contains(levelId) || levelId.Contains(level_comp));
                bool isMatch = manufactureId == int.Parse(manId);
                if (isMatch)
                {
                    string itemId = fields[18];
                    return int.Parse(itemId);
                }
            }
        }
        return -1;
    }

    /// <summary>
    /// Brings a item into existence.
    /// </summary>
    public Item MakeItem(uint characterId, uint itemId, Inventory inventory)
    {
        if (!_assets.Items.ContainsKey(itemId))
        {
            Logger.Error("MakeItem::item data not found");

            foreach (var item1 in _assets.Items)
            {
                if(item1.Key > 7000 && item1.Key < 7010)
                Logger.Debug($"Key:{item1.Key} Name:{item1.Value.Name} ItemId:{item1.Value.ItemId}");
            }


            return null;
        }

        ItemInfo itemInfo = _assets.Items[itemId];

        if (!itemInfo.KeepCopy)
        {
            Logger.Error($"MakeItem:: Items with 'KeepCopy==false' not supported ({itemInfo})");
            return null;
        }

        Item item = new Item();
        switch (itemInfo.MainClass)
        {
            case ItemClass.Item:
            {
                // TODO check Quest type etc..
                item.PosColumn = ItemColumnType.Item;
                break;
            }
            case ItemClass.Equipment:
            {
                item.PosColumn = ItemColumnType.BoxEquip;
                break;
            }
            default:
            {
                return null;
            }
        }

        item.CharacterId = characterId;
        item.CreatedBy = $"{characterId}";
        item.Quantity = 1;
        item.ItemId = itemId;
        if (!inventory.Add(item))
        {
            return null;
        }

        return item;
    }
}