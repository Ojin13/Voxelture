using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour
{

    public WeaponModels WeaponModel_control;
    
    // Start is called before the first frame update
    void Start()
    {
        displayWeaponAccordingToEquip();
    }

    public static bool hasEquippedWeapon()
    {
        if (PlayerController.Player.GetComponent<Equipment>().Weapon.GetComponent<ItemUI>().invItem.ID == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    
    public void displayWeaponAccordingToEquip(String source="")
    {
        //Unequip weapon
        if (source == "unequip")
        {
            if (!hasEquippedWeapon())
            {
                if (GetComponent<Attack>().isAttacking == false)
                {
                    turnOffWeapon(true);
                    PlayerController.MovementControl.anim.SetTrigger("UnequipEquippedWeapon");
                }
            }
        }
        //Equip new weapon
        else if (source == "equip")
        {
            if(hasEquippedWeapon())
            {
                WeaponModel_control.setWeaponModel();
                turnOffWeapon(false);
                DrawingWeapon.DrawingWeaponContoller.WeaponOnTheBackOn();
                DrawingWeapon.DrawingWeaponContoller.WeaponCaseOn();
            }
        }
        else
        {
            if (hasEquippedWeapon())
            {
                turnOffWeapon();
            }
            else
            {
                turnOffWeapon(true);
            }
        }
    }

    
    
    public void turnOffWeapon(bool completely = false)
    {
        DrawingWeapon.DrawingWeaponContoller.ActiveWeaponOff();
        DrawingWeapon.DrawingWeaponContoller.weaponIsDrawn = false;
        if(PlayerController.MovementControl.anim)
        {
            PlayerController.MovementControl.anim.SetBool("WeaponDrawn", false);
        }

        if (completely)
        {
            DrawingWeapon.DrawingWeaponContoller.WeaponOnTheBackOff();
            DrawingWeapon.DrawingWeaponContoller.WeaponCaseOff();
        }
    }
}
