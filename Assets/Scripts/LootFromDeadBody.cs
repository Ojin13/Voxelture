using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootFromDeadBody : MonoBehaviour
{
    public bool canLoot;
    public GameObject E_symbol_UI;
    public GameObject lootedCopse;
    public bool isLooting;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (lootedCopse != null)
        {
            if (lootedCopse.GetComponent<Item>() != null)
            {
                E_symbol_UI.SetActive(true);
            }
            else
            {
                E_symbol_UI.SetActive(false);
            }
        
            loot();   
        }
    }



    public void loot()
    {
        if(Input.GetKeyDown(KeyCode.E))
        {
            if(canLoot && PlayerController.MovementControl.canMove && lootedCopse.GetComponent<Item>() != null)
            {
                PlayerController.MovementControl.anim.SetTrigger("CanLoot");
                PlayerController.MovementControl.canMove = false;
                isLooting = true;
            }
        }
    }


    public void OnCollisionEnter(Collision col)
    {
        if (col.transform.parent != null)
        {
            if (col.transform.parent.tag == "Enemy" && col.transform.parent.GetComponent<EnemyController>().isDead)
            {
                canLoot = true;
                lootedCopse = col.transform.parent.gameObject;
            }
        }
    }


    public void OnCollisionExit(Collision col)
    {
        if (col.transform.parent != null)
        {
            if (col.transform.parent.tag == "Enemy" && col.transform.parent.GetComponent<EnemyController>().isDead)
            {
                canLoot = false;
                if (!isLooting)
                {
                    lootedCopse = null;
                }
            }
        }
    }
    
    public void takeItemFromCorpse()
    {
        if (lootedCopse != null)
        {
            if (lootedCopse.GetComponent<Item>() != null)
            {
                Debug.Log("Looting dead body");
                Inventory.inventory.addItem(lootedCopse.GetComponent<Item>());
                Destroy(lootedCopse.GetComponent<EnemyController>().LootParticles);
                Destroy(lootedCopse.GetComponent<Item>());   
            }
            else
            {
                //enemy has no loot
                Debug.Log("Enemy corpse has no loot");
            }
        }

        isLooting = false;
    }
}
