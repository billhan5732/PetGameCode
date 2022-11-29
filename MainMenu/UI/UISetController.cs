using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UISetController : MonoBehaviour
{
    public Transform mainCamera;

    public Transform[] camPositions;

    float screenSwapSpeed = 30f;
    int currentCamPosition = 0;
    int targetCamPosition = 0;

    public GameObject mainUIButtons;
    public GameObject dockUIButtons;

    public ScreenBlackOut screenBlackOut;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        camMovementUpdates();
    }
    public void PlayButtonClicked()
    {
        //Gaming.settingNewPet(Inventory.selectedPet);
        StartCoroutine(transitionToNextScene("Boss Fight Island"));
    }

    IEnumerator transitionToNextScene(string sceneName)
    {
        screenBlackOut.BringBackBlackOutScreen();
        yield return new WaitForSeconds(screenBlackOut.time);
        SceneManager.LoadScene(sceneName);
    }
    void camMovementUpdates()
    {

        if (targetCamPosition != currentCamPosition)
        {
            var step = screenSwapSpeed * Time.deltaTime;
            mainCamera.position = Vector3.MoveTowards(mainCamera.position, camPositions[targetCamPosition].position, step);
            if (Mathf.Abs(Vector3.Distance(mainCamera.position, camPositions[targetCamPosition].position)) <= 0.01f)
            {
                currentCamPosition = targetCamPosition;
                uiSetSwapUpdate();
            }
        }
    }

    public void moreButtonRightClicked()

    {
        targetCamPosition = 0;

        //mainCamera.position = Vector3.MoveTowards(mainCamera, camPositions[1]);
    }

    public void moreButtonLeftClicked() 
    {
        targetCamPosition = 1;
    }

    void uiSetSwapUpdate() 
    {
        switch (currentCamPosition) 
        {
            case 0:
                dockUIButtons.SetActive(false);
                mainUIButtons.SetActive(true);
                break;
            case 1:
                dockUIButtons.SetActive(true);
                mainUIButtons.SetActive(false);
                break;
        }
    } 

}
