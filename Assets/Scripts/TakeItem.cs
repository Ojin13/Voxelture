using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TakeItem : MonoBehaviour
{
    public GameObject currentObjectToTake;
    public GameObject E_SymbolUI;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(currentObjectToTake != null)
        {
            if(!UIController.UIControl.DialoguePanelIsActive)
            {
                E_SymbolUI.SetActive(true);    
            }
            
            if(Input.GetKeyDown(KeyCode.E))
            {
                Inventory.inventory.addItem(currentObjectToTake.GetComponent<Item>(), currentObjectToTake);
                SaveGameController.GameSaver.SaveGameData();
            }
        }
        else
        {
            E_SymbolUI.SetActive(false);
        }
    }



    public void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Item")
        {
            if(currentObjectToTake == null)
            {
                currentObjectToTake = collision.gameObject;
            }
        }
    }


    public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Item")
        {
            if (currentObjectToTake == collision.gameObject)
            {
                currentObjectToTake = null;
            }
        }
    }

}
