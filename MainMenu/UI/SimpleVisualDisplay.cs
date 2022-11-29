using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SimpleVisualDisplay : MonoBehaviour
{
    public GameObject iconModelPlaceHolderParent;
    public GameObject modelPlaceHolder;
    public Pet pet = null;
    
    Vector3 modelLocation, modelScale;
    Quaternion modelRotation;


    public bool hasIconModel;
    public GameObject newPetModel;
    public Text nameDisplayText;

    public bool willSpin;

    bool placeHoldersLoaded = false;

    void Awake()
    {

        modelRotation = modelPlaceHolder.transform.rotation;
        modelScale = modelPlaceHolder.transform.localScale;
    }

    void Start()
    {
        hasIconModel = false;
        modelLocation = modelPlaceHolder.transform.position;
        modelRotation = modelPlaceHolder.transform.rotation;
        modelScale = modelPlaceHolder.transform.localScale;
        //modelPlaceHolder.SetActive(false);
        //iconModelPlaceHolderParent.SetActive(false);


    }


    public void displayOn()
    {
        iconModelPlaceHolderParent.SetActive(true);
        iconModelPlaceHolderParent.layer = 5;
    }

    public void ChangePet(Pet newPet)
    {
        ClearSlot();
        //AddPet(newPet);
        this.pet = newPet;
        CreateNewPetModel(newPet);

        displayOn();
    }

    public void CreateNewPetModel(Pet pet)
    {
        if (pet == null) 
        {
            ClearSlot();
            return;
        }

        //Debug.Log("Slot: " + this.gameObject.name + ": Is creating a new Model Icon");
        modelLocation = modelPlaceHolder.transform.position;

        Destroy(newPetModel);
        newPetModel = Instantiate(this.pet.getPhysicalManisfestation(), modelLocation, modelRotation, iconModelPlaceHolderParent.transform) as GameObject;
        //newPetModel.transform.position = this.modelLocation;
        newPetModel.transform.localScale = modelScale;
        newPetModel.layer = 5;
        newPetModel.SetActive(true);
        iconModelPlaceHolderParent.SetActive(true);
        modelPlaceHolder.SetActive(false);
        iconModelPlaceHolderParent.layer = 5;
        foreach (Transform transform in iconModelPlaceHolderParent.GetComponentsInChildren<Transform>(true))
        {
            transform.gameObject.layer = 5;
        }
        hasIconModel = true;

    }

    public void ClearSlot()
    {
        pet = null;
        //iconModel = null;
        Destroy(newPetModel);
        hasIconModel = false;
        iconModelPlaceHolderParent.SetActive(false);
    }

    void Update() 
    {
        if (willSpin) 
        {
            newPetModel.transform.Rotate(0, 50 * Time.deltaTime, 0);
        }
    }
}
