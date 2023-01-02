using System;
using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class NewDamageText : MonoBehaviour
{
    public static NewDamageText DamageTextController;
    public GameObject DamageText;
    public GameObject canvas;
    void Awake()
    {
        DamageTextController = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void newText(int Damage,Vector3 enemyPos, GameObject DamageCausedTo, string text = "")
    {
        Vector3 spawnPosition = new Vector3(enemyPos.x+Random.Range(-2,2),enemyPos.y+Random.Range(1,4),enemyPos.z+Random.Range(-2,2));
        GameObject DmgText = Instantiate(DamageText,spawnPosition,Quaternion.identity);
        DmgText.GetComponent<DamageText>().DamageCausedTo = DamageCausedTo;
        DmgText.transform.parent = canvas.transform;

        if (text == "")
        {
            DmgText.GetComponent<TextMeshProUGUI>().text = Damage.ToString();
        }
        else
        {
            DmgText.GetComponent<TextMeshProUGUI>().text = text;
        }

        if (Damage >= PlayerController.Player.GetComponent<PlayerController>().DMG*0.90)
        {
            DmgText.GetComponent<TextMeshProUGUI>().fontSize = 0.8f;
            return;
        }
        else if (Damage >= PlayerController.Player.GetComponent<PlayerController>().DMG * 0.80f)
        {
            DmgText.GetComponent<TextMeshProUGUI>().fontSize = 0.5f;
            return;
        }
        else
        {
            DmgText.GetComponent<TextMeshProUGUI>().fontSize = 0.2f;
        }
        
    }
    
}
