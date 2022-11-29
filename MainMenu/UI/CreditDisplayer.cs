using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CreditDisplayer : MonoBehaviour
{
    public Text text;

    Inventory inventory;

    void Start()
    {
        this.text = GetComponentInChildren<Text>();
        inventory = Inventory.instance;
        UpdateDisplay();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateDisplay()
    {
        AccountStats.LoadDataHolder();
        //Separate 3 digits From back
        string textToAdd = "";
        int index = 0;
        string creditAmmountString = AccountStats.dataHolder.coins.ToString();//Inventory.getCreditAmmount().ToString();
        char[] characterArray = creditAmmountString.ToCharArray();

        for (int i = (characterArray.Length - 1); i >= 0; i -= 1) { 

            textToAdd += characterArray[i].ToString();
            index++;
            if (index % 3 == 0)
            {
                textToAdd += ",";
            }

        }
        //Reversing
        char[] booger = textToAdd.ToCharArray();
        System.Array.Reverse(booger);
        string newText = new string(booger);
        //Setting
        this.text.text = newText;
    }
}
