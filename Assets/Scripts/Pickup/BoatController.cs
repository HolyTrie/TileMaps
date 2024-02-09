using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class BoatController : KeyboardMover
{
    // Start is called before the first frame update
    [SerializeField] AllowedTiles allowedTiles = null; // tiles that are moveable for boat
    [SerializeField] Tilemap tilemap = null;

    private Vector3 player_return_position;

    private GameObject player;

    bool MoveShip = false;

    void Start()
    {
        Debug.Log("Ship is here!");
    }
    private TileBase TileOnPosition(Vector3 worldPosition)
    {
        Vector3Int cellPosition = tilemap.WorldToCell(worldPosition);
        return tilemap.GetTile(cellPosition);
    }
    void Update()
    {
        if (MoveShip)
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
            if (Input.GetKeyDown(KeyCode.E) && !allowedTiles.Contains(tileOnNewPosition)) // pressed E and reached near shore
            {
                // enable player again after he presses E, should also check that player is on valid ground, or he'll drown
                player.transform.parent = null;
                player.transform.position = player_return_position;
                player.GetComponent<KeyboardMoverByTile>().enabled = true;
                MoveShip = false;
            }
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            player = other.gameObject;
            player_return_position = other.transform.position;
            Debug.Log("Player touches boat");
            player.transform.parent = this.transform;
            player.transform.position = new Vector3(this.transform.position.x, this.transform.position.y, 0);
            player.GetComponent<KeyboardMoverByTile>().enabled = false; // now we tranisiton the moving to the ship.
            MoveShip = true;
        }
    }

}
