using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestController : MonoBehaviour
{
    public QuestData[] Quests;
    public int QuestNumber;
    public static QuestController QuestControl;
    public bool isMainController;
    public QuestController[] Controllers;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        if (isMainController)
        {
            QuestControl = this;
        }
    }
    
    public void nextQuest()
    {
        if (Quests[QuestNumber].newChapterOnComplete)
        {
            //ChapterController.ChapterControl.nextChapter();    
        }
        
        QuestNumber++;
    }
}
