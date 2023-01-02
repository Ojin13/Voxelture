using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public bool leftSideActive;
    public GameObject leftDialog;
    public GameObject rightDialog;
    public Dialogue currentDialog;
    public static DialogueController DialogueControl;
    public int sentenceNumber;
    
    void Awake()
    {
        DialogueControl = this;
    }

    public void nextSentence()
    {
        if (currentDialog.data[sentenceNumber].rightSide)
        {
            rightDialog.SetActive(true);
            leftDialog.SetActive(false);
            rightDialog.GetComponent<DialogueUI>().changeData(currentDialog.data[sentenceNumber].Name,currentDialog.data[sentenceNumber].Sentence,currentDialog.data[sentenceNumber].Image);
        }
        else
        {
            rightDialog.SetActive(false);
            leftDialog.SetActive(true);
            leftDialog.GetComponent<DialogueUI>().changeData(currentDialog.data[sentenceNumber].Name,currentDialog.data[sentenceNumber].Sentence,currentDialog.data[sentenceNumber].Image);
        }
    }
    
    public void nextQuestSentence(QuestController quest)
    {
        if (quest.Quests[quest.QuestNumber].QuestDialog.data[sentenceNumber].rightSide)
        {
            rightDialog.SetActive(true);
            leftDialog.SetActive(false);

            string QuestDialogueTalkingEntityName = quest.Quests[quest.QuestNumber].QuestDialog.data[sentenceNumber].Name;
            string QuestDialogueTalkingEntitySentence = quest.Quests[quest.QuestNumber].QuestDialog.data[sentenceNumber].Sentence;
            Texture QuestDialogueTalkingEntityImage = quest.Quests[quest.QuestNumber].QuestDialog.data[sentenceNumber].Image;
            
            rightDialog.GetComponent<DialogueUI>().changeData(QuestDialogueTalkingEntityName,QuestDialogueTalkingEntitySentence,QuestDialogueTalkingEntityImage);
        }
        else
        {
            rightDialog.SetActive(false);
            leftDialog.SetActive(true);
            
            string QuestDialogueTalkingEntityName = quest.Quests[quest.QuestNumber].QuestDialog.data[sentenceNumber].Name;
            string QuestDialogueTalkingEntitySentence = quest.Quests[quest.QuestNumber].QuestDialog.data[sentenceNumber].Sentence;
            Texture QuestDialogueTalkingEntityImage = quest.Quests[quest.QuestNumber].QuestDialog.data[sentenceNumber].Image;
            
            leftDialog.GetComponent<DialogueUI>().changeData(QuestDialogueTalkingEntityName,QuestDialogueTalkingEntitySentence,QuestDialogueTalkingEntityImage);
        }
    }
}



/*
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueController : MonoBehaviour
{
    public bool leftSideActive;
    public GameObject leftDialog;
    public GameObject rightDialog;
    public Dialogue currentDialog;
    public static DialogueController DialogueControl;
    public int sentenceNumber;
    
    void Awake()
    {
        DialogueControl = this;
    }

    public void nextSentence()
    {
        if (currentDialog.data[sentenceNumber].rightSide)
        {
            rightDialog.SetActive(true);
            leftDialog.SetActive(false);
            rightDialog.GetComponent<DialogueUI>().changeData(currentDialog.data[sentenceNumber].Name,currentDialog.data[sentenceNumber].Sentence,currentDialog.data[sentenceNumber].Image);
        }
        else
        {
            rightDialog.SetActive(false);
            leftDialog.SetActive(true);
            leftDialog.GetComponent<DialogueUI>().changeData(currentDialog.data[sentenceNumber].Name,currentDialog.data[sentenceNumber].Sentence,currentDialog.data[sentenceNumber].Image);
        }
    }
    
    public void nextQuestSentence(QuestController quest)
    {
        if (QuestController.QuestControl.Quests[QuestController.QuestControl.QuestNumber].QuestDialog.data[sentenceNumber].rightSide)
        {
            rightDialog.SetActive(true);
            leftDialog.SetActive(false);

            string QuestDialogueTalkingEntityName = QuestController.QuestControl.Quests[QuestController.QuestControl.QuestNumber].QuestDialog.data[sentenceNumber].Name;
            string QuestDialogueTalkingEntitySentence = QuestController.QuestControl.Quests[QuestController.QuestControl.QuestNumber].QuestDialog.data[sentenceNumber].Sentence;
            Texture QuestDialogueTalkingEntityImage = QuestController.QuestControl.Quests[QuestController.QuestControl.QuestNumber].QuestDialog.data[sentenceNumber].Image;
            
            rightDialog.GetComponent<DialogueUI>().changeData(QuestDialogueTalkingEntityName,QuestDialogueTalkingEntitySentence,QuestDialogueTalkingEntityImage);
        }
        else
        {
            rightDialog.SetActive(false);
            leftDialog.SetActive(true);
            
            string QuestDialogueTalkingEntityName = QuestController.QuestControl.Quests[QuestController.QuestControl.QuestNumber].QuestDialog.data[sentenceNumber].Name;
            string QuestDialogueTalkingEntitySentence = QuestController.QuestControl.Quests[QuestController.QuestControl.QuestNumber].QuestDialog.data[sentenceNumber].Sentence;
            Texture QuestDialogueTalkingEntityImage = QuestController.QuestControl.Quests[QuestController.QuestControl.QuestNumber].QuestDialog.data[sentenceNumber].Image;
            
            leftDialog.GetComponent<DialogueUI>().changeData(QuestDialogueTalkingEntityName,QuestDialogueTalkingEntitySentence,QuestDialogueTalkingEntityImage);
        }
    }
}
*/