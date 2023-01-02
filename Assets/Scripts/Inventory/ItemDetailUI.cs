using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDetailUI : MonoBehaviour
{
    public int itemID;
    public RawImage itemImage;
    public TextMeshProUGUI ItemNameText;
    public TextMeshProUGUI ItemStatsText;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }


    public void setUpStats(InventoryItem item)
    {
        itemImage.texture = (Texture)Resources.Load(Inventory.inventory.getItemSpecsFromID(item.ID).imagePath);
        ItemNameText.text = item.name;

        string statText = "";
        statText += "<b>Rarity: </b>" + item.rarityLevel;
        statText += "\n<b>Category: </b>" + item.itemType;
        
        if (Inventory.inventory.getItemSpecsFromID(item.ID).EquipType != null)
        {
            if (item.EquipType != Inventory.inventory.getItemSpecsFromID(item.ID).EquipType)
            {
                statText += "\n<b>Equp Type: </b>" + item.EquipType;
            }
            else
            {
                statText += "\n<b>Equp Type: </b>" + Inventory.inventory.getItemSpecsFromID(item.ID).EquipType;
            }
        }
        
        //Damage
        if (Inventory.inventory.getItemSpecsFromID(item.ID).damage != 0)
        {
            if (item.damage != Inventory.inventory.getItemSpecsFromID(item.ID).damage)
            {
                statText += "\n<b>Damage: +</b>" + item.damage;
            }
            else
            {
                statText += "\n<b>Damage: +</b>" + Inventory.inventory.getItemSpecsFromID(item.ID).damage;
            }
        }
        
        //Defence
        if (Inventory.inventory.getItemSpecsFromID(item.ID).defence != 0)
        {
            if (item.defence != Inventory.inventory.getItemSpecsFromID(item.ID).defence)
            {
                statText += "\n<b>Defence: +</b>" + item.defence;
            }
            else
            {
                statText += "\n<b>Defence: +</b>" + Inventory.inventory.getItemSpecsFromID(item.ID).defence;
            }
        }
        
        //Luck
        if (Inventory.inventory.getItemSpecsFromID(item.ID).luck != 0)
        {
            if (item.luck != Inventory.inventory.getItemSpecsFromID(item.ID).luck)
            {
                statText += "\n<b>Luck: +</b>" + item.luck;
            }
            else
            {
                statText += "\n<b>Luck: +</b>" + Inventory.inventory.getItemSpecsFromID(item.ID).luck;
            }
        }
        
        //Precision
        if (Inventory.inventory.getItemSpecsFromID(item.ID).precision != 0)
        {
            if (item.precision != Inventory.inventory.getItemSpecsFromID(item.ID).precision)
            {
                statText += "\n<b>Precison: +</b>" + item.precision;
            }
            else
            {
                statText += "\n<b>Precion: +</b>" + Inventory.inventory.getItemSpecsFromID(item.ID).precision;
            }
        }
        
        //add HP
        if (Inventory.inventory.getItemSpecsFromID(item.ID).addHP != 0)
        {
            if (item.addHP != Inventory.inventory.getItemSpecsFromID(item.ID).addHP)
            {
                statText += "\n<b>Add: </b>" + item.addHP+"HP";
            }
            else
            {
                statText += "\n<b>Add: </b>" + Inventory.inventory.getItemSpecsFromID(item.ID).addHP+"HP";
            }
        }
        
        //Heal Hunger
        if (Inventory.inventory.getItemSpecsFromID(item.ID).addHunger != 0)
        {
            if (item.addHunger != Inventory.inventory.getItemSpecsFromID(item.ID).addHunger)
            {
                statText += "\n<b>Add: </b>" + item.addHunger+"Hunger";
            }
            else
            {
                statText += "\n<b>Add: </b>" + Inventory.inventory.getItemSpecsFromID(item.ID).addHunger+"Hunger";
            }
        }
        
        //Add EXP
        if (Inventory.inventory.getItemSpecsFromID(item.ID).addEXP != 0)
        {
            if (item.addEXP != Inventory.inventory.getItemSpecsFromID(item.ID).addEXP)
            {
                statText += "\n<b>Add: </b>" + item.addEXP+"EXP";
            }
            else
            {
                statText += "\n<b>Add: </b>" + Inventory.inventory.getItemSpecsFromID(item.ID).addEXP+"EXP";
            }
        }
        
        //Increase max HP
        if (Inventory.inventory.getItemSpecsFromID(item.ID).increaseMaxHP != 0)
        {
            if (item.increaseMaxHP != Inventory.inventory.getItemSpecsFromID(item.ID).increaseMaxHP)
            {
                statText += "\n<b>Add: </b>" + item.increaseMaxHP+"MAX HP";
            }
            else
            {
                statText += "\n<b>Add: </b>" + Inventory.inventory.getItemSpecsFromID(item.ID).increaseMaxHP+"MAX HP";
            }
        }
        
        //Increase max hunger
        if (Inventory.inventory.getItemSpecsFromID(item.ID).increaseMaxHunger != 0)
        {
            if (item.increaseMaxHunger != Inventory.inventory.getItemSpecsFromID(item.ID).increaseMaxHunger)
            {
                statText += "\n<b>Add: </b>" + item.increaseMaxHunger+"MAX Hunger";
            }
            else
            {
                statText += "\n<b>Add: </b>" + Inventory.inventory.getItemSpecsFromID(item.ID).increaseMaxHunger+"MAX Hunger";
            }
        }
        
        //Increase Movement Speed
        if (Inventory.inventory.getItemSpecsFromID(item.ID).speedModification != 0)
        {
            if (item.speedModification != Inventory.inventory.getItemSpecsFromID(item.ID).speedModification)
            {
                var percentage = (item.speedModification*100)/5;
                statText += "\n<b>Speed: </b>" + (item.speedModification > 0 ? "+" : "") + percentage + "%";
            }
            else
            {
                var percentage = (Inventory.inventory.getItemSpecsFromID(item.ID).speedModification*100)/5;
                statText += "\n<b>Speed: </b>"+ (Inventory.inventory.getItemSpecsFromID(item.ID).speedModification > 0 ? "+": "") +percentage+"%";
            }
        }
        
        //Description
        if (Inventory.inventory.getItemSpecsFromID(item.ID).itemDesc != null)
        {
            if (item.itemDesc != Inventory.inventory.getItemSpecsFromID(item.ID).itemDesc)
            {
                statText += "\n<b>Description: </b>\n" + item.itemDesc;
            }
            else
            {
                statText += "\n<b>Description: </b>\n" + Inventory.inventory.getItemSpecsFromID(item.ID).itemDesc;
            }
        }
        
        ItemStatsText.text = statText;
    }
}
