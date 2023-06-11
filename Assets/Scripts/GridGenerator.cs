using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    [SerializeField] private int rowCount;
    [SerializeField] private int colCount;

    [Range(0, 3)]
    [SerializeField] private float gridIntervalHorz;
    [Range(0, 3)]
    [SerializeField] private float gridIntervalVert;

    [SerializeField] private float tileSpawnHeight;
    [SerializeField] private Tile tilePrefab;

    private void Start()
    {
        GenerateGrids();
    }

    private void GenerateGrids()
    {
        if (rowCount < 1 || colCount < 1)
            return;

        int currentRowIndex = -(rowCount / 2);
        int currentColIndex = -(colCount / 2);

        for (int rowIndex = 0 ; rowIndex < rowCount; rowIndex++)
        {
            for (int colIndex = 0; colIndex < colCount; colIndex++)
            {
                float spawnPosX = currentRowIndex * ((rowCount / 2.0f) + gridIntervalHorz);
                float spawnPosZ = currentColIndex * ((colCount / 2.0f) + gridIntervalVert);
                Instantiate(tilePrefab, new Vector3(spawnPosX, transform.position.y + tileSpawnHeight,
                    spawnPosZ), Quaternion.identity, transform);
                currentColIndex++;
            }
            currentColIndex = -(colCount / 2);
            currentRowIndex++;
        }
    }

    /*private void OnValidate()
    {

    }*/
}
