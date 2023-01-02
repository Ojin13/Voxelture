using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveObj
{
    public List<InventoryItem> Items = new List<InventoryItem>();
    public List<InventoryItem> Equipment = new List<InventoryItem>();
    public List<EnemySerializable> Enemies = new List<EnemySerializable>();
    public List<InventoryItem> LootOnTheMap = new List<InventoryItem>();
    public List<int> skillIDfromSkillBar = new List<int>();
    public float dayProgress;
    public String currentDayState;
    public int day;

    public float[] playerPosition = new float[3]; //0=x 1=y 2=z
    public bool isDead = false;
    public int HP;
    public int MAXHP;
    public int Hunger;
    public int MaxHunger;
    public int LEVEL;
    public int EXP;
    public int DMG;
    public int DEF;
    public int PRECISION;
    public int LUCK;
    public List<int> unlockedSkills = new List<int>();
    public int SP;
    public float MovementSpeed;
    public int QuestNumber_KFC;
    public int QuestNumber_Cube;
    
}