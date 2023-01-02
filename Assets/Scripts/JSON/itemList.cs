using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class itemList : MonoBehaviour
{
    public static itemList itemsController;
    public JSON_itemList itemsFromJSON = new JSON_itemList();


    // Start is called before the first frame update
    void Awake()
    {
        itemsController = this;

        TextAsset asset = Resources.Load("JSON/items") as TextAsset;
        if (asset != null)
        {
            itemsFromJSON = JsonUtility.FromJson<JSON_itemList>(asset.text);
        }
        else
        {
            Debug.Log("Loading items from JSON failed!");
        }
    }
}
