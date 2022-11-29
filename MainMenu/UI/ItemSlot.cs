using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    Item slotItem;
    public Image image;
    public static SelectItemPrompt parent;
    public void SetItem(Item item) 
    {
        image.sprite = ItemsInventoryUI.getItemImage(item.name);
        slotItem = item;
    }

    public Item GetItem() 
    {
        return slotItem;
    }
}
