using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    public int ID;
    public string name = "Uknown item";
    public Texture image;
    public GameObject itemObject;
    public string rarityLevel;
    public string itemType;
    public string EquipType;
    public string EquipSubType;
    public int damage;
    public int defence;
    public int precision;
    public int increaseMaxHP;
    public int increaseMaxHunger;
    public int luck;
    public int addHP;
    public int addHunger;
    public int addEXP;
    public string itemDesc;
    public float speedModification;
    public bool notFromSave;

    // Start is called before the first frame update
    void Awake()
    {
        itemObject = this.gameObject;
    }

    void Start()
    {

        if (SaveLootController.SaveLootControl.LootOnTheMap.Contains(gameObject) == false && !gameObject.CompareTag("Enemy"))
        {
            SaveLootController.SaveLootControl.LootOnTheMap.Add(gameObject);
        }
        
        if (notFromSave)
        {
            Invoke("checkForDestroyConditions",0.01f);
            initializeItem();
        }
    }


    public void initializeItem()
    {
        if (Inventory.inventory == null)
        {
            Debug.LogError("INVENTORY ISNT INITIALIZED!");
            return;
        }

        if (ID == 0)
        {
            Debug.LogError("Item with position "+gameObject.transform.position+" has ID 0!");
            Destroy(gameObject);
            return;
        }
        
        itemType = Inventory.inventory.getItemSpecsFromID(ID).itemType;
        EquipType = Inventory.inventory.getItemSpecsFromID(ID).EquipType;
        EquipSubType = Inventory.inventory.getItemSpecsFromID(ID).EquipSubType;
        
        float rarityStatBooster = 1;

        if (string.IsNullOrEmpty(rarityLevel))
        {
            int rarityChance = Random.Range(0, 100);

            if (rarityChance >= 0 && rarityChance <= 50)
            {
                rarityLevel = "Common";
                rarityStatBooster = 1;
            }
            else if (rarityChance > 50 && rarityChance <= 70)
            {
                rarityLevel = "Rare";
                rarityStatBooster = 1.3f;
            }
            else if (rarityChance > 70 && rarityChance <= 85)
            {
                rarityLevel = "Antic";
                rarityStatBooster = 1.8f;
            }
            else if (rarityChance > 85 && rarityChance <= 95)
            {
                rarityLevel = "Legendary";
                rarityStatBooster = 2.5f;
            }
            else if (rarityChance > 95 && rarityChance <= 100)
            {
                rarityLevel = "God Like";
                rarityStatBooster = 5;
            }
        }
        
        
        if (damage == 0 || damage == null)
        {
            damage = (int)(Inventory.inventory.getItemSpecsFromID(ID).damage*rarityStatBooster);
        }
        
        if (defence == 0 || defence == null)
        {
            defence = (int)(Inventory.inventory.getItemSpecsFromID(ID).defence*rarityStatBooster);
        }
        
        if (precision == 0 || precision == null)
        {
            precision = (int)(Inventory.inventory.getItemSpecsFromID(ID).precision*rarityStatBooster);
        }
        
        if (luck == 0 || luck == null)
        {
            luck = (int)(Inventory.inventory.getItemSpecsFromID(ID).luck*rarityStatBooster);
        }
        
        if (increaseMaxHP == 0 || increaseMaxHP == null)
        {
            increaseMaxHP = (int)(Inventory.inventory.getItemSpecsFromID(ID).increaseMaxHP*rarityStatBooster);
        }
        
        if (increaseMaxHunger == 0 || increaseMaxHunger == null)
        {
            increaseMaxHunger = (int)(Inventory.inventory.getItemSpecsFromID(ID).increaseMaxHunger*rarityStatBooster);
        }

        if (speedModification == 0 || speedModification == null)
        {
            speedModification = (Inventory.inventory.getItemSpecsFromID(ID).speedModification);
        }
        
        
        
        
        
        
        
        //------------POTIONS------------
        if (addHP == 0 || addHP == null)
        {
            if (rarityLevel == "Common")
            {
                addHP = (int)(Inventory.inventory.getItemSpecsFromID(ID).addHP*rarityStatBooster);
            }
            else if (rarityLevel == "Rare")
            {
                addHP = (int)(Inventory.inventory.getItemSpecsFromID(ID).addHP*rarityStatBooster*5);
            }
            else if (rarityLevel == "Antic")
            {
                addHP = (int)(Inventory.inventory.getItemSpecsFromID(ID).addHP*rarityStatBooster*10);
            }
            else if (rarityLevel == "Legendary")
            {
                addHP = (int)(Inventory.inventory.getItemSpecsFromID(ID).addHP*rarityStatBooster*25);
            }
            else if(rarityLevel == "GodLike")
            {
                addHP = (int)(Inventory.inventory.getItemSpecsFromID(ID).addHP*rarityStatBooster*50);
            }
        }
        
        if (addHunger == 0 || addHunger == null)
        {
            if (rarityLevel == "Common")
            {
                addHunger = (int)(Inventory.inventory.getItemSpecsFromID(ID).addHunger*rarityStatBooster);
            }
            else if (rarityLevel == "Rare")
            {
                addHunger = (int)(Inventory.inventory.getItemSpecsFromID(ID).addHunger*rarityStatBooster*5);
            }
            else if (rarityLevel == "Antic")
            {
                addHunger = (int)(Inventory.inventory.getItemSpecsFromID(ID).addHunger*rarityStatBooster*10);
            }
            else if (rarityLevel == "Legendary")
            {
                addHunger = (int)(Inventory.inventory.getItemSpecsFromID(ID).addHunger*rarityStatBooster*25);
            }
            else if(rarityLevel == "GodLike")
            {
                addHunger = (int)(Inventory.inventory.getItemSpecsFromID(ID).addHunger*rarityStatBooster*50);
            }
        }
        
        if (addEXP == 0 || addEXP == null)
        {
            if (rarityLevel == "Common")
            {
                addEXP = (int)(Inventory.inventory.getItemSpecsFromID(ID).addEXP*rarityStatBooster);
            }
            else if (rarityLevel == "Rare")
            {
                addEXP = (int)(Inventory.inventory.getItemSpecsFromID(ID).addEXP*rarityStatBooster*5);
            }
            else if (rarityLevel == "Antic")
            {
                addEXP = (int)(Inventory.inventory.getItemSpecsFromID(ID).addEXP*rarityStatBooster*10);
            }
            else if (rarityLevel == "Legendary")
            {
                addEXP = (int)(Inventory.inventory.getItemSpecsFromID(ID).addEXP*rarityStatBooster*25);
            }
            else if(rarityLevel == "GodLike")
            {
                addEXP = (int)(Inventory.inventory.getItemSpecsFromID(ID).addEXP*rarityStatBooster*50);
            }
        }
        
        
        
        
        
        
        
        if (string.IsNullOrEmpty(itemDesc))
        {
            itemDesc = Inventory.inventory.getItemSpecsFromID(ID).itemDesc;
        }

        if (name == "Uknown item" || name == "" || name == null)
        {
            name = Inventory.inventory.getItemSpecsFromID(ID).name;
        }
        
        if (image == null)
        {
            image = (Texture) Resources.Load(Inventory.inventory.getItemSpecsFromID(ID).imagePath);
        }
    }
    
    public void checkForDestroyConditions()
    {
        if (SaveGameController.GameSaver.lootLoaded)
        {
            SaveLootController.SaveLootControl.LootOnTheMap.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
