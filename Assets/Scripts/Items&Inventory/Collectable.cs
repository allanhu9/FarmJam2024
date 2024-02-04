using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(Item))]
public class Collectable : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other) {
        Player player = other.GetComponent<Player>();
        if (player) {
            player.inventory.Add(GetComponent<Item>());
            Destroy(gameObject);
        }
    }
}
