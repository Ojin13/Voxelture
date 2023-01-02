using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public DialogueSentence_data[] data;
    public bool uzJsmeSiPromluvili;
    public bool questZadan;
    public bool canTalk;
    public Dialogue questInProgress;
    public bool isQuestInProgress;
    public Dialogue questDone;
    public QuestController quests;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCollider") && !isQuestInProgress)
        {
            canTalk = true;
            UIController.UIControl.TalkSymbol.SetActive(true);
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCollider") && !isQuestInProgress)
        {
            canTalk = false;
            UIController.UIControl.deactivateTalkSymbol();
        }
    }

    private void Awake()
    {
        quests = GetComponent<QuestController>();
    }

    public void Update()
    {
        if (isQuestInProgress)
        {
            return;;    
        }
        
        if (canTalk)
        {
            if (UIController.UIControl.inGameMenuIsActive)
            {
                UIController.UIControl.deactivateTalkSymbol();
            }
            else
            {
                UIController.UIControl.TalkSymbol.SetActive(true);
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                //Pokud není NPC s kterým si hráč musí promluvit
                try
                {
                    if (quests.Quests[quests.QuestNumber].mustTalkTo != this.gameObject)
                    {
                        startSmallTalk();
                    }
                    else
                    {
                        startQuestConversation();
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    startSmallTalk();
                    Debug.LogError("No existing quests!");
                }
            }
        }
    }

    public void startQuestConversation()
    {
        if (UIController.UIControl.DialoguePanelIsActive)
        {
            if (DialogueController.DialogueControl.sentenceNumber < quests.Quests[quests.QuestNumber].QuestDialog.data.Length-1)
            {
                if (questZadan == false)
                {
                    //next sentence
                    DialogueController.DialogueControl.sentenceNumber++;
                    DialogueController.DialogueControl.nextQuestSentence(quests);
                }
                else
                {
                    questAction();
                }
            }
            else
            {
                //end dialog
                DialogueController.DialogueControl.sentenceNumber = 0;
                DialogueController.DialogueControl.currentDialog = null;
                UIController.UIControl.deactivateDialogues();
                PlayerController.Player.GetComponent<MovementController>().canMove = true;
                uzJsmeSiPromluvili = true;
                questZadan = true;
                if (quests.QuestNumber < quests.Quests.Length - 1)
                {
                    questAction();
                }
                else
                {
                    //all quests completed
                    //QuestController.QuestControl.Quests[QuestController.QuestControl.QuestNumber].mustTalkTo = null;
                
                
                    //    completed chapter - implement this later
                    /*
                    if (QuestController.QuestControl.Quests[QuestController.QuestControl.QuestNumber].newChapterOnComplete)
                    {
                        ChapterController.ChapterControl.nextChapter();
                    }
                    */
                }
            }
        }
        else
        {
            //start dialog
            if (questZadan)
            {
                questAction();
            }
            else
            {
                UIController.UIControl.activateDialogues();
                DialogueController.DialogueControl.nextQuestSentence(quests);
            }
        }
    }

    public void startSmallTalk(Dialogue customDialog = null)
    {
        if (UIController.UIControl.DialoguePanelIsActive)
        {
            if (DialogueController.DialogueControl.sentenceNumber < DialogueController.DialogueControl.currentDialog.data.Length-1)
            {
                //next sentence
                DialogueController.DialogueControl.sentenceNumber++;
                DialogueController.DialogueControl.nextSentence();
            }
            else
            {
                //quest done
                if (questDone == DialogueController.DialogueControl.currentDialog)
                {
                    SaveGameController.GameSaver.SaveGameData();
                    
                    //Holy cube
                    if (quests.Quests[quests.QuestNumber].QuestName == "Neznámý válečník")
                    {
                        Inventory.inventory.removeItem(1,"Legendary",false);
                        PlayerController.PlayerControl.EXP += 1500;
                        SkillTreeController.SkillController.FreeSkillPoints += 5;
                    }

                    
                    int kridylek = 0;
                    int potreba_kridylek = 3;
                    if (quests.Quests[quests.QuestNumber].QuestName == "Be smart")
                    {
                        PlayerController.PlayerControl.EXP += 1500;
                        SkillTreeController.SkillController.FreeSkillPoints += 10;
                        foreach (var item in Inventory.inventory.InventoryItems)
                        {
                            if (item.ID == 706 && kridylek < potreba_kridylek)
                            {
                                if (item.amountOwned <= potreba_kridylek)
                                {
                                    if (item.amountOwned < potreba_kridylek - kridylek)
                                    {
                                        kridylek += item.amountOwned;
                                        Inventory.inventory.InventoryItems.Remove(item);
                                    }
                                    else
                                    {
                                        item.amountOwned -= potreba_kridylek - kridylek;
                                        kridylek += potreba_kridylek - kridylek;
                                    }
                                }
                                else
                                {
                                    int x = potreba_kridylek - kridylek;
                                    kridylek += x;
                                    item.amountOwned -= x;
                                }
                            }
                        }
                    }
                    
                    
                    
                    quests.nextQuest();
                    questZadan = false;
                    SaveGameController.GameSaver.SaveGameData();
                }
                
                //end dialog
                DialogueController.DialogueControl.sentenceNumber = 0;
                DialogueController.DialogueControl.currentDialog = null;
                UIController.UIControl.deactivateDialogues();
                PlayerController.Player.GetComponent<MovementController>().canMove = true;
                uzJsmeSiPromluvili = true;

            }
        }
        else
        {
            //start dialog
            if (!customDialog)
            {
                DialogueController.DialogueControl.currentDialog = this;
            }
            else
            {
                DialogueController.DialogueControl.currentDialog = customDialog;
            }
            
            UIController.UIControl.activateDialogues();
            DialogueController.DialogueControl.nextSentence();
        }
    }

    public void questAction()
    {
        if (quests.Quests[quests.QuestNumber].QuestName == "Neznámý válečník")
        {
            bool hasCube = false;
            foreach (var item in Inventory.inventory.InventoryItems)
            {
                if (item.ID == 1)
                {
                    hasCube = true;
                    break;
                }
            }
            
            
            if (hasCube)
            {
                if (questDone)
                {
                    startSmallTalk(questDone);
                    return;
                }
            }
            else
            {
                if (questInProgress)
                {
                    startSmallTalk(questInProgress);
                    return;
                }
            }
        }
        
        
        if (quests.Quests[quests.QuestNumber].QuestName == "Be smart")
        {
            int kridelka = 0;
            foreach (var item in Inventory.inventory.InventoryItems)
            {
                if (item.ID == 706)
                {
                    kridelka++;
                }
            }
            
            
            if (kridelka >= 3)
            {
                if (questDone)
                {
                    startSmallTalk(questDone);
                    return;
                }
            }
            else
            {
                if (questInProgress)
                {
                    startSmallTalk(questInProgress);
                    return;
                }
            }
        }
        
        
        quests.nextQuest();
        questZadan = false;
    }
}




/*
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using UnityEngine;

public class Dialogue : MonoBehaviour
{
    public DialogueSentence_data[] data;
    public bool uzJsmeSiPromluvili;
    public bool questZadan;
    public bool canTalk;
    public Dialogue questInProgress;
    public bool isQuestInProgress;
    public Dialogue questDone;
    public QuestController quests;

    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCollider") && !isQuestInProgress)
        {
            canTalk = true;
        }
    }

    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag("PlayerCollider") && !isQuestInProgress)
        {
            canTalk = false;
        }
    }

    public void Update()
    {
        if (isQuestInProgress)
        {
            return;;    
        }
        
        if (canTalk)
        {
            if (UIController.UIControl.inGameMenuIsActive == false)
            {
                UIController.UIControl.TalkSymbol.SetActive(true);
            }
            else
            {
                UIController.UIControl.TalkSymbol.SetActive(false);
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                //Pokud není NPC s kterým si hráč musí promluvit
                try
                {
                    if (QuestController.QuestControl.Quests[QuestController.QuestControl.QuestNumber].mustTalkTo != this.gameObject)
                    {
                        startSmallTalk();
                    }
                    else
                    {
                        startQuestConversation();
                    }
                }
                catch (IndexOutOfRangeException e)
                {
                    startSmallTalk();
                    Debug.LogError("No existing quests!");
                }
            }
        }
        else
        {
            UIController.UIControl.deactivateTalkSymbol();
        }
    }

    public void startQuestConversation()
    {
        if (UIController.UIControl.DialoguePanelIsActive)
        {
            if (DialogueController.DialogueControl.sentenceNumber < QuestController.QuestControl.Quests[QuestController.QuestControl.QuestNumber].QuestDialog.data.Length-1)
            {
                if (questZadan == false)
                {
                    //next sentence
                    DialogueController.DialogueControl.sentenceNumber++;
                    DialogueController.DialogueControl.nextQuestSentence();
                }
                else
                {
                    questAction();
                }
            }
            else
            {
                //end dialog
                DialogueController.DialogueControl.sentenceNumber = 0;
                DialogueController.DialogueControl.currentDialog = null;
                UIController.UIControl.deactivateDialogues();
                PlayerController.Player.GetComponent<MovementController>().canMove = true;
                uzJsmeSiPromluvili = true;
                questZadan = true;
                if (QuestController.QuestControl.QuestNumber < QuestController.QuestControl.Quests.Length - 1)
                {
                    questAction();
                }
                else
                {
                    //all quests completed
                    //QuestController.QuestControl.Quests[QuestController.QuestControl.QuestNumber].mustTalkTo = null;
                
                
                    //    completed chapter - implement this later
                    /*
                    if (QuestController.QuestControl.Quests[QuestController.QuestControl.QuestNumber].newChapterOnComplete)
                    {
                        ChapterController.ChapterControl.nextChapter();
                    }
                    
                }
            }
        }
        else
        {
            //start dialog
            if (questZadan)
            {
                questAction();
            }
            else
            {
                UIController.UIControl.activateDialogues();
                DialogueController.DialogueControl.nextQuestSentence();
            }
        }
    }

    public void startSmallTalk(Dialogue customDialog = null)
    {
        if (UIController.UIControl.DialoguePanelIsActive)
        {
            if (DialogueController.DialogueControl.sentenceNumber < DialogueController.DialogueControl.currentDialog.data.Length-1)
            {
                //next sentence
                DialogueController.DialogueControl.sentenceNumber++;
                DialogueController.DialogueControl.nextSentence();
            }
            else
            {
                //end dialog
                if (questDone == DialogueController.DialogueControl.currentDialog)
                {
                    QuestController.QuestControl.nextQuest();
                    questZadan = false;
                }
                
                DialogueController.DialogueControl.sentenceNumber = 0;
                DialogueController.DialogueControl.currentDialog = null;
                UIController.UIControl.deactivateDialogues();
                PlayerController.Player.GetComponent<MovementController>().canMove = true;
                uzJsmeSiPromluvili = true;

            }
        }
        else
        {
            //start dialog
            if (!customDialog)
            {
                DialogueController.DialogueControl.currentDialog = this;
            }
            else
            {
                DialogueController.DialogueControl.currentDialog = customDialog;
            }
            
            UIController.UIControl.activateDialogues();
            DialogueController.DialogueControl.nextSentence();
        }
    }

    public void questAction()
    {
        if (QuestController.QuestControl.QuestNumber == 0)
        {
            if (PlayerController.PlayerControl.HP <= 10)
            {
                if (questDone)
                {
                    startSmallTalk(questDone);
                    return;
                }
            }
            else
            {
                if (questInProgress)
                {
                    startSmallTalk(questInProgress);
                    return;
                }
            }
        }
        
        
        QuestController.QuestControl.nextQuest();
        questZadan = false;
    }
}

*/