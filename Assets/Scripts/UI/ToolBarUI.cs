using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using Unity.Profiling;
using Unity.VisualScripting;
using UnityEngine;

public class ToolBarUI : MonoBehaviour
{
    [SerializeField] private List<SlotUI> slots = new List<SlotUI>();
    private SlotUI selectedSlot;
    private int selectedSlotIndex;
    // Start is called before the first frame update
    void Start()
    {
        SelectSlot(0);
    }

    void Update() {
        CheckAlphaNumericKeys();
        CheckScrollWheel();
    }
    public SlotUI GetSelectedSlot() {
        return selectedSlot;
    }

    public SlotUI GetSlot(int index) {
        return slots[index];
    }

    public void SetItem(int index, Inventory.Slot slot) {
        slots[index].SetItem(slot);
    }

    public void SetEmpty(int index) {
        slots[index].SetEmpty();
    }

    public int Count() {
        return slots.Count;
    }

    public void SelectSlot(int index) {
        if (selectedSlot) {
            selectedSlot.ToggleHighlight();
        }
        if (slots.Count == 7) {
            selectedSlot = slots[index];
            selectedSlotIndex = index;
            selectedSlot.ToggleHighlight();
        }
    }

    private void CheckAlphaNumericKeys() {
        if (Input.GetKeyDown(KeyCode.Alpha1)) {
            SelectSlot(0);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2)) {
            SelectSlot(1);
        }
        if (Input.GetKeyDown(KeyCode.Alpha3)) {
            SelectSlot(2);
        }
        if (Input.GetKeyDown(KeyCode.Alpha4)) {
            SelectSlot(3);
        }
        if (Input.GetKeyDown(KeyCode.Alpha5)) {
            SelectSlot(4);
        }
        if (Input.GetKeyDown(KeyCode.Alpha6)) {
            SelectSlot(5);
        }
        if (Input.GetKeyDown(KeyCode.Alpha7)) {
            SelectSlot(6);
        }
    }

    private void CheckScrollWheel() {
        if (Input.mouseScrollDelta.y > 0) {
            if (selectedSlotIndex == 6) {
                SelectSlot(0);
            } else {
                SelectSlot(selectedSlotIndex + 1);
            }
        } if (Input.mouseScrollDelta.y < 0) {
            if (selectedSlotIndex == 0) {
                SelectSlot(6);
            } else {
                SelectSlot(selectedSlotIndex - 1);
            }
        }
    }
}
