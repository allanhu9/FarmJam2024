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
