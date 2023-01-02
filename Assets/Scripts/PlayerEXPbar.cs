using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerEXPbar : MonoBehaviour
{
    public TextMeshProUGUI EXP_text;
    private float startingWidth;

    public float EXPtoGain;
    public float gainedEXP;
    public float LevelPercentage;
    private void Start()
    {
        startingWidth = GetComponent<RectTransform>().sizeDelta.x;
    }

    void Update()
    {
        PlayerController player = PlayerController.Player.GetComponent<PlayerController>();
        EXPController expController = PlayerController.Player.GetComponent<EXPController>();
        
        float currentEXP = player.EXP;
        float NextLevelExp = expController.levelTable[player.LEVEL - 1];
        
        float currentLevelMinEXP;
        if (player.LEVEL <= 1)
        {
            currentLevelMinEXP = 0;
        }
        else
        {
            currentLevelMinEXP = expController.levelTable[player.LEVEL - 2];
        }
        
        EXPtoGain = NextLevelExp - currentLevelMinEXP;
        
        gainedEXP = (currentEXP - currentLevelMinEXP);
        
        LevelPercentage = (gainedEXP / EXPtoGain) * 100; 
        
        GetComponent<RectTransform>().sizeDelta = new Vector2((startingWidth/100)*LevelPercentage, GetComponent<RectTransform>().sizeDelta.y);

        EXP_text.text = Math.Round(LevelPercentage,2) + " %";
    }
}