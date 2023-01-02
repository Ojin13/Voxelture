using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForwardPosition : MonoBehaviour
{
    public GameObject forwardHit;

    void Awake()
    {
        forwardHit = new GameObject();
    }

    void Update()
    {
        RaycastHit hit;
        Vector3 fwd = gameObject.transform.forward;
        Debug.DrawRay(gameObject.transform.position+new Vector3(0,3,0), fwd * 50, Color.green);
        if (Physics.Raycast(gameObject.transform.position+new Vector3(0,3,0), fwd, out hit, 100))
        {
            forwardHit.transform.position = hit.point;
        }
        else
        {
            forwardHit.transform.position = transform.position + transform.forward;
        }
    }
}
