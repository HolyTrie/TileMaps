using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component just keeps a list of allowed tiles.
 * Such a list is used both for pathfinding and for movement.
 */
public class AllowedTiles : MonoBehaviour
{
    [SerializeField] List<TileBase> allowedTiles = new List<TileBase>();

    public bool Contains(TileBase tile)
    {
        return allowedTiles.Contains(tile);
    }

    /*adds a new tile to the pallete in case something happens*/
    public void AddTile(TileBase tile)
    {
        allowedTiles.Add(tile);
    }

    /*removes a tile from the pallete*/

    public void RemoveTile(TileBase tile)
    {
        allowedTiles.Remove(tile);
    }

    public List<TileBase> Get() { return allowedTiles; }
}
