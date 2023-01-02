using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class EnemyNavigator : MonoBehaviour
{
    public bool playerDetected;
    public NavMeshAgent agent;
    public Vector3 spawnLocation;
    public bool idlingAround;
    private bool uniqueAction;
    public bool canStop;
    public float distanceFromPlayer;
    public float distanceFromSpawnPoint;
    public bool enemyIsGettingHit;
    public bool nigerundaio;
    
    // Start is called before the first frame update
    void Awake()
    {
        spawnLocation = this.transform.position;
        agent = GetComponent<NavMeshAgent>();
    }

    void Start()
    {
        snapToNavMesh();
    }
    // Update is called once per frame
    void Update()
    {
        //spawned outside navmesh
        if (agent.isOnNavMesh == false)
        {
            Destroy(gameObject);    
        }
        
        if (GetComponent<EnemyController>().friendly == false)
        {
            if (playerDetected)
            {
                if (!PlayerController.Player.GetComponent<GetClosestEnemy>().playerFollowedBy.Contains(gameObject))
                {
                    PlayerController.Player.GetComponent<GetClosestEnemy>().playerFollowedBy.Add(gameObject);
                }
            }
            else
            {
                if (PlayerController.Player.GetComponent<GetClosestEnemy>().playerFollowedBy.Contains(gameObject))
                {
                    PlayerController.Player.GetComponent<GetClosestEnemy>().playerFollowedBy.Remove(gameObject);
                }
            }
            
            distanceFromSpawnPoint = Vector3.Distance(transform.position, spawnLocation);
            distanceFromPlayer = Vector3.Distance(transform.position, PlayerController.Player.transform.position);

            if (distanceFromPlayer >= 18)
            {
                if(playerDetected)
                {
                    if(distanceFromPlayer <= 40 && distanceFromSpawnPoint <= 120)
                    {
                        playerDetected = true;
                    }
                    else
                    {
                        playerDetected = false;
                    }
                }
                else
                {
                    playerDetected = false;
                }
            }
            else
            {
                if (!PlayerController.MovementControl.crounched)
                {
                    playerDetected = true;
                }
                else
                {
                    if (GetComponentsInChildren<EnemyFOVController>() != null)
                    {
                        EnemyFOVController[] FOV = GetComponentsInChildren<EnemyFOVController>();
                        foreach (EnemyFOVController EnemyFOV in FOV)
                        {
                            if (EnemyFOV.seePlayer)
                            {
                                playerDetected = true;
                            }
                        }
                    }
                }
            }

            

            if (playerDetected)
            {
                idlingAround = false;
                if (distanceFromPlayer > GetComponent<NavMeshAgent>().stoppingDistance)
                {
                    GetComponent<EnemyController>().anim.ResetTrigger("CanAttack");
                    if (!GetComponent<EnemyAttack>().isAttacking && !enemyIsGettingHit)
                    {
                        agent.SetDestination(PlayerController.Player.transform.position);
                        GetComponent<EnemyController>().anim.SetBool("CanMove", true);
                    }
                    else
                    {
                        agent.SetDestination(transform.position);
                    }
                }
                else
                {
                    agent.SetDestination(transform.position);
                    GetComponent<EnemyController>().anim.SetBool("CanMove", false);
                    if(!PlayerController.Player.GetComponent<PlayerController>().isDead)
                    {
                        GetComponent<EnemyController>().anim.SetTrigger("CanAttack");
                    }

                    lookAtPlayer();
                }
            }
            else
            {
                if(distanceFromSpawnPoint >= 90)
                {
                    GetComponent<EnemyController>().anim.SetBool("CanMove", true);
                    agent.SetDestination(spawnLocation);
                }
                else
                {
                    if(idlingAround == false)
                    {
                        idlingAround = true;
                        Invoke("idleAround", Random.Range(5,10));
                    }
                    else
                    {
                        if (agent.remainingDistance <= agent.stoppingDistance)
                        {
                            GetComponent<EnemyController>().anim.SetBool("CanMove", false);
                            
                            if(!canStop)
                            {
                                canStop = true;
                                Invoke("disableIdlingAround", 8);
                            }
                        }
                        else
                        {
                            GetComponent<EnemyController>().anim.SetBool("CanMove", true);
                        }
                    }
                }
            }
        }
        else
        {
            
            float distanceFromPlayer = Vector3.Distance(PlayerController.Player.transform.position, transform.position);

            if (nigerundaio && distanceFromPlayer <= 18)
            {
                nigerundaio = false;
                GetComponent<Animator>().SetBool("Run", false);
                GetComponent<Animator>().SetTrigger("Chillin");
            }

            if (nigerundaio)
            {
                GetComponent<Animator>().SetBool("Run", true);
            }
                
            
            if (nigerundaio)
            {
                idlingAround = true;
                GetComponent<EnemyController>().anim.SetBool("CanMove", true);

                if (!agent.hasPath)
                {
                    idleAround();
                }
                
                if (agent.remainingDistance <= agent.stoppingDistance)
                {
                    idleAround();
                }
            }
            else
            {
                if(idlingAround == false)
                {
                    idlingAround = true;
                    Invoke("idleAround", Random.Range(5,10));
                }
                else
                {
                    if (agent.hasPath)
                    {
                        if (agent.remainingDistance <= agent.stoppingDistance)
                        {
                            GetComponent<EnemyController>().anim.SetBool("CanMove", false);
                            idlingAround = false;
                        }
                        else
                        {
                            GetComponent<EnemyController>().anim.SetBool("CanMove", true);
                        }
                    }
                }
            }
            
        }
    }
    
    
    
    
    
    
    

    public void disableIdlingAround() { idlingAround = false; canStop = false; }

    
    
    void snapToNavMesh()
    {
        //snap to closest edge - cast ray from height to prevent spawning INSIDE structures
        NavMeshHit hit;

        if (NavMesh.SamplePosition(transform.position+new Vector3(0,200,0), out hit, 250f, NavMesh.AllAreas))
        {
            agent.Warp(hit.position);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    
    

    public void lookAtPlayer()
    {
        //look at player
        Vector3 targetPostition = new Vector3(PlayerController.Player.transform.position.x, this.transform.position.y, PlayerController.Player.transform.position.z);
        var targetRotation = Quaternion.LookRotation(targetPostition - transform.position);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 5 * Time.deltaTime);
    }

    public void idleAround()
    {
        if(!playerDetected || GetComponent<EnemyController>().friendly)
        {
            Vector3 idlePoint;
            int RandomX = Random.Range(-10, 10);
            int RandomZ = Random.Range(-10, 10);

            if (RandomX < 2)
            {
                if (RandomX >= 0)
                {
                    RandomX = 5;
                }
                else
                {
                    if (RandomX < -2)
                    {
                        RandomX = -5;
                    }
                }
            }

            if (RandomZ < 2)
            {
                if (RandomZ >= 0)
                {
                    RandomZ = 5;
                }
                else
                {
                    if (RandomZ < -2)
                    {
                        RandomZ = -5;
                    }
                }
            }
            
            idlePoint = new Vector3(transform.position.x+RandomX,transform.position.y,transform.position.z+RandomZ);
            GetComponent<EnemyController>().anim.SetBool("CanMove", true);
            if (agent.isOnNavMesh)
            {
                agent.SetDestination(idlePoint);
            }
            else
            {
                snapToNavMesh();
            }
        }
    }

    public void isGettingHit()
    {
        GetComponent<EnemyAttack>().isAttacking = false;
        enemyIsGettingHit = true;
    }
    
    public void isNotGettingHit()
    {
        enemyIsGettingHit = false;
    }
}
