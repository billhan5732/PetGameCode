using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections;
using System.Collections.Generic;

public class InventoryUI : MonoBehaviour
{
    public Button petInventoryButton;
    public Button closeInventoryButton;

    public InventoryPetPreview inventoryPetPreview;

    public GameObject uiButtons;
    public Text petInventoryButtonText;
    public GameObject petInventory;

    public Button left1Button, right1Button, left10Button, right10Button;
    public Text pageNumberText;


    public Transform slotsParent;
    public int inventoryPageNumber = 0;

    InventorySlot[] slots;

    private bool petInventoryIsOpen = false;

    Inventory inventory;

    public int teamSlot = 1;
    public GameObject[] teamSlotObjects;
    WorldPetDisplay[] teamDisplays;

    static Gaming gameController;

    // Start is called before the first frame update
    public void Begin()
    {

        Debug.Log("Begining");

        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<Gaming>();

        inventory = Inventory.instance;
        slots = slotsParent.GetComponentsInChildren<InventorySlot>();
        //UpdateUI();
        petInventoryButton.onClick.AddListener(OpenPetInventory);
        closeInventoryButton.onClick.AddListener(ClosePetInventory);

        left1Button.onClick.AddListener(delegate { ChangePageBy(-1); });
        left10Button.onClick.AddListener(delegate { ChangePageBy(-10); });

        right1Button.onClick.AddListener(delegate { ChangePageBy(1); });
        right10Button.onClick.AddListener(delegate { ChangePageBy(10); });

        Inventory.onPetChangedCallback += UpdateUI;

        inventoryPetPreview.Start();
        teamDisplays = new WorldPetDisplay[teamSlotObjects.Length];
        //Debug.Log("TeamDisplays: " + teamSlotObjects.Length.ToString());
        for (int i = 0; i < teamSlotObjects.Length; i++) 
        {
            teamDisplays[i] = teamSlotObjects[i].GetComponent<WorldPetDisplay>();
        }
    }

    // Update is called once per frame
    void Update()
    {
        //UpdateUI();
    }

    public void UpdateUI()
    {
        Time.timeScale = 0f;

        for (int i = 0; i < slots.Length; i++) 
        {

            int petListIndex = i + inventoryPageNumber * 20;
            bool itWorksLol = true;
            try { string name = (Inventory.pets[petListIndex].name); }
            catch (Exception e)
            {
                slots[i].ClearSlot();
                itWorksLol = false;
            }

            if (i < Inventory.pets.Count && itWorksLol)
            {
                slots[i].ChangePet(Inventory.pets[petListIndex]);
            }
            else 
            {
                slots[i].ClearSlot();
            }
        }
        UpdateTeamDisplay();

        Time.timeScale = 1f;
    }

    public void OpenPetInventory()
    {
        //UpdateUI();
        ChangePageBy(0);
        petInventory.SetActive(true);
        UpdateUI();
        
        uiButtons.SetActive(false);
        //closeInventoryButton.onClick.AddListener(ClosePetInventory);
    }
    public void ClosePetInventory() 
    {
        petInventory.SetActive(false);
        uiButtons.SetActive(true);
        Inventory.saveDataHolder();
        //petInventoryButton.onClick.AddListener(OpenPetInventory);
    }

    public void ChangePageBy(int x) 
    {
        if (inventoryPageNumber + x <= 0)
        {
            x = -1 * inventoryPageNumber;
            //return;
        }
        else if (inventoryPageNumber + x > Inventory.space) 
        {
            x = Inventory.space - inventoryPageNumber;
        }

        inventoryPageNumber += x;
        pageNumberText.text = inventoryPageNumber.ToString();
        UpdateUI();

    }

    public void SetTeamSlotSelector(int teamSlot) 
    {
        teamSlotObjects[this.teamSlot - 1].transform.Find("Selected BKG").gameObject.SetActive(false);
        this.teamSlot = teamSlot;
        teamSlotObjects[teamSlot - 1].transform.Find("Selected BKG").gameObject.SetActive(true);

        //Update Stage Displays
        gameController.UpdateStageDisplays();
        //UpdateTeamDisplay();
        UpdateUI();
    }

    void UpdateTeamDisplay() 
    {
        for (int i = 0; i < Inventory.petSquadCount; i++) 
        {
            teamDisplays[i].ChangePet(Inventory.petSquad[i]);
        }
    }

}
