using System.Collections;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Collections.Generic;
using UnityEngine;

public class BackPack : MonoBehaviour
{
    #region Singleton
    public static BackPack instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of BackPack found");
            return;
        }
        instance = this;


    }

    #endregion

    public static int slots = 10;
    public static int stackSize = 5;

    static int[] itemCounts;// = new int[slots];
    static string[] itemNames;// = new string[slots];
    public static GameObject[] itemGOs;

    static BackPackSave dataHolder;// = new BackPackSave();

    void Start()
    {
        //
        //ClearDataHolder();
        itemCounts = new int[slots];
        itemNames = new string[slots];
        itemGOs = new GameObject[slots];
        

        //dataHolder = new BackPackSave(itemCounts, itemNames, slots, stackSize);
        //SaveDataHolder();
        LoadDataHolder();
        //ClearDataHolder();
    }

    void CheckOut() { }

    void Update()
    {
        
    }

    public static void AddItem(Drop drop) 
    {
        for (int i = 0; i < itemNames.Length; i++) 
        {
            if ((itemNames[i] == drop.name) && itemCounts[i] < stackSize) { itemCounts[i]++; IGGameController.PlayerHasPickedUpItem(drop.name); return; }

        }

        //Create New Stack
        for (int i = 0; i < itemNames.Length; i++) 
        {
            if (itemNames[i] == "") 
            {
                itemNames[i] = drop.name; 
                itemCounts[i] = 1;
                //itemGOs[i] = drop.gameObjectModel;
                //drop.setAsStatic();
                GameObject newObj = Instantiate(drop.gameObjectModel) as GameObject;
                itemGOs[i] = newObj;
                drop.gameObject.SetActive(false);
                //itemGOs[i] = drop; 
                //Debug.Log("Somehting");
                SaveDataHolder();
                IGGameController.PlayerHasPickedUpItem(drop.name);
                return;
            }
        }
        
    }

    public static string[] getNames() { return itemNames; }
    public static int[] getCounts() { return itemCounts; }
    public static GameObject[] getGOs() { return itemGOs; }



    public static void LoadDataHolder()
    {
        var serializer = new XmlSerializer(typeof(BackPackSave));
        var fileStream = new FileStream(Application.dataPath + "/XmlStuff/backpack_data.xml", FileMode.Open);
        dataHolder = serializer.Deserialize(fileStream) as BackPackSave;
        fileStream.Close();

        //itemAmmounts = dataHolder.item_ammounts.Clone() as int[];
        slots = dataHolder.slots; stackSize = dataHolder.stackSize;
        itemCounts = dataHolder.itemCounts.Clone() as int[];
        itemNames = dataHolder.itemNames.Clone() as string[];
        itemGOs = new GameObject[slots];
        //ConfigResourceManager.instance.loadResources();
        for (int i = 0; i < itemNames.Length; i++) 
        {
            if (!itemNames[i].Equals(""))
            {
                //Debug.Log("Hi there");
                
                GameObject newIconGO = Instantiate(ConfigResourceManager.instance.getModelByName(itemNames[i])) as GameObject;
                newIconGO.GetComponent<Drop>().setAsStatic();
                Debug.Log("Backpack Loading - " + newIconGO.GetComponent<Drop>().name);
                itemGOs[i] = newIconGO;
                //itemGOs[i].GetComponent<Drop>().setAsStatic();
            }
            else 
            {
                Debug.Log("Backpack Loading - " + i.ToString());
            }
            
        }

        Debug.Log("Backpack data loaded");
    }

    public static void LoadDataHolderForMI()
    {
        var serializer = new XmlSerializer(typeof(BackPackSave));
        var fileStream = new FileStream(Application.dataPath + "/XmlStuff/backpack_data.xml", FileMode.Open);
        dataHolder = serializer.Deserialize(fileStream) as BackPackSave;
        fileStream.Close();

        //itemAmmounts = dataHolder.item_ammounts.Clone() as int[];
        slots = dataHolder.slots; stackSize = dataHolder.stackSize;
        itemCounts = dataHolder.itemCounts.Clone() as int[];
        itemNames = dataHolder.itemNames.Clone() as string[];

        

    }

    public static void SaveDataHolder()
    {
        //dataHolder.item_ammounts = itemAmmounts.Clone() as int[];
        //dataHolder = new BackPackSave(itemCounts, itemNames, slots, stackSize);

        //Debug.Log("Saving");
        if (itemNames[0] != "")
        {
            dataHolder.SetValues(itemCounts, itemNames, slots, stackSize);
        }
        else 
        {
            //Debug.Log("AAAAAAAAAAAAAAAAAAA");
            //dataHolder.SetValues(new int[slots], new string[slots], slots, stackSize);
            dataHolder.SetValues(itemCounts, itemNames, slots, stackSize);
        }

        var serializer = new XmlSerializer(typeof(BackPackSave));
        var fileStream = new FileStream(Application.dataPath + "/XmlStuff/backpack_data.xml", FileMode.Create);
        serializer.Serialize(fileStream, dataHolder);
        fileStream.Close();
    }

    public static void ClearDataHolder() 
    {

        itemNames = new string[slots];
        itemCounts = new int[slots];

        for (int i = 0; i < slots; i++) 
        {
            itemNames[i] = "";
            itemCounts[i] = 0;
        }

        SaveDataHolder();
    }

}

public class BackPackSave 
{
    public int slots;
    public int stackSize;

    public int[] itemCounts;// = new int[slots];
    public string[] itemNames;

    public void SetValues(int[] counts, string[] names, int slots, int stackSize) 
    {
        this.slots = slots;
        this.stackSize = stackSize;
        this.itemNames = names.Clone() as string[];
        this.itemCounts = counts.Clone() as int[];
    }

    public BackPackSave() { }

    public BackPackSave(int[] counts, string[] names, int slots, int stackSize) 
    {
        this.itemCounts = counts.Clone() as int[];
        this.itemNames = names.Clone() as string[];
        this.slots = slots;
        this.stackSize = stackSize;
    }

}
