using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SkillTreeController : MonoBehaviour
{
    public static SkillTreeController SkillController;
    public RawImage SelectedSkillImage;
    public TextMeshProUGUI SelectedSkillNameText;
    public TextMeshProUGUI SelectedSkillDescText;
    public TextMeshProUGUI FreeSkillPointsText;
    public GameObject UnlockButton;
    public Sprite locked;
    public Sprite unlocked;
    public Image Lock;
    public SkillPanelController SkillPanel;

    public int FreeSkillPoints;
    public Skill selectedSkill;
    public List<int> unlockedSkills;
    public Skill[] skillList;
    
    
    // Start is called before the first frame update
    void Awake()
    {
        SkillController = this;
        skillList = GetComponentsInChildren<Skill>();

        if (selectedSkill == null)
        {
            SelectSkill(skillList[0]);
        }
    }

    // Update is called once per frame
    void Update()
    {
        FreeSkillPointsText.text = FreeSkillPoints.ToString();

        if (selectedSkill != null)
        {
            if (selectedSkill.unlocked)
            {
                UnlockButton.SetActive(false);
            }
            else
            {
                UnlockButton.SetActive(true);
            }
        }
    }

    public void SelectSkill(Skill skill)
    {
        selectedSkill = skill;

        if (skill.skillImage != null)
        {
            SelectedSkillImage.texture = skill.skillImage.texture;
        }
        
        SelectedSkillNameText.text = skill.name;
        SelectedSkillDescText.text =
            "<b>Description:</b>\n" +
            skill.desc + "\n" + "\n";

        if (skill.cooldown > 0)
        {
            SelectedSkillDescText.text += "<b>Cooldown:</b>\n" + skill.cooldown + " s\n" + "\n";
        }
        
        SelectedSkillDescText.text += 
            "<b>Required SP:</b>\n"+
            skill.requiredSkillPoints+"\n"+"\n";

        if (skill.requiredToUnlock.Length > 0)
        {
            SelectedSkillDescText.text += "<b>Requied to learn first:</b>\n";

            for (int i = 0; i < skill.requiredToUnlock.Length; i++)
            {
                SelectedSkillDescText.text += skill.requiredToUnlock[i].name+"\n";
            }
        }
        

        UnlockButton.GetComponentInChildren<TextMeshProUGUI>().text = "Unlock for " + skill.requiredSkillPoints+" SP";
        
        if(!canUnlock())
        {
            Lock.sprite = locked;
        }
        else
        {
            Lock.sprite = unlocked;
        }
    }

    public void UnlockSelectedSkill()
    {
        UnlockSkill(selectedSkill);
    }

    public void UnlockSkill(Skill skillToUnlock = null, bool fromSave = false)
    {
        if (skillToUnlock == null)
        {
            Debug.LogError("Unlock skill: null!");
            return;
        }

        if (fromSave)
        {
            skillToUnlock.unlocked = true;
            unlockedSkills.Add(skillToUnlock.skillID);
            return;
        }
        
        if (canUnlock() && skillToUnlock.unlocked == false)
        {
            FreeSkillPoints -= skillToUnlock.requiredSkillPoints;
            skillToUnlock.unlocked = true;
            unlockedSkills.Add(skillToUnlock.skillID);

            if (skillToUnlock.canBeInSlot)
            {
                foreach (SkillSlot slot in SkillPanel.SkillSlots)
                {
                    if (slot.skill == null)
                    {
                        slot.newSkillAsigned(skillToUnlock);
                        break;
                    }
                }
            }
        }
    }

    public bool canUnlock()
    {
        if (selectedSkill.requiredSkillPoints > FreeSkillPoints)
        {
            return false;
        }
        
        for (int i = 0; i < selectedSkill.requiredToUnlock.Length; i++)
        {
            if (selectedSkill.requiredToUnlock[i].unlocked == false)
            {
                return false;
            }
        }
        return true;
    }
    
    bool canBeUsed(bool unlocked)
    {
        if (unlocked)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
