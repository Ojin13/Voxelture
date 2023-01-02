using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActiveMap : MonoBehaviour
{
    public static ActiveMap ActiveMapControl;
    public Camera cam;

    public void Awake()
    {
        ActiveMapControl = this;
    }

    public void Start()
    {
        if (GetComponent<BoxCollider>() == null || GetComponent<MeshCollider>() == null)
        {
            foreach (var obj in GetComponentsInChildren<MeshRenderer>())
            {
                if (Vector3.Distance(PlayerController.Player.transform.position, obj.transform.position) > 1000)
                {
                    obj.gameObject.GetComponent<MeshRenderer>().enabled = false;
                }
                else
                { 
                    obj.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
                }
            }
        }
    }


    public void OnTriggerEnter(Collider obj)
    {
        if (obj.gameObject.GetComponent<MeshRenderer>() != null)
        {
            setVisible(obj.GetComponent<MeshRenderer>());
        }

        if (obj.gameObject.GetComponent<CapsuleCollider>() != null && obj.gameObject.transform.parent)
        {
            if (obj.gameObject.transform.parent.CompareTag("Enemy"))
            {
                obj.gameObject.transform.parent.GetComponentInChildren<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
            }
        }
    }

    public void OnTriggerExit(Collider obj)
    {
        if (obj.GetComponent<MeshRenderer>() != null)
        {
            setInvisible(obj.gameObject.GetComponent<MeshRenderer>());
        }
        
        if (obj.gameObject.GetComponent<CapsuleCollider>() != null && obj.gameObject.transform.parent)
        {
            if (obj.gameObject.transform.parent.CompareTag("Enemy"))
            {
                obj.gameObject.transform.parent.GetComponentInChildren<SkinnedMeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
            }
        }
    }
    

    public void setVisible(MeshRenderer obj)
    {
        obj.gameObject.GetComponent<MeshRenderer>().enabled = true;
        obj.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
    }
    
    public void setInvisible(MeshRenderer obj)
    {
        if (Vector3.Distance(PlayerController.Player.transform.position, obj.transform.position) > 1000)
        {
            obj.gameObject.GetComponent<MeshRenderer>().enabled = false;
        }
        else
        {
            obj.gameObject.GetComponent<MeshRenderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.ShadowsOnly;
        }
    }
    
    
    public bool isOnCamera(GameObject obj)
    {
        Vector3 screenPoint = cam.WorldToViewportPoint(obj.transform.position);
        return screenPoint.z > 0 && screenPoint.x > 0 && screenPoint.x < 1 && screenPoint.y > 0 && screenPoint.y < 1;
    }
}