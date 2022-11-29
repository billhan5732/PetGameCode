using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class InSessionUI : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject player;

    public Slider energySlider, healthSlider;

    public GameObject backPackUI;

    bool backPackIsOpen = false;

    public Transform slotsParent;
    DropSlot[] slots;

    public Text timeRemaining;

    public TaskUI taskUI;
    public CreditDisplayer creditDisplayer;

    void Start()
    {
        //button.onClick.AddListener(doThing);
        slots = slotsParent.GetComponentsInChildren<DropSlot>();
        healthSlider.maxValue = player.GetComponent<Player>().charStats.fullHP;
        energySlider.maxValue = player.GetComponent<Player>().activePetFullEnergy;




        UpdatePlayer();
        StartCoroutine(UpdateTimer());
        
    }

    // Update is called once per frame
    void Update()
    {
        SetEnergy(player.GetComponent<Player>().activePetCurrentEnergy, player.GetComponent<Player>().activePetFullEnergy);
        UpdateHP();
        KeyCheckUpdate();
    }

    IEnumerator UpdateTimer()
    {
        while (IGGameController.hasTimeLeft) 
        {
            timeRemaining.text = ((int)(IGGameController.timeRemaining)).ToString() + " s";
            yield return new WaitForSeconds(1f);
        }
        yield break;
    }

    void UpdatePlayer() 
    {
        //player.GetComponent<Player>().SetPet();
        player.GetComponent<Player>().activePetCurrentEnergy -= 50;
        SetEnergy(player.GetComponent<Player>().activePetCurrentEnergy, player.GetComponent<Player>().activePetFullEnergy);
    }

    public TaskUI GetTaskUI() 
    {
        return taskUI;
    }

    void KeyCheckUpdate() 
    {
        if (Input.GetKeyDown("b")) { if (backPackIsOpen) { CloseBackpack(); } else { OpenBackpack(); } BackPack.SaveDataHolder(); } 
    }

    public void SetEnergy(float energy, float maxEnergy) 
    {
        energySlider.value = energy;
        energySlider.maxValue = maxEnergy;
    }

    public void UpdateHP() 
    {
        //
        healthSlider.value = player.GetComponent<Player>().charStats.hp;
    }

    #region BackPack
    void OpenBackpack() { backPackIsOpen = true; backPackUI.SetActive(true); UpdateBackPackDisplay(); }

    void CloseBackpack() { backPackIsOpen = false; backPackUI.SetActive(false); }

    void UpdateBackPackDisplay() 
    {
        BackPack.LoadDataHolder();

        int[] stackCounts = BackPack.getCounts();
        string[] stackNames = BackPack.getNames();
        GameObject[] objects = BackPack.itemGOs;
        //GameObject[] objects = new GameObject[stackNames.Length];
        /*
        for (int i = 0; i < stackNames.Length; i++) 
        {
            if (!stackNames[i].Equals(""))
            {
                GameObject newObject = Instantiate(ConfigResourceManager.instance.getModelByName(stackNames[i])) as GameObject;
                newObject.GetComponent<Drop>().setAsStatic();
                objects[i] = newObject;
            }
            
        }

        for (int i = 0; i < stackCounts.Length; i++) 
        {
            try
            {
                Debug.Log(i.ToString() + " - " + stackNames[i] + " - " + stackCounts[i].ToString() + " Game Object Name: " + objects[i].name);
            }
            catch (Exception e) { Debug.Log("Broken"); }
        }
        */

        Debug.Log(slots.Length);
        
        for (int i = 0; i < slots.Length; i++) 
        {
            //Debug.Log(stackNames[i] + i.ToString());
            if (i < BackPack.slots)
            {
                if (stackNames[i].Equals("") || stackCounts[i] <= 0)
                {
                    slots[i].ClearSlot();
                }
                else
                {
                    //objects[i].SetActive(true);
                    objects[i].transform.SetParent(slots[i].modelParentHolder);
                    slots[i].SetSlotDisplay(stackNames[i], stackCounts[i], objects[i]);
                }
                
            }
            else
            {
                slots[i].ClearSlot();

            }
        }
        


    }

    #endregion
}
