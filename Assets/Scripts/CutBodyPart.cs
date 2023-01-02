using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CutBodyPart : MonoBehaviour
{

    public List<GameObject> CutableBodyParts;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void CutPartOfBody(GameObject Enemy,int chanceToCut = 50, bool isDeadStrike = false)
    {
        if (!PlayerController.Player.GetComponent<DrawingWeapon>().weaponIsDrawn)
        {
            return;
        }
        
        Transform[] AllBodyParts;
        AllBodyParts = Enemy.GetComponentsInChildren<Transform>();

        if (CutableBodyParts.Count <= 0 || isDeadStrike)
        {
            if (isDeadStrike)
            {
                CutableBodyParts.Clear();
            }

            foreach (Transform bodyPart in AllBodyParts)
            {
                if (!isDeadStrike)
                {
                    if (bodyPart.gameObject.tag == "CutablePartOfBody")
                    {
                        CutableBodyParts.Add(bodyPart.gameObject);
                    }
                }
                else
                {
                    if (bodyPart.gameObject.tag == "CutablePartOfBodyAfterDeath")
                    {
                        CutableBodyParts.Add(bodyPart.gameObject);
                    }
                }
            }
        }




        //Cut body part
        int CanCut = Random.Range(0, 100);
        if (CanCut < chanceToCut)
        {
            int randomBodyPart = Random.Range(0, CutableBodyParts.Count);
            CutableBodyParts[randomBodyPart].transform.localScale = new Vector3(0, 0, 1);


            if (CutableBodyParts[randomBodyPart].GetComponent<BoxCollider>() != null)
            {
                Destroy(CutableBodyParts[randomBodyPart].GetComponent<BoxCollider>());
            }

            if (CutableBodyParts[randomBodyPart].GetComponent<CapsuleCollider>() != null)
            {
                Destroy(CutableBodyParts[randomBodyPart].GetComponent<CapsuleCollider>());
            }

            Instantiate(Enemy.GetComponent<EnemyController>().BloodType, CutableBodyParts[randomBodyPart].transform.position, Quaternion.identity, CutableBodyParts[randomBodyPart].transform.parent);

            CutableBodyParts.Remove(CutableBodyParts[randomBodyPart]);
        }
    }
}
