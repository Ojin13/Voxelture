using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuickAccessItem : MonoBehaviour
{
    public static QuickAccessItem QuickAccessItemController;
    public InventoryItem qucikAccessItem;
    public RawImage itemImage;
    public TextMeshProUGUI ItemNameText;
    public TextMeshProUGUI AmountText;
    private ItemUI QuickItemSlot;

    // Start is called before the first frame update
    void Awake()
    {
        QuickAccessItemController = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (qucikAccessItem == null)
        {
            return;
        }

        if (qucikAccessItem.amountOwned > 0 && qucikAccessItem.ID != 0)
        {
            itemImage.enabled = true;
            itemImage.texture = (Texture)Resources.Load(Inventory.inventory.getItemSpecsFromID(qucikAccessItem.ID).imagePath);
            ItemNameText.text = qucikAccessItem.name+" [R]";
            AmountText.text = qucikAccessItem.amountOwned.ToString();
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                QuickItemSlot.useItem(QuickItemSlot.invItem);
            }
        }
        else
        {
            itemImage.enabled = false;
            ItemNameText.text = "";
            AmountText.text = "";
        }
    }


    public void setQuickAccesItem(ItemUI item)
    {
        if (item.invItem == qucikAccessItem)
        {
            qucikAccessItem.isQuickAccessItem = false;
            qucikAccessItem = null;
            QuickItemSlot = null;
        }
        else
        {
            qucikAccessItem = item.invItem;
            QuickItemSlot = item;
            qucikAccessItem.isQuickAccessItem = true;
        }
    }
}
