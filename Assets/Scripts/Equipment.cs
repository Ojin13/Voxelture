using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public List<InventoryItem> EquippedItems = new List<InventoryItem>();
    
    public GameObject Weapon;
    public GameObject Head;
    public GameObject Pants;
    public GameObject Shoes;
    public GameObject Chest;
    public GameObject Accessory;
    

    public void Equip(InventoryItem invItem)
    {
        string equipType = Inventory.inventory.getItemSpecsFromID(invItem.ID).EquipType;
        GameObject correctSlot = new  GameObject();
        
        if (equipType == "Head")
        {
            correctSlot = Head;
        }
        else if(equipType == "Chest")
        {
            correctSlot = Chest;
        }
        else if(equipType == "Pants")
        {
            correctSlot = Pants;
        }
        else if(equipType == "Shoes")
        {
            correctSlot = Shoes;
        }
        else if(equipType == "Weapon")
        {
            correctSlot = Weapon;
        }
        else if(equipType == "Accessory")
        {
            correctSlot = Accessory;
        }
        
        InventoryItem itemToEquip = copyItem(invItem);
        
        //if slot is empty yet
        if (correctSlot.GetComponent<ItemUI>().invItem.ID == 0)
        {
            EquippedItems.Add(itemToEquip);
        }
        else
        {
            Inventory.inventory.addItem(null,null, correctSlot.GetComponent<ItemUI>().invItem);
            EquippedItems.Remove(correctSlot.GetComponent<ItemUI>().invItem);
            EquippedItems.Add(itemToEquip);
            if (DrawingWeapon.DrawingWeaponContoller.weaponIsDrawn)
            {
                PlayerController.MovementControl.anim.SetTrigger("UnequipEquippedWeapon");
            }
        }
        
        correctSlot.GetComponent<ItemUI>().invItem = itemToEquip;
        GetComponent<ArtefactController>().checkSpecialEffectsOfItem(correctSlot.GetComponent<ItemUI>().invItem,"equip");
        correctSlot.GetComponent<ItemUI>().itemImage.texture = (Texture) Resources.Load(Inventory.inventory.getItemSpecsFromID(itemToEquip.ID).imagePath);
        GetComponent<PlayerController>().setUpStats();
        if (correctSlot == Weapon)
        {
            GetComponent<WeaponController>().displayWeaponAccordingToEquip("equip");
        }
    }
    
    public void Unequip(InventoryItem invItem)
    {
        if (invItem.ID != 0 && !PlayerController.Player.GetComponent<Attack>().isAttacking)
        {
            Debug.Log("Unequip");
            Inventory.inventory.addItem(null,null, invItem,true);
            EquippedItems.Remove(invItem);
            string equipType = Inventory.inventory.getItemSpecsFromID(invItem.ID).EquipType;
            GameObject correctSlot = new  GameObject();
            InventoryItem emptyItem = new  InventoryItem();
            
            if (equipType == "Head")
            {
                correctSlot = Head;
            }
            else if(equipType == "Chest")
            {
                correctSlot = Chest;
            }
            else if(equipType == "Pants")
            {
                correctSlot = Pants;
            }
            else if(equipType == "Shoes")
            {
                correctSlot = Shoes;
            }
            else if(equipType == "Weapon")
            {
                correctSlot = Weapon;
            }
            else if(equipType == "Accessory")
            {
                correctSlot = Accessory;
            }
        
            GetComponent<ArtefactController>().checkSpecialEffectsOfItem(correctSlot.GetComponent<ItemUI>().invItem,"unequip");
            correctSlot.GetComponent<ItemUI>().invItem = emptyItem;
            correctSlot.GetComponent<ItemUI>().itemImage.texture = null;
            GetComponent<PlayerController>().setUpStats();
            
            if (correctSlot == Weapon)
            {
                GetComponent<WeaponController>().displayWeaponAccordingToEquip("unequip");
            }
        }
    }


    private InventoryItem copyItem(InventoryItem oldItem)
    {
        InventoryItem newItem = new  InventoryItem();
        
        newItem.ID = oldItem.ID;
        newItem.name = oldItem.name;
        newItem.rarityLevel = oldItem.rarityLevel;
        newItem.itemType = oldItem.itemType;
        newItem.EquipType = oldItem.EquipType;
        newItem.EquipSubType = oldItem.EquipSubType;
        newItem.damage = oldItem.damage;
        newItem.defence = oldItem.defence;
        newItem.precision = oldItem.precision;
        newItem.luck = oldItem.luck;
        newItem.increaseMaxHunger = oldItem.increaseMaxHunger;
        newItem.increaseMaxHP = oldItem.increaseMaxHP;
        newItem.addHP = oldItem.addHP;
        newItem.addHunger = oldItem.addHunger;
        newItem.addEXP = oldItem.addEXP;
        newItem.itemDesc = oldItem.itemDesc;
        newItem.speedModification = oldItem.speedModification;
        return newItem;
    }

    //check if item is eqquiped
    public bool isEquipped(int ID = 0, string name = "")
    {
        if (ID != 0)
        {
            foreach (var equipped in EquippedItems)
            {
                if (equipped.ID == ID)
                {
                    return true;
                }
                else
                {
                    continue;
                }
            }
        }
        
        if (name != "")
        {
            foreach (var equipped in EquippedItems)
            {
                if (equipped.name == name)
                {
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
}
