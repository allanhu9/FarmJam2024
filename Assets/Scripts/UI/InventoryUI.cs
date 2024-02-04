using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryUI : MonoBehaviour
{
    public GameObject inventoryPanel;
    public Player player;
    public List<SlotUI> slots;
    // Start is called before the first frame update
    void Start()
    {
        
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
            inventoryPanel.SetActive(true);
        } else {
            inventoryPanel.SetActive(false);
        }
        //Setup();
    }

    public void Setup() {
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
}
