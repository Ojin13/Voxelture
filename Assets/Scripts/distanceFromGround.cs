using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class distanceFromGround : MonoBehaviour
{

    public float distanceToGround;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        RaycastHit hit = new RaycastHit();
        if (PlayerController.MovementControl.anim != null)
        {
            if (Physics.Raycast(transform.position, Vector3.down, out hit))
            {
                distanceToGround = hit.distance;
                if(PlayerController.MovementControl.anim != null)
                {
                    PlayerController.MovementControl.anim.SetFloat("distanceFromGround", distanceToGround);
                }
            }
            else
            {
                distanceToGround = 1000;
                PlayerController.MovementControl.anim.SetFloat("distanceFromGround", distanceToGround);
            }   
        }
    }

    public void ignoreFreeFall()
    {
        GetComponent<MovementController>().anim.SetBool("ignoreFreeFall", true);
    }
    
    public void dontIgnoreFreeFall()
    {
        GetComponent<MovementController>().anim.SetBool("ignoreFreeFall", false);
    }
    
}
