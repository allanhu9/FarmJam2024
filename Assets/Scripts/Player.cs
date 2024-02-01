using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    Rigidbody2D body;   
    public float speed = 1;
    [SerializeField] float diagonalMultiplier = 0.7f;
    float horizontal;
    float vertical;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");
        vertical = Input.GetAxisRaw("Vertical");
        if (horizontal != 0 && vertical != 0) {
            horizontal *= diagonalMultiplier;
            vertical *= diagonalMultiplier;
        }

        if (horizontal < 0) {
            body.SetRotation(90); // moving left animation
        } else if (horizontal > 0) {
            body.SetRotation(-90); // moving right animation
        } else if (vertical > 0) {
            body.SetRotation(0); // moving up animation
        } else if (vertical < 0) {
            body.SetRotation(180); // moving down animation
        }
        
    }

    private void FixedUpdate() {
        body.velocity = new Vector2(horizontal * speed, vertical * speed);
    }
}
