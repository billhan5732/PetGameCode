using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PromptYN : MonoBehaviour
{
    public Button yesButton;
    public Button noButton;
    public Text promptText;

    public KeyCode yesKey;
    public KeyCode noKey;

    bool hasUserInputted = false;
    bool response;

    public GameObject selfObject;

    public delegate void TestDelegate(); // This defines what type of method you're going to call.
    public TestDelegate m_methodToCall;

    // Start is called before the first frame update
    void Start()
    {
        yesButton.onClick.AddListener(yesButtonOnClick);
        noButton.onClick.AddListener(noButtonOnClick);
    }

    public void yesButtonOnClick() 
    {
        response = true; hasUserInputted = true; //gameObject.SetActive(false);
    }
    public void noButtonOnClick() 
    {
        response = false; hasUserInputted = true;  //gameObject.SetActive(false);
    }

    public void getUserInput(string prompt,TestDelegate methodCallIfTrue) 
    {
        //invoke()

        promptText.text = prompt;

        m_methodToCall = methodCallIfTrue;

        hasUserInputted = false;

        selfObject.SetActive(true);
        //Time.timeScale = 0;
        StartCoroutine(DoThing());
        //Time.timeScale = 1;

        

        //selfObject.SetActive(false);
        //return response;
    }

    IEnumerator waitForUserInput()
    {

        hasUserInputted = false;

        
        while (!hasUserInputted) // essentially a "while true", but with a bool to break out naturally
        {
            //Debug.Log("Waiting");
            yield return null; // wait until next frame, then continue execution from here (loop continues)
        }

        // now this function returns
        Debug.Log("Done");
        //gameObject.SetActive(false);
    }


    private IEnumerator DoThing()
    {
        // do stuff here, show win screen, etc.

        // just a simple time delay as an example
        hasUserInputted = false;
        //yield return new WaitForSeconds(2.5f);

        bool done = false;
        while (!hasUserInputted) 
        {
            if (Input.GetKeyDown(yesKey))
            {
                response = true;
                hasUserInputted = true;//Resetting
            }
            else if (Input.GetKeyDown(noKey))
            {
                response = false;
                hasUserInputted = true;
            }
            yield return null;
        }
        //Debug.Log("Response: " + response.ToString());

        if (response)
        {
            m_methodToCall();
            selfObject.SetActive(false);
            yield break;
        }
        else 
        {
            Debug.Log("User Declined");
            selfObject.SetActive(false);
            yield break;
        }
        
        
        
        // do other stuff after key press
    }

    private IEnumerator waitForKeyPress(KeyCode yesKey, KeyCode noKey)
    {
        bool done = false;
        //bool response = false;
        while (!done) // essentially a "while true", but with a bool to break out naturally
        {
            if (Input.GetKeyDown(yesKey) || hasUserInputted)
            {
                done = true; // breaks the loop
                response = true;
                hasUserInputted = false;
            }
            else if (Input.GetKeyDown(noKey) || hasUserInputted) 
            {
                done = true;
                response = false;
                hasUserInputted = false;
            }

            yield return null; // wait until next frame, then continue execution from here (loop continues)
        }

        //Debug.Log("Test");


        // now this function returns
    }
    void Update()
    {
        
    }
}
