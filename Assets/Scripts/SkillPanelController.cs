using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillPanelController : MonoBehaviour
{

    public static SkillPanelController SkillPanelControl;
    public SkillSlot[] SkillSlots;


    void Awake()
    {
        SkillPanelControl = this;
        SkillSlots = GetComponentsInChildren<SkillSlot>();
    }

    // Update is called once per frame
    void Update()
    {
        int index = 0;
        
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            skillKeyPressed(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            skillKeyPressed(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            skillKeyPressed(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            skillKeyPressed(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            skillKeyPressed(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            skillKeyPressed(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            skillKeyPressed(6);
        }
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            skillKeyPressed(7);
        }
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            skillKeyPressed(8);
        }
    }

    void skillKeyPressed(int index)
    {
        if (SkillSlots[index].skill != null)
        {
            if (SkillSlots[index].skill.ready)
            {
                SkillSlots[index].skill.useSKill();
                SkillSlots[index].animateUse();
            }
        }
    }
}
