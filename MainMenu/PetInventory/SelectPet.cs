using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectPet : MonoBehaviour
{
    public GameObject previewHolder;
    public GameObject UIButtons;
    public GameObject BreedingPage;
    public GameObject ScrollViewContentParent;
    public GameObject slot;

    private SelectPetPreview preview;
    public Pet selectedPet;

    

    void Start()
    {
        preview = previewHolder.GetComponent<SelectPetPreview>();
    }

    public void OpenThisMenu(bool gender) 
    {
        //Resetting
        
        foreach (Transform child in ScrollViewContentParent.transform)
        {
            GameObject.Destroy(child.gameObject);
        }
        
        //Debug.Log(Inventory.pets.Count);

        for (int i = 0; i < Inventory.pets.Count; i++) 
        {
            if (Inventory.pets[i].gender == gender && Inventory.pets[i].fusions < 3) 
            {
                //Debug.Log(Inventory.pets.Count);
                GameObject doofus = Instantiate(slot,ScrollViewContentParent.transform, true) as GameObject;
                //doofus.transform.SetParent(ScrollViewContentParent.transform);
                doofus.GetComponent<InventorySlot>().ClearSlot();

                Vector3 newLocation = new Vector3 (doofus.transform.position.x, doofus.transform.position.y, -20f);

                doofus.GetComponent<InventorySlot>().modelLocation = newLocation;
                
                //doofus.GetComponent<InventorySlot>().AddPet(Inventory.pets[i]);
                doofus.GetComponent<InventorySlot>().ChangePet(Inventory.pets[i]);
                //doofus.GetComponent<InventorySlot>().AddPet(Inventory.pets[i]);
                doofus.GetComponent<InventorySlot>().selfButton.onClick.RemoveAllListeners();
                doofus.GetComponent<InventorySlot>().selfButton.onClick.AddListener(delegate { SlotButtonOnClick(doofus); });
                
            }
        }

        //UIButtons.SetActive(false);
        BreedingPage.SetActive(false);

    }

    public void CloseAndReturn() 
    {
        if (selectedPet == null) 
        {
            Debug.Log("Error: Selected Pet Is Null or User Has No Pet of Specified Gender");
            return;
        }
        this.gameObject.SetActive(false);
        BreedingPage.SetActive(true);
        BreedingPage.GetComponent<BreedingPage>().SelectPetResponse(selectedPet);


    }

    public void SlotButtonOnClick(GameObject slot) 
    {
        preview.NewPet(slot.GetComponent<InventorySlot>().pet);
        //slot.GetComponent<InventorySlot>().selfButton.onClick.AddListener()
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
