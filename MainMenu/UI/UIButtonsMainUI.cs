using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class UIButtonsMainUI : MonoBehaviour
{
    public Transform mainCamera;

    public Transform[] camPositions;

    float screenSwapSpeed = 10f;
    int currentCamPosition = 0;
    int targetCamPosition = 0;

    int transitionStep = 0;//0 - active, 1 - in transit, 2 - off screen

    public Transform rightBarT, playButtonT;

    Vector3 rBOGPosition, pBOGPosition;

    public Transform rightBarOGPosition, playButtonOGPosition;
    public Transform rightBarNewPosition, playButtonNewPosition;
    public Transform rightBarTargetPosition, playButtonTargetPosition;
    // Start is called before the first frame update
    void Start()
    {
        rBOGPosition = rightBarOGPosition.position;
        pBOGPosition = playButtonOGPosition.position;
    }

    // Update is called once per frame
    void Update()
    {
        //camMovementUpdates();
        //updateUIChangeScreen();
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
            }
        }
    }

    

    public void updateUIChangeScreen() 
    {
        if (transitionStep != 1) { return; }
        /*
        var step = screenSwapSpeed * Time.deltaTime;
        rightBarT.position = Vector3.MoveTowards(rightBarT.position, rightBarTargetPosition.position, step);
        playButtonT.position = Vector3.MoveTowards(playButtonT.position, playButtonTargetPosition.position, step);
        if (getDistance(rightBarT,rightBarTargetPosition) <= 0.01f) { transitionStep = 3; warpSnap(rightBarTargetPosition, playButtonTargetPosition); }
        if (getDistance(playButtonT, playButtonTargetPosition) <= 0.01f) { transitionStep = 3; warpSnap(rightBarTargetPosition, playButtonTargetPosition); }
        */


        var step = screenSwapSpeed * Time.deltaTime;
        Vector3 newPos = rightBarT.position;
        newPos.x += step;
        rightBarT.position = newPos;

        if (rightBarT.position.x <= rBOGPosition.x) { transitionStep = 3; }

        //rightBarT.position


    }

    float getDistance(Transform t1, Transform t2) 
    {
        return Mathf.Abs(Vector3.Distance(t1.position, t2.position));
    }

    void warpSnap(Transform newPosTRB, Transform newPosTPB) 
    {
        rightBarT.position = newPosTRB.position;
        playButtonT.position = newPosTPB.position;
    }

    public void moreButtonRightClicked() 
    
    {
        //targetCamPosition = 1;
        transitionStep = 1;
        rightBarTargetPosition = rightBarNewPosition;
        playButtonTargetPosition = playButtonNewPosition;
        //mainCamera.position = Vector3.MoveTowards(mainCamera, camPositions[1]);
    }
    
}
