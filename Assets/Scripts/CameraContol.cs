using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraContol : MonoBehaviour
{
    public static GameObject CameraObject;
    // Start is called before the first frame update
    void Awake()
    {
        CameraObject = this.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
