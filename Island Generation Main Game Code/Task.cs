using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Task
{
    public static List<Task> allBasicTaskTypes = new List<Task>();

    public float difficulty;
    public float completionPercentage = 0f;
    public string taskMessage;

    public string latestAction;

    public bool isComplete;
    public int coinReward;
    public int xpGain;

    public virtual void TaskUpdate() 
    {
        Debug.Log("Not Overriden");
    }

    public void TaskProgress(string thing) { }

    void Awake() 
    {
        LoadAllTasks();
    }

    void LoadAllTasks() 
    {
        allBasicTaskTypes.Add(new CollectionTask(1f));
        allBasicTaskTypes.Add(new ClearAllHostilesTask(1f));
    }

    public Task createRandomTask(float difficulty) 
    {
        //Task newTask = allBasicTaskTypes[Random.Range(0, allBasicTaskTypes.Count)];
        return new CollectionTask(1f);
    }

}

public class ClearAllHostilesTask : Task
{
    public int NumOfEnemies;
    public int EnemiesLeft;
    public ClearAllHostilesTask(float difficultyLevel)
    {
        base.difficulty = difficultyLevel;
        NumOfEnemies = 5;
        EnemiesLeft = NumOfEnemies;

        base.coinReward = (int)(base.difficulty * 50) + 150;
        base.xpGain = (int)(base.difficulty * NumOfEnemies * 50);
        IGGameController.numOfEnemies = EnemiesLeft;
        taskMessage = "Clear the Island of the Plague!\nThere are " + EnemiesLeft.ToString() + " Enemies left";
    }

    public void HostilePropagated(int amt) 
    {
        NumOfEnemies += amt;
        EnemiesLeft += amt;
    }

    public override void TaskUpdate()
    {
        if (latestAction.Equals("Killed Enemy"))
        {
            EnemiesLeft -= 1;
            IGGameController.numOfEnemies = EnemiesLeft;
            if (EnemiesLeft == 0)
            {
                base.isComplete = true;
                IGGameController.PlayerHasCompletedTask();
                taskMessage = "COMPLETED";
            }
            taskMessage = "Clear the Island of the Plague!\nThere are " + EnemiesLeft.ToString() + " Enemies left";
        }
    }
}

public class CollectionTask : Task 
{
    int itemsCollected;
    string itemToCollect;
    int numItemsNeeded;

    public CollectionTask(float difficultyLevel) 
    {
        itemToCollect = ConfigResourceManager.dropGOs[Random.Range(0, ConfigResourceManager.dropGOs.Length)].GetComponent<Drop>().name;
        base.difficulty = 1;
        base.completionPercentage = 0f;
        
        itemsCollected = 0;
        //base.taskMessage = ""
        SetObjectiveAmmounts();

        base.coinReward = (int)(base.difficulty * 50);
        base.xpGain = numItemsNeeded*5;
    }
    void SetObjectiveAmmounts() 
    {
        numItemsNeeded = (int)(base.difficulty * 1.5f * BackPack.stackSize);
        base.taskMessage = "Collect " + numItemsNeeded.ToString() + " " + itemToCollect;
    }

    public override void TaskUpdate() 
    {
        Debug.Log("Player Picked Up Item");
        if (base.latestAction.Equals("Added "+itemToCollect+" to BackPack") && !base.isComplete) 
        {
            numItemsNeeded -= 1;
            base.taskMessage = "Collect " + numItemsNeeded.ToString() + " " + itemToCollect;
            if (numItemsNeeded <= 0)
            {
                Debug.Log("Task Finished");
                base.isComplete = true;
                base.taskMessage = "Complete!";
                Debug.Log(base.coinReward);
                IGGameController.PlayerHasCompletedTask();
            }
        }

        

    }

}


