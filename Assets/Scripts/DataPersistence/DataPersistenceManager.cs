using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;

public class DataPersistenceManager : MonoBehaviour
{
  public static DataPersistenceManager singleton { get; private set; }

    [Header("File Storage Config")]
    [SerializeField] public string fileName;
    
    
    private GameData gameData;
    private List<DataPersistable> dataPersistables;
    private FileDataHandler dataHandler;

    // EFFECTS: Creates a Singleton DataPersistenceManager
    // MODIFIES: this
    private void Awake()
    {
        if (singleton != null && singleton != this)
        { 
            Destroy(this.gameObject);
        }
        else
        {
            singleton = this;
        }
        DontDestroyOnLoad(this.gameObject);
        this.dataHandler = new FileDataHandler(Application.persistentDataPath, fileName);
    }
    

    // EFFECTS: Starts a new game
    // MODIFIES: this
    public void NewGame()
    {
        this.gameData = new GameData();
    }

    public void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
    }
    public void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
        SceneManager.sceneUnloaded -= OnSceneUnloaded;
    }
    public void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        this.dataPersistables = FindAllDataPersistables();
        LoadGame();
        Debug.LogError("Loaded");
    }

    public void OnSceneUnloaded(Scene scene)
    {
        SaveGame();
        Debug.LogError("Saved");
    }
    
    // EFFECTS: Loads a game from saved data. If there is no saved data, start a new game
    // MODIFIES: this, gameData
    public void LoadGame()
    {
        this.gameData = dataHandler.Load();
        
        
        if(this.gameData == null)
        {
            NewGame();
        }

        foreach(DataPersistable dataPersistable in this.dataPersistables)
        {
            dataPersistable.LoadData(gameData);
        }
   
        

    }
    // EFFECTS: Saves the current game state to gameData
    // MODIFIES: this, gameData
    public void SaveGame()
    {
        // TODO: pass the data to the rest of the system
        foreach(DataPersistable dataPersistable in this.dataPersistables)
        {
            dataPersistable.SaveData(ref gameData);
        }
        

        dataHandler.Save(gameData);
    }
    
    //EFFECTS: Saves the game when the application is quit
    //MODIFIES: this, gameData
    public void OnApplicationQuit()
    {
        SaveGame();
    }

    //EFFECTS: Find all data persistables that are also of type MonoBehaviour
    private List<DataPersistable> FindAllDataPersistables()
    {
        IEnumerable<DataPersistable> dataPersistables = FindObjectsOfType<MonoBehaviour>().OfType<DataPersistable>();
        
        return new List<DataPersistable>(dataPersistables);
    }
}
