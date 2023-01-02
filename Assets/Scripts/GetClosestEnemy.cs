using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEngine;
using UnityEngine.XR;

public class GetClosestEnemy : MonoBehaviour
{
    public GameObject HandCombat__closestEnemy;
    public GameObject RangeComat__closestEnemy;
    public GameObject focusRing;
    public static bool playerIsBeingFollowedByEnemy;
    public List<GameObject> playerFollowedBy;
    public GameObject selectedEnemy;


    void Update()
    {
        if (playerFollowedBy.Count <= 0)
        {
            playerIsBeingFollowedByEnemy = false;
        }
        else
        {
            playerIsBeingFollowedByEnemy = true;
        }

        if (GetComponent<MovementController>().anim != null)
        {
            GetComponent<MovementController>().anim.SetBool("isBeingFollowedByEnemy", playerIsBeingFollowedByEnemy);
        }
        
        HandCombat__closestEnemy = getClosestEnemy(GetComponent<PlayerController>().getEnemiesFromCloseWeaponArea.Enemies, HandCombat__closestEnemy);
        RangeComat__closestEnemy = getClosestEnemy(GetComponent<PlayerController>().getEnemiesFromMagicArea.Enemies, RangeComat__closestEnemy);

        if (HandCombat__closestEnemy != null)
        {
            focusRing.transform.position = HandCombat__closestEnemy.transform.position;
            selectedEnemy = HandCombat__closestEnemy;
        }

        if (HandCombat__closestEnemy == null && RangeComat__closestEnemy != null)
        {
            focusRing.transform.position = RangeComat__closestEnemy.transform.position;
            selectedEnemy = RangeComat__closestEnemy;
        }

        if (RangeComat__closestEnemy == null && HandCombat__closestEnemy == null)
        {
            focusRing.SetActive(false);
            selectedEnemy = null;
        }
        else
        {
            focusRing.SetActive(true);
        }
    }
    
    
    public GameObject getClosestEnemy(List<GameObject> Enemies, GameObject CompareTo)
    {
        foreach (GameObject Enemy in Enemies)
        {
            if (Enemy != null)
            {
                if (!Enemy.GetComponent<EnemyController>().isDead)
                {
                    if (CompareTo == null)
                    {
                        return Enemy;
                    }

                    if (Vector3.Distance(Enemy.transform.position, transform.position) <= Vector3.Distance(CompareTo.transform.position, transform.position))
                    {
                        return Enemy;
                    }
                }
            }
        }

        if (Enemies.Count == 0)
        {
            return null;
        }
        
        return null;
    }

    public bool isSet()
    {
        if (HandCombat__closestEnemy == null && RangeComat__closestEnemy == null)
        {
            return false;
        }
        else
        {
            return true;
        }
    }



    public bool isClosest(GameObject entity)
    {
        if (entity == selectedEnemy)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

}
