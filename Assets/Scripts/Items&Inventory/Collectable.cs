using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// REQUIRES: Item is also on this GameObject
[RequireComponent(typeof(Item))]
public class Collectable : MonoBehaviour
{
    // EFFECTS: Puts the Item Object that is on this script in the player's inventory
    private void OnTriggerEnter2D(Collider2D other) {
        Player player = other.GetComponent<Player>();
        if (player) {
            player.inventory.Add(GetComponent<Item>());
            Destroy(gameObject);
        }
    }
}
