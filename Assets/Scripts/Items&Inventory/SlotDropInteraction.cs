using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class SlotDropInteraction : MonoBehaviour, IDropHandler
{
    private Inventory inventory;
    public int slotIndex;

    private void Start() {
        Player player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        inventory = player.inventory;
    }

    public void OnDrop(PointerEventData eventData)
    {
        //Debug.Log("Slot got Item!");
        GameObject dropped = eventData.pointerDrag; // the object that was dropped here
        Draggable draggable = dropped.GetComponent<Draggable>(); // the image that draggable was on
        Transform slotFrom = draggable.parentAfterDrag; // the transform of the old slot
        SlotDropInteraction dropFrom = slotFrom.GetComponentInParent<SlotDropInteraction>(); // the slotDrop script from the old slot
        inventory.Swap(slotIndex, dropFrom.slotIndex); // swap the two slots
        inventory.UI.Refresh(); // Refesh the inventory UI
    }

    public void setInventory(Inventory inventory) {
        this.inventory = inventory;
    }
}
