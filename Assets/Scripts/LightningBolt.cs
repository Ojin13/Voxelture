using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class LightningBolt : MonoBehaviour
{
    public GameObject startPos;
    public GameObject endPos;
    public GameObject Explosion;

    // Start is called before the first frame update
    void Start()
    {
        PlayerController.Player.GetComponent<SoundPlayer>().sound_bolt();
        startPos.transform.position = PlayerController.Player.transform.position + new Vector3(0, 100, 0);

        if (PlayerController.Player.GetComponent<GetClosestEnemy>().isSet())
        {
            endPos.transform.position = PlayerController.Player.GetComponent<GetClosestEnemy>().RangeComat__closestEnemy.transform.position;    
        }
        else
        {
            RaycastHit hit;
            Vector3 fwd = gameObject.transform.forward;
            Debug.DrawRay(gameObject.transform.position+new Vector3(0,3,0), fwd * 50, Color.green);
            endPos.transform.position = PlayerController.Player.GetComponent<ForwardPosition>().forwardHit.transform.position;
        }

        Instantiate(Explosion, endPos.transform.position, Quaternion.identity);
        PlayerController.Player.GetComponent<CinemachineImpulseSource>().GenerateImpulse();
        Invoke("selfDestruction",0.5f);
        Invoke("dealLightingDamage",0.1f);
    }

    void selfDestruction()
    {
        Destroy(gameObject);
    }

    void dealLightingDamage()
    {
        foreach (GameObject target in endPos.GetComponent<GetEnemiesFromZone>().Enemies)
        {
            PlayerController.Player.GetComponent<Attack>().attackImpact(target.gameObject,10, true, "magic");
        }
    }
}
