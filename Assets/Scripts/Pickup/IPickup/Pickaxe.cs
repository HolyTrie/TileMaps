using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Pickaxe : MonoBehaviour,IPickupEffect {
    [SerializeField] private List<TileBase> DestroyableTiles = new List<TileBase>();
    [SerializeField] private List<TileBase> ReplacementTiles = new List<TileBase>();
    public void OnPickup(GameObject go)
    {
        throw new System.NotImplementedException();
        //player.add(destroyableTiles,Replacementtiles)
        // -> add logic into mover that allows destroying and replacing by using those list and a random for replacement.
    }
}
