using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoreInfoPage : MonoBehaviour
{
    public GameObject iconModel;
    public GameObject modelPlaceHolder;
    public Pet petInPreview;
    Transform[] parts; //for assigning each part to the UI pets layer
    Vector3 modelLocation;
    Quaternion modelRotation;
    Vector3 modelScale;

    bool hasIconModel;
    GameObject newPetModel;
    public SimpleVisualDisplay display;

    public Button closeButton;
    public Button deletePetButton;
    public GameObject uiButtons;
    public GameObject inventoryUI;

    public Image selectedItemImage;

    public Slider xpBar;

    //Stats
    public Text nameText;
    public Text hpText;
    public Text dmgText;
    public Text defText;
    public Text spdText;
    public Text levelText;
    public Text bodyTypeText;
    public Text fusionsText;
    public Text genderText;
    public Text minLevelBoostText;
    public Text maxLevelBoostText;
    public Text overallRarityText;
    //Traits
    public Text pcText;
    public Text scText;
    public Text ecText;
    public Text pmText;
    public Text smText;
    public Text emText;
    //Previews
    public GameObject pcPreview;
    public GameObject scPreview;
    public GameObject ecPreview;
    public GameObject pmPreview;
    public GameObject smPreview;
    public GameObject emPreview;
    //Rarities
    public GameObject pcRarity;
    public GameObject scRarity;
    public GameObject ecRarity;
    public GameObject pmRarity;
    public GameObject smRarity;
    public GameObject emRarity;

    Inventory inventory;
    

    public void Begin()
    {
        //Debug.Log("Here");
        inventory = Inventory.instance;
        hasIconModel = false;
        modelLocation = modelPlaceHolder.transform.position;
        modelRotation = modelPlaceHolder.transform.rotation;
        modelScale = modelPlaceHolder.transform.localScale;
        
        //iconModel.SetActive(false);

        closeButton.onClick.AddListener(Close);
        deletePetButton.onClick.AddListener(deletePet);
    }

    public void NewPet(Pet newPet)
    {
        

        ClearSlot();
        this.petInPreview = newPet;
        loadStats();

        display.ChangePet(newPet);

        /*
        ClearSlot();
        this.petInPreview = newPet;
        loadStats();
        if (hasIconModel)
        {
            iconModel.SetActive(true);
            modelPlaceHolder.SetActive(false);
            iconModel.layer = 5;
            foreach (Transform transform in iconModel.GetComponentsInChildren<Transform>(true))
            {
                transform.gameObject.layer = 5;
            }
        }
        else
        {
            newPetModel = Instantiate(this.petInPreview.getPhysicalManisfestation());
            newPetModel.transform.parent = iconModel.transform;
            newPetModel.transform.position = modelLocation;
            newPetModel.transform.rotation = modelRotation;
            newPetModel.transform.localScale = modelScale;
            newPetModel.layer = 5;
            iconModel.SetActive(true);
            modelPlaceHolder.SetActive(false);
            iconModel.layer = 5;
            foreach (Transform transform in iconModel.GetComponentsInChildren<Transform>(true))
            {
                transform.gameObject.layer = 5;
            }
            hasIconModel = true;
        }
        */


    }

    public void loadStats()
    {
        int[] statsArray = petInPreview.getStats();
        Color[] colorArray = petInPreview.getColors();
        Material[] materialArray = petInPreview.getMats();

        //Stats
        nameText.text = petInPreview.name + " Lvl: " + this.petInPreview.Level;
        hpText.text = "Max Health: " + statsArray[0].ToString();
        dmgText.text = "Damage: " + statsArray[1].ToString();
        defText.text = "Defense: " + statsArray[2].ToString();
        spdText.text = "Speed: " + statsArray[3].ToString();
        levelText.text = "Level: "+petInPreview.Level;
        bodyTypeText.text = "Body Type: " + petInPreview.petBodyType.name;
        fusionsText.text = "Fusions: " + petInPreview.fusions;
        
        minLevelBoostText.text = "Min LVL Boost: " + statsArray[4].ToString();
        maxLevelBoostText.text = "Max LVL Boost: " + statsArray[5].ToString();
        overallRarityText.text = "Overall Rarity";

        if (petInPreview.gender)
        {
            genderText.text = "Gender: Male";
        }
        else 
        {
            genderText.text = "Gender: Female";
        }


    //Colors
        pcPreview.transform.GetComponent<MeshRenderer>().material.color = colorArray[0];
        scPreview.transform.GetComponent<MeshRenderer>().material.color = colorArray[1];
        ecPreview.transform.GetComponent<MeshRenderer>().material.color = colorArray[2];

        pmPreview.transform.GetComponent<MeshRenderer>().material = materialArray[0];
        smPreview.transform.GetComponent<MeshRenderer>().material = materialArray[1];
        emPreview.transform.GetComponent<MeshRenderer>().material = materialArray[2];

        pcText.text = "Primary Color: [" + ((int)((colorArray[0].r * 255))).ToString() + ","
            + ((int)((colorArray[0].g * 255))).ToString() + ","
            + ((int)((colorArray[0].b * 255))).ToString()
            + "]";
        scText.text = "Secondary Color: [" + ((int)((colorArray[1].r * 255))).ToString() + ","
            + ((int)((colorArray[1].g * 255))).ToString() + ","
            + ((int)((colorArray[1].b * 255))).ToString()
            + "]";
        ecText.text = "Eye Color: [" + ((int)((colorArray[2].r * 255))).ToString() + ","
            + ((int)((colorArray[2].g * 255))).ToString() + ","
            + ((int)((colorArray[2].b * 255))).ToString()
            + "]";

        //Rarities
        int[] rarities = petInPreview.getRarities();
        pcRarity.GetComponent < RarityDisplay > ().setRarity(rarities[0]);
        scRarity.GetComponent<RarityDisplay>().setRarity(rarities[1]);
        ecRarity.GetComponent<RarityDisplay>().setRarity(rarities[2]);
        pmRarity.GetComponent<RarityDisplay>().setRarity(rarities[3]);
        smRarity.GetComponent<RarityDisplay>().setRarity(rarities[4]);
        emRarity.GetComponent<RarityDisplay>().setRarity(rarities[5]);

        //Material Names
        pmText.text = "Primary Material: " + petInPreview.primaryMaterial.name;
        smText.text = "Secondary Material: " + petInPreview.secondaryMaterial.name;
        emText.text = "Eye Material: " + petInPreview.eyeMaterial.name;

        ItemsInventoryUI.instance.LoadItemSprites();
        selectedItemImage.sprite = ItemsInventoryUI.getItemImage(petInPreview.selectedItem.name);

        xpBar.maxValue = Pet.XpUntilNextLevel(petInPreview);
        xpBar.value = petInPreview.XP;

    }

    public void ClearSlot()
    {
        this.petInPreview = null;
        Destroy(newPetModel);
        hasIconModel = false;
        iconModel.SetActive(false);
    }

    public void deletePet() 
    {
        Inventory.Remove(petInPreview);
        inventoryUI.GetComponent<InventoryUI>().inventoryPetPreview.ClearSlot();
        //inventoryUI.GetComponent<InventoryUI>().inventoryPetPreview.CreateNewPetModel(Gaming.defaultPet);
        Close();
    }

    void Update()
    {
        Spin();
    }

    public void Open()
    {
        this.gameObject.SetActive(true);
        uiButtons.SetActive(false);
        
    }
    public void Close()
    {
        //uiButtons.SetActive(true);
        this.gameObject.SetActive(false);
        inventoryUI.GetComponent<InventoryUI>().OpenPetInventory();
        //petInventoryButton.onClick.AddListener(OpenPetInventory);
    }

    public void Spin() 
    {
        if (pcPreview.activeInHierarchy)
        {
            pcPreview.transform.Rotate(0, 50 * Time.deltaTime, 50 * Time.deltaTime);
            scPreview.transform.Rotate(0, 50 * Time.deltaTime, 50 * Time.deltaTime);
            ecPreview.transform.Rotate(0, 50 * Time.deltaTime, 50 * Time.deltaTime);

            pmPreview.transform.Rotate(0, 50 * Time.deltaTime, 50 * Time.deltaTime);
            smPreview.transform.Rotate(0, 50 * Time.deltaTime, 50 * Time.deltaTime);
            emPreview.transform.Rotate(0, 50 * Time.deltaTime, 50 * Time.deltaTime);

            if (hasIconModel)
            {

                newPetModel.transform.Rotate(0, 50 * Time.deltaTime, 0);
            }
        }
    }

    public void SetItem() 
    {
        SelectItemPrompt.GetItemFromInventory();
        //Time.timeScale = 0f;
        Transform traits = transform.Find("TraitsBKG");
        traits.gameObject.SetActive(false);
        transform.Find("PreviewDisplayBKG").gameObject.SetActive(false);
        
        StartCoroutine(balls());
        


    }

    IEnumerator balls() 
    {
        
        while (!SelectItemPrompt.instance.isGettable()) 
        {
            yield return null;
        }

        petInPreview.selectedItem = SelectItemPrompt.instance.itemInPreview.getIndividual();
        selectedItemImage.sprite = ItemsInventoryUI.getItemImage(petInPreview.selectedItem.name);

        //gameObject.SetActive(true);
        transform.Find("TraitsBKG").gameObject.SetActive(true);
        transform.Find("PreviewDisplayBKG").gameObject.SetActive(true);
        yield break;
    }
}
