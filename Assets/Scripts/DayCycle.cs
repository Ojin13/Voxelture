using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DayCycle : MonoBehaviour
{
    public int day = 0;
    public String currentDayState;
    public bool decreaseIntensity;
    public float dayProgress;
    public AudioSource Forest_day;
    public AudioSource Forest_night;
    public static DayCycle DayCycleController;
    public Skill skill_nightVision;
    
    // Start is called before the first frame update
    void Awake()
    {
        DayCycleController = this;
    }

    private float speed = 0.001f;
    // Update is called once per frame
    void Update()
    {
        dayProgress = GetComponent<Animator>().GetCurrentAnimatorStateInfo(0).normalizedTime;

        if (decreaseIntensity)
        {
            if (!skill_nightVision.unlocked)
            {
                if (RenderSettings.ambientIntensity > 0.3f)
                {
                    RenderSettings.ambientIntensity -= speed;
                }
                else
                {
                    RenderSettings.ambientIntensity = 0.3f;
                }

                if (RenderSettings.reflectionIntensity > 0.3f)
                {
                    RenderSettings.reflectionIntensity -= speed;
                }
                else
                {
                    RenderSettings.reflectionIntensity = 0.3f;
                }
            }
            else
            {
                RenderSettings.ambientIntensity = 1f;
                RenderSettings.reflectionIntensity = 1;
            }
            

            if (Forest_day.volume > 0)
            {
                Forest_day.volume -= speed;
            }
            else
            {
                Forest_day.volume = 0;

                if (Forest_night.volume < 1)
                {
                    Forest_night.volume += speed;
                }
                else
                {
                    Forest_night.volume = 1;
                }
            }
        }
        else
        {
            if (RenderSettings.ambientIntensity < 1)
            {
                RenderSettings.ambientIntensity += speed;
            }
            else
            {
                RenderSettings.ambientIntensity = 1f;
            }
            
            if (RenderSettings.reflectionIntensity < 1f)
            {
                RenderSettings.reflectionIntensity += speed;
            }
            else
            {
                RenderSettings.reflectionIntensity = 1f;
            }
            
            
            if (Forest_night.volume > 0)
            {
                Forest_night.volume -= speed;
            }
            else
            {
                Forest_night.volume = 0;

                if (Forest_day.volume < 1)
                {
                    Forest_day.volume += speed;
                }
                else
                {
                    Forest_day.volume = 1;
                }
            }
        }
    }


    public void nextDay()
    {
        day++;
    }

    public void setNight()
    {
        currentDayState = "night";
    }

    public void setDay()
    {
        currentDayState = "day";
    }

    public void sunRise()
    {
        currentDayState = "sunrise";
    }

    public void decreaseReflection()
    {
        decreaseIntensity = true;
        currentDayState = "afternoon";
    }
    
    public void increaseReflection()
    {
        decreaseIntensity = false;
    }

    public void setDayProgress(float progress)
    {
        //0 - 1
        GetComponent<Animator>().Play("DayCycle", 0, progress);
    }
}
