using System;
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
    public List<SavedTile> interactableMap;
    public List<SavedTile> interactedMap;
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
    }
}
[Serializable]
public class SavedTile
{
    public SavedTile(Vector3Int pos, Tile mapTile)
    {
        this.pos = pos;
        this.mapTile = mapTile;
    }
    public Vector3Int pos;
    public TileBase mapTile;
}


