using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * A graph that represents a tilemap, using only the allowed tiles.
 */
public class TilemapGraph: IGraph<Vector3Int> {
    private readonly Tilemap tilemap;
    private readonly List<TileBase> allowedTiles;

    public Tilemap Tilemap => tilemap;
    public List<TileBase> AllowedTiled => allowedTiles;

    public TilemapGraph(Tilemap tilemap, List<TileBase> allowedTiles) {
        this.tilemap = tilemap;
        this.allowedTiles = allowedTiles;
    }

    static Vector3Int[] directions = {
            new(-1, 0, 0),
            new(1, 0, 0),
            new(0, -1, 0),
            new(0, 1, 0),
    };

    public IEnumerable<Vector3Int> Neighbors(Vector3Int node) {
        foreach (var direction in directions) {
            Vector3Int neighborPos = node + direction;
            TileBase neighborTile = tilemap.GetTile(neighborPos);
            if (allowedTiles.Contains(neighborTile))
                yield return neighborPos;
        }
    }
}
