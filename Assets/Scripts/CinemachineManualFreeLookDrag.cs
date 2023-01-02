using System;
using Cinemachine;
using UnityEngine;

public class CinemachineManualFreeLookDrag : MonoBehaviour
{
    public static CinemachineFreeLook freeLook;
    public float maxXAxisSpeed;
    public float maxYAxisSpeed;
    public Transform WatchAfterDeath;
    public static CinemachineManualFreeLookDrag MouseController;
    
    private void Awake()
    {
        MouseController = this;
        freeLook = GetComponent<CinemachineFreeLook>();
        maxXAxisSpeed = freeLook.m_XAxis.m_MaxSpeed;
        maxYAxisSpeed = freeLook.m_YAxis.m_MaxSpeed;
    }

    private void Start()
    {
        turnOffCursor();
        gameObject.GetComponent<CinemachineFreeLook>().LookAt = PlayerController.Player.transform;
        gameObject.GetComponent<CinemachineFreeLook>().Follow = PlayerController.Player.transform;
        
        WatchAfterDeath = PlayerController.Player.transform;
    }

    private void Update()
    {
        
        if (!Input.GetMouseButton(1))
        {
            //turnOffCursor();
        }
        else
        {
            //turnOnCurson();
        }


        if(PlayerController.Player.GetComponent<PlayerController>().isDead)
        {
            freeLook.Follow = WatchAfterDeath;
            freeLook.LookAt = WatchAfterDeath;
            Screen.lockCursor = false;
            Cursor.visible = true;
            freeLook.m_XAxis.m_MaxSpeed = 10;
            freeLook.m_YAxis.m_MaxSpeed = 0.1f;
        }
        
    }

    public void turnOnCursor()
    {
        Screen.lockCursor = false;
        Cursor.visible = true;
        freeLook.m_XAxis.m_MaxSpeed = 10;
        freeLook.m_YAxis.m_MaxSpeed = 0.1f;
    }

    public void turnOffCursor()
    {
        Screen.lockCursor = true;
        Cursor.visible = false;
        freeLook.m_XAxis.m_MaxSpeed = maxXAxisSpeed;
        freeLook.m_YAxis.m_MaxSpeed = maxYAxisSpeed;
    }
}