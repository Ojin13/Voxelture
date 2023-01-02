using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public GetEnemiesFromZone getEnemiesFromCloseWeaponArea;
    public GetEnemiesFromZone getEnemiesFromPunchArea;
    public GetEnemiesFromZone getEnemiesFromMagicArea;
    public static GameObject Player;
    public static PlayerController PlayerControl;
    public static MovementController MovementControl;

    //Stats
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






    // Start is called before the first frame update
    void Awake()
    {
        Player = gameObject;
        PlayerControl = this;
        MovementControl = GetComponent<MovementController>();
        setUpStats();
    }

    // Update is called once per frame
    void Update()
    {
        if (LEVEL <= 0)
        {
            LEVEL = 1;
        }
        
        if(HP <= 0 && !isDead)
        {
            isDead = true;
            GetComponent<Die>().DiePlease(this.gameObject, true);
        }

        if (HP > MAXHP)
        {
            HP = MAXHP;
        }
        
        if (Hunger > MaxHunger)
        {
            Hunger = MaxHunger;
        }
    }


    public void setUpStats()
    {
        if (LEVEL <= 0)
        {
            LEVEL = 1;
        }

        int multiplier = 113;
        int temp_MAXHP = multiplier+(LEVEL*20);
        int temp_MaxHunger = 100;
        int temp_DMG = LEVEL*multiplier/50;
        int temp_DEF = LEVEL*multiplier/85;
        int temp_PRECISION = LEVEL*multiplier/82;
        float temp_SPEED = 5;
        int temp_LUCK;

        if (LEVEL <= 10)
        {
            temp_LUCK = LEVEL*2;
        }
        else
        {
            temp_LUCK = (LEVEL / 5)+20;
        }

        if (temp_LUCK > 30)
        {
            temp_LUCK = 30;
        }
        
        foreach (InventoryItem equip in GetComponent<Equipment>().EquippedItems)
        {
            temp_MAXHP += equip.increaseMaxHP;
            temp_MaxHunger += equip.increaseMaxHunger;
            temp_DMG += equip.damage;
            temp_DEF += equip.defence;
            temp_PRECISION += equip.precision;
            temp_LUCK += equip.luck;
            temp_SPEED += equip.speedModification;
        }
        
        MAXHP = temp_MAXHP;
        MaxHunger = temp_MaxHunger;
        DMG = temp_DMG;
        DEF = temp_DEF;
        PRECISION = temp_PRECISION;
        LUCK = temp_LUCK;
        GetComponent<MovementController>().MovementSpeed = temp_SPEED;
        
        Debug.Log("Stats setup according to equipment!");
    }
}
