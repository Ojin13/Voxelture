using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoKill : MonoBehaviour
{
    public void OnCollisionEnter(Collision obj)
    {
        if (obj.gameObject.CompareTag("Player"))
        {
            PlayerController.Player.GetComponent<Die>().DiePlease(PlayerController.Player, true);
        }
        else
        {
            Destroy(obj.gameObject);
        }
    }
}
