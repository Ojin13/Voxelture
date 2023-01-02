using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GUIhider : MonoBehaviour
{

    private Canvas GUItoHide;
    public bool hideGUI = false;
    
    // Start is called before the first frame update
    void Awake()
    {
        GUItoHide = this.GetComponent<Canvas>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.F1))
        {
            hideGUI = !hideGUI;
        }

        if (hideGUI)
        {
            GUItoHide.enabled = false;
        }
        else
        {
            GUItoHide.enabled = true;
        }
    }
}
