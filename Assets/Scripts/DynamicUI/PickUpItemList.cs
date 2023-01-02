using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PickUpItemList : MonoBehaviour
{
    public GameObject PickupItemUI;

    public void createPickUpItem(InventoryItem item)
    {
        GameObject createdUI = Instantiate(PickupItemUI, gameObject.transform.position, Quaternion.identity);
        createdUI.transform.parent = gameObject.transform;

        createdUI.GetComponentInChildren<TextMeshProUGUI>().text = "+ "+item.name;
        createdUI.GetComponentInChildren<RawImage>().texture = (Texture)Resources.Load(Inventory.inventory.getItemSpecsFromID(item.ID).imagePath);
        
        StartCoroutine(destoryUI(createdUI, 3f));
    }

    IEnumerator destoryUI(GameObject element,float delayTime)
    {
        yield return new WaitForSeconds(delayTime);
        Destroy(element);
    }
}
