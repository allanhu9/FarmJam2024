using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using System.Linq;

public class DataPersistenceManager : MonoBehaviour
{
  public static DataPersistenceManager singleton { get; private set; }

    private GameData gameData;
    private List<DataPersistable> dataPersistables;

    // EFFECTS: Creates a Singleton DataPersistenceManager
    // MODIFIES: this
    private void Awake()
    {
        if (singleton != null && singleton != this)
        { 
            Destroy(gameObject);
        }
        else
        {
            singleton = this;
        }
    }
    // EFFECTS: On the scene starting load the game
    // MODIFIES: this, gameData
    private void Start()
    {
        this.dataPersistables = FindAllDataPersistables();
        LoadGame();
    }

    // EFFECTS: Starts a new game
    // MODIFIES: this
    public void NewGame()
    {
        this.gameData = new GameData();
    }
    
    // EFFECTS: Loads a game from saved data. If there is no saved data, start a new game
    // MODIFIES: this, gameData
    public void LoadGame()
    {
        //TODO: Load save data from a file
        if(this.gameData == null)
        {
            NewGame();
        }

        foreach(DataPersistable dataPersistable in this.dataPersistables)
        {
            dataPersistable.LoadData(gameData);
        }
        //TODO: Ensure the rest of the system has the save data

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
        // TODO: Save the data to a file using the data handler
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
