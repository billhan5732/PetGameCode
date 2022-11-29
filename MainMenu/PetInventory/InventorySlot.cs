using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour
{
    public GameObject iconModel;
    public GameObject modelPlaceHolder;

    public GameObject slotSelectedIMG;
    public Text teamSlotText;

    public Color colorOne;
    public Color colorTwo;

    public Pet pet = null;
    Transform[] parts; //for assigning each part to the UI pets layer
    public Vector3 modelLocation;
    public static Quaternion modelRotation;
    public static Vector3 modelScale;

    public bool hasIconModel;
    public GameObject newPetModel;

    public Button selfButton;

    public InventoryPetPreview preview;

    public bool isSelectedPetSlot = false;

    

    void Awake() 
    {
        
        modelRotation = modelPlaceHolder.transform.rotation;
        modelScale = modelPlaceHolder.transform.localScale;
    }

    void Start() 
    {
        modelLocation = modelPlaceHolder.transform.position;
        hasIconModel = false;
        
        //modelPlaceHolder.SetActive(false);
        //iconModel.SetActive(false);

        selfButton.onClick.AddListener(OnClickSLot);

    }

    public void AddPet(Pet newPet) //when you want pet to be in icon
    {
        //ClearSlot();//BIG NOTE: Need to come up with something more lag friendly
        this.pet = newPet;
        //CreateNewPetModel(this.pet);
        
        if (hasIconModel)
        {
            displayOn();
        }
        else 
        {
            ChangePet(newPet);
            CreateNewPetModel(this.pet);
        }
        
    }

    public void displayOn() 
    {
        iconModel.SetActive(true);
        iconModel.layer = 5;
    }

    public void ChangePet(Pet newPet) 
    {
        isSelectedPetSlot = false;
        ClearSlot();
        //AddPet(newPet);
        this.pet = newPet;
        CreateNewPetModel(newPet);

        HandleSelectedSlotIcon(newPet);

        displayOn();
    }

    public void CreateNewPetModel(Pet pet) 
    {
        //Debug.Log("Slot: " + this.gameObject.name + ": Is creating a new Model Icon");
        modelLocation = modelPlaceHolder.transform.position;

        Destroy(newPetModel);
        newPetModel = Instantiate(this.pet.getPhysicalManisfestation(),modelLocation,modelRotation,iconModel.transform) as GameObject;
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

    void HandleSelectedSlotIcon(Pet pet) 
    {
        if (pet.selectedSlotNumber > 0)
        {
            slotSelectedIMG.SetActive(true);
            slotSelectedIMG.GetComponent<Image>().color = colorTwo;
            teamSlotText.gameObject.SetActive(true);
            teamSlotText.text = pet.selectedSlotNumber.ToString();
            isSelectedPetSlot = true;
        }
        else
        {
            slotSelectedIMG.SetActive(false);
            teamSlotText.gameObject.SetActive(false);
        }
    }

    public void ClearSlot() 
    {
        pet = null;
        //iconModel = null;
        Destroy(newPetModel);
        hasIconModel = false;
        iconModel.SetActive(false);
        slotSelectedIMG.SetActive(false);
    }

    public void OnClickSLot() 
    {
        //Debug.Log("Pressed Pet: "+hasIconModel);
        if (this.pet != null) 
        {
            preview.ClearSlot();
            preview.NewPet(this.pet,this);
            slotSelectedIMG.SetActive(true);
            slotSelectedIMG.GetComponent<Image>().color = colorOne;
        }
    }

    public void UnClickSlot() 
    {
        slotSelectedIMG.SetActive(false);
        unSelectPet();
    }

    public void unSelectPet()
    {
        isSelectedPetSlot = false;
        if(pet != null){ this.pet.selectedSlotNumber = 0; }
    }
}
