using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigValues : MonoBehaviour
{

    #region Singleton
    public static ConfigValues instance;
    //public static List<Pet> petsListOG = new List<Pet>();
    void Awake()
    {
        //Debug.Log("Initializing Instance Singleton");

        //dataHolder.pets = new List<Pet>();//temp
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of ConfigValues found");
            return;
        }
        instance = this;

        ALL_MATERIALS = Resources.LoadAll<Material>("Materials");
        ALL_PET_BODIES = Resources.LoadAll<GameObject>("Prefabs");
        isLoaded = true;

    }

    #endregion
    //public readonly float chance = 0.f;
    //public readonly int Cost = ;
    //public readonly float[] chanceSet = {,,,,}; common : 0,rare : 1,ultra rare : 2, legend : 3, mythic : 4 | total should be = to 1

    static bool isLoaded = false;

    public static readonly int COMMON = 0;
    public static readonly int RARE = 1;
    public static readonly int ULTRA_RARE = 2;
    public static readonly int LEGEND = 3;
    public static readonly int MYTHIC = 4;

    public static readonly Color COMMON_COLOR = new Color(150/255f, 150 / 255f, 150 / 255f);
    public static readonly Color RARE_COLOR = new Color(100 / 255f, 200 / 255f, 255 / 255f);
    public static readonly Color ULTRA_RARE_COLOR = new Color(255 / 255f, 248 / 255f, 145 / 255f);
    public static readonly Color LEGEND_COLOR = new Color(192 / 255f, 105 / 255f, 255 / 255f);
    public static readonly Color MYTHIC_COLOR = new Color(255 / 255f, 75 / 255f, 200 / 255f);


    public static readonly float[] commonEggChanceSet = {0.8f,0.15f,0.05f,0f,0f };
    public static readonly int commonEggCost = 20;

    public static readonly float[] rareEggChanceSet = { 0.3f, 0.6f, 0.1f, 0f, 0f };
    public static readonly int rareEggCost = 100;

    public static readonly float[] ultraRareEggChanceSet = { 0.1f, 0.2f, 0.69f, 0.01f, 0f };
    public static readonly int ultraRareEggCost = 500;

    public static readonly float[] legendEggChanceSet = { 0.095f, 0.1f, 0.4f, 0.4f, .005f };
    public static readonly int legendEggCost = 1000;

    public static readonly int MAX_LEVEL = 35 -1;

    //{fullHealthPoints, damage,armor,speed,lowerBoostBound,upperBoostBound}
    public static readonly float[] BOOST_SCALER = {5f,1f,1f,1f,1f,1f };

    public static Material[] ALL_MATERIALS;
    public static GameObject[] ALL_PET_BODIES;
    //Other Things that need to be initialized
    #region Functions
    public int bodyTypeToIndex(GameObject m)
    {
        if (!isLoaded) { Awake(); }

        for (int i = 0; i < ALL_PET_BODIES.Length; i++)
        {
            if (m.name == (ALL_PET_BODIES[i].name))
            {
                return i;
            }
        }

        return -1;
    }

    public int materialToIndex(Material m) 
    {
        if (!isLoaded) { Awake(); }

        for (int i = 0; i < ALL_MATERIALS.Length; i++) 
        {
            if (m.name == ALL_MATERIALS[i].name)
            {
                return i;
            }
        }

        return -1;
    }

    public Material getMaterialByName(string name) 
    {

        if (!isLoaded) { Awake(); }

        for (int i = 0; i < ALL_MATERIALS.Length; i++) 
        {
            if (name.Equals(ALL_MATERIALS[i].name)) 
            {
                return ALL_MATERIALS[i];
            }
        }

        return null;
    
    }

    public GameObject getGOByName(string name)
    {

        if (!isLoaded) { Awake(); }

        for (int i = 0; i < ALL_PET_BODIES.Length; i++)
        {
            if (name.Equals(ALL_PET_BODIES[i].name))
            {
                return ALL_PET_BODIES[i];
            }
        }

        return null;

    }
    
    public bool randBool()
    {
        int num = Random.Range(0, 2);

        return num == 1;
    }

    public static int rollRNG_V2(float[] weights) 
    {

        float current = 0f;
        float random_roll = UnityEngine.Random.value;

        for (int i = 0; i < weights.Length; i++)
        {

            current += weights[i];
            if (random_roll < current)
            {
                return i;
            }


        }

        return UnityEngine.Random.Range(0,weights.Length);

    }

    public int[] generateStatsArray(int[] rarities) 
    {
        int[] stats = new int[6];

        float avgRarity = 0f;
        for (int i = 0; i < rarities.Length; i++){ avgRarity += (rarities[i] + 1); } avgRarity /= rarities.Length;

        //Base Stats

        for (int i = 0; i < stats.Length;i++) {
            stats[i] = (int)((rarities[i] + 1) * BOOST_SCALER[i]);
        }

        //Random Boost Stat Bonuses
        bool keepGoing = true;
        
        int count = 0;
        float odds = (float)(avgRarity*10/( Mathf.Pow((float)(count),2f) + (avgRarity*10) ));
        ///*
        while (keepGoing) 
        {
            float rng = UnityEngine.Random.value;
            if (rng < odds)
            {
                count++;
                odds = (avgRarity * 10 / (Mathf.Pow((float)(count), 2f) + (avgRarity * 10)));
                int statToBoost = getRandomStat();
                stats[statToBoost] += (int)(BOOST_SCALER[statToBoost]);
            }
            else { keepGoing = false; }
        }

        //BoostCheck

        if (stats[4] > stats[5]) 
        {
            int temp = stats[4];
            stats[4] = stats[5];
            stats[5] = temp;
        }

        //*/

        return stats;
    }

    public int getRandomStat() 
    {
        int statNumber = UnityEngine.Random.Range(0,4);
        return statNumber; 
    }

    #endregion

}
