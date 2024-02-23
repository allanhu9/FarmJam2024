using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

// Handles items being dropped into the slot that this script is attached to
public class SlotDropInteraction : MonoBehaviour, IDropHandler
{
    private Inventory inventory;
    public int slotIndex;

    // EFFECTS: gets a reference to the player's inventory
    private void Start() {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        inventory = player.inventory;
    }

    // This is a function that is called when a draggable is released when the mouse is hovering over the object that this script is on
    // eventData includes the dropped GameObject (an item GameObject in this case)
    // TODO: Make this stack same items, rather than swapping them

    // EFFECTS: Swaps the index's of the dropped item with the item in this slot
    // MODIFIES: this, Player.inventory, Player.inventory.UI
    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("Slot got Item!");
        GameObject dropped = eventData.pointerDrag; // the object that was dropped here
        Draggable draggable = dropped.GetComponent<Draggable>(); // the image that draggable was on
        Transform slotFrom = draggable.previousParent; // the transform of the old slot
        SlotDropInteraction dropFrom = slotFrom.GetComponentInParent<SlotDropInteraction>(); // the slotDrop script from the old slot
        if (inventory.ItemCount(dropFrom.slotIndex) != 0) {
            inventory.SwapOrStack(dropFrom.slotIndex, slotIndex); // swap or stack the slots
            inventory.UI.Refresh(); // Refesh the inventory UI
        }
    }
}
