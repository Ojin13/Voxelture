using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPotion : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            if(!PlayerController.Player.GetComponent<PlayerController>().isDead)
            {
                if((PlayerController.Player.GetComponent<PlayerController>().HP + 10) >= PlayerController.Player.GetComponent<PlayerController>().MAXHP)
                {
                    PlayerController.Player.GetComponent<PlayerController>().HP = PlayerController.Player.GetComponent<PlayerController>().MAXHP;
                }
                else
                {
                    PlayerController.Player.GetComponent<PlayerController>().HP += 10;
                }
            }
        }
    }
}
