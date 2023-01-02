using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Inventory : MonoBehaviour
{

    public static Inventory inventory;
    public List<InventoryItem> InventoryItems = new List<InventoryItem>();
    public TextMeshProUGUI IneventoryEmptyText;
    public GameObject ItemUI;
    public GameObject detailUI;
    public PickUpItemList PickupUI;

    void Awake()
    {
        inventory = this;
    }

    void Update()
    {
        inventoryEmpty();
    }

    InventoryItem serializedItem;

    //add item to inventory
    public void addItem(Item NewItem = null, GameObject ItemModel = null, InventoryItem ItemFromSave = null,bool isFromUnequip = false)
    {
        //decide if its item from save or a new item
        if (NewItem != null)
        {
            serializedItem = new InventoryItem();
            copyItem(NewItem, null, 0);
        }
        else
        {
            serializedItem = ItemFromSave;
        }

        //check if rarity is set
        Item itemRarity;
        if (NewItem == null)
        {
            itemRarity = null;
        }
        else
        {
            itemRarity = NewItem;
        }

        if (!alreadyOwned(serializedItem, itemRarity))
        {
            //If new item

            InventoryItems.Add(serializedItem);
            
            //Create UI representation
            GameObject ItemInventoryUI = Instantiate(ItemUI, transform.position, Quaternion.identity);
            ItemInventoryUI.transform.parent = gameObject.transform;
            ItemInventoryUI.GetComponent<ItemUI>().invItem = serializedItem;

            //Set UI info
            if (serializedItem.name.Length > 15)
            {
                ItemInventoryUI.GetComponent<ItemUI>().ItemNameText.fontSize = 15;
            }
            else
            {
                ItemInventoryUI.GetComponent<ItemUI>().ItemNameText.fontSize = 20;
            }
            
            
            ItemInventoryUI.GetComponent<ItemUI>().ItemNameText.text = serializedItem.name;
            ItemInventoryUI.GetComponent<ItemUI>().ItemRarityText.text = serializedItem.rarityLevel;
            ItemInventoryUI.GetComponent<ItemUI>().AmountText.text = serializedItem.amountOwned.ToString();

            //Check if is QuickAccess Item
            if (serializedItem.isQuickAccessItem)
            {
                QuickAccessItem.QuickAccessItemController.setQuickAccesItem(ItemInventoryUI.GetComponent<ItemUI>());
            }
            
            //Set image
            if (NewItem != null)
            {
                ItemInventoryUI.GetComponent<ItemUI>().itemImage.texture = (Texture)Resources.Load(getItemSpecsFromID(NewItem.ID).imagePath);
            }
            else
            {
                ItemInventoryUI.GetComponent<ItemUI>().itemImage.texture = (Texture)Resources.Load(getItemSpecsFromID(ItemFromSave.ID).imagePath);
            }
        }
        else
        {
            //Get UI representation to update amount of items owned
            ItemUI[] UIpanels;
            UIpanels = GetComponentsInChildren<ItemUI>();

            foreach (ItemUI UIpanel in UIpanels)
            {
                if (UIpanel.invItem.ID == serializedItem.ID && UIpanel.ItemRarityText.text == serializedItem.rarityLevel)
                {
                    int ownedAmount = int.Parse(UIpanel.AmountText.text);
                    ownedAmount++;
                    UIpanel.AmountText.text = ownedAmount.ToString();
                    break;
                }
                else
                {
                    continue;
                }
            }
        }


        if (ItemModel != null)
        {
            if (SaveLootController.SaveLootControl.LootOnTheMap.Contains(ItemModel))
            {
                SaveLootController.SaveLootControl.LootOnTheMap.Remove(ItemModel);
            }
            Destroy(ItemModel);
        }

        if (!isFromUnequip && ItemFromSave == null)
        {
            PickupUI.createPickUpItem(serializedItem);       
        }
    }


    //remove item from inventory
    public void removeItem(int itemID, string itemRarity, bool dropItem=true)
    {
        foreach (InventoryItem ownedItem in InventoryItems)
        {
            if (itemID == ownedItem.ID && itemRarity == ownedItem.rarityLevel)
            {
                ownedItem.amountOwned--;
                
                if (dropItem)
                {
                    DropItem.dropController.drop(ownedItem);    
                }
                
                if (ownedItem.amountOwned <= 0)
                {
                    InventoryItems.Remove(ownedItem);
                }
                break;
            }
            else
            {
                continue;
            }
        }
    }


    //if inventory is empy
    public void inventoryEmpty()
    {
        if (InventoryItems.Count == 0)
        {
            IneventoryEmptyText.enabled = true;
        }
        else
        {
            IneventoryEmptyText.enabled = false;
        }
    }


    //Check if item with specific ID and rarity is already in  inventory
    public bool alreadyOwned(InventoryItem item, Item itemRarity)
    {
        foreach (InventoryItem ownedItem in InventoryItems)
        {
            if (itemRarity != null)
            {
                if (item.ID == ownedItem.ID && itemRarity.rarityLevel == ownedItem.rarityLevel)
                {
                    ownedItem.amountOwned++;
                    return true;
                }
                else
                {
                    continue;
                }
            }
            else
            {
                if (item.ID == ownedItem.ID && item.rarityLevel == ownedItem.rarityLevel)
                {
                    ownedItem.amountOwned++;
                    return true;
                }
                else
                {
                    continue;
                }
            }
        }
        return false;
    }




    //--------------------------------------------------------------------------------------------------------------
    public void copyItem(Item item = null, InventoryItem invItem = null, int ID = 0)
    {
        //if got only item ID - transfer data from JSON to inventory item (
        if (ID != 0)
        {
            foreach (JSON_itemTemplate JSON_item_info in itemList.itemsController.itemsFromJSON.items)
            {
                if (ID == JSON_item_info.ID)
                {
                    if (DropItem.dropController.nextDrop == null)
                    {
                        Debug.LogError("Next drop is not defined!");
                        return;
                    }
                    
                    DropItem.dropController.nextDrop.GetComponent<Item>().ID = JSON_item_info.ID;
                    DropItem.dropController.nextDrop.GetComponent<Item>().name = JSON_item_info.name;
                    DropItem.dropController.nextDrop.GetComponent<Item>().rarityLevel = "Common";
                    DropItem.dropController.nextDrop.GetComponent<Item>().itemType = JSON_item_info.itemType;
                    DropItem.dropController.nextDrop.GetComponent<Item>().EquipType = JSON_item_info.EquipType;
                    DropItem.dropController.nextDrop.GetComponent<Item>().EquipSubType = JSON_item_info.EquipSubType;
                    DropItem.dropController.nextDrop.GetComponent<Item>().damage = JSON_item_info.damage;
                    DropItem.dropController.nextDrop.GetComponent<Item>().defence = JSON_item_info.defence;
                    DropItem.dropController.nextDrop.GetComponent<Item>().precision = JSON_item_info.precision;
                    DropItem.dropController.nextDrop.GetComponent<Item>().luck = JSON_item_info.luck;
                    DropItem.dropController.nextDrop.GetComponent<Item>().increaseMaxHP = JSON_item_info.increaseMaxHP;
                    DropItem.dropController.nextDrop.GetComponent<Item>().increaseMaxHunger = JSON_item_info.increaseMaxHunger;
                    DropItem.dropController.nextDrop.GetComponent<Item>().addHP = JSON_item_info.addHP;
                    DropItem.dropController.nextDrop.GetComponent<Item>().addHunger = JSON_item_info.addHunger;
                    DropItem.dropController.nextDrop.GetComponent<Item>().addEXP = JSON_item_info.addEXP;
                    DropItem.dropController.nextDrop.GetComponent<Item>().itemDesc = JSON_item_info.itemDesc;
                    DropItem.dropController.nextDrop.GetComponent<Item>().speedModification = JSON_item_info.speedModification;
                    return;
                }
                else
                {
                    continue;
                }

            }
        }
        //transfer data from item to inventory item (PICK UP)
        else if (item != null)
        {
            serializedItem.ID = item.ID;
            serializedItem.name = item.name;
            serializedItem.rarityLevel = item.rarityLevel;
            serializedItem.itemType = item.itemType;
            serializedItem.EquipType = item.EquipType;
            serializedItem.EquipSubType = item.EquipSubType;
            serializedItem.damage = item.damage;
            serializedItem.defence = item.defence;
            serializedItem.precision = item.precision;
            serializedItem.luck = item.luck;
            serializedItem.increaseMaxHP = item.increaseMaxHP;
            serializedItem.increaseMaxHunger = item.increaseMaxHunger;
            serializedItem.addHP = item.addHP;
            serializedItem.addHunger = item.addHunger;
            serializedItem.addEXP = item.addEXP;
            serializedItem.itemDesc = item.itemDesc;
            serializedItem.speedModification = item.speedModification;
            return;
        }
        //transfer data from inventory item to item (DROP it)
        else if (invItem != null)
        {
            DropItem.dropController.nextDrop.GetComponent<Item>().ID = invItem.ID;
            DropItem.dropController.nextDrop.GetComponent<Item>().name = invItem.name;
            DropItem.dropController.nextDrop.GetComponent<Item>().rarityLevel = invItem.rarityLevel;
            DropItem.dropController.nextDrop.GetComponent<Item>().itemType = invItem.itemType;
            DropItem.dropController.nextDrop.GetComponent<Item>().EquipType = invItem.EquipType;
            DropItem.dropController.nextDrop.GetComponent<Item>().EquipSubType = invItem.EquipSubType;
            DropItem.dropController.nextDrop.GetComponent<Item>().damage = invItem.damage;
            DropItem.dropController.nextDrop.GetComponent<Item>().defence = invItem.defence;
            DropItem.dropController.nextDrop.GetComponent<Item>().precision = invItem.precision;
            DropItem.dropController.nextDrop.GetComponent<Item>().luck = invItem.luck;
            DropItem.dropController.nextDrop.GetComponent<Item>().increaseMaxHP = invItem.increaseMaxHP;
            DropItem.dropController.nextDrop.GetComponent<Item>().increaseMaxHunger = invItem.increaseMaxHunger;
            DropItem.dropController.nextDrop.GetComponent<Item>().addHP = invItem.addHP;
            DropItem.dropController.nextDrop.GetComponent<Item>().addHunger = invItem.addHunger;
            DropItem.dropController.nextDrop.GetComponent<Item>().addEXP = invItem.addEXP;
            DropItem.dropController.nextDrop.GetComponent<Item>().itemDesc = invItem.itemDesc;
            DropItem.dropController.nextDrop.GetComponent<Item>().speedModification = invItem.speedModification;
            return;
        }
    }



    public JSON_itemTemplate getItemSpecsFromID(int ID)
    {
        foreach (JSON_itemTemplate JSON_item_info in itemList.itemsController.itemsFromJSON.items)
        {
            if (ID == JSON_item_info.ID)
            {
                return JSON_item_info;
            }
            else
            {
                continue;
            }
        }
        return null;
    }
    //--------------------------------------------------------------------------------------------------------------

}
