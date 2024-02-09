using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Player player;
    public List<SlotUI> slots;
    [SerializeField] private GameObject slotPrefab;
    private GameObject slotContainer;

    void Awake() {
        slotContainer = transform.GetChild(0).transform.GetChild(0).gameObject;
        GameObject slot;
        //List<SlotDropInteraction> temp = new List<SlotDropInteraction>(slots.Capacity);

        for (int i = 0; i < slots.Capacity; i++) {
            slot = Instantiate(slotPrefab, slotContainer.transform.position, Quaternion.identity, slotContainer.transform);
            SlotUI slotUI = slot.GetComponent<SlotUI>();
            SlotDropInteraction slotDropInteraction = slot.GetComponent<SlotDropInteraction>();
            slotDropInteraction.slotIndex = i;
            slots[i] = slotUI;
        }
        /*foreach (SlotDropInteraction dropInteraction in temp) {
            dropInteraction.setInventory(player.inventory);
        }*/
    }

    // Update is called once per frame
    void Update()
    {  
       if(Input.GetKeyDown(KeyCode.F)) {
        ToggleInventory();
       } 
    }

    public void ToggleInventory() {
        // if turned off
        if (!inventoryPanel.activeSelf) {
            Refresh();
            player.inventoryOpen = true;
            inventoryPanel.SetActive(true);
        } else {
            player.inventoryOpen = false;
            inventoryPanel.SetActive(false);
        }
        //Setup();
    }

    public void Refresh() { // displays the correct item in each slot and the correct amount of each item
        if(slots.Count == player.inventory.slots.Count) {
            for (int i = 0; i < slots.Count; i++) {
                if (player.inventory.slots[i].itemName != ""){
                    slots[i].SetItem(player.inventory.slots[i]);
                } else {
                    slots[i].SetEmpty();
                }
            }
        }
    }
    public void Remove(int slotID) {
        player.inventory.Remove(slotID);
        Refresh();
    }
}
