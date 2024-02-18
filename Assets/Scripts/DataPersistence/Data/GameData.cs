using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[System.Serializable] 
public class GameData : MonoBehaviour
{
    public int deathCount;
    public float startTime;
    public int Day;
    public int Hour;
    public int Minute;

    // Notes on what needs to be saved:
    // TileManager
    // Inventory
    // Grab saved info at the correct time from Inventory's constructor and use Add function (UI will naturally refresh).
    // Grab saved tilemaps on start from TileManager.

    // EFFECTS: Constructs a gameData object that stores all the game data
    public GameData()
    {
        this.deathCount = 0;
        this.startTime = Time.time;
        this.Day = 1;
        this.Minute = 0;
        this.Hour = 8;
    }
}
