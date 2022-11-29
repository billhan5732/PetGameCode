using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Xml;
using System.Xml.Serialization;
using System.IO;

[System.Serializable]
[XmlInclude(typeof(CraftableItem))]
public class Item
{
    // Start is called before the first frame update

    public string name;

    public string description;

    public int ammount;

    public string itemType = "Regular";

    public Item() 
    {
        name = "Default Item";
        description = "Default Description";
        ammount = 1;

    }

    public Item(string name, int amt, string description) 
    {
        this.name = name;
        this.ammount = amt;
        this.description = description;
    }

    public Item getIndividual() 
    {
        ammount -= 1;
        return new Item(name,1,description);
    }

}
[System.Serializable]
public class CraftableItem: Item
{

    public Recipe recipe;
    public CraftableItem(): base()
    {
        recipe = new Recipe();
        base.itemType = "Craftable";
    }

    public CraftableItem(string name, int ammount, string description, Recipe recipe) : base(name,ammount, description)
    {
        this.recipe = recipe;
        base.itemType = "Craftable";
    }

    public static CraftableItem CreateCopy(CraftableItem src) 
    {
        return new CraftableItem(src.name, src.ammount, src.description, src.recipe);
    }

}
[System.Serializable]
public class Recipe 
{
    List<string> materialNames = new List<string>();
    List<int> materialAmmounts = new List<int>();

    public Recipe() 
    {

    }

    public Recipe(List<string> names, List<int> ammounts) 
    {
        materialNames = names;
        materialAmmounts = ammounts;
    }

}