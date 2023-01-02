using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Respawn : MonoBehaviour
{
    public void reloadScene()
    {
        Application.LoadLevel(Application.loadedLevel);
    }
}
