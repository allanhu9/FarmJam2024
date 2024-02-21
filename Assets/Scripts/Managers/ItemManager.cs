using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// This class is currently unused, but it should have a copy of every item so the correct item script can be easily acquired through the gameManager singleton
public class ItemManager : MonoBehaviour, DataPersistable
{
    public Item[] items; // Item prefabs are added here in the editor.

    // dictionary is itemName(String)->item(Item)
    // should be treated like a catalogue for coding purposes
    private Dictionary<string, Item> itemsDict = new Dictionary<string, Item>();

    private void Awake() {
        foreach(Item item in items) {
            AddItem(item);
        }
    }

    // EFFECTS: adds item to the item dictionary
    // MODIFIES: this
    private void AddItem(Item item) {
        if(!itemsDict.ContainsKey(item.data.name)) {
            itemsDict.Add(item.data.name, item);
        }
    }

    // EFFECTS: returns item given itemName
    public Item GetItemByName(string key) {
        if(itemsDict.ContainsKey(key)) {
            return itemsDict[key];
        }
        return null;
    }

    public void LoadData(GameData data)
    {
        
    }

    public void SaveData(ref GameData data)
    {
        
    }
}
