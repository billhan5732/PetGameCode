using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

public class MaterialsInventory : MonoBehaviour
{
    #region Singleton
    public static MaterialsInventory instance;
    //public static List<Pet> petsListOG = new List<Pet>();
    void Awake()
    {
        //dataHolder.pets = new List<Pet>();//temp
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found");
            return;
        }
        instance = this;


    }

    #endregion

    public static MaterialStorageDataHolder dataHolder = new MaterialStorageDataHolder();

    public static string[] itemNames = {"Wood", "Coconut", "Fibers", "Steel", "Titanium", "Gold", "Saphire", "Ruby", "Stone", "Obsidian", "Red Berry", "Blue Berry", "Orange"};
    public static int[] itemAmmounts = new int[itemNames.Length];

    void Start() { LoadDataHolder(); if (itemNames != null) { return; } }

    //public static void InitializeLists

    public static void EmptyAmmounts() { for (int i = 0; i < itemNames.Length; i++) { itemAmmounts[i] = 0; } }

    public static void LoadDataHolder() 
    {
        var serializer = new XmlSerializer(typeof(MaterialStorageDataHolder));
        var fileStream = new FileStream(Application.dataPath + "/XmlStuff/material_storage_data.xml", FileMode.Open);
        dataHolder = serializer.Deserialize(fileStream) as MaterialStorageDataHolder;
        fileStream.Close();

        itemAmmounts = dataHolder.item_ammounts.Clone() as int[];
    }
    public static void SaveDataHolder() 
    {
        dataHolder.item_ammounts = itemAmmounts.Clone() as int[];

        var serializer = new XmlSerializer(typeof(MaterialStorageDataHolder));
        var fileStream = new FileStream(Application.dataPath + "/XmlStuff/material_storage_data.xml", FileMode.Create);
        serializer.Serialize(fileStream, dataHolder);
        fileStream.Close();
    }

    public static void AddResources(string name, int ammount) { itemAmmounts[FindByName(name)] += Mathf.Abs(ammount); }
    public static void UseUpResources(string name, int ammount) { itemAmmounts[FindByName(name)] -= Mathf.Abs(ammount); }

    static int FindByName(string name) { for (int i = 0; i < itemNames.Length; i++) { if (name.Equals(itemNames[i])) { return i; } } return -1; }

    public static void AddBackPack() 
    {
        BackPack.LoadDataHolderForMI();
        string[] names = BackPack.getNames();
        int[] counts = BackPack.getCounts();

        for (int i = 0; i < names.Length; i++) 
        {
            if (names[i] != "") 
            {
                AddResources(names[i], counts[i]);
            }
            
        }
        BackPack.ClearDataHolder();
    }

    public static rawMaterial getMaterial(string name) 
    {
        
        for (int i = 0; i < itemNames.Length; i++) 
        {
            if (name.Equals(itemNames[i])) 
            {
                return new rawMaterial(itemNames[i], itemAmmounts[i]);
            }
        }

        return null;
    }

}

[System.Serializable]
public class MaterialStorageDataHolder 
{
    public int[] item_ammounts;
}

public class rawMaterial
{
    public string name;
    public int ammount;

    public rawMaterial() 
    {
        this.name = "Material";
        this.ammount = 0;
    }
    public rawMaterial(string name, int amt) 
    {
        this.name = name;
        this.ammount = amt;
    }
}