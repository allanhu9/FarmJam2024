using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Rendering;
using UnityEngine;

// This class is a collection of slots, which is defined within. (Saving the slots array is probably most important)

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
            //Debug.Log("Adding item with name: " + item.data.name);
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

    // EFFECTS: Instantiates slot array with numSlots slots.
    // MODIFIES: this
    public Inventory(int numSlots) {
        for (int i = 0; i < numSlots; i++) {
            Slot slot = new Slot();
            slots.Add(slot);
        }
        GameObject UIObject = GameObject.FindGameObjectWithTag("Inventory");
        UI = UIObject.GetComponent<InventoryUI>();

        Debug.Log(numSlots);

    }

    // EFFECTS: adds an item to the first available slot or stacks it with an existing item, then refreshes the UI to reflect the change
    // MODIFIES: this
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

    // EFFECTS: Adds 1 item to an empty slot or slot with same item at index, returns true if successful
    // MODIFIES: this
    public bool AddToSlot(int index, Item itemToAdd) {
        if (slots[index].itemName == "" || slots[index].itemName.Equals(itemToAdd.data.name) && !slots[index].IsFull()) {
            slots[index].AddItem(itemToAdd);
            UI.Refresh();
            return true;
        }
        return false;
    }

    // EFFECTS: Moves 1 item from index1 to index2
    // MODIFIES: this
    public void MoveToSlot(int index1, int index2) {
        if (ItemCount(index1) > 0) {
            Item itemToAdd = GameManager.singleton.itemManager.GetItemByName(slots[index1].itemName);
            if (AddToSlot(index2, itemToAdd))
                Remove(index1);
        }
    }

    // EFFECTS: Removes item at index
    // MODIFIES: this
    public void Remove(int index) {
        slots[index].RemoveItem();
        UI.Refresh();
    }

    // EFFECTS: Removes all items from the slot and returns the item correlated to it
    // MODIFIES: this
    public Item Pop(int index) {
        Item itemToDrop = GameManager.singleton.itemManager.GetItemByName(slots[index].itemName);
        //Debug.Log(ItemCount(index));
        int count = ItemCount(index);
        for (int i = 0; i < count; i++) {
            Remove(index);
        }
        return itemToDrop;
    }

    // EFFECTS: Returns the amount of items in the given slot
    public int ItemCount(int index) {
        return slots[index].count;
    }
    // EFFECTS: Swaps items at index1 and index2 if they are different items, stacks into index2 otherwise.
    // MODIFIES: this
    public void SwapOrStack(int index1, int index2) {
        if (slots[index1].itemName.Equals(slots[index2].itemName) && !(slots[index1].maxCount == slots[index1].count) 
        && !(slots[index2].maxCount == slots[index2].count)) {
            int count = ItemCount(index1);
            for (int i = 0; i < count; i++) {
                MoveToSlot(index1, index2);
            }
        } else {
            Swap(index1, index2);
        }
    }
    // EFFECTS: Swaps items at index1 and index2
    // MODIFIES: this
    public void Swap(int index1, int index2) {
        //Debug.Log(slots.Capacity);
        Slot temp = slots[index1];
        slots[index1] = slots[index2];
        slots[index2] = temp;
    }
}
