using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InvisibleController : MonoBehaviour
{

    public Renderer playersSkin;
    public Material normalSkin;
    public Material transparentSkin;
    public bool isInvisible;
    

    public void setVisible()
    {
        playersSkin.material = normalSkin;
        isInvisible = false;
    }

    public void SetInvisible()
    {
        playersSkin.material = transparentSkin;
        isInvisible = true;
    }
}
