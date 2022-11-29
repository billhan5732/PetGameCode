using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Prompt : MonoBehaviour
{

    static bool isOpen = false;

    public static Text promptText;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        keyCheck();
    }

    public void keyCheck() 
    {
        if (!isOpen) { return; }

        if (Input.GetKeyUp("e")) { Debug.Log("User Confirmed"); closePrompt(); }

        if (Input.GetKeyUp("q")) { Debug.Log("User Denied"); closePrompt(); }
    }

    public void newPrompt(string newPromptText) 
    {
        gameObject.SetActive(true);
        isOpen = true;
        promptText.text = newPromptText; 
        
    }

    public void closePrompt() { this.gameObject.SetActive(false); isOpen = false; }
}
