using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileDestroyer : MonoBehaviour,IPickupEffect {
    [SerializeField] private List<TileBase> destroyableTiles = new();
    [SerializeField] private List<TileBase> replacementTiles = new();
    public void OnPickup(GameObject go)
    {
        var playerMover = go.GetComponent<KeyboardMoverByTile>();
        playerMover.DestroyableTiles.Add(destroyableTiles,replacementTiles);
        gameObject.SetActive(false);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            OnPickup(other.gameObject);
        }
    }
}
