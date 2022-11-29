using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ItemRowDisplay : MonoBehaviour
{
    public static Color baseItemColor = new Color(132f/255f,183f,255f,182f/255f);
    //public static Color proceduralItemColor = new Color(132f / 255f, 183f, 255f, 182f / 255f);

    public Text name, count, buttonText;
    public Image image;
    public Button button;
    public Item itemToDisplay;


    public ItemRowDisplay(Item item) 
    {
        this.itemToDisplay = item;
        UpdateDisplay();
    }

    public void UpdateDisplay() 
    {
        if (itemToDisplay.itemType.Equals("Craftable"))
        {
            buttonText.text = "CRAFT";
            gameObject.GetComponent<Image>().color = baseItemColor;
        }
        else 
        {
            buttonText.text = "INFO";
        }

        name.text = itemToDisplay.name;
        count.text = itemToDisplay.ammount.ToString();

        image.sprite = ItemsInventoryUI.getItemImage(itemToDisplay.name);
    }
}
