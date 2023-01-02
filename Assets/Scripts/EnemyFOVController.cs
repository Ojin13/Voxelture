using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFOVController : MonoBehaviour
{
    public bool seePlayer;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }



    public void OnTriggerExit(Collider collision)
    {
        if(collision.tag == "PlayerCollider")
        {
            seePlayer = false;
        }
    }

    public void OnTriggerStay(Collider collision)
    {
        if(collision.tag == "PlayerCollider")
        {
            if (PlayerController.Player.GetComponent<InvisibleController>().isInvisible == false)
            {
                seePlayer = true;
            }
        }
    }



}
