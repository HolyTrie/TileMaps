using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class DestroyableTiles : MonoBehaviour
{
    /*
        Wrapper class to manage destroyable tiles and their replacements, just like AllowedTiles.
    */
    [SerializeField] private List<TileBase> destroyableTiles = new();
    [SerializeField] private List<TileBase> replacementTiles = new();
    public bool ContainsDestroyable(TileBase tile)
    {
        return destroyableTiles.Contains(tile);
    }
    public bool ContainsReplacement(TileBase tile)
    {
        return replacementTiles.Contains(tile);
    }

    public void AddDestroyable(TileBase tile)
    {
        destroyableTiles.Add(tile);
    }

    public void AddDestroyables(IList<TileBase> tiles)
    {
        foreach(var tile in tiles)
        {
            destroyableTiles.Add(tile);
        }
    }
    public void AddReplacement(TileBase tile)
    {
        replacementTiles.Add(tile);
    }
    public void AddReplacements(IList<TileBase> tiles)
    {
        foreach(var tile in tiles)
        {
            replacementTiles.Add(tile);
        }
    }

    public void Add(IList<TileBase> destroyables,IList<TileBase> replacements)
    {
        AddReplacements(replacements);
        AddDestroyables(destroyables);
    }

    public void RemoveDestroyable(TileBase tile)
    {
        destroyableTiles.Remove(tile);
    }
    public void RemoveReplacement(TileBase tile)
    {
        replacementTiles.Remove(tile);
    }

    public List<TileBase> GetDestroyables() { return destroyableTiles; }
    public List<TileBase> GetReplacements() { return replacementTiles; }

    public TileBase GetRandomReplacement()
    {
        int index = Random.Range(0,replacementTiles.Count);
        TileBase tile = replacementTiles[index];
        return tile;
    }
}
