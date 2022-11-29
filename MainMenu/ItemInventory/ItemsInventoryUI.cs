using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemsInventoryUI : MonoBehaviour
{
    // Start is called before the first frame update
    public static ItemsInventoryUI instance;
    private void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Items Inventory found");
            return;
        }
        instance = this;
    }

    public GameObject UIButtons;

    public GameObject defaultSlot;

    public Transform contentHolder;

    //public string[] itemNameArray;
    public Sprite[] itemImageArray;
    
    public Dictionary<string,Sprite> imageDictionary = new Dictionary<string,Sprite>();

    public static bool isLoaded = false;

    void Start()
    {
        for (int i = 0; i < itemImageArray.Length; i++) 
        {
            //imageDictionary.Add(itemImageArray[i].name, itemImageArray[i]);
        }

        List<Item> itemListTest = new List<Item>();

        //Debug.Log("Account Stats Test");
        AccountStats.LoadDataHolder();

        AccountStats.dataHolder.ResetItemsInventory();
        AccountStats.SaveDataHolder();
        UpdateInventoryMenu();
        //itemListTest.Add(new CraftableItem("Pot", 1, "Literally Just Weed", new Recipe()));
        //itemListTest.Add(new Item());
        //AccountStats.dataHolder.items.Add(new CraftableItem());
        isLoaded = true;
    }
    void UpdateInventoryMenu() 
    {
        int length = AccountStats.dataHolder.items.Count;
        for (int i = 0; i < length; i++) 
        {
            GameObject newRow = Instantiate(defaultSlot, contentHolder) as GameObject;
            newRow.GetComponent<ItemRowDisplay>().itemToDisplay = AccountStats.dataHolder.items[i];
            newRow.GetComponent<ItemRowDisplay>().UpdateDisplay();
        }

        
    }
    // Update is called once per frame
    public void LoadItemSprites()
    {
        if (isLoaded) { return; }

        Awake();

        for (int i = 0; i < itemImageArray.Length; i++)
        {
            imageDictionary.Add(itemImageArray[i].name, itemImageArray[i]);
        }
        isLoaded = true;

        UpdateInventoryMenu();
    }

    public static Sprite getItemImage(string name) 
    {

        if (instance.imageDictionary.ContainsKey(name)) { return instance.imageDictionary[name]; }
        Debug.Log(name + " is not in the Dictionary");
        return null;
    }

    public void OpenItemsInventoryPage() { UIButtons.SetActive(false); gameObject.SetActive(true); }
    public void CloseItemsInventoryPage() { UIButtons.SetActive(true); gameObject.SetActive(false);/* Save Inventory?? */ }

}
