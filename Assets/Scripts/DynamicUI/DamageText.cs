using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageText : MonoBehaviour
{
    public GameObject DamageCausedTo;
    public GameObject DesiredRotation;
    void Start()
    {
        DesiredRotation = CameraContol.CameraObject;
        Destroy(gameObject,1.5f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 x = new Vector3(DesiredRotation.transform.eulerAngles.x, DesiredRotation.transform.eulerAngles.y, DesiredRotation.transform.eulerAngles.z);
        GetComponent<RectTransform>().rotation = Quaternion.Euler(x);
    }
}
