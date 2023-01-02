using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class StatsController : MonoBehaviour
{

    public TextMeshProUGUI HealthText;
    public TextMeshProUGUI HungerText;
    public TextMeshProUGUI DamageText;
    public TextMeshProUGUI DefenceText;
    public TextMeshProUGUI LevelText;
    public TextMeshProUGUI SpeedText;
    public TextMeshProUGUI LuckText;
    public TextMeshProUGUI PrecisionText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        PlayerController player = PlayerController.Player.GetComponent<PlayerController>();
        HealthText.text = player.HP.ToString();
        HungerText.text = player.Hunger.ToString();
        DamageText.text = player.DMG.ToString();
        int x = (100 + player.DEF / 2);
        float y = 1-(float)100 / x;
        float z = (float) y * 100;
        DefenceText.text = player.DEF.ToString()+" ("+Math.Round(z,1)+"%)";
        LevelText.text = player.LEVEL.ToString();

        float percentageSpeed = (int)((100 * player.GetComponent<MovementController>().MovementSpeed) / 5);
        SpeedText.text = (percentageSpeed.ToString()+"%");
        
        
        
        LuckText.text = player.LUCK.ToString();
        PrecisionText.text = player.PRECISION.ToString();
    }
}
