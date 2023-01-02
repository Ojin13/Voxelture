using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class Shooting : MonoBehaviour
{
    public GameObject fireball;
    public GameObject spawnPoint;
    public Skill FireFist;

    //Fireball no jutsu
    public void Katon__fireball()
    {
        if (FireFist.unlocked)
        {
            GameObject katon = Instantiate(fireball, spawnPoint.transform.position, Quaternion.Euler(-90,0,PlayerController.Player.transform.eulerAngles.y));
            PlayerController.Player.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
            katon.GetComponent<Rigidbody>().AddForce(transform.forward*1000);   
        }
    }
}