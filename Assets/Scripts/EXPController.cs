using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EXPController : MonoBehaviour
{
    public int[] levelTable;


    private void Update()
    {
        checkLevel();
    }

    void Awake()
    {
        //Initialize EXP needed for level array
        for (int lvl = 0; lvl < 99; lvl++)
        {
            if (lvl == 0)
            {
                levelTable[0] = 100;
                continue;
            }
            
            levelTable[lvl] =  (int)(levelTable[lvl-1] + Math.Pow(lvl, 2) / 0.02f);
        }
    }
    

    public void AddEXPFromKill(EnemyController killedEnemy)
    {
        checkLevel();
    }

    public void AddEXP(int EXPToAdd)
    {
        PlayerController.Player.GetComponent<PlayerController>().EXP += EXPToAdd;
        checkLevel();
    }


    
    
    public void checkLevel()
    {
        int correctLevel=0;
        int currentEXP = PlayerController.Player.GetComponent<PlayerController>().EXP;
        
        for (int i = 0; i < 99; i++)
        {
            if (i == 99)
            {
                correctLevel = 99;
                break;
            }
            
            if (currentEXP < levelTable[0])
            {
                correctLevel = 1;
                break;;
            }
            
            if (currentEXP == levelTable[i])
            {
                correctLevel = i+2;
                break;
            }
            
            if (currentEXP > levelTable[i] && currentEXP < levelTable[i + 1])
            {
                correctLevel = i+2;
                break;
            }
            else
            {
                continue;
            }
        }
        
        if (correctLevel != PlayerController.Player.GetComponent<PlayerController>().LEVEL)
        {
            if (correctLevel > PlayerController.Player.GetComponent<PlayerController>().LEVEL)
            {
                Debug.Log("LEVEL UP!");
                GetComponent<MovementController>().anim.SetTrigger("LevelUp");

                if (correctLevel <= 10)
                {
                    SkillTreeController.SkillController.FreeSkillPoints += 1;
                }
                else if(correctLevel <= 30)
                {
                    SkillTreeController.SkillController.FreeSkillPoints += 2;   
                }
                else
                {
                    SkillTreeController.SkillController.FreeSkillPoints += 3;
                }
            }
            else
            {
                Debug.Log("LEVEL DOWN!");
            }
            
            PlayerController.Player.GetComponent<PlayerController>().LEVEL = correctLevel;
            PlayerController.Player.GetComponent<PlayerController>().setUpStats();
        }
        
    }
}
