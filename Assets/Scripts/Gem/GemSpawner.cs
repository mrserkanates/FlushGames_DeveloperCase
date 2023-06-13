using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemSpawner : MonoBehaviour
{
    public static GemSpawner instance;

    [SerializeField] private List<GemType> gemTypeList; // scriptable object list of the gem types

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void SpawnRandomGem(Tile tile)
    {
        // spawns a random gem on the tile
        int randomIndex = Random.Range(0, gemTypeList.Count);
        GemType randomGemType = gemTypeList[randomIndex];
        GameObject gemObject = Instantiate(randomGemType.prefab, tile.GemSpawnPos.position,
            Quaternion.identity, tile.transform);
        if (gemObject.TryGetComponent<Gem>(out Gem gem)){
            // initialize variables of the gem
            gem.Name = randomGemType.name;
            gem.InitialSalePrice = randomGemType.initialSalePrice;
            gem.Icon = randomGemType.icon;
        }
        
    }
}
