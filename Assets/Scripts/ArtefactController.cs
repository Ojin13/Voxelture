using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArtefactController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (GetComponent<MovementController>().anim != null)
        {
            if (GetComponent<MovementController>().anim.GetBool("Crounched") == false)
            {
                GetComponent<InvisibleController>().setVisible();
            }   
        }
    }
    
    public void checkSpecialEffectsOfItem(InventoryItem isSpecialItem, string action)
    {
        if (isSpecialItem.ID == 7 || isSpecialItem.name == "The one ring")
        {
            if (action == "unequip")
            {
                GetComponent<InvisibleController>().setVisible();
            }
            
            if (action == "equip" && GetComponent<MovementController>().crounched)
            {
                GetComponent<InvisibleController>().SetInvisible();
            }
        }
    }
}
