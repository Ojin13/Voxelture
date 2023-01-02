using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestLocation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        //start dialog
        //UIController.UIControl.activateDialogues();
        //DialogueController.DialogueControl.nextQuestSentence();
        
        
        Debug.Log("Quest Zone enteted");
        
        if (other.gameObject.tag == "Player")
        {
            //Go to Koppel
            if (QuestController.QuestControl.QuestNumber == 2)
            {
                QuestController.QuestControl.nextQuest();
                Destroy(gameObject);
            }
            
            //Go for Rose
            if (QuestController.QuestControl.QuestNumber == 6)
            {
                QuestController.QuestControl.nextQuest();
                Destroy(gameObject);
            }
            
            //Leave Hammeln
            if (QuestController.QuestControl.QuestNumber == 8)
            {
                QuestController.QuestControl.nextQuest();
                Destroy(gameObject);
            }
            
            //suicide
            if (QuestController.QuestControl.QuestNumber == 12)
            {
                QuestController.QuestControl.nextQuest();
                Destroy(gameObject);
            }
        }
    }
}
