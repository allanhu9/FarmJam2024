using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;


[System.Serializable] 
public class GameData
{
    public int deathCount;
    public int Day;
    public int Hour;
    public int Minute;
    public Tilemap interactableMap;
    public Tilemap interactedMap;
    // Notes on what needs to be saved:
    // TileManager
    // Inventory
    // Grab saved info at the correct time from Inventory's constructor and use Add function (UI will naturally refresh).
    // Grab saved tilemaps on start from TileManager.

    // EFFECTS: Constructs a gameData object that stores all the game data
    public GameData()
    {
        this.deathCount = 0;
        this.Day = 1;
        this.Minute = 0;
        this.Hour = 8;
        interactableMap = GameObject.FindWithTag("InteractableTiles").GetComponent<Tilemap>();
        interactedMap = GameObject.FindWithTag("InteractedTiles").GetComponent<Tilemap>();
    }
}
