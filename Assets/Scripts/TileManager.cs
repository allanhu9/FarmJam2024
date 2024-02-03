using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour
{

    [SerializeField] private Tilemap interactableMap;
    [SerializeField] private Tilemap highlightMap;
    [SerializeField] private Tile hiddenInteractableTile;
    [SerializeField] private Tile borderTile;
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
    // Update is called once per frame
    void Update()
    {
        
    }
}
