using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SkillSlotDrag : MonoBehaviour, IDragHandler,IEndDragHandler
{
    public Skill draggedSkillData = null;
    public GameObject draggedItemTemplate;
    public GameObject draggedUI;
    private bool createCopy;
    private Vector3 initialPosition;
    public GameObject InitParent;

    void Awake()
    {
        initialPosition = transform.position;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (GetComponent<Skill>() == null)
        {
            draggedSkillData = GetComponent<SkillSlot>().skill;
            createCopy = false;
        }
        else
        {
            draggedSkillData = GetComponent<Skill>();
            createCopy = true;
        }

        if (draggedSkillData == null)
        {
            return;
        }
        
        if (draggedSkillData.unlocked && draggedSkillData.canBeInSlot && draggedSkillData.ready)
        {
            if (draggedUI == null)
            {
                if (createCopy)
                {
                    draggedUI = Instantiate(draggedItemTemplate, transform.position, Quaternion.identity,gameObject.transform);
                    draggedUI.transform.SetParent(InitParent.transform);
                    draggedUI.GetComponent<RawImage>().texture = draggedSkillData.skillImage.texture; 
                }
                else
                {
                    draggedUI = gameObject.GetComponentsInChildren<RawImage>()[0].gameObject;
                }
            }
            draggedUI.transform.position = Input.mousePosition;   
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        float closestMatch = 100;
        SkillSlot putToSlot = null;
        
        foreach (SkillSlot slot in SkillPanelController.SkillPanelControl.SkillSlots)
        {
            if (draggedUI == null)
            {
                return;
            }
            
            float distance = Vector2.Distance(draggedUI.GetComponent<RectTransform>().transform.position,slot.gameObject.GetComponent<RectTransform>().transform.position);
            if (distance < 40)
            {
                if (distance < closestMatch)
                {
                    closestMatch = distance;
                    putToSlot = slot;
                }
            }
        }

        if (putToSlot != null)
        {
            if (createCopy)
            {
                putToSlot.skill = draggedSkillData;
                Destroy(draggedUI);
            }
            else
            {
                if (putToSlot.skill != null)
                {
                    if (putToSlot.skill.ready)
                    {
                        GetComponent<SkillSlot>().skill = putToSlot.skill;
                        putToSlot.skill = draggedSkillData;
                    }
                }
                else
                {
                    putToSlot.skill = draggedSkillData;
                    GetComponent<SkillSlot>().skill = null;
                }
            }
        }
        else
        {
            if (createCopy)
            {
                Destroy(draggedUI);
            }
            else
            {
                GetComponent<SkillSlot>().skill = null;
            }    
        }
        
        draggedSkillData = null;
        draggedUI.transform.position = initialPosition;
        draggedUI = null;
    }
}
