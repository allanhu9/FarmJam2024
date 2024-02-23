using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Manages the UI for the player inventory

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Player player;
    public List<SlotUI> slots;
    [SerializeField] private ToolBarUI toolbarUI;
    [SerializeField] private GameObject slotPrefab;
    private GameObject slotContainer;

    // Using the slot prefab, instantiates the slot prefabs (16, set in editor) in the Slots GameObject in the Inventory GameObject
    void Awake() {
        slotContainer = transform.GetChild(0).transform.GetChild(0).gameObject;
        GameObject slot;

        for (int i = 0; i < slots.Capacity; i++) {
            slot = Instantiate(slotPrefab, slotContainer.transform.position, Quaternion.identity, slotContainer.transform);
            SlotUI slotUI = slot.GetComponent<SlotUI>();
            SlotDropInteraction slotDropInteraction = slot.GetComponent<SlotDropInteraction>(); // sets the index of each slot, so they are swapped properly during drags and drops
            slotDropInteraction.slotIndex = i;
            slots[i] = slotUI;
        }
    }

    void Start() {
        Refresh();
    }

    // EFFECTS: When "F" is pressed, toggle inventory.
    // TODO: This should be put in Player class using the PlayerInput feature of Unity
    void Update()
    {  
       if(Input.GetKeyDown(KeyCode.F)) {
            ToggleInventory();
       } 
    }

    // EFFECTS: Toggles InventoryPanel GameObject active or inactive
    // MODIFIES: this, InventoryPanel
    public void ToggleInventory() {
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

    // EFFECTS: displays the correct item in each slot and the correct amount of each item based on the inventory class
    // MODIFIES: this, slots
    public void Refresh() { 
        if(slots.Count + toolbarUI.Count() == player.inventory.slots.Count) {
            for (int i = 0; i < slots.Count; i++) {
                if (player.inventory.slots[i].itemName != ""){
                    slots[i].SetItem(player.inventory.slots[i]);
                } else {
                    slots[i].SetEmpty();
                }
            }

            for (int i = slots.Count; i < slots.Count+toolbarUI.Count(); i++) {
                if (player.inventory.slots[i].itemName != "") {
                        toolbarUI.SetItem(i - slots.Count, player.inventory.slots[i]);
                    } else {
                        toolbarUI.SetEmpty(i - slots.Count);
                    }
            }
        } else {
            Debug.Log("Player inventory size doesn't match toolbar + UI inventory size");
        }
    }
}
