using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponModels : MonoBehaviour
{
    public List<GameObject> Weapons;
    public GameObject StandardSword;
    public GameObject StandardAxe;
    public GameObject StandardMace;
    public GameObject StandardHammer;
    public bool activeWeapon;
    public WeaponModels WeaponModelOnTheBack;
    
    // Start is called before the first frame update
    void Awake()
    {
        foreach (var weaponModel in GetComponentsInChildren<WeaponModel>())
        {
            Weapons.Add(weaponModel.gameObject);
            weaponModel.gameObject.SetActive(false);
        }
    }

    public void setWeaponModel()
    {
        if (WeaponModelOnTheBack != null)
        {
            WeaponModelOnTheBack.setWeaponModel();
        }
        
        bool modelFound = false;
        if (WeaponController.hasEquippedWeapon())
        {
            PlayerController.Player.GetComponent<DrawingWeapon>().ActiveWeaponOff();
            
            foreach (var weapon in Weapons)
            {
                if (PlayerController.Player.GetComponent<Equipment>().Weapon.GetComponent<ItemUI>().invItem.ID == weapon.GetComponent<WeaponModel>().equipID)
                {
                    if (activeWeapon)
                    {
                        PlayerController.Player.GetComponent<DrawingWeapon>().ActiveWeapon = weapon;
                    }
                    else
                    {
                        PlayerController.Player.GetComponent<DrawingWeapon>().WeaponOnTheBack = weapon;
                    }
                    modelFound = true;
                }
                else
                {
                    weapon.SetActive(false);
                }
            }

            if (!modelFound)
            {
                if (PlayerController.Player.GetComponent<Equipment>().Weapon.GetComponent<ItemUI>().invItem.EquipSubType == "Sword")
                {
                    if (activeWeapon)
                    {
                        PlayerController.Player.GetComponent<DrawingWeapon>().ActiveWeapon = StandardSword;
                    }
                    else
                    {
                        PlayerController.Player.GetComponent<DrawingWeapon>().WeaponOnTheBack = StandardSword;
                    }
                }
                
                if (PlayerController.Player.GetComponent<Equipment>().Weapon.GetComponent<ItemUI>().invItem.EquipSubType == "Axe")
                {
                    if (activeWeapon)
                    {
                        PlayerController.Player.GetComponent<DrawingWeapon>().ActiveWeapon = StandardAxe;
                    }
                    else
                    {
                        PlayerController.Player.GetComponent<DrawingWeapon>().WeaponOnTheBack = StandardAxe;
                    }
                }
                
                if (PlayerController.Player.GetComponent<Equipment>().Weapon.GetComponent<ItemUI>().invItem.EquipSubType == "Mace")
                {
                    if (activeWeapon)
                    {
                        PlayerController.Player.GetComponent<DrawingWeapon>().ActiveWeapon = StandardMace;
                    }
                    else
                    {
                        PlayerController.Player.GetComponent<DrawingWeapon>().WeaponOnTheBack = StandardMace;
                    }
                }
                
                if (PlayerController.Player.GetComponent<Equipment>().Weapon.GetComponent<ItemUI>().invItem.EquipSubType == "Hammer")
                {
                    if (activeWeapon)
                    {
                        PlayerController.Player.GetComponent<DrawingWeapon>().ActiveWeapon = StandardHammer;
                    }
                    else
                    {
                        PlayerController.Player.GetComponent<DrawingWeapon>().WeaponOnTheBack = StandardHammer;
                    }
                }
            }
        }
    }
}
