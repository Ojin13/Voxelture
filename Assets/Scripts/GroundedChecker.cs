using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundedChecker : MonoBehaviour
{
    
    public List<GameObject> CollisionsObjects = new List<GameObject>();
    public float CollisionCount;
    public float startTime;
    public float fallingTime;
    public float timeSinceLevelLoad;

    // Update is called once per frame
    void Update()
    {
        timeSinceLevelLoad = Time.timeSinceLevelLoad;
        //collider can be deleted from enemy on its death - check if collider exist
        if (CollisionsObjects.Count > 0)
        {
            foreach (var objectInCollision in CollisionsObjects)
            {
                if (objectInCollision == null)
                {
                    CollisionsObjects.Remove(objectInCollision);
                    CollisionCount--;
                    
                    if(CollisionCount <= 0)
                    {
                        startTime = timeSinceLevelLoad;
                    }
                    continue;
                }
            
                bool hasCollider = false;
                if (objectInCollision.GetComponent<BoxCollider>() != null) {hasCollider = true;}
                if(objectInCollision.GetComponent<CapsuleCollider>() != null) { hasCollider = true;}
                if(objectInCollision.GetComponent<MeshCollider>() != null) { hasCollider = true;}
                if(objectInCollision.GetComponent<SphereCollider>() != null) { hasCollider = true;}
                if(objectInCollision.GetComponent<TerrainCollider>() != null) { hasCollider = true;}

                if (!hasCollider)
                {
                    CollisionsObjects.Remove(objectInCollision);
                    CollisionCount--;
                }
            }
        }
        
        
        if (CollisionCount <= 0)
        {
            if(GetComponent<distanceFromGround>().distanceToGround >= 0.1f && PlayerController.MovementControl.anim != null) //když se jde z kopce hráč se dotýká země jen občas
            {
                PlayerController.MovementControl.anim.SetBool("Grounded", false);
            }
        }
        else
        {
            if (PlayerController.MovementControl.anim != null)
            {
                PlayerController.MovementControl.anim.SetBool("Grounded", true);
            }
        }
    }
 

    public void OnCollisionExit(Collision collision)
    {
        CollisionCount--;
        CollisionsObjects.Remove(collision.transform.gameObject);
        if (CollisionCount <= 0)
        {
            startTime = timeSinceLevelLoad;
        }
    }

    public void OnCollisionEnter(Collision collision)
    {
        CollisionCount++;
        CollisionsObjects.Add(collision.transform.gameObject);

        if (CollisionCount == 1)
        {
            fallingTime = timeSinceLevelLoad - startTime;
            GetComponent<FallDamage>().calculateFallDamage(fallingTime);
        }
    }
}
