using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemManager : MonoBehaviour
{
    public Item[] items;

    // dictionary is itemName->item
    private Dictionary<string, Item> itemsDict = new Dictionary<string, Item>();

    private void Awake() {
        foreach(Item item in items) {
            AddItem(item);
        }
    }

    private void AddItem(Item item) {
        if(!itemsDict.ContainsKey(item.data.name)) {
            itemsDict.Add(item.data.name, item);
        }
    }

    public Item GetItemByName(string key) {
        if(itemsDict.ContainsKey(key)) {
            return itemsDict[key];
        }
        return null;
    }
}
