using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisplayPedestal : MonoBehaviour
{
    // Start is called before the first frame update

    public Pet selectedPet;
    public GameObject modelPetDisplayGO;

    public GameObject modelPlaceHolder;

    public GameObject primaryColorSampleDisplay;
    public GameObject secondaryColorSampleDisplay;
    public GameObject eyeColorSampleDisplay;
    public GameObject primaryMaterialSampleDisplay;
    public GameObject secondaryMaterialSampleDisplay;
    public GameObject eyeMaterialSampleDisplay;

    Inventory inventory;

    bool hasInstantiatedModel;
    Vector3 modelLocation;
    Quaternion modelRotation;
    Vector3 modelScale;

    GameObject displayModel;

    void Start()
    {
        //hasIconModel = false;
        modelLocation = modelPlaceHolder.transform.position;
        modelRotation = modelPlaceHolder.transform.rotation;
        modelScale = modelPlaceHolder.transform.localScale;

        if (selectedPet != null)
        {
            GameObject displayModel = Instantiate(selectedPet.getPhysicalManisfestation()) as GameObject;
            modelPetDisplayGO = displayModel;
            hasInstantiatedModel = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void selectNewPet(Pet newPet) 
    {
        selectedPet = newPet;
        newPet.updatePhysicalManisfestationVisuals();
        GameObject displayModel = Instantiate(selectedPet.getPhysicalManisfestation()) as GameObject;
        modelPetDisplayGO = displayModel;
        hasInstantiatedModel = true;
    }

    public void UpdateDisplay(Vector3 location, Vector3 scale, Quaternion modelRotation) 
    {
        modelPetDisplayGO = displayModel;
        modelPetDisplayGO.transform.position = location;
        modelPetDisplayGO.transform.localScale = scale;
        modelPetDisplayGO.transform.rotation = modelRotation;

    }

    public void activeOnScreen() 
    {
        modelPetDisplayGO.SetActive(true);
    }
}
