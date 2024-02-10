using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TileUnlocker : MonoBehaviour, IPickupEffect
{
    [SerializeField] private List<TileBase> unlocksTiles = new();
    public void OnPickup(GameObject go)
    {
        var PlayerMover = go.GetComponent<KeyboardMoverByTile>();
        foreach (var tile in unlocksTiles)
        {
            Debug.Log("ASDASDASDASDASDSAD");
            PlayerMover.AllowedTiles.AddTile(tile);
        }
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            OnPickup(other.gameObject);
        }
    }
}
