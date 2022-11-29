using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using UnityEngine;

public class AccountStats : MonoBehaviour
{
    #region Singleton
    public static AccountStats instance;
    //public static List<Pet> petsListOG = new List<Pet>();
    void Awake()
    {
        //dataHolder.pets = new List<Pet>();//temp

        LoadDataHolder();
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found");
            return;
        }
        instance = this;


    }

    #endregion

    public static AccountStatsDataHolder dataHolder;

    public static void AddCoins(int ammount) 
    {
        LoadDataHolder();
        dataHolder.coins += ammount;
        Debug.Log(ammount.ToString() + " added to account. Total now: " + dataHolder.coins);
        
        SaveDataHolder();
    }

    public static void RemoveCoins(int ammount)
    {
        LoadDataHolder();
        dataHolder.coins -= ammount;
        SaveDataHolder();
    }

    #region XML
    public static void LoadDataHolder()
    {
        var serializer = new XmlSerializer(typeof(AccountStatsDataHolder));
        var fileStream = new FileStream(Application.dataPath + "/XmlStuff/account_data.xml", FileMode.Open);
        dataHolder = serializer.Deserialize(fileStream) as AccountStatsDataHolder;
        fileStream.Close();

        //itemAmmounts = dataHolder.item_ammounts.Clone() as int[];
    }
    public static void SaveDataHolder()
    {
        //dataHolder.item_ammounts = itemAmmounts.Clone() as int[];

        var serializer = new XmlSerializer(typeof(AccountStatsDataHolder));
        var fileStream = new FileStream(Application.dataPath + "/XmlStuff/account_data.xml", FileMode.Create);
        serializer.Serialize(fileStream, dataHolder);
        fileStream.Close();
    }
    #endregion



}
public class AccountStatsDataHolder 
{
    #region BaseItems
    static CraftableItem breedingCharm1 = new CraftableItem("Breeding Charm LVL 1",0,"Half the Remaining Breeding Time",new Recipe(new List<string> { "Material" }, new List<int> { 1 }));
    static CraftableItem breedingCharm2 = new CraftableItem("Breeding Charm LVL 2", 0, "One Third the Remaining Breeding Time", new Recipe(new List<string> { }, new List<int> { }));
    static CraftableItem rarityCharm1 = new CraftableItem("Rarity Charm LVL 1", 0, "", new Recipe(new List<string> { "Material" }, new List<int> { 1 }));
    static CraftableItem rarityCharm2 = new CraftableItem("Rarity Charm LVL 1", 0, "", new Recipe(new List<string> { "Material" }, new List<int> { 1 }));
    static CraftableItem traitCharm1 = new CraftableItem("Trait Charm LVL 1", 0, "", new Recipe(new List<string> { "Material" }, new List<int> { 1 }));
    static CraftableItem traitCharm2 = new CraftableItem("Trait Charm LVL 2", 0, "", new Recipe(new List<string> { "Material" }, new List<int> { 1 }));
    static CraftableItem damageCharm = new CraftableItem("Damage Charm LVL 1", 0, "", new Recipe(new List<string> { "Material" }, new List<int> { 1 }));
    static CraftableItem defenseCharm = new CraftableItem("Defense Charm LVL 1", 0, "", new Recipe(new List<string> { "Material" }, new List<int> { 1 }));
    static CraftableItem healthCharm = new CraftableItem("Health Charm LVL 1", 0, "", new Recipe(new List<string> { "Material" }, new List<int> { 1 }));
    static CraftableItem speedCharm = new CraftableItem("Speed Charm LVL 1", 0, "", new Recipe(new List<string> { "Material" }, new List<int> { 1 }));
    static CraftableItem[] baseItems = {breedingCharm1,breedingCharm2,rarityCharm1,rarityCharm2,traitCharm1,traitCharm2,damageCharm,defenseCharm,healthCharm,speedCharm };
    #endregion

    public int accountLevel = 0;

    public int accountTotalXP = 0;

    public int coins = 0;

    public List<Item> items = new List<Item>();

    public CraftableItem craftableItemTest;

    public AccountStatsDataHolder() { }//Parameterless Constructor

    public AccountStatsDataHolder(int level, int txp, int coins) 
    {
        this.accountLevel = level;
        this.accountTotalXP = txp;
        this.coins = coins;
    }

    public void ResetItemsInventory()
    {
        items.Clear();
        //Adding Base Items
        for (int i = 0; i < baseItems.Length; i++) 
        {
            items.Add(CraftableItem.CreateCopy(baseItems[i]));
        }
        //items.Add(breedingCharm1);

    }

}