using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class scrollController : MonoBehaviour
{
    RectTransform rt;




    // Start is called before the first frame update
    void Start()
    {
        //rt.anchoredPosition = new Vector2(191.085f, rt.anchoredPosition.y - (Inventory.inventory.InventoryItems.Count / 2 * 90) - 90);
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(rt.anchoredPosition.y - (Inventory.inventory.InventoryItems.Count / 2 * 90));
        rt = GetComponent<RectTransform>();
        rt.sizeDelta = new Vector2(382, 20+(90 * Inventory.inventory.InventoryItems.Count));
    }
}