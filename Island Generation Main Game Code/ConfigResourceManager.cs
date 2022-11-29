using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ConfigResourceManager : MonoBehaviour
{

    #region Singleton
    public static ConfigResourceManager instance;
    void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of ConfigResourceManager found");
            return;
        }
        instance = this;

        loadResources();
    }

    #endregion

    public static GameObject[] dropGOs;

    //Load All Resources

    void loadResources() 
    {
        dropGOs = Resources.LoadAll<GameObject>("IG/Drops");
    }
    
    void Start()
    {

    }

    public GameObject getModelByName(string name) 
    {
        dropGOs = Resources.LoadAll<GameObject>("IG/Drops");

        for (int i = 0; i < dropGOs.Length; i++) 
        {
            //Debug.Log(dropGOs[i].GetComponent<Drop>().name);
            if (dropGOs[i].GetComponent<Drop>().name.Equals(name)) 
            {
                return dropGOs[i];
                //return Instantiate(dropGOs[i]) as GameObject; 
            }
        }
        Debug.Log("Item: "+ name + " not found");
        return null;
    }
}
