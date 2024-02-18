using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface DataPersistable 
{
    //EFFECTS: Loads Data to the persistable
    //MODIFIES: this
    void LoadData(GameData data);
    //EFFECTS: Saves the Data of the script
    //MODIFIES: this
    void SaveData(ref GameData data);
}
