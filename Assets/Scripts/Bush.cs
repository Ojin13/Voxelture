using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bush : MonoBehaviour
{
    public Skill skill_hideInBush;
    public void OnTriggerExit(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerCollider"))
        {
            PlayerController.Player.GetComponent<InvisibleController>().setVisible();
        }
    }

    public void OnTriggerStay(Collider other)
    {
        if(other.gameObject.CompareTag("PlayerCollider"))
        {
            for(int i = 0; i < SkillTreeController.SkillController.skillList.Length; i++)
            {
                if (SkillTreeController.SkillController.skillList[i].unlocked && SkillTreeController.SkillController.skillList[i].skillID == 4)
                {
                    PlayerController.Player.GetComponent<InvisibleController>().SetInvisible();
                }
            }
        }
    }
}