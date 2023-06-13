using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Tile : MonoBehaviour
{
    [SerializeField] private Transform gemSpawnPos; // gem spawn position

    private void Start()
    {
        SpawnRandomGem();
    }

    public void SpawnRandomGem()
    {
        // spawns a random gem on the tile
        GemSpawner.instance.SpawnRandomGem(this);
    }

    public Transform GemSpawnPos
    {
        get { return gemSpawnPos; }
    }

}
