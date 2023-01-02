using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyCanHitPlayer : MonoBehaviour
{

    public bool canHitPlayer;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void OnTriggerEnter(Collider col)
    {
        if (col.tag == "PlayerCollider")
        {
            canHitPlayer = true;
        }
    }


    public void OnTriggerExit(Collider col)
    {
        if (col.tag == "PlayerCollider")
        {
            canHitPlayer = false;
        }
    }

}
