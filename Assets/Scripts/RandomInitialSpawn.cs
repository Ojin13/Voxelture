using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomInitialSpawn : MonoBehaviour
{
    public List<GameObject> spawnPositions;
    void Start()
    {
        foreach (var spawnPos in GetComponentsInChildren<Transform>())
        {
            if (spawnPos.gameObject != gameObject)
            {
                spawnPositions.Add(spawnPos.gameObject);
            }
        }
        
        //choose respawn Location
        if(SaveGameController.GameSaver.checkIfCanLoad() == false)
        {
            int RandomSpawn = Random.Range(0, spawnPositions.Count-1);
            PlayerController.Player.transform.position = spawnPositions[RandomSpawn].transform.position+new Vector3(0,5,0);
        }
        else
        {
            //load save
            return;
        }
    }
}
