using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HungerController : MonoBehaviour
{
    public float hungerToRemoveProgress = 0;
    public float HPtoRemoveProgress = 0;
    public string currentStatus;
    private string lastStatus;
    public Skill skill_regeneration;
    
    void Awake()
    {
        currentStatus = getStatus();
        InvokeRepeating("Starve", 0, 0.05f);
        InvokeRepeating("zakruc",0,30);
        InvokeRepeating("regenerateHP",0,2);
    }

    void Starve()
    {
        currentStatus = getStatus();
        //running
        if (GetComponent<MovementController>().InputForce > 0.5f)
        {
            hungerToRemoveProgress += 0.01f;
        }
        //standing
        else if(GetComponent<MovementController>().InputForce == 0)
        {
            hungerToRemoveProgress += 0.0005f;
        }
        //walking
        else
        {
            hungerToRemoveProgress += 0.001f;   
        }
        
        //attacking
        if (GetComponent<Attack>().isAttacking)
        {
            hungerToRemoveProgress += 0.02f;
        }
        
        //rolling
        if (GetComponent<MovementController>().isRolling)
        {
            hungerToRemoveProgress += 0.0015f;
        }


        if (GetComponent<PlayerController>().Hunger <= 0)
        {
            HPtoRemoveProgress += 0.1f;
        }
        else if (GetComponent<PlayerController>().Hunger <= 5)
        {
            HPtoRemoveProgress += 0.05f;
        }
        else if (GetComponent<PlayerController>().Hunger <= 15)
        {
            HPtoRemoveProgress += 0.03f;
        }
        else if (GetComponent<PlayerController>().Hunger > 15)
        {
            HPtoRemoveProgress = 0;
        }

        if (HPtoRemoveProgress >= 1 && !GetComponent<PlayerController>().isDead)
        {
            int HPtoRemove = PlayerController.PlayerControl.MAXHP / 100;
            GetComponent<PlayerController>().HP -= HPtoRemove;
            HPtoRemoveProgress = 0;
        }
        
        if (hungerToRemoveProgress >= 1)
        {
            //int HungerToRemove = PlayerController.PlayerControl.MaxHunger / 100;
            if (GetComponent<PlayerController>().Hunger > 0)
            {
                GetComponent<PlayerController>().Hunger -= (int)hungerToRemoveProgress;
            }
            
            hungerToRemoveProgress = 0;
        }
    }

    string getStatus()
    {
        if (GetComponent<PlayerController>().Hunger <= GetComponent<PlayerController>().MaxHunger / 4)
        {
            return "Hungry :(";
        }
        else
        {
            return "Full :)";
        }
    }


    public void zakruc()
    {
        if (PlayerController.PlayerControl.isDead == false && currentStatus == "Hungry :(")
        {
            if (Random.Range(0, 100) > 25)
            {
                SoundPlayer.PlayerSoundController.sound_hungry();
            }
        }
    }


    public void regenerateHP()
    {
        if (GetComponent<PlayerController>().Hunger >= GetComponent<PlayerController>().MaxHunger / 1.1f)
        {
            if (GetComponent<PlayerController>().HP < GetComponent<PlayerController>().MAXHP)
            {
                if (skill_regeneration.unlocked)
                {
                    GetComponent<PlayerController>().HP += 1;
                }
            }
        }
    }
}
