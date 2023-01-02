using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LootStation : MonoBehaviour
{
    void Start()
    {
        if (SaveGameController.GameSaver.lootLoaded == false)
        {
            int randomID = itemList.itemsController.itemsFromJSON.items[Random.Range(0, itemList.itemsController.itemsFromJSON.items.Count)].ID;
            GameObject randomItem = SaveLootController.SaveLootControl.chooseModel(null,randomID);
            
            //Spawn Random Item
            GameObject X = Instantiate(randomItem, transform.position,Quaternion.identity);
            X.GetComponent<Item>().ID = randomID;
            X.GetComponent<Item>().notFromSave = true;
            
            //Spawn random food
            spawn__food();
        }
    }

    public void spawn__food()
    {
        int randomID = itemList.itemsController.itemsFromJSON.items[Random.Range(0, itemList.itemsController.itemsFromJSON.items.Count)].ID;
        
        if (randomID >= 700)
        {
            GameObject randomFood = SaveLootController.SaveLootControl.chooseModel(null, randomID);

            GameObject X = Instantiate(randomFood,transform.position,Quaternion.identity);
            X.GetComponent<Item>().ID = randomID;
            X.GetComponent<Item>().notFromSave = true;
        }
        else
        {
            spawn__food();
        }
    }
}
