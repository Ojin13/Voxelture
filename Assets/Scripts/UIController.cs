using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIController : MonoBehaviour
{
    public GameObject UserPanel;
    public bool UserPanelIsActive = false;

    public GameObject SkillTreePanel;
    public bool SkillTreePanelIsActive = false;
    
    public GameObject DialoguePanel;
    public bool DialoguePanelIsActive = false;
    
    public GameObject RespawnPanel;
    public bool RespawnPanelIsActive = false;

    public GameObject QuickAccessPanel;
    public bool QuickAccessPanelIsActive;
    
    public GameObject Esymbol;
    public bool EsymbolIsActive;

    public GameObject TalkSymbol;
    public bool TalkSymbolActive;

    public GameObject SwordKeyHint;
    public bool SwordKeyHintIsActive;

    public GameObject TorchKeyHint;
    public bool TorchKeyIsActive;

    public GameObject MiniMap;
    public bool MiniMapIsActive;
    public Skill map;
    
    public GameObject inGameMenu;
    public GameObject MenuButton;
    public bool inGameMenuIsActive;

    public static UIController UIControl;


    void Awake()
    {
        UIControl = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        UserPanelIsActive = false;
        UserPanel.SetActive(false);
        
        SkillTreePanelIsActive = false;
        SkillTreePanel.SetActive(false);
        
        DialoguePanelIsActive = false;
        DialoguePanel.SetActive(false);
        
        inGameMenuIsActive = false;
        inGameMenu.SetActive(false);
        
        QuickAccessPanel.SetActive(true);
        QuickAccessPanelIsActive = true;
        
        MiniMapIsActive = false;
        MiniMap.SetActive(false);
        
        deactivateTalkSymbol();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            if(!UserPanelIsActive)
            {
                activateUserPanel();
            }
            else
            {
                deactivateUserPanel();
            }
        }
        
        if(Input.GetKeyDown(KeyCode.L))
        {
            if(!SkillTreePanelIsActive)
            {
                activateSkillTree();
            }
            else
            {
                deactivateSkillTree();
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!SkillTreePanelIsActive && !UserPanelIsActive && !inGameMenuIsActive && !DialoguePanelIsActive && !RespawnPanelIsActive)
            {
                activateInGameMenu();
            }
            else
            {
                deactivateSkillTree();
                deactivateUserPanel();
                deactivateInGameMenu();
                deactivateMiniMap();
            }
        }

        if (PlayerController.Player.GetComponent<Equipment>().Accessory.GetComponent<ItemUI>().invItem.name == "Torch")
        {
            activateTorchHint();
        }
        else
        {
            deactivateTorchHint();
        }
        
        if (PlayerController.Player.GetComponent<Equipment>().Weapon.GetComponent<ItemUI>().invItem.ID != 0)
        {
            activateSwordHint();
        }
        else
        {
            deactivateSwordHint();
        }

        if (Input.GetKeyDown(KeyCode.M))
        {
            if (MiniMapIsActive)
            {
                deactivateMiniMap();
            }
            else
            {
                activateMiniMap();
            }
        }
        
        checkMouseControl();
    }

    
    //deactivate section
    public void deactivateDialogues()
    {
        DialoguePanel.SetActive(false);
        DialoguePanelIsActive = false;
        activateQuickAccessPanel();
    }

    public void deactivateSkillTree()
    {
        SkillTreePanel.SetActive(false);
        SkillTreePanelIsActive = false;
    }

    public void deactivateUserPanel()
    {
        UserPanel.SetActive(false);
        UserPanelIsActive = false;
    }
    
    public void deactivateQuickAccessPanel()
    {
        QuickAccessPanel.SetActive(false);
        QuickAccessPanelIsActive = false;
    }

    public void deactivateEsymbol()
    {
        Esymbol.SetActive(false);
        EsymbolIsActive = false;
    }

    public void deactivateTalkSymbol()
    {
        TalkSymbol.SetActive(false);
        TalkSymbolActive = false;
    }

    public void deactivateTorchHint()
    {
        TorchKeyHint.SetActive(false);
        TorchKeyIsActive = false;
    }
    
    public void deactivateSwordHint()
    {
        SwordKeyHint.SetActive(false);
        SwordKeyHintIsActive = false;
    }

    public void deactivateMiniMap()
    {
        MiniMap.SetActive(false);
        MiniMapIsActive = false;
    }
    
    public void deactivateInGameMenu()
    {
        inGameMenu.SetActive(false);
        inGameMenuIsActive = false;
        MenuButton.SetActive(true);
    }
    
    
    //activate section
    public void activateDialogues()
    {
        deactivateEsymbol();
        deactivateTalkSymbol();
        deactivateSkillTree();
        deactivateUserPanel();
        deactivateQuickAccessPanel();
        deactivateSwordHint();
        deactivateTorchHint();
        deactivateMiniMap();
        DialoguePanel.SetActive(true);
        DialoguePanelIsActive = true;
    }

    public void activateSkillTree()
    {
        deactivateMiniMap();
        SkillTreePanel.SetActive(true);
        SkillTreePanelIsActive = true;
    }

    public void activateUserPanel()
    {
        UserPanel.SetActive(true);
        UserPanelIsActive = true;
    }

    public void activateRespawnPanel()
    {
        RespawnPanel.SetActive(true);
        RespawnPanelIsActive = true;
    }

    public void activateQuickAccessPanel()
    {
        QuickAccessPanel.SetActive(true);
        QuickAccessPanelIsActive = true;
    }
    
    public void activateTorchHint()
    {
        TorchKeyHint.SetActive(true);
        TorchKeyIsActive = true;
    }
    
    public void activateSwordHint()
    {
        SwordKeyHint.SetActive(true);
        SwordKeyHintIsActive = true;
    }

    public void activateInGameMenu()
    {
        deactivateEsymbol();
        deactivateTalkSymbol();
        deactivateSkillTree();
        deactivateUserPanel();
        deactivateQuickAccessPanel();
        deactivateSwordHint();
        deactivateTorchHint();
        deactivateMiniMap();
        MenuButton.SetActive(false);
        inGameMenu.SetActive(true);
        inGameMenuIsActive = true;
    }

    public void activateMiniMap()
    {
        if (map.unlocked)
        {
            MiniMap.SetActive(true);
            MiniMapIsActive = true;
        }
    }


    //set up mouse controll
    public void checkMouseControl()
    {
        if (UserPanelIsActive || SkillTreePanelIsActive || DialoguePanelIsActive || RespawnPanelIsActive || inGameMenuIsActive)
        {
            CinemachineManualFreeLookDrag.MouseController.turnOnCursor();
        }
        else
        {
            CinemachineManualFreeLookDrag.MouseController.turnOffCursor();
        }
    }
}
