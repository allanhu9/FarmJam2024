using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{

    [SerializeField] private Tilemap interactableMap;
    [SerializeField] private Tilemap interactedMap;
    [SerializeField] private Tilemap highlightMap;
    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile hightlightTile;
    [SerializeField] private Tile interactedTile;
    private Vector3Int highlightedPosition = new Vector3Int(0, 0, 0);
    // Start is called before the first frame update
    void Start()
    {
        foreach(var position in interactableMap.cellBounds.allPositionsWithin) { // set all interactable tiles to be invisible
            TileBase tile = interactableMap.GetTile(position);
            if (tile != null && tile.name == "Interactable_Visible")
                interactableMap.SetTile(position, hiddenInteractableTile);
        }
    }
    
    public bool IsInteractable(Vector3Int position) {
        TileBase tile = interactableMap.GetTile(position);
        if(tile != null) 
            return tile.name == "Interactable";
        return false;
    }

    public void SetInteracted(Vector3Int position) {
        interactedMap.SetTile(position, interactedTile);
    }

    public void SetHightlighted(Vector3Int position) {
        TileBase tile = highlightMap.GetTile(position);
        bool interactable = IsInteractable(position);
        if ((tile == null || tile.name != "HollowTile") && interactable) {
            highlightMap.SetTile(highlightedPosition, null);
            highlightMap.SetTile(position, hightlightTile);
            highlightedPosition = position;
        } else if (!interactable) {
            highlightMap.SetTile(highlightedPosition, null);
        }
            
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
