using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Pet// : ScriptableObject
{
    #region Atributes
    //public static int[] XP_LEVEL_AMTS = {};

    //Basic Stats
    public string name;
    public string petUID;
    public int Level;
    public bool gender;

    public int XP;

    //Traits
    public GameObject petBodyType;
    public GameObject bodyTypeManisfestation;

    public Color primaryColor;
    public Material primaryMaterial;
    public Color secondaryColor;
    public Material secondaryMaterial;
    public Color eyeColor;
    public Material eyeMaterial;

    // ??? public Transform BodyType;
    private Transform[] parts;
    public Transform primaryparts;
    public Transform secondaryparts;
    public Transform eyeparts;

    //Rarities
    private int primaryColorRarity;
    private int primaryMaterialRarity;
    private int secondaryColorRarity;
    private int secondaryMaterialRarity;
    private int eyeColorRarity;
    private int eyeMaterialRarity;

    //Stats
    private int currentHealthPoints;
    private int fullHealthPoints;
    private int damage;
    private int armor;
    private int speed;
    private int lowerBoostBound;
    private int upperBoostBound;

    public int fusions = 0;


    public int selectedSlotNumber = 0;

    public Item selectedItem = new Item();

    #endregion Stats

    public void Awake() 
    {
        /*
        if (petBodyType != null)
        {
            this.bodyTypeManisfestation = Instantiate(petBodyType, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
            //this.bodyTypeManisfestation.SetActive(false);
        }
        */

    }
    #region Initializing Functions

    public Pet() 
    {
        this.petUID = System.Guid.NewGuid().ToString();

    }

    public void InitVisuals(GameObject petBodyType, Color primaryColor, Material primaryMaterial, Color secondaryColor, Material secondaryMaterial, Color eyeColor, Material eyeMaterial) 
    {
        this.petBodyType = petBodyType;
        this.bodyTypeManisfestation = UnityEngine.Object.Instantiate(petBodyType, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        this.bodyTypeManisfestation.layer = 3;
        this.bodyTypeManisfestation.transform.SetParent(GameObject.FindWithTag("HiddenStuff").transform);

        //this.bodyTypeManisfestation.SetActive(false);
        this.primaryColor = primaryColor;
        this.primaryMaterial = primaryMaterial;
        this.secondaryColor = secondaryColor;
        this.secondaryMaterial = secondaryMaterial;
        this.eyeColor = eyeColor;
        this.eyeMaterial = eyeMaterial;
        updatePhysicalManisfestationVisuals();
    }

    public void InitVisuals(GameObject petBodyType, Color[] colors, Material[] materials) 
    {
        this.petBodyType = petBodyType;
        this.bodyTypeManisfestation = UnityEngine.Object.Instantiate(petBodyType, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;
        this.bodyTypeManisfestation.layer = 3;
        this.bodyTypeManisfestation.transform.SetParent(GameObject.FindWithTag("HiddenStuff").transform);

        this.primaryColor = colors[0];
        this.primaryMaterial = materials[0];
        this.secondaryColor = colors[1];
        this.secondaryMaterial = materials[1];
        this.eyeColor = colors[2];
        this.eyeMaterial = materials[2];
        updatePhysicalManisfestationVisuals();
    }

    public void InitStats(string newName, int level, int fullHealthPoints, int damage, int armor, int speed, int lowerBoost, int upperBoost) 
    {
        this.name = newName;
        this.Level = level;
        this.fullHealthPoints = fullHealthPoints;
        this.currentHealthPoints = fullHealthPoints;
        this.damage = damage;
        this.armor = armor;
        this.speed = speed;
        this.lowerBoostBound = lowerBoost;
        this.upperBoostBound = upperBoost;
    }

    public void InitStats(string newName, int level,int[] stats) 
    {
        this.name = newName;
        this.Level = level;

        this.fullHealthPoints = stats[0];
        this.damage = stats[1];
        this.armor = stats[2];
        this.speed = stats[3];
        this.lowerBoostBound = stats[4];
        this.upperBoostBound = stats[5];
    }

    public void InitRarity(int pcRarity, int scRarity, int ecRarity, int pmRarity, int smRarity, int emRarity) 
    {
        this.primaryColorRarity = pcRarity;
        this.secondaryColorRarity = scRarity;
        this.eyeColorRarity = ecRarity;
        this.primaryMaterialRarity = pmRarity;
        this.secondaryMaterialRarity = smRarity;
        this.eyeMaterialRarity = emRarity;
    }

    public void InitRarity(int[] raritySet) 
    {
        this.primaryColorRarity = raritySet[0];
        this.secondaryColorRarity = raritySet[1];
        this.eyeColorRarity = raritySet[2];
        this.primaryMaterialRarity = raritySet[3];
        this.secondaryMaterialRarity = raritySet[4];
        this.eyeMaterialRarity = raritySet[5];
    }
    #endregion

    #region Visuals
    void separateParts() 
    {
        //bodyTypeManisfestation = UnityEngine.Object.Instantiate(petBodyType, new Vector3(0, 0, 0), Quaternion.identity) as GameObject;

        this.primaryparts = this.bodyTypeManisfestation.transform.Find("PrimaryParts");
        this.secondaryparts = this.bodyTypeManisfestation.transform.Find("SecondaryParts");
        this.eyeparts = this.bodyTypeManisfestation.transform.Find("EyeParts");



    }

    public void updatePhysicalManisfestationVisuals() {
        separateParts();

        foreach (Transform child in primaryparts)
        {
            child.GetComponent<MeshRenderer>().material = primaryMaterial;
            child.GetComponent<MeshRenderer>().material.color = primaryColor;
            child.gameObject.layer = 3;
        }
        foreach (Transform child2 in secondaryparts)
        {
            child2.GetComponent<MeshRenderer>().material = secondaryMaterial;
            child2.GetComponent<MeshRenderer>().material.color = secondaryColor;
            child2.gameObject.layer = 3;
        }
        foreach (Transform child in eyeparts)
        {
            child.GetComponent<MeshRenderer>().material = eyeMaterial;
            child.gameObject.layer = 3;
            child.GetComponent<MeshRenderer>().material.color = eyeColor;
        }
        this.bodyTypeManisfestation.transform.SetParent(GameObject.FindWithTag("HiddenStuff").transform);
        
    }
    #endregion

    #region Get Functions
    public GameObject getPhysicalManisfestation() 
    {
        updatePhysicalManisfestationVisuals();
        //Vector3 newPosition = new Vector3(x, y, z);
        //bodyTypeManisfestation.transform.position = newPosition;
        bodyTypeManisfestation.layer = 3;
        return bodyTypeManisfestation;
    }

    public int[] getStats() {
        int[] array = {this.fullHealthPoints, this.damage,this.armor,this.speed,this.lowerBoostBound,this.upperBoostBound};
        return array;
    
    }
    
    public int[] getRarities()
    {
        int[] returnArray = { this.primaryColorRarity, this.secondaryColorRarity, this.eyeColorRarity, this.primaryMaterialRarity, this.secondaryMaterialRarity, this.eyeMaterialRarity };
        return returnArray;
    }

    public Color[] getColors() {
        Color[] array = {this.primaryColor,this.secondaryColor,this.eyeColor };
        return array;
    
    }

    public Material[] getMats()
    {
        Material[] array = { this.primaryMaterial, this.secondaryMaterial, this.eyeMaterial };
        return array;
    }

    #endregion

    #region Misc
    public void deletePet() 
    {
        UnityEngine.Object.Destroy(this.bodyTypeManisfestation);
    }

    public void levelUp() {
        int[] stats = getStats();
        
        if (this.Level > ConfigValues.MAX_LEVEL) {
            return;
        }
        this.Level++;
        this.XP = 0;

        //int statToBoost = stats[UnityEngine.Random.Range(0,getStats().Length)];
        int boostIndex = UnityEngine.Random.Range(0, getStats().Length - 2);
        int boostAmount = UnityEngine.Random.Range(lowerBoostBound,upperBoostBound+1);
        stats[boostIndex] += (int)(boostAmount * ConfigValues.BOOST_SCALER[boostIndex]);
        //getStats()[UnityEngine.Random.Range(0, getStats().Length)] += boostAmount;
        InitStats(this.name,this.Level,stats[0], stats[1], stats[2], stats[3], stats[4], stats[5]);
    
    }

    public void GiveXP(int xp) 
    {
        XP += xp;
        int XpUntilNext = XpUntilNextLevel(this);
        if (XP >= XpUntilNext) 
        {
            int overflow = XP - XpUntilNext;

            levelUp();

            XP = overflow;
        }
    }

    public static int XpUntilNextLevel(Pet pet) 
    {
        int currentLevel = pet.Level;

        int xp = (int)(Math.Pow(((currentLevel + 1)), 3.9f));

        return xp;
    }

    #endregion

    #region Saving

    public static Pet UnpackPetDataForm(PetDataForm data) 
    {
        Pet pet = new Pet();
        ConfigValues config = ConfigValues.instance;
        

        //Colors

        Color pc = new Color(data.primaryColor[0], data.primaryColor[1], data.primaryColor[2]);
        Color sc = new Color(data.secondaryColor[0], data.secondaryColor[1], data.secondaryColor[2]);
        Color ec = new Color(data.eyeColor[0], data.eyeColor[1], data.eyeColor[2]);
        //Materials
        Material pm = config.getMaterialByName(data.primaryMaterial);
        Material sm = config.getMaterialByName(data.secondaryMaterial);
        Material em = config.getMaterialByName(data.eyeMaterial);

        GameObject pBT = config.getGOByName(data.petBodyType);

        //string newName, int level, int fullHealthPoints, int damage, int armor, int speed, int lowerBoost, int upperBoost
        pet.InitVisuals(pBT, pc, pm, sc, sm, ec, em);
        pet.InitStats(data.name, data.Level, data.statsArr[0], data.statsArr[1], data.statsArr[2], data.statsArr[3], data.statsArr[4], data.statsArr[5]);
        pet.InitRarity(data.rarities);
        pet.gender = data.gender;

        pet.selectedSlotNumber = data.selectedSlotNum;
        pet.XP = data.totalXP;

        pet.petUID = data.petUID;
        if (data.selectedItem != null) { pet.selectedItem = data.selectedItem; }

        //pet.updatePhysicalManisfestationVisuals();
        return pet;
    }

    public static PetDataForm ConvertToPetDataForm(Pet pet)
    {

        ConfigValues config = ConfigValues.instance;

        float[,] colors = new float[3, 3]
            {
            {pet.primaryColor.r,pet.primaryColor.g,pet.primaryColor.b },
            {pet.secondaryColor.r,pet.secondaryColor.g,pet.secondaryColor.b },
            {pet.eyeColor.r,pet.eyeColor.g,pet.eyeColor.b }
        };

        string[] materialNames = { pet.primaryMaterial.name,pet.secondaryMaterial.name,pet.eyeMaterial.name };

        int[] stats = pet.getStats();

        int[] rarities = pet.getRarities();
        PetDataForm data = new PetDataForm(pet.name, pet.gender, pet.Level, pet.fusions, pet.petBodyType.name, stats, rarities, colors, materialNames);//string name, bool gender, int Level, int Fusions, string body,int[] stats, int[] rarities, float[,] colors, string[] materialNames
                                                                                                                                                       //PetDataForm.loadFromArrays(data, stats, rarities, colors, materialNames);

        data.selectedSlotNum = pet.selectedSlotNumber;
        data.totalXP = pet.XP;
        data.petUID = pet.petUID;

        if(pet.selectedItem != null){ data.selectedItem = pet.selectedItem; }

        return data;

    }
    #endregion
}


[System.Serializable]
public class PetDataForm
{
    public string name = "";
    public string petUID = "";
    public bool gender = false;
    public int Level = 0;
    public int totalXP;

    //Traits
    public string petBodyType = ""; // Indexed Game Object

    public float[] primaryColor = new float[3]; // Color to float[]
    public string primaryMaterial = ""; // Indexed Material
    public float[] secondaryColor = new float[3]; // Color to float[]
    public string secondaryMaterial = ""; // Indexed Material
    public float[] eyeColor = new float[3]; // Color to float[]
    public string eyeMaterial = ""; // Indexed Material

    //Rarities
    public int primaryColorRarity = 0;
    public int primaryMaterialRarity = 0;
    public int secondaryColorRarity = 0;
    public int secondaryMaterialRarity = 0;
    public int eyeColorRarity = 0;
    public int eyeMaterialRarity = 0;

    //Stats
    public int fullHealthPoints = 0;
    public int damage = 0;
    public int armor = 0;
    public int speed = 0;
    public int lowerBoostBound = 0;
    public int upperBoostBound = 0;

    public int[] rarities = new int[6];
    public int[] statsArr = new int[6];

    public int fusions = 0;
    public int selectedSlotNum;

    public Item selectedItem;

    public PetDataForm() { }

    public PetDataForm(string name, bool gender, int Level, int Fusions, string body,int[] stats, int[] rarities, float[,] colors, string[] materialNames) 
    {
        this.name = name;
        this.gender = gender;
        this.Level = Level;
        this.fusions = Fusions;
        this.petBodyType = body;

        this.rarities = rarities;
        this.statsArr = stats;

        this.primaryColor[0] = colors[0, 0]; this.primaryColor[1] = colors[0, 1]; this.primaryColor[2] = colors[0, 2];
        this.secondaryColor[0] = colors[1, 0]; this.secondaryColor[1] = colors[1, 1]; this.secondaryColor[2] = colors[1, 2];
        this.eyeColor[0] = colors[2, 0]; this.eyeColor[1] = colors[2, 1]; this.eyeColor[2] = colors[2, 2];

        this.primaryMaterial = materialNames[0];
        this.secondaryMaterial = materialNames[1];
        this.eyeMaterial = materialNames[2];

    }
    public static void loadFromArrays(PetDataForm petData, int[] stats, int[] rarities, float[,] colors, string[] materialNames) 
    {
        //{this.fullHealthPoints, this.damage,this.armor,this.speed,this.lowerBoostBound,this.upperBoostBound};
        //{ this.primaryColorRarity, this.secondaryColorRarity, this.eyeColorRarity, this.primaryMaterialRarity, this.secondaryMaterialRarity, this.eyeMaterialRarity }
        petData.fullHealthPoints = stats[0];
        petData.damage = stats[1];
        petData.armor = stats[2];
        petData.speed = stats[3];
        petData.lowerBoostBound = stats[4];
        petData.upperBoostBound = stats[5];

        petData.primaryColorRarity = rarities[0]; petData.secondaryColorRarity = rarities[1]; petData.eyeColorRarity = rarities[2];
        petData.primaryMaterialRarity = rarities[3]; petData.secondaryMaterialRarity = rarities[4]; petData.eyeColorRarity = rarities[5];
        petData.rarities = rarities;

        petData.primaryColor[0] = colors[0,0]; petData.primaryColor[1] = colors[0, 1]; petData.primaryColor[2] = colors[0, 2];
        petData.secondaryColor[0] = colors[1,0]; petData.secondaryColor[1] = colors[1, 1]; petData.secondaryColor[2] = colors[1, 2];
        petData.eyeColor[0] = colors[2,0]; petData.eyeColor[1] = colors[2, 1]; petData.eyeColor[2] = colors[2, 2];

        petData.primaryMaterial = materialNames[0];
        petData.secondaryMaterial = materialNames[1];
        petData.eyeMaterial = materialNames[2];
    }

}
