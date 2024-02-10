using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering;
using UnityEngine;
[System.Serializable]
public class Inventory
{
    public InventoryUI UI;

    [System.Serializable]
    public class Slot{
        public int count;
        public int maxCount;
        public string itemName = "";
        public Sprite icon;

        public Slot() {
            count = 0;
            maxCount = 36;
        }
        public bool IsFull() {
            return count >= maxCount;
        }
        
        public void AddItem(Item item) {
            itemName = item.data.name;
            icon = item.data.icon;
            count++;
        }

        public void RemoveItem() {
            if (count > 0) {
                count--;

                if (count == 0) {
                    icon = null;
                    itemName = "";
                }
            }
        }
    }

    public List<Slot> slots = new List<Slot>();

    public Inventory(int numSlots) {
        for (int i = 0; i < numSlots; i++) {
            Slot slot = new Slot();
            slots.Add(slot);
        }
        GameObject UIObject = GameObject.FindGameObjectWithTag("Inventory");
        UI = UIObject.GetComponent<InventoryUI>();

    }

    public void Add(Item itemToAdd) {
        foreach(Slot slot in slots) {
            if (slot.itemName.Equals(itemToAdd.data.name) && !slot.IsFull()) {
                slot.AddItem(itemToAdd);
                UI.Refresh();
                return;
            }
        }

        foreach(Slot slot in slots) {
            if(slot.itemName == "") {
                slot.AddItem(itemToAdd);
                UI.Refresh();
                return;
            }
        }
    }

    public void Remove(int index) {
        slots[index].RemoveItem();
        UI.Refresh();
    }

    public void Swap(int index1, int index2) {
        //Debug.Log(slots.Capacity);
        Slot temp = slots[index1];
        slots[index1] = slots[index2];
        slots[index2] = temp;
    }
}
