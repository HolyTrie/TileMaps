using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

/**
 * This component allows the player to move by clicking the arrow keys,
 * but only if the new position is on an allowed tile.
 */
public class KeyboardMoverByTile : KeyboardMover
{
    [SerializeField] Tilemap tilemap = null;
    [SerializeField] AllowedTiles allowedTiles = null;
    [SerializeField] DestroyableTiles destroyableTiles = null;
    public AllowedTiles AllowedTiles => allowedTiles;
    public DestroyableTiles DestroyableTiles => destroyableTiles;
    private TilemapGraph tilemapGraph = null;
    private TileBase TileOnPosition(Vector3 worldPosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }

    private void Start()
    {
        tilemapGraph = new TilemapGraph(tilemap, allowedTiles.Get());
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
        if(DestroyableTiles != null)
        {
            if (DestroyableTiles.ContainsDestroyable(tileOnNewPosition))
            {
                tilemap.SetTile(tileNewPosition, DestroyableTiles.GetRandomReplacement());

            }
        }
    }
    public void TrySetPlayerPosition(int gridSize, int tileThreshold)
    {
        int rx = Random.Range(0,gridSize);
        int ry = Random.Range(0,gridSize);
        Vector3Int currNode = new();
        bool isValidPlacement = false;
        while(!isValidPlacement)
        {
            Debug.Log("Trying to find position for player...");
            currNode = new Vector3Int(rx,ry,0);
            while(!allowedTiles.Contains(TileOnPosition(currNode)))
            {
                rx = Random.Range(0,gridSize);
                ry = Random.Range(0,gridSize);
                currNode = new Vector3Int(rx,ry,0);
            }
            isValidPlacement = NodeHasXNeighbours(tilemapGraph,currNode,allowedTiles,tileThreshold);
        }
        Debug.Log("Found good start position");
        transform.position = currNode;
    }

    

    private bool NodeHasXNeighbours(
            IGraph<Vector3Int> graph, 
            Vector3Int startNode,
            AllowedTiles allowedTiles,
            int X = -1,
            int maxiterations=1000)
    {
        /*
            adding a tile mask to a generic solution was impossible with the current BFS generics so we made it non generic :(
        */
        var ignoreThreshold = X == -1;
        var threshold = X;
        Queue<Vector3Int> openQueue = new();
        HashSet<Vector3Int> openSet = new();
        Dictionary<Vector3Int, Vector3Int> previous = new();
        openQueue.Enqueue(startNode);
        openSet.Add(startNode);
        int count = 0;
        int i;
        Vector3Int curr;
        for (i = 0; i < maxiterations; ++i) 
        { // After maxiterations, stop and return an empty path
            if (openQueue.Count == 0) 
            {
                break;
            } 
            if(count >= threshold && !ignoreThreshold)
            {
                return true;
            }
            else 
            {
                curr = openQueue.Dequeue();
                count++;
                foreach (var neighbor in graph.Neighbors(curr)) 
                {
                    if (openSet.Contains(neighbor) || !allowedTiles.Contains(tilemapGraph.Tilemap.GetTile(neighbor)))
                        continue;
                    openQueue.Enqueue(neighbor);
                    openSet.Add(neighbor);
                    previous[neighbor] = curr;
                }
            }
        }
        return false;
    }

}
