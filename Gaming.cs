using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Gaming : MonoBehaviour
{
    public static Pet defaultPet = new Pet();

    public string[] defaultNames = {"Jamal","Retardus","Googus","Foog" };

    public WorldPetDisplay[] stages;
    
    void Start()
    {
        LoadDefaultPet();

        Inventory.loadDataHolder();
        UpdateStageDisplays();
    }

    public void UpdateStageDisplays() 
    {
        for (int i = 0; i < Inventory.petSquadCount; i++)
        {
            stages[i].ChangePet(Inventory.petSquad[i]);
        }
    }

    public void LoadDefaultPet() 
    {
        Material defMatI = ConfigValues.instance.getMaterialByName("Normal");
        Color[] defCol = new Color[] { new Color(1f, 1f, 1f), new Color(1f, 1f, 1f), new Color(1f, 1f, 1f) };
        Material[] defMat = new Material[] { defMatI, defMatI, defMatI };

        defaultPet.InitStats("BLANK", 0, 10, 1, 1, 1, 1, 1);
        defaultPet.InitVisuals(ConfigValues.instance.getGOByName("Blockodile"), defCol, defMat);
        defaultPet.InitRarity(0, 0, 0, 0, 0, 0);
    }
}
