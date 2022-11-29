using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class IGGameController : MonoBehaviour
{

    static string[] islandTypes = {"RegularIsland"};
    public string islandType;
    public bool needToGenerateTerrain;
    public Color mainTileColor;
    public static IGGameController instance;

    public GameObject player;

    public PromptYN prompt;

    public Camera mainCam;

    public InSessionUI uiController;

    public delegate void OnTaskUpdateCallback(); // This defines what type of method you're going to call.
    public static OnTaskUpdateCallback m_TaskUpdate;

    public static Task currentTask;

    public static float timeRemaining;
    public static bool hasTimeLeft = true;

    static int runCount = 0;

    public static int numOfEnemies = 0;

    GameObject[] enemyPrefabs;

    void Awake()
    {
        #region Singleton
        if (instance != null)
        {
            Debug.LogWarning("More than one instance of IGGameController found");
            return;
        }
        instance = this;
        #endregion

        //islandType = islandTypes[Random.Range(0, islandTypes.Length)];
        Generation.islandType = islandType;
        TilePiece.landColor = mainTileColor;

    }
    void Start()
    {
        numOfEnemies = 0;
        Cursor.lockState = CursorLockMode.Locked;
        //player = GameObject.FindGameObjectsWithTag("Player")[0];
        if (needToGenerateTerrain) 
        {
            GetComponent<Generation>().LOAD_MAP();
        }
        
        player.GetComponent<Player>().SetPet();
        
        timeRemaining = 500f;
        hasTimeLeft = true;

        runCount++;
        Debug.Log("Run Number: " + runCount.ToString());

        enemyPrefabs = Resources.LoadAll<GameObject>("IG/Enemies/RegularIsland");// + islandType);

        uiController.GetTaskUI().SetMessage("Locate the Altar and complete it\'s quest!");

        switch (islandType) 
        {
            case ("Regular Island Enemy Objective"):
                currentTask = new ClearAllHostilesTask(runCount + 1);
                uiController.GetTaskUI().NewTask(currentTask);
                SpawnEnemies(runCount + 1);
                break;
            default:
                break;

        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftAlt)) { Cursor.lockState = CursorLockMode.Confined; }
        handleTimer();


    }

    void SpawnEnemies(int difficulty) 
    {
        for (int i = 0; i < numOfEnemies; i++) 
        {
            GameObject newEnemy = Instantiate(enemyPrefabs[Random.Range(0, enemyPrefabs.Length)]) as GameObject;
            GetComponent<Generation>().SpawnGameObjectOnRandomTile(newEnemy, true);
        }
        m_TaskUpdate += ((currentTask)).TaskUpdate;
        m_TaskUpdate += uiController.GetTaskUI().UpdateDescription;
        //uiController.GetTaskUI().UpdateDescription();
    }

    void handleTimer() 
    {
        if (hasTimeLeft)
        {
            timeRemaining -= Time.deltaTime;
            if (timeRemaining <= 0f) { hasTimeLeft = false; }
        }
    }

    public static void PlayerHasPickedUpItem(string itemName) 
    {
        if (m_TaskUpdate != null)
        {
            //Debug.Log("Check");
            string action = "Added " + itemName + " to BackPack";
            Debug.Log(action);
            currentTask.latestAction = action;
            m_TaskUpdate.Invoke();
        }
    }

    public static void PlayerHasKilledEnemy()
    {
        if ((m_TaskUpdate == null)){return;}
        string action = "Killed Enemy";
        currentTask.latestAction = action;
        m_TaskUpdate.Invoke();
    }

    public static void PlayerHasCompletedTask() 
    {
        m_TaskUpdate -= currentTask.TaskUpdate;
        m_TaskUpdate = null;
        AccountStats.AddCoins(currentTask.coinReward);
        instance.uiController.creditDisplayer.UpdateDisplay();
        currentTask = null;

        instance.uiController.GetTaskUI().SetMessage("COMPLETED");
        //Debug.Log("Task Complete - IGG");

    }

    public void ReturnToMenu() 
    {
        Cursor.lockState = CursorLockMode.None;

        SceneManager.LoadScene("SampleScene");
    }



    public static void SetTagDeeply(GameObject obj, string newTag  )
    {
        obj.tag = newTag;

        foreach (Transform child in obj.transform)
        {
            SetTagDeeply(child.gameObject, newTag);
        }
    }
}
