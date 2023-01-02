using System;
using System.Collections;
using System.Collections.Generic;
using System.Timers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillSlot : MonoBehaviour
{
    public RawImage skillImage;
    public Skill skill;
    public Image loadingCyrcle;

    private float timer;
    private float timeToReadyState;
    private bool skillState;

    void Start()
    {
        
    }


    void Update()
    {
        if (skill == null || skill.unlocked == false)
        {
            skill = null;
            skillImage.texture = null;
            skillImage.color = new Color32(0,0,0,125);
        }
        else
        {
            skillImage.texture = skill.skillImage.texture;
            skillImage.color = new Color32(255,255,255,255);
            
            checkSkillStatus();
            
            if (skill.ready == false)
            {
                loadingPercentage();
            }
            else
            {
                timer = 0.0f;
                loadingCyrcle.fillAmount = 0;
                timeToReadyState = 0;
            }
        }
    }
    
    
    public void loadingPercentage()
    {
        timer += Time.deltaTime;
        timeToReadyState = timer % 60;
        loadingCyrcle.fillAmount = ((timeToReadyState-skill.cooldown)*(-1))/skill.cooldown;
    }
    
    public void checkSkillStatus()
    {
        //status changed
        if (skill.ready != skillState)
        {
            skillState = skill.ready;
            if (!skillState)
            {
                timeToReadyState = skill.cooldown;
            }
            else
            {
                animateUse();
                timeToReadyState = 0;
            }
        }
    }

    public void animateUse()
    {
        GetComponent<Animator>().Play("UseSlot");
    }

    public void newSkillAsigned(Skill skillToUnlock)
    {
        skill = skillToUnlock;
        skillState = skill.ready;
    }

    public void clickedOnSlot()
    {
        if (skill != null)
        {
            if (skill.ready)
            {
                skill.useSKill();
            }
        }
    }
}
