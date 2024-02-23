using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Drawing;

// Class for making items in your inventory draggable (This is applied to the icon in a slot prefab)

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // NOTE: gameObject in MonoBehaviour classes is the GameObject that the script/component is attached to in the editor
    public Image icon;
    private Player player;
    private PointerEventData currData;
    private bool dragging = false;
    [HideInInspector] public Transform previousParent; // Remembers the parent gameObject of this GameObject before the drag 

    // EFFECTS: Remember this gameObject's parent, and set it to the top layer. Prevent raycasting so onDrop doesn't think we're dropping on the object we're dragging
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Begin Drag");
        previousParent = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        icon.raycastTarget = false;
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();
        dragging = true;
        currData = eventData;
    }

    // EFFECTS: Sets the location of gameObject to the mouse
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Dragging");
        transform.position = Input.mousePosition;
        currData = eventData;
    }

    // EFFECTS: Returns GameObject's transform to previousParent
    public void OnEndDrag(PointerEventData eventData)
    {
        dragging = false;
        GameObject droppedOn = eventData.pointerCurrentRaycast.gameObject;
        if (droppedOn) {
            transform.SetParent(previousParent);
            icon.raycastTarget = true;
        } else {
            transform.SetParent(previousParent);
            icon.raycastTarget = true;
            GameObject dropped = eventData.pointerDrag; // the object that was dropped here
            Draggable draggable = dropped.GetComponent<Draggable>(); // the image that draggable was on
            Transform slotFrom = draggable.previousParent; // the transform of the old slot
            SlotDropInteraction dropFrom = slotFrom.GetComponentInParent<SlotDropInteraction>(); // the slotDrop script from the old slot
            player.DropItem(dropFrom.slotIndex);
        }
        
    }

    public void Update() {
        // EFFECTS: When right clicking while dragging an item, it places one of that item into the slot you're hovering over, removing it from the slot you dragged from
        // if there are still that item in the slot you took from
        // MODIFIES: player.inventory
        if(Input.GetKeyDown(KeyCode.Mouse1) && dragging) {
            //Debug.Log("Clicking!");
            GameObject droppedOnObject = currData.pointerCurrentRaycast.gameObject;
            //Debug.Log(droppedOnObject);
            if (droppedOnObject) {
                SlotDropInteraction droppedOn = droppedOnObject.GetComponentInParent<SlotDropInteraction>(); // slotDrop script from the TO slot
                if (droppedOn) {
                    //Debug.Log("Dropped On slot exists");
                    GameObject dropped = currData.pointerDrag;
                    Draggable draggable = dropped.GetComponent<Draggable>();
                    Transform slotFrom = draggable.previousParent; 
                    SlotDropInteraction dropFrom = slotFrom.GetComponentInParent<SlotDropInteraction>(); // the slotDrop script from the FROM slot
                    player.inventory.MoveToSlot(dropFrom.slotIndex, droppedOn.slotIndex);
                }
            }
       }
    }
    // See OnDrop in SlotDropInteraction class
}
