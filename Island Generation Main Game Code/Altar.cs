using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Altar : MonoBehaviour
{
    // Start is called before the first frame update

    public SphereCollider enterCollider;

    public PromptYN prompt;

    public Task task;

    public IGGameController gameController;

    public delegate void TestDelegate(); // This defines what type of method you're going to call.
    public TestDelegate m_methodToCall;

    void Start()
    {
        gameController = GameObject.FindGameObjectWithTag("GameController").GetComponent<IGGameController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter(Collider collision) 
    {
        if (collision.gameObject.tag == "Player" && task == null) 
        {
            Debug.Log("Player Wants to Interact");
            //Time.timeScale = 0;
            prompt.getUserInput("Start Island Challenge?", ChallengeActivate);

            //Debug.Log("Poggers");
        }
    }

    public void ChallengeActivate() 
    {
        Debug.Log("Challenge Started");
        Task newTask = new CollectionTask(1f);
        this.task = newTask;
        gameController.uiController.GetTaskUI().NewTask(newTask);
        IGGameController.currentTask = newTask;
        IGGameController.m_TaskUpdate += ((CollectionTask)(newTask)).TaskUpdate;
        IGGameController.m_TaskUpdate += gameController.uiController.GetTaskUI().UpdateDescription;
    }

    
}
