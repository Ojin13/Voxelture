using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SaveGameController : MonoBehaviour
{


    public static SaveGameController GameSaver;

    public bool Debug_load_save;
    public bool enemiesLoaded;
    public bool lootLoaded;
    
    // Start is called before the first frame update
    void Awake()
    {
        GameSaver = this;
    }

    void Start()
    {
        if (Debug_load_save)
        {
            LoadGame();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    

    private SaveObj CreateSaveGameObject()
    {
        SaveObj save = new SaveObj();
        
        //save time
        save.dayProgress = DayCycle.DayCycleController.dayProgress;
        save.day = DayCycle.DayCycleController.day;
        save.currentDayState = DayCycle.DayCycleController.currentDayState;
        
        //save items in inventory
        foreach (InventoryItem itemInInventory in Inventory.inventory.InventoryItems)
        {
            save.Items.Add(itemInInventory);
        }
        
        //save equipment
        foreach (InventoryItem equip in PlayerController.Player.GetComponent<Equipment>().EquippedItems)
        {
            save.Equipment.Add(equip);
        }
        
        //save unlocked skills
        foreach (int unlockedSkillID in SkillTreeController.SkillController.unlockedSkills)
        {
            save.unlockedSkills.Add(unlockedSkillID);
        }
        
        //save SkillPointes
        save.SP = SkillTreeController.SkillController.FreeSkillPoints;
        
        //save skillbar slots
        foreach (SkillSlot slot in SkillPanelController.SkillPanelControl.SkillSlots)
        {
            if (slot.skill == null)
            {
                save.skillIDfromSkillBar.Add(0);
            }
            else
            {
                save.skillIDfromSkillBar.Add(slot.skill.skillID);
            }
        }
        
        
        //save Quests
        save.QuestNumber_KFC = QuestController.QuestControl.Controllers[0].QuestNumber;
        save.QuestNumber_Cube = QuestController.QuestControl.Controllers[1].QuestNumber;
        
        //save Enemies
        foreach (var enemy in EnemySaveController.enemySaveControl.Enemies)
        {
            save.Enemies.Add(EnemySaveController.enemySaveControl.convert__EnemyToSerializable(enemy.GetComponent<EnemyController>()));
        }
        
        //save Loot on the map
        foreach (var item in SaveLootController.SaveLootControl.LootOnTheMap)
        {
            if(item != null)
            {
                save.LootOnTheMap.Add(SaveLootController.SaveLootControl.convert__ItemToSerializable(item.GetComponent<Item>()));
            }
        }

        //save Players position
        PlayerController Player = PlayerController.Player.GetComponent<PlayerController>();
        save.playerPosition[0] = PlayerController.Player.transform.position.x;
        save.playerPosition[1] = PlayerController.Player.transform.position.y;
        save.playerPosition[2] = PlayerController.Player.transform.position.z;


        //save players stats
        save.isDead = Player.isDead;
        save.HP = Player.HP;
        save.MAXHP = Player.MAXHP;
        save.Hunger = Player.Hunger;
        save.MaxHunger = Player.MaxHunger;
        save.LEVEL = Player.LEVEL;
        save.EXP = Player.EXP;
        save.DMG = Player.DMG;
        save.DEF = Player.DEF;
        save.PRECISION = Player.PRECISION;
        save.LUCK = Player.LUCK;
        save.MovementSpeed = Player.GetComponent<MovementController>().MovementSpeed;
        return save;
    }




    public void SaveGameData(String afterAction = null)
    {
        SaveObj save = CreateSaveGameObject();

        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.save");
        bf.Serialize(file, save);
        file.Close();
        Debug.Log("Game Saved");

        if (afterAction != null)
        {
            if (afterAction == "endGame")
            {
                Application.Quit();
            }
            else if(afterAction == "goToMenu")
            {
                SceneManager.LoadScene("Menu");
            }
            
        }
    }

    public bool checkIfSaveExist()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.save"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public void LoadGame()
    {
        if (checkIfSaveExist())
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            SaveObj save = (SaveObj)bf.Deserialize(file);
            file.Close();

            if (save.isDead)
            {
                return;
            }
            
            //load time
            DayCycle.DayCycleController.setDayProgress(save.dayProgress);
            DayCycle.DayCycleController.day = save.day;
            DayCycle.DayCycleController.currentDayState = save.currentDayState;

            //time to change lighting settins
            if (save.currentDayState == "afternoon" || save.currentDayState == "night")
            {
                DayCycle.DayCycleController.decreaseReflection();
                RenderSettings.ambientIntensity = 0.3f;
            }
            else if (save.currentDayState == "sunrise")
            {
                RenderSettings.ambientIntensity = 0.3f;
                DayCycle.DayCycleController.increaseReflection();
            }

                
            //Load players stats
            PlayerController Player = PlayerController.Player.GetComponent<PlayerController>();
            Player.transform.position = new Vector3(save.playerPosition[0], save.playerPosition[1]+0.5f, save.playerPosition[2]);
            
            Player.HP = save.HP;
            Player.MAXHP = save.MAXHP;
            Player.Hunger = save.Hunger;
            Player.MaxHunger = save.MaxHunger;
            Player.LEVEL = save.LEVEL;
            Player.EXP = save.EXP;
            Player.DMG = save.DMG;
            Player.DEF = save.DEF;
            Player.PRECISION = save.PRECISION;
            Player.LUCK = save.LUCK;
            Player.GetComponent<MovementController>().MovementSpeed = save.MovementSpeed;

            //Load inventory
            foreach (InventoryItem itemInInventory in save.Items)
            {
                Inventory.inventory.addItem(null,null,itemInInventory);
            }

            //Load Equip
            foreach (InventoryItem EquippedItem in save.Equipment)
            {
                PlayerController.Player.GetComponent<Equipment>().Equip(EquippedItem);
            }
            
            //Load unlocked skills
            for (int i = 0; i < save.unlockedSkills.Count; i++)
            {
                for (int y = 0; y < SkillTreeController.SkillController.skillList.Length; y++)
                {
                    if (SkillTreeController.SkillController.skillList[y].skillID == save.unlockedSkills[i])
                    {
                        SkillTreeController.SkillController.UnlockSkill(SkillTreeController.SkillController.skillList[y], true);
                    }       
                }
            }
            
            //Load Quests
            QuestController.QuestControl.Controllers[0].QuestNumber = save.QuestNumber_KFC;
            QuestController.QuestControl.Controllers[1].QuestNumber = save.QuestNumber_Cube;
            
            //Load SP
            SkillTreeController.SkillController.FreeSkillPoints = save.SP;
            
            //Load Skillbar Slots
            int slotIndex = 0;
            
            foreach (int skillID in save.skillIDfromSkillBar)
            {
                if (skillID == 0)
                {
                    slotIndex++;
                    continue;
                }
                else
                {
                    for (int i = 0; i < SkillTreeController.SkillController.skillList.Length; i++)
                    {
                        if (SkillTreeController.SkillController.skillList[i].skillID == skillID)
                        {
                            SkillPanelController.SkillPanelControl.SkillSlots[slotIndex].skill = SkillTreeController.SkillController.skillList[i];
                            break;;
                        }       
                    }
                    slotIndex++;
                }
            }
            
            
            //load Enemies
            if (save.Enemies != null || save.Enemies.Count <= 0)
            {
                foreach (var enemy in save.Enemies)
                {
                    if (enemy.isDead == false)
                    {
                        EnemySaveController.enemySaveControl.LoadEnemyFromSave(enemy);
                    }
                }
                enemiesLoaded = true;
            }
            else
            {
                enemiesLoaded = false;
            }
            
            
            
            //load Loot
            if (save.LootOnTheMap != null)
            {
                foreach (var loot in save.LootOnTheMap)
                {
                    SaveLootController.SaveLootControl.LoadLootFromSave(loot);
                }
                Debug.Log("Loaded "+save.LootOnTheMap.Count+" loot");
                lootLoaded = true;
            }
            else
            {
                Debug.Log("Loot couldnt be loaded");
                lootLoaded = false;
            }

            Debug.Log("Game Loaded");
        }
        else
        {
            Debug.Log("No game saved!");
        }
    }
    

    public bool checkIfCanLoad()
    {
        if (checkIfSaveExist() && Debug_load_save)
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.save", FileMode.Open);
            SaveObj save = (SaveObj)bf.Deserialize(file);
            file.Close();

            if (save.isDead)
            {
                return false;
            }
            else
            {
                return true;
            }
        }
        else
        {
            return false;
        }
    }
}
