using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.Tilemaps;

public class TileManager : MonoBehaviour, DataPersistable
{
    // Each map is for a different level of interaction, saving these tile maps is probably most important
    [SerializeField] private Tilemap interactableMap; // marks the tiles where you can interact with the world
    [SerializeField] private Tilemap interactedMap; // where the interactions actually appear (tilled ground, ex.)
    [SerializeField] private Tilemap highlightMap; // empty map for the highlight square which shows what you will interact with
    [SerializeField] private Tile hiddenInteractableTile; // an invisible tile to replace all the interactable markers (arrows) with on start
    [SerializeField] private Tile hightlightTile; // a hollow square for highlighting
    [SerializeField] private Tile interactedTile; // temp, this is just tilled ground rn, but will need to add a system based on tools later
    private Vector3Int highlightedPosition = new Vector3Int(0, 0, 0); // the position that is to be highlighted
    // Start is called before the first frame update
    void Start()
    {
        InitializeInteractableTiles();
    }
    
    // EFFECTS: returns true if the tile at position is interactable
    public bool IsInteractable(Vector3Int position) {
        TileBase tile = interactableMap.GetTile(position);
        if(tile != null) 
            return tile.name == "Interactable";
        return false;
    }
    // EFFECTS: Sets all the tiles invisible
    public void InitializeInteractableTiles()
    {
        foreach (var position in interactableMap.cellBounds.allPositionsWithin)
        { // set all interactable tiles to be invisible
            TileBase tile = interactableMap.GetTile(position);
            if (tile != null && tile.name == "Interactable_Visible")
                interactableMap.SetTile(position, hiddenInteractableTile);
        }
    }
    // EFFECTS: (temp) tills the ground at position, in future will probably make this (position, tile) -> (changes the ground at position to tile)
    public void SetInteracted(Vector3Int position) {
        interactedMap.SetTile(position, interactedTile);
    }

    // EFFECTS: highlights the given position
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

    public void LoadData(GameData data)
    {
        highlightMap = GameObject.FindWithTag("HighlightedTile").GetComponent<Tilemap>();
        interactableMap = GameObject.FindWithTag("InteractableTiles").GetComponent<Tilemap>();
        interactedMap = GameObject.FindWithTag("InteractedTiles").GetComponent<Tilemap>();
        foreach(SavedTile tile in data.interactableMap)
        {
            interactableMap.SetTile(tile.pos, tile.mapTile);
        }
        foreach (SavedTile tile in data.interactedMap)
        {
            interactedMap.SetTile(tile.pos, tile.mapTile);
        }
        InitializeInteractableTiles();

    }

    public void SaveData(ref GameData data)
    {
        data.interactableMap = getTilesFromMap(interactableMap);
        data.interactedMap = getTilesFromMap(interactedMap);
    }

    // EFFECTS: Returns all the tiles of a map
    private List<SavedTile> getTilesFromMap(Tilemap map) {
        List<SavedTile> tiles = new List<SavedTile>();
        foreach (Vector3Int pos in map.cellBounds.allPositionsWithin)
        {
            if (map.HasTile(pos))
            {
                Tile mapTile = map.GetTile<Tile>(pos);
                SavedTile tile = new SavedTile(pos, mapTile);
                tiles.Add(tile);
            }
        }
        return tiles;
    }
}
