using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour
{
    public static DropItem dropController;
    public GameObject nextDrop;


    // Start is called before the first frame update
    void Awake()
    {
        dropController = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void drop(InventoryItem itemToDrop=null, GameObject dropedBy=null)
    {
        if(itemToDrop != null)
        {
            //Create bag of loot
            nextDrop = Instantiate(SaveLootController.SaveLootControl.chooseModel(itemToDrop), PlayerController.Player.transform.position + new Vector3(Random.Range(-1, 1), Random.Range(0.5f, 1.5f), Random.Range(-1, 1)), Quaternion.identity);
            Inventory.inventory.copyItem(null, itemToDrop, 0);
        }
        else
        {
            //Choose random item from possible loot from enemy
            int amountOfPossibleDrops = dropedBy.GetComponent<EnemyController>().DROPS.Length-1;
            int chooseRandomDrop = Random.Range(0, amountOfPossibleDrops);

            //add choosen loot to enemy dead body
            Item itemFromDeadBody = dropedBy.AddComponent<Item>();
            itemFromDeadBody.ID = dropedBy.GetComponent<EnemyController>().DROPS[chooseRandomDrop];
            itemFromDeadBody.initializeItem();
            dropedBy.GetComponent<EnemyController>().LootParticles.SetActive(true);
        }
    }
}
