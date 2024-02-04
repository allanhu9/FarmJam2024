using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    private Rigidbody2D rb;   
    [SerializeField] private float speed = 1;
    private Vector2 movement;
    private Vector3Int interactPosition;
    private Vector2 facingDirection;
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
        SetFacingDirection();
        //steps.Play();
        if (movement.x != 0 || movement.y != 0) {
            animator.SetFloat("X", movement.x);
            animator.SetFloat("Y", movement.y);
            animator.SetBool("isMoving", true);
        } else {
            animator.SetBool("isMoving", false);
        }
    }

    private void SetFacingDirection() {
        if (movement.y > 0) {
            facingDirection = new Vector2(0, 1);
        } else if (movement.x > 0) {
            facingDirection = new Vector2(1, 0);
        } else if (movement.y < 0) {
            facingDirection = new Vector2(0, -1);
        } else if (movement.x < 0){
            facingDirection = new Vector2(-1, 0);
        }
    }

    // Eventually this code should check what item you have highlighted in the hotbar.
    private void OnUseItem(){ // for now this just plows the ground
       if(GameManager.singleton.tileManager.IsInteractable(interactPosition)) {
        GameManager.singleton.tileManager.SetInteracted(interactPosition);
       }
    }
    

    // Update is called once per frame
    private void Update()
    { 
        interactPosition = new Vector3Int(Mathf.RoundToInt(transform.position.x + 0.5f*facingDirection.x), Mathf.RoundToInt(transform.position.y + 0.5f*facingDirection.y), 0);
        GameManager.singleton.tileManager.SetHightlighted(interactPosition);
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }

}
