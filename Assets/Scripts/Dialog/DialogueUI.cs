using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DialogueUI : MonoBehaviour
{
    public TextMeshProUGUI EntityName;
    public TextMeshProUGUI EntityText;
    public RawImage EntityImage;
    public String side;
    public Texture PlayerImage;
    public Texture NPC_image;

    public void Awake()
    {
        if (side == "right")
        {
            DialogueController.DialogueControl.rightDialog = gameObject;
        }

        if (side == "left")
        {
            DialogueController.DialogueControl.leftDialog = gameObject;
        }
    }

    public void changeData(String Name, String Text, Texture Image)
    {
        EntityName.text = Name;

        if (Image == null)
        {
            if (Name == "Hráč")
            {
                EntityImage.texture = PlayerImage;
            }
            else
            {
                EntityImage.texture = NPC_image;
            }
        }
        else
        {
            EntityImage.texture = Image;
        }
        
        EntityText.text = Text;

        //StopAllCoroutines();
        //StartCoroutine(TypeSentence(Text));
    }

    
    /*
    IEnumerator TypeSentence(string sentence)
    {
        EntityText.text = "";
        foreach (char letter in EntityText.text.ToCharArray())
        {
            EntityText.text += letter;
            string dot_str = ".";
            char[] dot = dot_str.ToCharArray();
            if (letter == dot[0])
            {
                yield return new WaitForSeconds(0.3f);
            }
            else
            {
                yield return null;
            }
        }
    }
    
    */
}
