using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RarityDisplay : MonoBehaviour
{
    // Start is called before the first frame update

    enum Rarity { Common, Rare, UltraRare, Legend, Mythic, Event }


    public int rarityInt;
    private string rarityString;
    public Image image;
    public Text text;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void setRarity(int rarity)
    {
        rarityString = "RARITY";
        switch (rarity)
        {
            case (0):
                rarityInt = ConfigValues.COMMON;
                rarityString = "COMMON";
                this.image.GetComponent<Image>().color = ConfigValues.COMMON_COLOR;
                break;
            case (1):
                rarityInt = ConfigValues.RARE;
                rarityString = "RARE";
                this.image.GetComponent<Image>().color = ConfigValues.RARE_COLOR;
                break;
            case (2):
                rarityInt = ConfigValues.ULTRA_RARE;
                rarityString = "ULTRA RARE";
                this.image.GetComponent<Image>().color = ConfigValues.ULTRA_RARE_COLOR;
                break;
            case (3):
                rarityInt = ConfigValues.LEGEND;
                rarityString = "LEGEND";
                this.image.GetComponent<Image>().color = ConfigValues.LEGEND_COLOR;
                break;
            case (4):
                rarityInt = ConfigValues.MYTHIC;
                rarityString = "MYTHIC";
                this.image.GetComponent<Image>().color = ConfigValues.MYTHIC_COLOR;
                break;
        }

        //this.image.GetComponent<Image>().color = config.COMMON_COLOR;
        this.text.text = rarityString;

    }
}
