using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateLightningBolt : MonoBehaviour
{
    public GameObject Bolt;

    public void createBolt()
    {
        Instantiate(Bolt, transform.position, Quaternion.identity);
    }
}
