using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Item))]
public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        Player player = other.GetComponent<Player>();
        if (player) {
            Item item = GetComponent<Item>();
            if (item) {
                player.inventory.Add(item);
            }
            Destroy(gameObject);
        }
    }
}
