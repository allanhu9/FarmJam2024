using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour, DataPersistable
{
    private Rigidbody2D rb;   
    [SerializeField] private float speed = 1;
    private Vector2 movement;
    private Vector3Int interactPosition; // the tile that the player is currently trying to interact with
    private Vector2 facingDirection; // used to find correct direction of tile to interact with.
    public bool inventoryOpen; // used to turn off other mouse clicks while the inventory is open
    private AudioSource steps;
    private Animator animator;
    public Inventory inventory;

    // EFFECTS: Instantiate the inventory collection
    // MODIFIES: this, Inventory
    private void Awake() {
        inventory = new Inventory(16);
        inventoryOpen = false;
    }

    // Start is called before the first frame update
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        steps = GetComponent<AudioSource>();
    }

    // This is called whenever the PlayerInput component on Player detects input
    // EFFECTS: Sets facing direction and plays appropriate walking animation
    // MODIFIES: this, animator
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

    // EFFECTS: Sets the direction the player is facing for determining the tile to be interacted with
    // MODIFIES: this
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

    // EFFECTS: (temp) Tilles the ground
    // MODIFIES: tileManager
    private void OnUseItem(){ // for now this just plows the ground
        if (!inventoryOpen) {
            if(GameManager.singleton.tileManager.IsInteractable(interactPosition)) {
                GameManager.singleton.tileManager.SetInteracted(interactPosition);
            }
        }
       
    }

    // Update is called once per frame
    private void Update()
    { 
        // EFFECTS: Sets interact position based on current location and facingDirection, sets the tile to highlighted
        // MODIFIES: this, TileManager
        {
            interactPosition = new Vector3Int(Mathf.RoundToInt(transform.position.x + 0.5f*facingDirection.x), Mathf.RoundToInt(transform.position.y + 0.5f*facingDirection.y), 0);
            GameManager.singleton.tileManager.SetHightlighted(interactPosition);
        }
    }

    // EFFECTS: Sets the location of the player at 60fps
    private void FixedUpdate() {
        rb.MovePosition(rb.position + movement * speed * Time.fixedDeltaTime);
    }
    
    public void LoadData(GameData data)
    {
        this.inventory.slots = data.inventory;
    }

    public void SaveData(ref GameData data)
    {
        data.inventory = this.inventory.slots;
    }
}
