using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectItemPrompt : MonoBehaviour
{
    public Image largePreview;
    public Text previewItemName;
    public Text previewItemDescription;

    public GameObject slot;

    public Transform slotContainer;

    public Button confirmButton;

    public Item itemInPreview;

    bool responded = false;

    public static SelectItemPrompt instance;

    static Item blankItem = new Item();

    static bool isLoaded = false;

    public void Awake()
    {
        if (instance != null) { Debug.LogWarning("The fuckers are on the loose"); }
        instance = this;
    }

    void UpdateDisplay() 
    {
        foreach(Transform child in slotContainer) 
        {
            Destroy(child.gameObject);
        }
        ItemSlot.parent = this;
        GameObject defaultSlot = Instantiate(slot, slotContainer) as GameObject;
        defaultSlot.GetComponent<ItemSlot>().SetItem(blankItem);

        for (int i = 0; i < AccountStats.dataHolder.items.Count; i++) 
        {
            if (AccountStats.dataHolder.items[i].ammount == 0) 
            {
                continue;
            }
            GameObject newSlot = Instantiate(slot, slotContainer) as GameObject;
            newSlot.GetComponent<ItemSlot>().SetItem(AccountStats.dataHolder.items[i]);
            newSlot.GetComponent<Button>().onClick.AddListener(delegate { SetPreviewItem(newSlot.GetComponent<ItemSlot>().GetItem()); });
        }
    }

    public void SetPreviewItem(Item item) 
    {
        itemInPreview = item;
        largePreview.sprite = ItemsInventoryUI.getItemImage(item.name);
        previewItemName.text = item.name;
        previewItemDescription.text = item.description;

    }

    public void Load() 
    {
        //Awake();
        if (isLoaded) { return; }
        Awake();

    }
    public static void GetItemFromInventory() 
    {
        instance.Load();
        instance.gameObject.SetActive(true);
        instance.UpdateDisplay();
        //return null;
    }

    public void SelectButtonOnClick() 
    {
        if (itemInPreview == null) { return; }

        responded = true;

        gameObject.SetActive(false);

    }

    public bool isGettable() 
    {
        return ((itemInPreview != null) && (responded));
    }

}
