using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseInputManager : MonoBehaviour
{
    public Vector2 position {get; private set;}
    public event Action Clicked;

    private void OnLook(InputValue value) {
        position = value.Get<Vector2>();
    }

    private void OnAction(InputValue _) {
        Clicked?.Invoke();
    }

}
