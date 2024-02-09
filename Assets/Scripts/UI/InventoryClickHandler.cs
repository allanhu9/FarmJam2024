using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class InventoryClickHandler : MonoBehaviour
{
    [SerializeField] private UnityEvent _clicked;
    private MouseInputManager _mouse;

    private void Awake() {
        _mouse = FindObjectOfType<MouseInputManager>();
        _mouse.Clicked += MouseOnClicked;
    }

    private void MouseOnClicked() {
        _clicked?.Invoke();
    }

}
