using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryPetPreview : MonoBehaviour
{
    public GameObject iconModel;
    public GameObject modelPlaceHolder;
    public GameObject inventoryParent;
    public Pet petInPreview;
    Transform[] parts; //for assigning each part to the UI pets layer
    Vector3 modelLocation;
    Quaternion modelRotation;
    Vector3 modelScale;

    bool hasIconModel;
    GameObject newPetModel;

    public Text nameText;
    public Text hpText;
    public Text dmgText;
    public Text defText;
    public Text spdText;

    public GameObject moreInfoPage;
    public Button levelUpTestButton;
    public Button openMore;
    public Button selectButton;

    Inventory inventory;

    InventorySlot currentSlot;

    public InventoryUI inventoryUI;

    public void Start()
    {
        inventory = Inventory.instance;
        hasIconModel = false;
        modelLocation = modelPlaceHolder.transform.position;
        modelRotation = modelPlaceHolder.transform.rotation;
        modelScale = modelPlaceHolder.transform.localScale;
        iconModel.SetActive(false);

        levelUpTestButton.onClick.AddListener(TestLVLUP);
        openMore.onClick.AddListener(OpenMoreInfo);
        selectButton.onClick.AddListener(SetPreviewPetAsSelected);


    }

    public void CreateNewPetModel(Pet pet)
    {
        //Debug.Log("Slot: " + this.gameObject.name + ": Is creating a new Model Icon");
        modelLocation = modelPlaceHolder.transform.position;

        Destroy(newPetModel);
        newPetModel = Instantiate(this.petInPreview.getPhysicalManisfestation(), modelLocation, modelRotation, iconModel.transform) as GameObject;
        //newPetModel.transform.position = this.modelLocation;
        newPetModel.transform.localScale = modelScale;
        newPetModel.layer = 5;
        newPetModel.SetActive(true);
        iconModel.SetActive(true);
        modelPlaceHolder.SetActive(false);
        iconModel.layer = 5;
        foreach (Transform transform in iconModel.GetComponentsInChildren<Transform>(true))
        {
            transform.gameObject.layer = 5;
        }
        hasIconModel = true;

    }

    public void NewPet(Pet newPet, InventorySlot newPetSlot)
    {
        //ClearSlot();
        if ((currentSlot != null) && !currentSlot.isSelectedPetSlot) 
        {
            currentSlot.UnClickSlot();
        }
        
        currentSlot = newPetSlot;
        //this.petInPreview.selectedSlotNumber = 0;
        this.petInPreview = newPet;
        loadStats();

        CreateNewPetModel(newPet);

    }

    public void loadStats()
    {
        int[] statsArray = petInPreview.getStats();
        nameText.text = petInPreview.name + " Lvl: " + this.petInPreview.Level;
        hpText.text = "Max Health: " + statsArray[0].ToString();
        dmgText.text = "Damage: " + statsArray[1].ToString();
        defText.text = "Defense: " + statsArray[2].ToString();
        spdText.text = "Speed: " + statsArray[3].ToString();
        //Debug.Log(petInPreview.displayName +" | "+ petInPreview.name);
    }

    public void ClearSlot()
    {
        this.petInPreview = null;
        Destroy(newPetModel);
        hasIconModel = false;
        iconModel.SetActive(false);
    }

    public void TestLVLUP() {
        this.petInPreview.levelUp();
        loadStats();
    }

    public void OpenMoreInfo() {

        inventoryParent.GetComponent<InventoryUI>().ClosePetInventory();
        moreInfoPage.GetComponent<MoreInfoPage>().Open();
        moreInfoPage.GetComponent<MoreInfoPage>().NewPet(petInPreview);
    }

    public void SetPreviewPetAsSelected() 
    {
        //Debug.Log(petInPreview.name);
        inventory.settingNewPet(petInPreview, inventoryUI.teamSlot);
        
        inventoryUI.UpdateUI();
    }

    void Spin() {

        if (hasIconModel && newPetModel.activeInHierarchy)
        {

            newPetModel.transform.Rotate(0, 50 * Time.deltaTime, 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Spin();
    }
}
