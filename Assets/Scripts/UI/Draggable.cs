using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

// Class for making items in your inventory draggable (This is applied to the icon in a slot prefab)

public class Draggable : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    // NOTE: gameObject in MonoBehaviour classes is the GameObject that the script/component is attached to in the editor
    public Image icon;
    [HideInInspector] public Transform previousParent; // Remembers the parent gameObject of this GameObject before the drag 
    
    // EFFECTS: Remember this gameObject's parent, and set it to the top layer. Prevent raycasting so onDrop doesn't think we're dropping on the object we're dragging
    public void OnBeginDrag(PointerEventData eventData)
    {
        //Debug.Log("Begin Drag");
        previousParent = transform.parent;
        transform.SetParent(transform.root);
        transform.SetAsLastSibling();
        icon.raycastTarget = false;
    }

    // EFFECTS: Sets the location of gameObject to the mouse
    public void OnDrag(PointerEventData eventData)
    {
        //Debug.Log("Dragging");
        transform.position = Input.mousePosition;
    }

    // EFFECTS: Returns GameObject's transform to previousParent
    public void OnEndDrag(PointerEventData eventData)
    {
        transform.SetParent(previousParent);
        icon.raycastTarget = true;
    }

    // See OnDrop in SlotDropInteraction class
}
