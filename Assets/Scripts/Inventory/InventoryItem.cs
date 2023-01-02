using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[System.Serializable]
public class InventoryItem
{
    public int ID;
    public string name = "Uknown item";
    public string rarityLevel;
    public int amountOwned = 1;
    public string itemType;
    public string EquipType;
    public string EquipSubType;
    public int damage;
    public int defence;
    public int precision;
    public int increaseMaxHunger;
    public int increaseMaxHP;
    public bool equipped;
    public int luck;
    public int addHP;
    public int addHunger;
    public int addEXP;
    public string itemDesc = "";
    public bool isQuickAccessItem;
    public float[] lootPosition = new float[3]; //x=0 y=1 z=2
    public float speedModification;
}
