using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainUIController : MonoBehaviour
{
    public ConfigValues configValues;

    //Controllers
    public UISetController uiSetController;
    
    //Main Page References
    public InventoryUI inventoryUI;
    public MoreInfoPage moreInfoPetPage;
    public MIUI materialsUI;
    public ShopUI shopUI;
    public BreedingPage breedingPage;
    public ItemsInventoryUI itemsInventoryUI;

    //Prompt References
    public PromptYN prompt;
    public SelectPet selectPetPrompt;
    public SelectItemPrompt selectItemPrompt;

    void Start()
    {
        inventoryUI.Begin();
        moreInfoPetPage.Begin();
        shopUI.Begin();
        itemsInventoryUI.LoadItemSprites();
        selectItemPrompt.Awake();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
