using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// A template for a Unity Object which contains item data (you can make items in the editor based on this script)
[CreateAssetMenu(fileName = "Item Data", menuName = "Item Data", order = 50)]
public class ItemData : ScriptableObject
{
    public string itemName = "Item Name";
    public Sprite icon;
}
