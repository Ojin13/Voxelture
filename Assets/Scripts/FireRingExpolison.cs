using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class FireRingExpolison : MonoBehaviour
{
    public GameObject target;

    public void Start()
    {
        Invoke("Bakuretsu",6.5f);
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            gameObject.transform.position = target.transform.position + new Vector3(0,1f,0f);
        }
    }

    public void Bakuretsu()
    {
        PlayerController.Player.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        PlayerController.Player.GetComponent<Attack>().attackImpact(target,5,true, "magic");
        SoundPlayer.PlayerSoundController.sound_explosion(GetComponent<AudioSource>());
    }
}
