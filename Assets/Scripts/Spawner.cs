using System;
using UnityEngine;
using System.Collections.Generic;
using Random = UnityEngine.Random;

// Starting in 2 seconds.
// a projectile will be launched every 0.3 seconds

public class Spawner : MonoBehaviour
{
    public GameObject CreatureToSpawn;
    public float spawnAfterSeconds;
    public int maxEntities;
    public int spawnerID;
    public List<GameObject> alreadySpawned;
    public static List<Spawner> Spawners = new List<Spawner>();
    public static Spawner SpawnerControl;
    public GameObject waitingForPlayerToLookAway;
    void Awake()
    {

        SpawnerControl = this;
        
        if (Spawners.Contains(this) == false)
        {
              Spawners.Add(this);  
        }
        
        InvokeRepeating("SpawnMob", spawnAfterSeconds, spawnAfterSeconds);
    }

    public void Update()
    {
        if (waitingForPlayerToLookAway != null)
        {
            if (!ActiveMap.ActiveMapControl.isOnCamera(waitingForPlayerToLookAway))
            {
                waitingForPlayerToLookAway.SetActive(true);
                waitingForPlayerToLookAway = null;
            }
        }
    }

    void SpawnMob()
    {
        checkEmptyEntitis();
        
        if (alreadySpawned.Count < maxEntities)
        {
            GameObject entity = Instantiate(CreatureToSpawn, this.transform.position+new Vector3(Random.Range(-100,100),0,Random.Range(-100,100)), Quaternion.identity);
            
            //if spawn point is in players view field, wait for player to look away
            if (ActiveMap.ActiveMapControl.isOnCamera(entity))
            {
                entity.SetActive(false);
                if (waitingForPlayerToLookAway == null)
                {
                    waitingForPlayerToLookAway = entity;
                }
                else
                {
                    //force activation - player will see enemy to appear out of the air
                    waitingForPlayerToLookAway.SetActive(true);
                    waitingForPlayerToLookAway = entity;
                }
            }
            
            saveSpawnedEntity(entity);
        }
    }

    public void saveSpawnedEntity(GameObject entity)
    {
        entity.GetComponent<EnemyController>().notFromSave = true;
        entity.GetComponent<EnemyController>().spawnerID = spawnerID;
        alreadySpawned.Add(entity);
    }

    public void checkEmptyEntitis()
    {
        foreach (var soul in alreadySpawned)
        {
            if (soul == null)
            {
                alreadySpawned.Remove(soul);
                continue;
            }
            
            if (soul.GetComponent<EnemyController>().isDead)
            {
                alreadySpawned.Remove(soul);
            }
        }
    }

    public void LoadSpawnedEnemiesFromSave(EnemyController soul)
    {
        if (soul.spawnerID == spawnerID)
        {
            alreadySpawned.Add(soul.gameObject);
        }
    }

    public Spawner getSpawner(int ID)
    {
        foreach (var spawner in Spawners)
        {
            if (spawner.spawnerID == ID)
            {
                return spawner;
            }
        }

        return null;
    }
}