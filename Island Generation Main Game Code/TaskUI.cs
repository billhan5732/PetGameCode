using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TaskUI : MonoBehaviour
{
    // Start is called before the first frame update

    public Task currentTask;
    public Text taskDescription;

    //public float 

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void NewTask(Task newTask) 
    {
        currentTask = newTask;
        UpdateDescription();
    }

    public void UpdateDescription() 
    {
        //Debug.Log("MOOO");
        taskDescription.text = currentTask.taskMessage;
    }

    public void SetMessage(string newMessage) 
    {
        taskDescription.text = newMessage;
    }
}
