using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject Explosion;
    // Start is called before the first frame update
    void Start()
    {
        Invoke("SelfDestruction", 3);
    }

    // Update is called once per frame
    void Update()
    {
                
    }


    public void OnCollisionEnter(Collision target)
    {
        if (target.gameObject.transform.parent != null)
        {
            if (target.gameObject.transform.parent.gameObject.CompareTag("Enemy"))
            {
                PlayerController.Player.GetComponent<Attack>().attackImpact(target.gameObject.transform.parent.gameObject,3,true, "magic");
            }   
        }
        
        if(!target.gameObject.CompareTag("PlayerCollider"))
        {
            Instantiate(Explosion, transform.position, Quaternion.identity);
            Destroy(gameObject);
        }
    }

    public void SelfDestruction()
    {
        Instantiate(Explosion, transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
