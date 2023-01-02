using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyList : MonoBehaviour
{
    public static enemyList enemyController;
    public JSON_enemyList enemiesFromJSON = new JSON_enemyList();


    // Start is called before the first frame update
    void Awake()
    {
        enemyController = this;

        TextAsset asset = Resources.Load("JSON/enemies") as TextAsset;
        if (asset != null)
        {
            enemiesFromJSON = JsonUtility.FromJson<JSON_enemyList>(asset.text);
        }
        else
        {
            Debug.Log("Loading enemies from JSON failed!");
        }
    }
}