using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SelectPetPreview: MonoBehaviour
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
    SelectPet parentPage;

    public GameObject selectPetPage;

    public Text nameText;
    public Text hpText;
    public Text dmgText;
    public Text defText;
    public Text spdText;

    public Button selectButton;

    Inventory inventory;


    void Start()
    {
        inventory = Inventory.instance;
        hasIconModel = false;
        modelLocation = modelPlaceHolder.transform.position;
        modelRotation = modelPlaceHolder.transform.rotation;
        modelScale = modelPlaceHolder.transform.localScale;
        iconModel.SetActive(false);
        parentPage = selectPetPage.GetComponent<SelectPet>();

        selectButton.onClick.AddListener(SetPreviewPetAsSelected);


    }

    public void NewPet(Pet newPet)
    {
        ClearSlot();
        this.petInPreview = newPet;
        loadStats();
        if (hasIconModel)
        {
            iconModel.SetActive(true);
            modelPlaceHolder.SetActive(false);
            iconModel.layer = 5;
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

    public void TestLVLUP()
    {
        this.petInPreview.levelUp();
        loadStats();
    }

    public void SetPreviewPetAsSelected()
    {
        parentPage.selectedPet = petInPreview;
        parentPage.CloseAndReturn();
        //Gaming.tempPet = petInPreview;
    }

    void Spin()
    {

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
