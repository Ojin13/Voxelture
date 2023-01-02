using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public bool isAttacking;
    public GameObject SlashArea_obj;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void dealDamage(int bonusDmg=1)
    {
        if (bonusDmg <= 0)
        {
            bonusDmg = 1;
        }
        
        
        if(SlashArea_obj.GetComponent<EnemyCanHitPlayer>().canHitPlayer)
        {
            GetComponent<EnemyController>().anim.SetBool("CanMove", false);
            int Damage = (int)Random.Range(GetComponent<EnemyController>().DMG / 2f, GetComponent<EnemyController>().DMG) * bonusDmg;
            if(PlayerController.MovementControl.isRolling)
            {
                int ChangeToAvoid = Random.Range(1, 100);
                if(ChangeToAvoid >= 100-PlayerController.Player.GetComponent<PlayerController>().LUCK)
                {
                    Damage -= (Damage / Random.Range(2,5));
                    Debug.Log("Envade attack");
                }
            }
            PlayerController.Player.GetComponent<TakeDamage>().calculateDamage(Damage);
        }
    }


    public void setIsAttacking()
    {
        isAttacking = true;
    }

    public void setIsNotAttacking()
    {
        isAttacking = false;
    }

}
