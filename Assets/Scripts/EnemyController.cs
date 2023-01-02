using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyController : MonoBehaviour
{
    public int ID;
    public bool isDead = false;
    public string enemyName;
    public string enemyType;
    public string enemyDescription;
    public int HP;
    public int MAXHP;
    public int DMG;
    public int LEVEL;
    public int[] DROPS;
    public int EXP;
    public Texture image;
    public GameObject BloodType;
    public GameObject LootParticles;
    public Animator anim;
    public bool notFromSave;
    public bool superOrigin;
    public int spawnerID;
    public GameObject UIinfo;
    public GameObject UIinfoPrefab;
    public bool friendly;

    // Start is called before the first frame update
    void Awake()
    {
        anim = GetComponent<Animator>();
        InvokeRepeating("RandomSound",Random.Range(5,15),Random.Range(10,30));
    }


    void Start()
    {
        if (EnemySaveController.enemySaveControl.Enemies.Contains(gameObject) == false)
        {
            EnemySaveController.enemySaveControl.Enemies.Add(gameObject);
        }

        if (notFromSave)
        {
            Invoke("checkForDestroyConditions",0.01f);
            enemyType = getEnemyStatsFromID(ID).enemyType;


            int levelRange = Random.Range(getEnemyStatsFromID(ID).LEVEL, (int)(getEnemyStatsFromID(ID).LEVEL+15));
            int levelIncrement = (int)(levelRange - getEnemyStatsFromID(ID).LEVEL);
            float statBooster = 1;

            for (int x = 0; x < levelIncrement; x++)
            {
                statBooster += 0.1f;
            }

            if (DMG == 0)
            {
                DMG = (int)(getEnemyStatsFromID(ID).DMG*statBooster);
            }

            if (MAXHP == 0)
            {
                MAXHP = (int)(getEnemyStatsFromID(ID).MAXHP*statBooster);
                if (ID == 10)
                {
                    MAXHP = 1;
                }
            }
            
            if (HP == 0)
            {
                HP = MAXHP;
            }

            if (enemyName == "Uknown enemy" || enemyName == "")
            {
                enemyName = getEnemyStatsFromID(ID).name;
            }
            
            if (enemyDescription == "")
            {
                enemyDescription = getEnemyStatsFromID(ID).enemyDescription;
            }
            
            if (image == null)
            {
                image = (Texture) Resources.Load(getEnemyStatsFromID(ID).imagePath);
            }
            
            if (LEVEL == 0)
            {
                LEVEL = levelRange;
            }

            if (DROPS.Length == 0)
            {
                DROPS = getEnemyStatsFromID(ID).DROPS;
            }
            
            if (EXP == 0)
            {
                EXP = (int)(getEnemyStatsFromID(ID).EXP*statBooster);;
            }
        }
        
        //Create UI
        Vector3 spawnPosition = new Vector3(transform.position.x,GetComponentInChildren<SkinnedMeshRenderer>().bounds.max.y+0.5f,transform.position.z);
        UIinfo = Instantiate(UIinfoPrefab,spawnPosition,Quaternion.identity);
        UIinfo.GetComponent<CreatureUI>().Enemy = gameObject;
        if (CreatureUI.canvas != null)
        {
            UIinfo.transform.parent = CreatureUI.canvas.transform;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (HP <= 0 && isDead == false)
        {
            isDead = true;
            PlayerController.Player.GetComponent<GetClosestEnemy>().playerFollowedBy.Remove(gameObject);
            gameObject.GetComponent<Die>().DiePlease(this.gameObject);

            if (EnemySaveController.enemySaveControl.Enemies.Contains(gameObject))
            {
                EnemySaveController.enemySaveControl.Enemies.Remove(gameObject);
            }
        }

        if (Vector3.Distance(transform.position, PlayerController.Player.transform.position) > 500)
        {
            GetComponentInChildren<SkinnedMeshRenderer>().enabled = false;
            GetComponent<Animator>().enabled = false;
            GetComponent<AudioSource>().enabled = false;
            if (GetComponent<EnemyNavigator>() != null)
            {
                GetComponent<EnemyNavigator>().enabled = false;
            }
            GetComponent<NavMeshAgent>().enabled = false;
        }
        else
        {
            GetComponentInChildren<SkinnedMeshRenderer>().enabled = true;
            GetComponent<Animator>().enabled = true;
            GetComponent<AudioSource>().enabled = true;
            
            if (GetComponent<EnemyNavigator>() != null)
            {
                GetComponent<EnemyNavigator>().enabled = true;
            }

            if (GetComponent<NavMeshAgent>() != null)
            {
                GetComponent<NavMeshAgent>().enabled = true;
            }
        }
    }
    
    
    public JSON_enemyTemplate getEnemyStatsFromID(int ID)
    {
        foreach (JSON_enemyTemplate JSON_enemy_info in enemyList.enemyController.enemiesFromJSON.enemies)
        {
            if (ID == JSON_enemy_info.ID)
            {
                return JSON_enemy_info;
            }
            else
            {
                continue;
            }
        }
        return null;
    }

    public void TakeDamage(int damage, String source = null)
    {

        if (friendly)
        {
            GetComponent<EnemyNavigator>().nigerundaio = true;
        }
        
        if ((Random.Range(0, 100) > 40-PlayerController.PlayerControl.PRECISION) || source == "magic")
        {
            anim.SetTrigger("GetHit");
            HP -= damage;
            if (GetComponent<CutBodyPart>() != null)
            {
                GetComponent<CutBodyPart>().CutPartOfBody(gameObject, PlayerController.PlayerControl.PRECISION/2, false);
            }
            if (!GetComponent<EnemyController>().isDead)
            {
                anim.SetTrigger("GetHit");
                NewDamageText.DamageTextController.newText(damage,transform.position,gameObject);
                GetComponent<SoundPlayer>().sound_enemyHurt();
                GetComponent<EnemyNavigator>().playerDetected = true;
                
                //Try to alert other
                if (spawnerID != 0)
                {
                    List<GameObject> allies = Spawner.SpawnerControl.getSpawner(spawnerID).alreadySpawned;
                    
                    foreach (var ally in allies)
                    {
                        if (Vector3.Distance(ally.transform.position, gameObject.transform.position) <= 6.5f)
                        {
                            if (ally.GetComponent<EnemyNavigator>() != null)
                            {
                                ally.GetComponent<EnemyNavigator>().playerDetected = true;
                            }
                        }
                    }
                }
            }
        }
        else
        {
            NewDamageText.DamageTextController.newText(0,transform.position,gameObject,"MISS");
        }
        
    }

    public void checkForDestroyConditions()
    {
        if (SaveGameController.GameSaver.enemiesLoaded && superOrigin)
        {
            EnemySaveController.enemySaveControl.Enemies.Remove(gameObject);
            Destroy(gameObject);
        }
    }

    public void RandomSound()
    {
        GetComponent<SoundPlayer>().sound_enemyRandomSound();
    }
}
