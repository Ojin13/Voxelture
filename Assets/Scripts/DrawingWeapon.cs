using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawingWeapon : MonoBehaviour
{
    public static DrawingWeapon DrawingWeaponContoller;
    public bool weaponIsDrawn;
    public GameObject WeaponOnTheBack;
    public GameObject ActiveWeapon;
    public GameObject WeaponCase;

    // Start is called before the first frame update
    void Awake()
    {
        DrawingWeaponContoller = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (WeaponController.hasEquippedWeapon() && UIController.UIControl.DialoguePanelIsActive == false)
        {
            if(Input.GetKeyDown(KeyCode.Q) && PlayerController.MovementControl.anim.GetBool("Grounded"))
            {
                if(PlayerController.MovementControl.anim.GetBool("WeaponDrawn"))
                {
                    weaponIsDrawn = false;
                    PlayerController.MovementControl.anim.SetBool("WeaponDrawn", false);
                }
                else
                {
                    weaponIsDrawn = true;
                    PlayerController.MovementControl.anim.SetBool("WeaponDrawn", true); //INVOKE!
                }
            }   
        }
    }





    public void AllowWeaponGrab()
    {
        PlayerController.MovementControl.anim.SetTrigger("CanGrabWeaponFromBack");
    }

    public void ActiveWeaponOn()
    {
        ActiveWeapon.SetActive(true);
    }


    public void ActiveWeaponOff()
    {
        ActiveWeapon.SetActive(false);
    }


    public void WeaponOnTheBackOn()
    {
        WeaponOnTheBack.SetActive(true);
    }
    
    public void WeaponOnTheBackOff()
    {
        WeaponOnTheBack.SetActive(false);
    }

    public void WeaponCaseOff()
    {
        WeaponCase.SetActive(false);
    }
    
    public void WeaponCaseOn()
    {
        //if (PlayerController.Player.GetComponent<Equipment>().Weapon.GetComponent<ItemUI>().invItem.EquipSubType == "Sword")
        if(PlayerController.Player.GetComponent<Equipment>().Weapon.GetComponent<ItemUI>().invItem.ID == 600 || PlayerController.Player.GetComponent<Equipment>().Weapon.GetComponent<ItemUI>().invItem.ID == 603)
        {
            WeaponCase.SetActive(true);
        }
        else
        {
            WeaponCaseOff();
        }
    }
}
