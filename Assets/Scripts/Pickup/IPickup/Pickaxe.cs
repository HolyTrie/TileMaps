using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Pickaxe : MonoBehaviour, IPickupEffect
{
    [SerializeField] private List<TileBase> DestroyableTiles = new List<TileBase>();
    [SerializeField] private List<TileBase> ReplacementTiles = new List<TileBase>();

    private int zero = 0;
    public void OnPickup(GameObject other)
    {
        KeyboardMoverByTile player = other.gameObject.GetComponent<KeyboardMoverByTile>();
        player.UpdateTileToDestroy(DestroyableTiles[zero]);  // this can be expanded to multiple tiles to change.. for now only one
        player.UpdateTileToPut(ReplacementTiles[zero]);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // just in case
        {
            OnPickup(other.gameObject);
        }
    }
}
