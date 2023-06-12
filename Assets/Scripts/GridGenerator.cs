using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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

    private void GenerateGridsOnGUI()
    {
        if (rowCount < 1 || colCount < 1)
            return;

        foreach (Transform child in transform)
        {
            StartCoroutine(Destroy(child.gameObject));
        }

        int currentRowIndex = -(rowCount / 2);
        int currentColIndex = -(colCount / 2);

        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            for (int colIndex = 0; colIndex < colCount; colIndex++)
            {
                float spawnPosX = currentRowIndex * ((rowCount / 2.0f) + gridIntervalHorz);
                float spawnPosZ = currentColIndex * ((colCount / 2.0f) + gridIntervalVert);
                GameObject gm = PrefabUtility.InstantiatePrefab(tilePrefab.gameObject) as GameObject;
                gm.transform.position = new Vector3(spawnPosX, transform.position.y + tileSpawnHeight,
                    spawnPosZ);
                gm.transform.parent = transform;
                currentColIndex++;
            }
            currentColIndex = -(colCount / 2);
            currentRowIndex++;
        }
    }

    private IEnumerator Destroy(GameObject go)
    {
        yield return new WaitForSeconds(0.01f);
        DestroyImmediate(go);
    }

    private void OnValidate()
    {
        GenerateGridsOnGUI();
    }
}
