using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class BreedingPage : MonoBehaviour
{

    public GameObject UIButtons;
    public Button closeButton;
    public Button openThisPageButton;
    public Button selectMalePetButton;
    public Button selectFemalePetButton;

    public GameObject selectPetPrompt;
    public Button breedButton;
    public Text maleFusionsText;
    public GameObject maleVisualDisplay;
    public GameObject femaleVisualDisplay;

    private static readonly bool MALE = true;
    private static readonly bool FEMALE = false;

    public Pet malePet;
    public Pet femalePet;

    string[] names;

    public PromptYN prompt;

    ConfigValues configValues;
    GameObject gamecontrol;
    //public GameObject malePCD;
    #region Displays
    //Male Traits
    public Text malePCText, maleSCText, maleECText, malePMText, maleSMText, maleEMText;
    
    public GameObject malePCPrevew, maleSCPreview, maleECPreview, malePMPreview, maleSMPreview, maleEMPreview;
    
    public GameObject malePCRarity, maleSCRarity, maleECRarity, malePMRarity, maleSMRarity, maleEMRarity;

    
    //Female Traits
    public Text femalePCText, femaleSCText, femaleECText, femalePMText, femaleSMText, femaleEMText;

    public GameObject femalePCPrevew, femaleSCPreview, femaleECPreview, femalePMPreview, femaleSMPreview, femaleEMPreview;

    public GameObject femalePCRarity, femaleSCRarity, femaleECRarity, femalePMRarity, femaleSMRarity, femaleEMRarity;
    
    #endregion
    public void UpdateMaleTraitsDisplay(Pet pet) 
    {
        if (pet == null) 
        {
            return;
        }

        maleVisualDisplay.GetComponent<SimpleVisualDisplay>().ChangePet(pet);

        Color[] colorArray = pet.getColors();
        Material[] materialArray = pet.getMats();
        int[] rarities = pet.getRarities();

        //Colors
        malePCPrevew.transform.GetComponent<MeshRenderer>().material.color = colorArray[0];
        maleSCPreview.transform.GetComponent<MeshRenderer>().material.color = colorArray[1];
        maleECPreview.transform.GetComponent<MeshRenderer>().material.color = colorArray[2];

        malePMPreview.transform.GetComponent<MeshRenderer>().material = materialArray[0];
        maleSMPreview.transform.GetComponent<MeshRenderer>().material = materialArray[1];
        maleEMPreview.transform.GetComponent<MeshRenderer>().material = materialArray[2];

        malePCText.text = "Primary Color: [" + ((int)((colorArray[0].r * 255))).ToString() + ","
            + ((int)((colorArray[0].g * 255))).ToString() + ","
            + ((int)((colorArray[0].b * 255))).ToString()
            + "]";
        maleSCText.text = "Secondary Color: [" + ((int)((colorArray[1].r * 255))).ToString() + ","
            + ((int)((colorArray[1].g * 255))).ToString() + ","
            + ((int)((colorArray[1].b * 255))).ToString()
            + "]";
        maleECText.text = "Eye Color: [" + ((int)((colorArray[2].r * 255))).ToString() + ","
            + ((int)((colorArray[2].g * 255))).ToString() + ","
            + ((int)((colorArray[2].b * 255))).ToString()
            + "]";

        //Rarities
        malePCRarity.GetComponent<RarityDisplay>().setRarity(rarities[0]);
        maleSCRarity.GetComponent<RarityDisplay>().setRarity(rarities[1]);
        maleECRarity.GetComponent<RarityDisplay>().setRarity(rarities[2]);
        malePMRarity.GetComponent<RarityDisplay>().setRarity(rarities[3]);
        maleSMRarity.GetComponent<RarityDisplay>().setRarity(rarities[4]);
        maleEMRarity.GetComponent<RarityDisplay>().setRarity(rarities[5]);

        //Material Names
        malePMText.text = "Primary Material: " + pet.primaryMaterial.name;
        maleSMText.text = "Secondary Material: " + pet.secondaryMaterial.name;
        maleEMText.text = "Eye Material: " + pet.eyeMaterial.name;
    }

    public void UpdateFemaleTraitsDisplay(Pet pet)
    {
        if (pet == null)
        {
            return;
        }
        femaleVisualDisplay.GetComponent<SimpleVisualDisplay>().ChangePet(pet);

        Color[] colorArray = pet.getColors();
        Material[] materialArray = pet.getMats();
        int[] rarities = pet.getRarities();

        //Colors
        femalePCPrevew.transform.GetComponent<MeshRenderer>().material.color = colorArray[0];
        femaleSCPreview.transform.GetComponent<MeshRenderer>().material.color = colorArray[1];
        femaleECPreview.transform.GetComponent<MeshRenderer>().material.color = colorArray[2];

        femalePMPreview.transform.GetComponent<MeshRenderer>().material = materialArray[0];
        femaleSMPreview.transform.GetComponent<MeshRenderer>().material = materialArray[1];
        femaleEMPreview.transform.GetComponent<MeshRenderer>().material = materialArray[2];

        femalePCText.text = "Primary Color: [" + ((int)((colorArray[0].r * 255))).ToString() + ","
            + ((int)((colorArray[0].g * 255))).ToString() + ","
            + ((int)((colorArray[0].b * 255))).ToString()
            + "]";
        femaleSCText.text = "Secondary Color: [" + ((int)((colorArray[1].r * 255))).ToString() + ","
            + ((int)((colorArray[1].g * 255))).ToString() + ","
            + ((int)((colorArray[1].b * 255))).ToString()
            + "]";
        femaleECText.text = "Eye Color: [" + ((int)((colorArray[2].r * 255))).ToString() + ","
            + ((int)((colorArray[2].g * 255))).ToString() + ","
            + ((int)((colorArray[2].b * 255))).ToString()
            + "]";

        //Rarities
        femalePCRarity.GetComponent<RarityDisplay>().setRarity(rarities[0]);
        femaleSCRarity.GetComponent<RarityDisplay>().setRarity(rarities[1]);
        femaleECRarity.GetComponent<RarityDisplay>().setRarity(rarities[2]);
        femalePMRarity.GetComponent<RarityDisplay>().setRarity(rarities[3]);
        femaleSMRarity.GetComponent<RarityDisplay>().setRarity(rarities[4]);
        femaleEMRarity.GetComponent<RarityDisplay>().setRarity(rarities[5]);

        //Material Names
        femalePMText.text = "Primary Material: " + pet.primaryMaterial.name;
        femaleSMText.text = "Secondary Material: " + pet.secondaryMaterial.name;
        femaleEMText.text = "Eye Material: " + pet.eyeMaterial.name;
    }

    void Start()
    {
        gamecontrol = GameObject.FindGameObjectWithTag("GameController");
        configValues  = gamecontrol.GetComponent<ConfigValues>();
        names = gamecontrol.GetComponent<Gaming>().defaultNames;

        selectMalePetButton.onClick.AddListener(delegate { SelectMaleOnClick(MALE); });
        selectFemalePetButton.onClick.AddListener(delegate { SelectMaleOnClick(FEMALE); });
        closeButton.onClick.AddListener(CloseThisPage);

        breedButton.onClick.AddListener(BreedButtonOnClick);
        //openThisPageButton.onClick.AddListener(OpenThisPage)
    }

    public void SelectMaleOnClick(bool gender) 
    {
        UIButtons.SetActive(false);
        selectPetPrompt.SetActive(true);
        selectPetPrompt.GetComponent<SelectPet>().OpenThisMenu(gender);
        this.gameObject.SetActive(false);
    }

    public void SelectPetResponse(Pet pet) 
    {
        if (pet.gender == MALE)
        {
            malePet = pet;
            UpdateMaleTraitsDisplay(pet);
        }
        else if (pet.gender == FEMALE) 
        {
            femalePet = pet;
            UpdateFemaleTraitsDisplay(pet);
        }

        //UIButtons.SetActive(false);

    }

    public void ClearSelectedPets() 
    {
        UpdateMaleTraitsDisplay(Gaming.defaultPet);
        UpdateFemaleTraitsDisplay(Gaming.defaultPet);


        malePet = null;
        femalePet = null;

        maleVisualDisplay.GetComponent<SimpleVisualDisplay>().ChangePet(null);
        femaleVisualDisplay.GetComponent<SimpleVisualDisplay>().ChangePet(null);
        //Destroy(maleVisualDisplay);
        //Destroy(femaleVisualDisplay);


    }

    public void BreedButtonOnClick() 
    {

        prompt.getUserInput("Are You Sure You want to breed these two pets?", BreedNOW);

    }

    public void BreedNOW() 
    {
        if (malePet == null || femalePet == null)
        {
            Debug.Log("Not Eligeable Pairing");
            return;
        }

        float[] odds = { 0.5f, 0.5f, 0.5f, 0.5f, 0.5f, 0.5f };

        BreedSelectedPets(odds);

        Debug.Log("New Pet Was Succesfully Bred");
    }

    void CloseThisPage() 
    {
        this.gameObject.SetActive(false);
        UIButtons.SetActive(true);
        //ClearSelectedPets();
    }

    public void OpenThisPage() 
    {
        UIButtons.SetActive(false);
        this.gameObject.SetActive(true);
    }

    void Update()
    {
        
    }

    void BreedSelectedPets(float[] maleTraitModifiers) 
    {
        int[] maleParentRarities = malePet.getRarities();
        int[] femaleParentRarities = femalePet.getRarities();
        Color[] maleParentColors = malePet.getColors();
        Color[] femaleParentColors = femalePet.getColors();
        Material[] maleParentMats = malePet.getMats();
        Material[] femaleParentMats = femalePet.getMats();

        int[] randomGen = new int[6];
        int[] rarities = new int[6];
        //1 = Male, 0 = Female

        Color[] childColors = new Color[3];
        Material[] childMats = new Material[3];
        //Color childPC, childSC, childEC;
        //Material childPM, childSM, childEM;
        GameObject childBodyType;

        for (int i = 0; i < 6; i++) 
        {
            float rngValue = UnityEngine.Random.Range(0f,1f);
            //Debug.Log(rngValue);
            if (rngValue < maleTraitModifiers[i])
            {
                randomGen[i] = 1;
                rarities[i] = maleParentRarities[i];
            }
            else 
            {
                randomGen[i] = 0;
                rarities[i] = femaleParentRarities[i];
            }

            //Debug.Log(randomGen[i]);
        }

        

        for (int i = 0; i < 3; i++) 
        {
            if (randomGen[i] == 1)
            {
                childColors[i] = maleParentColors[i];
            }
            else 
            {
                childColors[i] = femaleParentColors[i];
            }
        }
        for (int i = 3; i < 6; i++)
        {
            if (randomGen[i] == 1)
            {
                childMats[i-3] = maleParentMats[i-3];
            }
            else
            {
                childMats[i-3] = femaleParentMats[i-3];
            }
        }
        //BodyType
        if (UnityEngine.Random.Range(0, 1) < 0.5){childBodyType = malePet.petBodyType;}
        else { childBodyType = femalePet.petBodyType; }

        Pet newPet = new Pet();
        int[] newStats = configValues.generateStatsArray(rarities);
        newPet.InitStats(names[UnityEngine.Random.Range(0, names.Length)], 0,newStats);
        newPet.InitRarity(rarities);
        newPet.InitVisuals(childBodyType,childColors,childMats);
        newPet.gender = configValues.randBool();
        
        malePet.fusions++;
        femalePet.fusions++;

        Inventory.Add(newPet);
        ClearSelectedPets();
    }
}
