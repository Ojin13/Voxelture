using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemUI : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public InventoryItem invItem;
    public RawImage itemImage;
    public TextMeshProUGUI ItemNameText;
    public TextMeshProUGUI ItemRarityText;
    public TextMeshProUGUI AmountText;
    public static GameObject currentlyHoverOver;
    
    public RawImage SetAsQuickAccess;
    public bool isQuickAccessItem = false;
    public Texture setQuickAccessBtn;
    public Texture unsetQuickAccessBtn;

    private void Awake()
    {
        if (GetComponent<Button>() != null)
        {
            GetComponent<Button>().onClick.AddListener(delegate { useItem(); });
        }
    }

    // Update is called once per frame
    void Update()
    {

        if (invItem.amountOwned <= 0)
        {
            Destroy(gameObject);
        }
        
        if (itemImage.texture != null)
        {
            itemImage.color = new Color32(255,255,255,255);
        }
        else
        {
            itemImage.color = new Color32(77,77,77,255);
        }
        
        if (currentlyHoverOver == null)
        {
            Inventory.inventory.detailUI.SetActive(false);
        }
        else
        {
            if (currentlyHoverOver == gameObject && invItem.ID == 0)
            {
                Inventory.inventory.detailUI.SetActive(false);
            }
        }
        
        
        if (SetAsQuickAccess != null)
        {
            if (invItem.itemType == "Consumable")
            {
                SetAsQuickAccess.enabled = true;
            }
            else
            {
                SetAsQuickAccess.enabled = false;
            }

            if (invItem == QuickAccessItem.QuickAccessItemController.qucikAccessItem)
            {
                SetAsQuickAccess.texture = unsetQuickAccessBtn;
                isQuickAccessItem = true;
            }
            else
            {
                SetAsQuickAccess.texture = setQuickAccessBtn;
                isQuickAccessItem = false;
            }
        }
    }

    public void useItem(InventoryItem itemToUse = null)
    {

        
        
        if (itemToUse == null)
        {
            itemToUse = invItem;
        }
        
        
        
        if (Inventory.inventory.getItemSpecsFromID(itemToUse.ID).itemType == "Quest Item")
        {
            return;
        }
        
        if (Inventory.inventory.getItemSpecsFromID(itemToUse.ID).itemType == "Wearable")
        {
            if (!PlayerController.Player.GetComponent<Attack>().isAttacking)
            {
                PlayerController.Player.GetComponent<Equipment>().Equip(itemToUse);
            }
        }
        else if(Inventory.inventory.getItemSpecsFromID(itemToUse.ID).itemType == "Consumable")
        {
            if (itemToUse.addHP != 0)
            {
                PlayerController.Player.GetComponent<PlayerController>().HP += itemToUse.addHP;
            }
            
            if (itemToUse.addHunger != 0)
            {
                PlayerController.Player.GetComponent<PlayerController>().Hunger += itemToUse.addHunger;
            }
            
            if (itemToUse.addEXP != 0)
            {
                PlayerController.Player.GetComponent<EXPController>().AddEXP(itemToUse.addEXP);
            }
            
            SoundPlayer.PlayerSoundController.sound_swallow();
        }
        
        removeItem(false);
    }

    public void Unequip()
    {
        if (!PlayerController.Player.GetComponent<Attack>().isAttacking)
        {
            PlayerController.Player.GetComponent<Equipment>().Unequip(invItem);
        }
    }

    public void removeItem(bool createDrop=true)
    {
        if (createDrop)
        {
            transform.parent.gameObject.GetComponent<Inventory>().removeItem(invItem.ID, ItemRarityText.text);
        }
        else
        {
            transform.parent.gameObject.GetComponent<Inventory>().removeItem(invItem.ID, ItemRarityText.text,false);
        }

        int newAmount = int.Parse(AmountText.text);
        newAmount--;
        if(newAmount <= 0)
        {
            Destroy(gameObject);
        }
        else
        {
            AmountText.text = newAmount.ToString();
        }
    }


    public void OnPointerEnter(PointerEventData eventData)
    {
        if (invItem.ID != 0)
        {
            Inventory.inventory.detailUI.SetActive(true);
            Inventory.inventory.detailUI.GetComponent<ItemDetailUI>().setUpStats(invItem);
            currentlyHoverOver = gameObject;
        }
    }


    public void OnPointerExit(PointerEventData eventData)
    {
        Inventory.inventory.detailUI.SetActive(false);
        currentlyHoverOver = gameObject;
        currentlyHoverOver = null;
    }


    public void SetQuickAccessItem()
    {
        QuickAccessItem.QuickAccessItemController.setQuickAccesItem(this);
    }
}
