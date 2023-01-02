using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SilentKill : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void prepareSilentKill()
    {
        if (GetComponent<DrawingWeapon>().weaponIsDrawn && GetComponent<MovementController>().crounched)
        {
            PlayerController.MovementControl.anim.SetTrigger("skill__Slilent Kill");
        }
    }
    
    
    public void silentKill(GameObject target = null)
    {
        if (target == null)
        {
            target = GetComponent<GetClosestEnemy>().HandCombat__closestEnemy;    
        }

        if (target != null)
        {
            if (target.GetComponent<EnemyNavigator>().playerDetected == false)
            {
                target.GetComponent<EnemyController>().HP -= (int)Mathf.Ceil(GetComponent<PlayerController>().DMG * 100);
            }
        }
        
        GetComponent<InvisibleController>().setVisible();
    }
}
