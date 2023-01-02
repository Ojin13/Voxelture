using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRing : MonoBehaviour
{
    public GameObject Ring;


    public void createRing()
    {
        GameObject createdRing = Instantiate(Ring, GetComponent<GetClosestEnemy>().RangeComat__closestEnemy.transform.position, Quaternion.identity);
        createdRing.GetComponent<FireRingExpolison>().target = GetComponent<GetClosestEnemy>().RangeComat__closestEnemy;
    }

    public bool canUse()
    {
        if (GetComponent<GetClosestEnemy>().RangeComat__closestEnemy != null && PlayerController.MovementControl.anim.GetBool("Grounded"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
