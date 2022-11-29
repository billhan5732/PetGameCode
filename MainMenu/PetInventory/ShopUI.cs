using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ShopUI : MonoBehaviour
{
    public Button openShopButton;
    public GameObject openShopButtonGameObject;
    public Button closeShopButton;
    public GameObject shopGameObject;
    public GameObject uiButtons;

    Inventory inventory;

    private GameObject gamecontrol;
    private ConfigValues config;
    public GameObject[] petPrefabs;
    public Material[] commonMaterials;
    string[] names;

    public void Begin()
    {
        gamecontrol = GameObject.FindGameObjectWithTag("GameController");
        config = ConfigValues.instance;
        commonMaterials = Resources.LoadAll<Material>("Materials"); ;
        petPrefabs = Resources.LoadAll<GameObject>("Prefabs");
        names = gamecontrol.GetComponent<Gaming>().defaultNames;
        //Debug.Log(commonMaterials.Length);
        //Debug.Log(petPrefabs.Length);

        openShopButton.onClick.AddListener(OpenShop);
        closeShopButton.onClick.AddListener(CloseShop);
        inventory = Inventory.instance;

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OpenShop() {
        shopGameObject.SetActive(true);
        uiButtons.SetActive(false);
        closeShopButton.onClick.AddListener(CloseShop);
    }
    void CloseShop() {
        shopGameObject.SetActive(false);
        uiButtons.SetActive(true);
        openShopButtonGameObject.SetActive(true);
    }

    public int[] rarityRolls(float[] chanceSet) 
    {
        //public readonly float[] chanceSet = {,,,,}; common : 0,rare : 1,ultra rare : 2, legend : 3, mythic : 4 | total should be = to 1
        int[] returnArrray = new int[6];
        //float total = 0f;

        float totalR = chanceSet[1] + chanceSet[0];
        float totalUR = totalR + chanceSet[2];
        float totalL = totalUR + chanceSet[3];
        float totalM = totalL + chanceSet[4];

        for (int i = 0; i < 6; i++) 
        {
            float randomNumber = Random.Range(0f, 1f);
            if (randomNumber <= chanceSet[0])
            {
                returnArrray[i] = 0;
            }
            else if (randomNumber > chanceSet[0] && randomNumber <= totalR)
            {
                returnArrray[i] = 1;
            }
            else if (randomNumber > totalR && randomNumber <= totalUR)
            {
                returnArrray[i] = 2;
            }
            else if (randomNumber > totalUR && randomNumber <= totalL)
            {
                returnArrray[i] = 3;
            }
            else if (randomNumber > totalL && randomNumber <= 1)
            {
                returnArrray[i] = 4;
            }
            else 
            {
                returnArrray[i] = 1;
            }
        }

        return returnArrray;

    }

    public bool randBool() 
    {
        int num = Random.Range(0,2);

        return num == 1;
    }

    public Pet createBasePet() 
    {
        GameObject newBodyType = petPrefabs[(Random.Range(0, (petPrefabs.Length)))];

        //GameObject newPetPhysicalManisfestation = Instantiate(newBodyType, new Vector3(0, 0, 0), Quaternion.identity);
        Color newPrimaryColor = new Color(Random.Range(0, 255) / 255f, Random.Range(0, 255) / 255f, Random.Range(0, 255) / 255f);
        Material newPrimaryMaterial = commonMaterials[Random.Range(0, (commonMaterials.Length))];
        Color newSecondaryColor = new Color(Random.Range(0, 255) / 255f, Random.Range(0, 255) / 255f, Random.Range(0, 255) / 255f);
        Material newSecondaryMaterial = commonMaterials[Random.Range(0, (commonMaterials.Length))];
        Color newEyeColor = new Color(Random.Range(0, 255) / 255f, Random.Range(0, 255) / 255f, Random.Range(0, 255) / 255f);
        Material newEyeMaterial = commonMaterials[Random.Range(0, (commonMaterials.Length))];

        Pet newPet = new Pet();// = ScriptableObject.CreateInstance("Pet") as Pet;
        newPet.InitVisuals(newBodyType, newPrimaryColor, newPrimaryMaterial, newSecondaryColor, newSecondaryMaterial, newEyeColor, newEyeMaterial);

        return newPet;
    }

    public void openCommonEgg()
    {
        int[] rarities = rarityRolls(ConfigValues.commonEggChanceSet);
        int[] stats = config.generateStatsArray(rarities);

        Pet newPet = createBasePet();
        
        newPet.InitRarity(rarities);
        newPet.InitStats(names[Random.Range(0, names.Length)], 0, stats[0], stats[1], stats[2], stats[3], stats[4], stats[5]);
        newPet.gender = randBool();
        Inventory.Add(newPet);
    }

    public void openRareEgg()
    {
        int[] rarities = rarityRolls(ConfigValues.rareEggChanceSet);
        int[] stats = config.generateStatsArray(rarities);

        Pet newPet = createBasePet();

        newPet.InitRarity(rarities);
        newPet.InitStats(names[Random.Range(0, names.Length)], 0, stats[0], stats[1], stats[2], stats[3], stats[4], stats[5]);
        newPet.gender = randBool();
        Inventory.Add(newPet);
    }

    public void openUltraRareEgg()
    {
        int[] rarities = rarityRolls(ConfigValues.ultraRareEggChanceSet);
        int[] stats = config.generateStatsArray(rarities);

        Pet newPet = createBasePet();

        newPet.InitRarity(rarities);
        newPet.InitStats(names[Random.Range(0, names.Length)], 0, stats[0], stats[1], stats[2], stats[3], stats[4], stats[5]);
        newPet.gender = randBool();
        Inventory.Add(newPet);
    }

    public void openLegendaryEgg()
    {
        int[] rarities = rarityRolls(ConfigValues.legendEggChanceSet);
        int[] stats = config.generateStatsArray(rarities);

        Pet newPet = createBasePet();

        newPet.InitRarity(rarities);
        newPet.InitStats(names[Random.Range(0, names.Length)], 0, stats[0], stats[1], stats[2], stats[3], stats[4], stats[5]);
        newPet.gender = randBool();
        Inventory.Add(newPet);
    }
}
