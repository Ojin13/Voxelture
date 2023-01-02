using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ProgressBar : MonoBehaviour
{
    public TextMeshProUGUI barText;
    private float startingWidth;
    public bool HPbar;
    public bool HungerBar;

    private void Start()
    {
        startingWidth = GetComponent<RectTransform>().sizeDelta.x;
    }

    void Update()
    {
        float currentValue;
        float maxValue;
        
        if (HPbar)
        {
            currentValue = PlayerController.Player.GetComponent<PlayerController>().HP; 
            maxValue = PlayerController.Player.GetComponent<PlayerController>().MAXHP;
            if (barText != null)
            {
                barText.text = "HP " + currentValue + " / " + maxValue;   
            }
        }
        else
        {
            currentValue = PlayerController.Player.GetComponent<PlayerController>().Hunger;
            maxValue = PlayerController.Player.GetComponent<PlayerController>().MaxHunger;
            if (barText != null)
            {
                barText.text = "Hunger " + currentValue + " / " + maxValue;   
            }
        }
        
        float currentBarValue = currentValue;
        float maxBarValue = maxValue;
        float barPercentage = (currentBarValue / maxBarValue) * 100;
        
        GetComponent<RectTransform>().sizeDelta = new Vector2((startingWidth/100)*barPercentage, GetComponent<RectTransform>().sizeDelta.y);
    }
}