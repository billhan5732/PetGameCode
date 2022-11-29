using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MIUI : MonoBehaviour
{
    public GameObject inventoryDisplayGroup;
    public GameObject UIButtons;
    public GameObject slotHolder;

    Text[] textBoxes = new Text[MaterialsInventory.itemAmmounts.Length];

    void Start()
    {
        LoadTexts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void GetTextsList() { int i = 0; 
        foreach (Transform child in slotHolder.transform) 
        {
            if (i >= MaterialsInventory.itemAmmounts.Length) { return; }
            foreach (Transform child2 in child) 
            {
                if (child2.gameObject.name.Equals("Count")) 
                { textBoxes[i] = child2.gameObject.GetComponent<Text>(); }
                
            }
            
            i++; 
        }
        Debug.Log(i); }
    void LoadTexts() { GetTextsList(); for(int i = 0; i < textBoxes.Length; i++) { textBoxes[i].text = SplitBy3Digits(MaterialsInventory.itemAmmounts[i]); } }

    string SplitBy3Digits(int number) 
    {
        string textToAdd = "";
        int index = 0;
        string creditAmmountString = number.ToString();//Inventory.getCreditAmmount().ToString();
        char[] characterArray = creditAmmountString.ToCharArray();
        if (characterArray.Length <= 3) { return creditAmmountString; }

        for (int i = (characterArray.Length - 1); i >= 0; i -= 1)
        {

            textToAdd += characterArray[i].ToString();
            index++;
            if (index % 3 == 0)
            {
                textToAdd += ",";
            }

        }
        //Reversing
        char[] booger = textToAdd.ToCharArray();
        Array.Reverse(booger);
        string newText = new string(booger);

        return newText;
    }

    public void OpenMaterialsInventoryPage() { UIButtons.SetActive(false); inventoryDisplayGroup.SetActive(true); MaterialsInventory.AddBackPack(); }
    public void CloseMaterialsInventoryPage() { UIButtons.SetActive(true); inventoryDisplayGroup.SetActive(false); MaterialsInventory.SaveDataHolder(); }


}
