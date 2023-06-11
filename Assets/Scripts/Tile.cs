using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Tile : MonoBehaviour
{
    private bool isGemCollected = false;
    private Vector3 dimensions;
    [SerializeField] private Transform gemSpawnPos;

    private void Awake()
    {
        dimensions = transform.GetComponent<Collider>().bounds.size;
    }

    private void Start()
    {
        SpawnRandomGem();
    }

    public void SpawnRandomGem()
    {
        GemSpawner.instance.SpawnRandomGem(this);
    }

    public Vector3 Dimensions
    {
        get { return dimensions; }
        set { dimensions = value; }
    }

    public Transform GemSpawnPos
    {
        get { return gemSpawnPos; }
    }

    public bool IsGemCollected
    {
        get { return isGemCollected; }
        set { isGemCollected = value; }
    }

}
