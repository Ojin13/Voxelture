using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class TakeDamage : MonoBehaviour
{

    public bool canShowGetHitAnim;

    public void calculateDamage(float dmg, bool ignoreDef=false)
    {
        float playersDef = GetComponent<PlayerController>().DEF;
        float calculatedDamage = dmg * (100 / (100 + playersDef/2));
        if (calculatedDamage < 1)
        {
            calculatedDamage = 1;
            
        }
    
        //fall damage
        if (ignoreDef)
        {
            calculatedDamage = dmg;
        }
        
        GetComponent<PlayerController>().HP -= (int)calculatedDamage;
        if (GetComponent<MovementController>().anim != null)
        {
            SoundPlayer.PlayerSoundController.sound_gettingHit();
            if (canShowGetHitAnim)
            {
                GetComponent<MovementController>().anim.SetTrigger("GetHit");
            }

            resetPropertiesAfterHit();
        }
        GetComponent<InvisibleController>().setVisible();
    }

    public void resetPropertiesAfterHit()
    {
        GetComponent<distanceFromGround>().dontIgnoreFreeFall();
        GetComponent<MovementController>().setCanMove();
        GetComponent<MovementController>().SetCanControllJumpDirection();
        GetComponent<Attack>().StopAttackChain();
        
        //RESET ALL TRIGGERS
        GetComponent<MovementController>().anim.ResetTrigger("CanAttack");
        GetComponent<MovementController>().anim.ResetTrigger("Punch");
        
        foreach (var skill in SkillTreeController.SkillController.skillList)
        {
            GetComponent<MovementController>().anim.ResetTrigger("skill__"+skill.name);
        }
    }

    public void canPlayGetHit()
    {
        canShowGetHitAnim = true;
    }
    
    public void cantPlayGetHit()
    {
        canShowGetHitAnim = false;
    }
}
