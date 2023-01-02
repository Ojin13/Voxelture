using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveLootController : MonoBehaviour
{

    public static SaveLootController SaveLootControl;
    public List<GameObject> LootOnTheMap;
    public GameObject[] dropModels;
    public GameObject lootBag;

    void Awake()
    {
        SaveLootControl = this;
    }

    public InventoryItem convert__ItemToSerializable(Item item)
    {
        InventoryItem data = new InventoryItem();
        data.lootPosition[0] = item.gameObject.transform.position.x;
        data.lootPosition[1] = item.gameObject.transform.position.y;
        data.lootPosition[2] = item.gameObject.transform.position.z;
        data.ID = item.ID;
        data.name = item.name;
        data.rarityLevel = item.rarityLevel;
        data.itemType = item.itemType;
        data.EquipType = item.EquipType;
        data.damage = item.damage;
        data.defence = item.defence;
        data.precision = item.precision;
        data.increaseMaxHunger = item.increaseMaxHunger;
        data.increaseMaxHP = item.increaseMaxHP;
        data.luck = item.luck;
        data.addHP = item.addHP;
        data.addHunger = item.addHunger;
        data.addEXP = item.addEXP;
        data.itemDesc = item.itemDesc;
        data.speedModification = item.speedModification;
        return data;
    }



    private GameObject loot;
    public void LoadLootFromSave(InventoryItem data)
    {
        loot = chooseModel(data);

        if (loot == null)
        {
            Debug.LogError("Loot null couldnt be loaded!");    
        }
        
        loot = Instantiate(loot, new Vector3(data.lootPosition[0], data.lootPosition[1], data.lootPosition[2]), Quaternion.identity);
        
        loot.GetComponent<Item>().ID = data.ID;
        loot.GetComponent<Item>().name = data.name;
        loot.GetComponent<Item>().image = (Texture)Resources.Load(Inventory.inventory.getItemSpecsFromID(data.ID).imagePath);
        loot.GetComponent<Item>().itemObject = loot;
        loot.GetComponent<Item>().rarityLevel = data.rarityLevel;
        loot.GetComponent<Item>().itemType = data.itemType;
        loot.GetComponent<Item>().EquipType = data.EquipType;
        loot.GetComponent<Item>().damage = data.damage;
        loot.GetComponent<Item>().defence = data.defence;
        loot.GetComponent<Item>().precision = data.precision;
        loot.GetComponent<Item>().increaseMaxHP = data.increaseMaxHP;
        loot.GetComponent<Item>().increaseMaxHunger = data.increaseMaxHunger;
        loot.GetComponent<Item>().luck = data.luck;
        loot.GetComponent<Item>().addHP = data.addHP;
        loot.GetComponent<Item>().addHunger = data.addHunger;
        loot.GetComponent<Item>().addEXP = data.addEXP;
        loot.GetComponent<Item>().itemDesc = data.itemDesc;
        loot.GetComponent<Item>().speedModification = data.speedModification;
    }

    public GameObject chooseModel(InventoryItem item = null, int ID = 0)
    {
        if (item != null)
        {
            for (int i = 0; i < dropModels.Length; i++)
            {
                if (item.ID == dropModels[i].GetComponent<Item>().ID)
                {
                    return dropModels[i];
                }
            }
        }

        if (ID != 0)
        {
            for (int i = 0; i < dropModels.Length; i++)
            {
                if (dropModels[i].GetComponent<Item>().ID == ID)
                {
                    return dropModels[i];
                }
            }
        }
        
        return lootBag;
    }
}
