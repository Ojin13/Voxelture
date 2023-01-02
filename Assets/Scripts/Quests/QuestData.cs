using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestData
{
    public String QuestName;
    public String QuestText;
    public bool newChapterOnComplete;
    public GameObject mustTalkTo;
    public DialogueSentences_data QuestDialog;
}
