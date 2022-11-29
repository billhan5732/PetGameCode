using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DropSlot : MonoBehaviour
{
    public static IGGameController gameController;
    int stackCount;
    string itemName;

    public Text stackCountDisplayText;

    public Transform modelParentHolder;

    public Button xButton;

    Vector3 zero = new Vector3(0f, 0f, 0f);
    Vector3 v3One = new Vector3(1f, 1f, 1f);

    void Awake() 
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<IGGameController>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetSlotDisplay(string itemName, int ammount, GameObject obj) 
    {
        xButton.gameObject.SetActive(true);
        stackCountDisplayText.gameObject.SetActive(true);

        modelParentHolder.gameObject.SetActive(true);

        this.itemName = itemName; this.stackCount = ammount;
        stackCountDisplayText.text = this.stackCount.ToString();

        SetSlotDisplayModel(obj);
    }

    public void SetSlotDisplayModel(GameObject obj) 
    {
        
        obj.transform.SetParent(modelParentHolder);
        obj.transform.localPosition = zero;
        obj.transform.localRotation = Quaternion.Euler(zero);
        obj.transform.localScale = v3One;
        //obj.layer = LayerMask.NameToLayer("UI");
        SetLayerRecursively(obj, LayerMask.NameToLayer("UI"));
        //Debug.Log("Correct");
    }

    public void ClearSlot() 
    {
        modelParentHolder.gameObject.SetActive(false);
        stackCountDisplayText.gameObject.SetActive(false);
        stackCountDisplayText.text = 0.ToString();
        xButton.gameObject.SetActive(false);
    }

    void SetLayerRecursively(GameObject obj, int newLayer  )
    {
        obj.layer = newLayer;

        foreach (Transform child in obj.transform)
        {
            SetLayerRecursively(child.gameObject, newLayer);
        }
    }

    public void DeleteSlotItem() 
    {

    }
}
