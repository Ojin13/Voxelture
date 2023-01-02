using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetEnemiesFromZone : MonoBehaviour
{

    public List<GameObject> Enemies = new List<GameObject>();
    public bool unableToEnter;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider col)
    {
        if (col.transform.parent != null)
        {
            if (col.transform.parent.tag == "Enemy" && col.gameObject.tag != "Ignore__EnemyCol" && !Enemies.Contains(col.transform.parent.gameObject))
            {
                Enemies.Add(col.transform.parent.gameObject);
            }
        }
    }


    public void OnTriggerExit(Collider col)
    {
        if (col.transform.parent != null)
        {
            if (col.transform.parent.tag == "Enemy")
            {
                Enemies.Remove(col.transform.parent.gameObject);
            }
        }
    }

    public void OnTriggerStay(Collider col)
    {
        if (unableToEnter)
        {
            if (col.transform.parent.tag == "Enemy" && col.gameObject.tag != "Ignore__EnemyCol" && !Enemies.Contains(col.transform.parent.gameObject))
            {
                Enemies.Add(col.transform.parent.gameObject);
            }
        }
    }
}
