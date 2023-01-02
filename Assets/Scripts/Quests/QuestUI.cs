using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class QuestUI : MonoBehaviour
{
    public TextMeshProUGUI QuestName;
    public TextMeshProUGUI QuestText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setQuestInfo()
    {
        QuestName.text = QuestController.QuestControl.Quests[QuestController.QuestControl.QuestNumber].QuestName;
        QuestText.text = QuestController.QuestControl.Quests[QuestController.QuestControl.QuestNumber].QuestText;
    }
}
