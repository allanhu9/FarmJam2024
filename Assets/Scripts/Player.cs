using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;   
    [SerializeField] private float speed = 1;
    private Vector2 movement;
    private AudioSource steps;

    private Animator animator;
    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        steps = GetComponent<AudioSource>();
    }

    private void OnMovement(InputValue value) {
        movement = value.Get<Vector2>();
        //steps.Play();
        if (movement.x != 0 || movement.y != 0) {
            animator.SetFloat("X", movement.x);
            animator.SetFloat("Y", movement.y);
            animator.SetBool("isMoving", true);
        } else {
            animator.SetBool("isMoving", false);
        }
    }

    // Update is called once per frame
    private void Update()
    { 

    }
    
    private void FixedUpdate() {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

}
