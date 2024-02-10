using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component allows the player to move by clicking the arrow keys,
 * but only if the new position is on an allowed tile.
 */
public class KeyboardMoverByTile : KeyboardMover
{
    [SerializeField] Tilemap tilemap = null;
    private TileBase TileToDestroy = null;
    private TileBase TileToPut = null;
    [SerializeField] AllowedTiles allowedTiles = null;
    public AllowedTiles AllowedTiles => allowedTiles;

    private TileBase TileOnPosition(Vector3 worldPosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }

    void Update()
    {
        Vector3 newPosition = NewPosition();
        TileBase tileOnNewPosition = TileOnPosition(newPosition);
        if (allowedTiles.Contains(tileOnNewPosition))
        {
            transform.position = newPosition;
        }
        else
        {
            Debug.Log("You cannot walk on " + tileOnNewPosition + "!");
        }

        //this part of code responsible to switch the tiles//
        Vector3Int tileNewPosition = tilemap.WorldToCell(newPosition); // converts wolrd position to integer grid position
        // Check if the tile at the player's position matches the tile to change
        if (tileOnNewPosition == TileToDestroy)
        {
            // Change the tile to the new tile
            tilemap.SetTile(tileNewPosition, TileToPut);

        }
    }

    public void UpdateTileToDestroy(TileBase other) { this.TileToDestroy = other; }
    public void UpdateTileToPut(TileBase other) { this.TileToPut = other; }


}
