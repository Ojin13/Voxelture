using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Skill : MonoBehaviour
{
    public int skillID;
    public int requiredSkillPoints;
    public String name;
    public String desc;
    public RawImage skillImage;
    public bool unlocked;
    public Skill[] requiredToUnlock;
    public GameObject UnlockableIndicator;

    public bool canBeInSlot;
    public int cooldown = 1;
    public bool ready = true;


    public void Start()
    {
        skillImage = GetComponent<RawImage>();
    }

    public void SelectSkill()
    {
        SkillTreeController.SkillController.SelectSkill(this);
    }
    
    void Update()
    {
        //check if skill can be unlocked
        if (requiredSkillPoints > SkillTreeController.SkillController.FreeSkillPoints && !unlocked)
        {
            GetComponent<RawImage>().color = new Color32(80,80,80,255);
            UnlockableIndicator.SetActive(false);
        }
        else
        {
            if (requiredToUnlock.Length >= 1)
            {
                for (int i = 0; i < requiredToUnlock.Length; i++)
                {
                    if (requiredToUnlock[i].unlocked == false)
                    {
                        GetComponent<RawImage>().color = new Color32(80,80,80,255);
                        UnlockableIndicator.SetActive(false);
                        return;
                    }
                }
            }
            GetComponent<RawImage>().color = new Color32(255,255,255,255);
            if (!unlocked)
            {
                UnlockableIndicator.SetActive(true);
            }
            else
            {
                UnlockableIndicator.SetActive(false);
            }
        }
    }

    public void useSKill()
    {
        if (ready == false || unlocked == false)
        {
            return;
        }
        else
        {
            bool wasReallyUsed = false;
            
            //Fire ring
            if (skillID == 13)
            {
                if (PlayerController.Player.GetComponent<FireRing>().canUse())
                {
                    PlayerController.Player.GetComponent<FireRing>().createRing();
                    wasReallyUsed = true;
                }
            }

            //Bolt
            if (skillID == 14)
            {
                if (PlayerController.Player.GetComponent<Attack>().isAttacking == false && PlayerController.MovementControl.anim.GetBool("Grounded"))
                {
                    PlayerController.MovementControl.setCantMove();
                    PlayerController.Player.GetComponent<SoundPlayer>().sound_thunder();
                    PlayerController.Player.GetComponent<Attack>().prepareToAttack("skill__Bolt Attack");
                    wasReallyUsed = true;
                }
            }
            
            
            //Katon fireball
            if (skillID == 12)
            {
                if (!PlayerController.Player.GetComponent<Attack>().isAttacking && !PlayerController.Player.GetComponent<DrawingWeapon>().weaponIsDrawn && PlayerController.MovementControl.anim.GetBool("Standing") && PlayerController.MovementControl.canMove)
                {
                    PlayerController.Player.GetComponent<Attack>().prepareToAttack();   
                    
                    PlayerController.MovementControl.anim.SetTrigger("skill__Punch");
                    if (PlayerController.Player.GetComponent<SpeedCalculator>().PlayerCurrentSpeed == 0f)
                    {
                        PlayerController.Player.GetComponent<MovementController>().setCantMove();
                    }
                }
            }
            
            
            //Jump Splash
            if (skillID == 10)
            {
                if (PlayerController.Player.GetComponent<Attack>().isAttacking == false && PlayerController.Player.GetComponent<DrawingWeapon>().weaponIsDrawn && PlayerController.MovementControl.anim.GetBool("Grounded"))
                {
                    PlayerController.MovementControl.anim.SetTrigger("skill__Splash Attack");
                    PlayerController.Player.GetComponent<Attack>().prepareToAttack();
                }
            }
            
            //map
            if (skillID == 1 || skillID == 2)
            {
                if (UIController.UIControl.MiniMapIsActive)
                {
                    UIController.UIControl.deactivateMiniMap();
                }
                else
                {
                    UIController.UIControl.activateMiniMap();
                }
            }
            
            
            //silent Kill
            if (skillID == 11)
            {
                PlayerController.Player.GetComponent<SilentKill>().prepareSilentKill();
            }

            if (wasReallyUsed)
            {
                ready = false;
                Invoke("chargingSkill", cooldown);   
            }
        }
    }
    
    public void chargingSkill()
    {
        ready = true;
    }
}
