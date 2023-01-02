using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySaveController : MonoBehaviour
{

    public static EnemySaveController enemySaveControl;
    public GameObject[] EnemiesPrefabs;
    public List<GameObject> Enemies;

    void Awake()
    {
        enemySaveControl = this;
    }

    public EnemySerializable convert__EnemyToSerializable(EnemyController enemy)
    {
        EnemySerializable data = new EnemySerializable();

        data.originalSpawnPosition[0] = enemy.gameObject.GetComponent<EnemyNavigator>().spawnLocation.x;
        data.originalSpawnPosition[1] = enemy.gameObject.GetComponent<EnemyNavigator>().spawnLocation.y;
        data.originalSpawnPosition[2] = enemy.gameObject.GetComponent<EnemyNavigator>().spawnLocation.z;
        data.enemyPosition[0] = enemy.transform.position.x;
        data.enemyPosition[1] = enemy.transform.position.y;
        data.enemyPosition[2] = enemy.transform.position.z;
        data.ID = enemy.ID;
        data.isDead = enemy.isDead;
        data.enemyName = enemy.enemyName;
        data.enemyType = enemy.enemyType;
        data.enemyDescription = enemy.enemyDescription;
        data.HP = enemy.HP;
        data.MAXHP = enemy.MAXHP;
        data.DMG = enemy.DMG;
        data.LEVEL = enemy.LEVEL;
        data.DROPS = enemy.DROPS;
        data.EXP = enemy.EXP;
        data.spawnerID = enemy.spawnerID;
        data.playerDetected = enemy.gameObject.GetComponent<EnemyNavigator>().playerDetected;
        return data;
    }
    
    public void LoadEnemyFromSave(EnemySerializable data)
    {
        GameObject enemy = null;

        foreach (var enemyModel in EnemiesPrefabs)
        {
            if (data.ID == enemyModel.GetComponent<EnemyController>().ID)
            {
                enemy = enemyModel;
                break;
            }
        }
        
        if (enemy == null)
        {
            Debug.LogError("Enemy null couldnt be loaded! Probably missing prefab.");
            return;
        }
        
        enemy = Instantiate(enemy, new Vector3(data.enemyPosition[0], data.enemyPosition[1], data.enemyPosition[2]), Quaternion.identity);
        
        enemy.GetComponent<EnemyNavigator>().spawnLocation = new Vector3(data.originalSpawnPosition[0],data.originalSpawnPosition[1],data.originalSpawnPosition[2]);
        enemy.GetComponent<EnemyController>().ID = data.ID;
        enemy.GetComponent<EnemyController>().isDead = data.isDead;
        enemy.GetComponent<EnemyController>().enemyName = data.enemyName;
        enemy.GetComponent<EnemyController>().enemyType = data.enemyType;
        enemy.GetComponent<EnemyController>().enemyDescription = data.enemyDescription;
        enemy.GetComponent<EnemyController>().HP = data.HP;
        enemy.GetComponent<EnemyController>().MAXHP = data.MAXHP;
        enemy.GetComponent<EnemyController>().DMG = data.DMG;
        enemy.GetComponent<EnemyController>().LEVEL = data.LEVEL;
        enemy.GetComponent<EnemyController>().DROPS = data.DROPS;
        enemy.GetComponent<EnemyController>().EXP = data.EXP;
        enemy.GetComponent<EnemyController>().spawnerID = data.spawnerID;
        enemy.GetComponent<EnemyNavigator>().playerDetected = data.playerDetected;
        
        //assign enemy to its spawner
        foreach (var spawner in Spawner.Spawners)
        {
            spawner.LoadSpawnedEnemiesFromSave(enemy.GetComponent<EnemyController>());
        }
    }
}
