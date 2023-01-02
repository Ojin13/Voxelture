using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using Cinemachine;
using UnityEngine;
using Random = UnityEngine.Random;


public class Attack : MonoBehaviour
{
    public bool isAttacking;
    public Skill skill__assasinate;

    
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {


            if (!GetComponent<MovementController>().anim)
            {
                return;
            }

            if (GetComponent<MovementController>().anim.GetBool("Standing") && PlayerController.MovementControl.anim.GetBool("Grounded") && PlayerController.MovementControl.canMove && !MouseOverUI.BlockedByUI && !isAttacking)
            {
                GameObject closestEnemy = GetComponent<GetClosestEnemy>().HandCombat__closestEnemy;

                if (closestEnemy == null)
                {
                    prepareToAttack();    
                }
                else
                {
                    if (Vector3.Distance(closestEnemy.transform.position, transform.position) > 2)
                    {
                        prepareToAttack();
                    }
                    else
                    {
                        //silent kill
                        if (!closestEnemy.GetComponent<EnemyNavigator>().playerDetected && skill__assasinate.unlocked && GetComponent<DrawingWeapon>().weaponIsDrawn)
                        {
                            GetComponent<SilentKill>().prepareSilentKill();
                        }
                        else
                        {
                            prepareToAttack();
                        }
                    }
                }

                //running punch or normal punch
                if (!GetComponent<DrawingWeapon>().weaponIsDrawn)
                {
                    //Punch
                    PlayerController.MovementControl.anim.SetTrigger("skill__Punch");
                    if (GetComponent<SpeedCalculator>().PlayerCurrentSpeed == 0f)
                    {
                        GetComponent<MovementController>().setCantMove();
                    }
                }
            }
        }
    }
    
    public void prepareToAttack(String specificAttack = null)
    {
        //uncrounch
        PlayerController.MovementControl.crounched = false;
        PlayerController.MovementControl.anim.SetBool("Crounched", false);
        PlayerController.MovementControl.anim.SetTrigger("CanAttack");

        if (specificAttack != null)
        {
            PlayerController.MovementControl.anim.SetTrigger(specificAttack);
        }
    }

    
    
    public void SetIsAttacking()
    {
        isAttacking = true;
    }

    public void SetIsNotAttacking()
    {
        isAttacking = false;
    }

    
    
    
    
    
    
    
    
    
    
    
    
    public void AttackChain()
    {
        PlayerController.MovementControl.MovementSpeed = 0f;
        PlayerController.MovementControl.desiredRotationSpeed = 0.02f;
    }


    public void StopAttackChain()
    {
        PlayerController.MovementControl.MovementSpeed = 5;
        PlayerController.MovementControl.desiredRotationSpeed = 0.25f;
        SetIsNotAttacking();
    }
    
    
    
    
    
    public void dealDamage(float bonusDMG = 1)
    {
        if (GetComponent<DrawingWeapon>().weaponIsDrawn)
        {
            foreach (GameObject Enemy in GetComponent<PlayerController>().getEnemiesFromCloseWeaponArea.Enemies)
            {
                //Weapon / magic
                attackImpact(Enemy,bonusDMG);
            }
        }
        else
        {
            foreach (GameObject Enemy in GetComponent<PlayerController>().getEnemiesFromPunchArea.Enemies)
            {
                //Punch
                attackImpact(Enemy,bonusDMG);
            }
        }
    }

    public void attackImpact(GameObject target, float bonusDMG = 1f, bool ignoreWeaponDMG = false, string source = "")
    {
        if(target != null && !target.GetComponent<EnemyController>().isDead)
        {
            target.GetComponent<EnemyController>().TakeDamage(calculateDamage(bonusDMG,ignoreWeaponDMG), source);
            PlayerController.Player.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        }
    }

    public int calculateDamage(float bonusDMG = 1, bool ignoreWeaponDMG = false)
    {
        if (bonusDMG <= 0)
        {
            bonusDMG = 1;
        }


        int Damage;
        if (GetComponent<DrawingWeapon>().weaponIsDrawn)
        {
            Damage = (int) Mathf.Ceil(Random.Range(GetComponent<PlayerController>().DMG/3, GetComponent<PlayerController>().DMG) * bonusDMG);
        }
        else
        {
            //check if has inactive weapon equipped
            if (GetComponent<Equipment>().Weapon.GetComponent<ItemUI>().invItem.ID != 0)
            {
                int WeaponDamage = GetComponent<Equipment>().Weapon.GetComponent<ItemUI>().invItem.damage;
                int DamageWithoutWeapon = GetComponent<PlayerController>().DMG - WeaponDamage;
                Damage = (int)Mathf.Ceil(Random.Range(DamageWithoutWeapon/3, DamageWithoutWeapon) * bonusDMG);
            }
            else
            {
                //if has no equipped weapon
                Damage = (int)(Random.Range(GetComponent<PlayerController>().DMG/3, GetComponent<PlayerController>().DMG) * bonusDMG);
            }
        }

        if (ignoreWeaponDMG)
        {
            int WeaponDamage = GetComponent<Equipment>().Weapon.GetComponent<ItemUI>().invItem.damage;
            int DamageWithoutWeapon = GetComponent<PlayerController>().DMG - WeaponDamage;
            Damage = (int)Mathf.Ceil(Random.Range(DamageWithoutWeapon/3f, DamageWithoutWeapon) * bonusDMG);
        }

        //Critical hit
        if (GetComponent<PlayerController>().PRECISION > Random.Range(0, 200))
        {
            Damage *= 2;
            Debug.Log("Critical strike");
        }
        
        if (Damage <= 0)
        {
            return  1;
        }
        else
        {
            return Damage;
        }
    }
}
