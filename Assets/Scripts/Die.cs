using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Die : MonoBehaviour
{
    public bool hasRagdoll;
    public Collider col_off;
    public Collider col_on;
    
    // Start is called before the first frame update
    void Start()
    {
        if(PlayerController.Player.GetComponent<PlayerController>().isDead || PlayerController.Player.GetComponent<PlayerController>().HP <= 0)
        {
            DiePlease(PlayerController.Player, true);
        }

        if (gameObject.GetComponentsInChildren<Rigidbody>().Length >= 3)
        {
            hasRagdoll = true;
        }
        else
        {
            hasRagdoll = false;
        }
    }

    public void DiePlease(GameObject whoShouldDie = null, bool isPlayer = false)
    {
        if (whoShouldDie == null)
        {
            whoShouldDie = gameObject;
        }
        
        if (!isPlayer)
        {
            //Cut body part
            if (whoShouldDie.GetComponent<CutBodyPart>() != null)
            {
                whoShouldDie.GetComponent<CutBodyPart>().CutPartOfBody(whoShouldDie, 20, true);
            }

            //Destroy Navigator
            Destroy(whoShouldDie.GetComponent<EnemyNavigator>());

            //Destroy Agent
            Destroy(whoShouldDie.GetComponent<UnityEngine.AI.NavMeshAgent>());

            //Calculate chance to drop some item
            int PlayersLuck = PlayerController.Player.GetComponent<PlayerController>().LUCK;
            int temp_luck;
            if (20 + PlayersLuck > 50)
            {
                temp_luck = 50;
            }
            else
            {
                temp_luck = 20 + PlayersLuck;
            }
            
            if (temp_luck > Random.Range(1, 100))
            {
                DropItem.dropController.drop(null, whoShouldDie);
            }
            
            //add EXP from kill
            PlayerController.Player.GetComponent<EXPController>().AddEXP(whoShouldDie.GetComponent<EnemyController>().getEnemyStatsFromID(whoShouldDie.GetComponent<EnemyController>().ID).EXP); 
        }
        else
        {
            SoundPlayer.PlayerSoundController.sound_death();
        }

        //Destroy current parent RigidBody
        Destroy(whoShouldDie.GetComponent<Rigidbody>());


        if (hasRagdoll)
        {
            //Destroy animator
            Destroy(whoShouldDie.GetComponent<Animator>());

            //Activate all body parts rigidbody
            Rigidbody[] Rigidbodies;
            Rigidbodies = whoShouldDie.GetComponentsInChildren<Rigidbody>();
            foreach (Rigidbody Rigidbody_of_ragdoll in Rigidbodies)
            {
                Rigidbody_of_ragdoll.isKinematic = false;
                Rigidbody_of_ragdoll.mass = 80;
                Rigidbody_of_ragdoll.collisionDetectionMode = CollisionDetectionMode.Continuous;
            
                //knockback force ?
                //Rigidbody_of_ragdoll.AddForce(transform.up * (-100000));
                //Rigidbody_of_ragdoll.AddForce(transform.forward * (Random.Range(-25000,25000)));
            }



            //Activate all body parts Capsule colliders
            CapsuleCollider[] CapsuleColliders;
            CapsuleColliders = whoShouldDie.GetComponentsInChildren<CapsuleCollider>();

            foreach (CapsuleCollider CapsuleCollider_of_ragdoll in CapsuleColliders)
            {
                if (CapsuleCollider_of_ragdoll.enabled)
                {
                    Destroy(CapsuleCollider_of_ragdoll);
                }
                else
                {
                    CapsuleCollider_of_ragdoll.enabled = true;
                }
            }

            //Activate all body parts Box colliders
            BoxCollider[] BoxColliders;
            BoxColliders = whoShouldDie.GetComponentsInChildren<BoxCollider>();

            foreach (BoxCollider BoxCollider_of_ragdoll in BoxColliders)
            {
                if (BoxCollider_of_ragdoll.enabled)
                {
                    BoxCollider_of_ragdoll.enabled = false;
                }
                else
                {
                    BoxCollider_of_ragdoll.enabled = true;
                }
            }
        }
        else
        {
            if (whoShouldDie.GetComponent<Animator>())
            {
                whoShouldDie.GetComponent<Animator>().SetTrigger("Die");
            }

            if (col_off && col_on)
            {
                col_off.enabled = false;
                col_on.enabled = true;
            }
        }

        if (isPlayer)
        {
            UIController.UIControl.activateRespawnPanel(); 
        }
            
        SaveGameController.GameSaver.SaveGameData();
    }
}
