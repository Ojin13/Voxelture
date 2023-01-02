using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectInvisiblePlayer : MonoBehaviour
{
    public void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            transform.parent.GetComponent<EnemyNavigator>().playerDetected = true;
        }
    }
}
