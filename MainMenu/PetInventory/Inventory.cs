using System.Collections;
using System.Collections.Generic;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System;

using UnityEngine;

public class Inventory : MonoBehaviour
{
    public delegate void OnPetChanged();
    public static OnPetChanged onPetChangedCallback;
    public static DataHolder dataHolder = new DataHolder();
    #region Singleton
    public static Inventory instance;
    //public static List<Pet> petsListOG = new List<Pet>();
    void Awake()
    {
        //dataHolder.pets = new List<Pet>();//temp
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of Inventory found");
            return;
        }
        instance = this;
    }

    #endregion

    #region Pet Inventory
    public static List<Pet> pets = new List<Pet>();
    public static Pet selectedPet, selectedPet2, selectedPet3;
    public static int petSquadCount = 0;
    public static Pet[] petSquad = new Pet[3];


    public static int space = 100;
    public static void Add(Pet pet)
    {
        if (pets.Count >= space)
        {
            Debug.Log("Not enough room");
            return;
        }
        pets.Add(pet);
        //DontDestroyOnLoad(pet);

        if (onPetChangedCallback != null)
        {
            onPetChangedCallback.Invoke();
        }

        //saveDataHolder();
    }
    public static void Remove(Pet pet)
    {
        pets.Remove(pet);
        pet.deletePet();
        if (onPetChangedCallback != null)
        {
            onPetChangedCallback.Invoke();
        }
        //saveDataHolder();
    }

    public void settingNewPet(Pet pet, int teamSlot)
    {
        switch (teamSlot) 
        {
            case (1):
                if (selectedPet != null) { selectedPet.selectedSlotNumber = 0; }
                selectedPet = pet;
                break;
            case (2):
                if (selectedPet2 != null) { selectedPet2.selectedSlotNumber = 0; }
                selectedPet2 = pet;
                break;
            case (3):
                if (selectedPet3 != null) { selectedPet3.selectedSlotNumber = 0; }
                selectedPet3 = pet;
                
                break;
        }
        pet.selectedSlotNumber = teamSlot;
        UpdatePetSquadList();
        /*
        selectedPet.selectedSlotNumber = 0;
        selectedPet = pet;
        pet.selectedSlotNumber = teamSlot;
        */
        //DontDestroyOnLoad(selectedPet);
    }

    #endregion

    #region Misc
    void Start()
    {
        //loadDataHolder();
    }

    public int getCreditAmmount()
    {
        return dataHolder.credits;
    }
    public void modifyCreditAmmount(int change)
    {
        dataHolder.credits += change;
    }
    #endregion

    #region Saving XML
    public static void saveDataHolder()
    {
        dataHolder.petDataList.Clear();
        for (int i = 0; i < pets.Count; i++)
        {
            //Debug.Log(pets[i].name);
            dataHolder.petDataList.Add(Pet.ConvertToPetDataForm(pets[i]));
            //PetDataForm data = pets[i].ThisPetConvertToPetDataForm();
            //dataHolder.petDataList.Add(data);
        }

        if (selectedPet != null)
        {
            dataHolder.selectedPetDataForm = Pet.ConvertToPetDataForm(selectedPet);
        }
        if (selectedPet2 != null){dataHolder.selectedPetDataForm2 = Pet.ConvertToPetDataForm(selectedPet2);}
        if (selectedPet3 != null) { dataHolder.selectedPetDataForm3 = Pet.ConvertToPetDataForm(selectedPet3); }

        //dataHolder.selectedPetDataForm = pTools.ConvertToPetDataForm(selectedPet);
        var serializer = new XmlSerializer(typeof(DataHolder));
        var fileStream = new FileStream(Application.dataPath + "/XmlStuff/save_data.xml", FileMode.Create);
        serializer.Serialize(fileStream, dataHolder);
        fileStream.Close();
    }
    static void UpdatePetSquadList()
    {
        if (selectedPet != null) {petSquad[0] = selectedPet;}
        if (selectedPet2 != null) { petSquad[1] = selectedPet2; }
        if (selectedPet3 != null) { petSquad[2] = selectedPet3; }

    }
    public static void loadDataHolder()
    {
        //Debug.Log("Loading Inventory");
        pets.Clear();
        var serializer = new XmlSerializer(typeof(DataHolder));
        var fileStream = new FileStream(Application.dataPath + "/XmlStuff/save_data.xml", FileMode.Open);
        dataHolder = serializer.Deserialize(fileStream) as DataHolder;
        fileStream.Close();

        for (int i = 0; i < dataHolder.petDataList.Count; i++)
        {
            Pet unloadingPet = Pet.UnpackPetDataForm(dataHolder.petDataList[i]);
            pets.Add(unloadingPet);


            //Add(unloadingPet);
        }
        petSquadCount = 0;
        if (dataHolder.selectedPetDataForm != null)
        {
            //Debug.Log(dataHolder.selectedPetDataForm.name);
            Pet selectedPetFromFile = Pet.UnpackPetDataForm(dataHolder.selectedPetDataForm);
            selectedPet = selectedPetFromFile;
            petSquadCount++;
        }
        
        if (dataHolder.selectedPetDataForm2 != null) { selectedPet2 = Pet.UnpackPetDataForm(dataHolder.selectedPetDataForm2); petSquadCount++; }
        if (dataHolder.selectedPetDataForm3 != null) { selectedPet3 = Pet.UnpackPetDataForm(dataHolder.selectedPetDataForm3); petSquadCount++; }
        UpdatePetSquadList();
        Debug.Log(dataHolder.petDataList.Count);

    }

    #endregion

}

[System.Serializable]
public class DataHolder
{
    public DataHolder() { }
    public List<PetDataForm> petDataList = new List<PetDataForm>();
    public PetDataForm selectedPetDataForm, selectedPetDataForm2, selectedPetDataForm3;
    public PetDataForm[] petSquad;
    //public List<Pet> pets = new List<Pet>();
    //public Pet selectedPet;
    public int credits = 69420;
}
