using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class TileDestroyer : MonoBehaviour,IPickupEffect {
    [SerializeField] private List<TileBase> destroyableTiles = new();
    [SerializeField] private List<TileBase> replacementTiles = new();
    public void OnPickup(GameObject go)
    {
        var PlayerMover = go.GetComponent<KeyboardMoverByTile>();
        PlayerMover.DestroyableTiles.Add(destroyableTiles,replacementTiles);
    }
    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player"))
        {
            OnPickup(other.gameObject);
        }
    }
}
