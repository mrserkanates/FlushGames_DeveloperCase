using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class GridGenerator : MonoBehaviour
{
    [Header("Row and Columns")]
    [SerializeField] private int rowCount; // row count of the grid
    [SerializeField] private int colCount; // column count of the grid

    [Header("Intervals")]
    [Range(0, 3)]
    [SerializeField] private float gridIntervalHorz; // horizontal space between grids
    [Range(0, 3)]
    [SerializeField] private float gridIntervalVert; // vertical space between grids

    [Header("Offsets")]
    [SerializeField] private float tileSpawnOffsetY; // Y axis offset of tile

    [Space]
    [SerializeField] private Tile tilePrefab;

    private void GenerateGridsOnGUI()
    {
        if (rowCount < 1 || colCount < 1)
            return;

        // destroy all children first
        foreach (Transform child in transform)
        {
            StartCoroutine(Destroy(child.gameObject));
        }

        // this function generates the row and column indexes of the tiles
        // indexes can be negative since this function generates grids at the
        // GridGenerator's position as much as possible
        // for example, 3x3 grid will be start from index -(3/2) = -1
        // then it will decide the positions of tiles using this index, tile scale and grid intervals

        int currentRowIndex = -(rowCount / 2);
        int currentColIndex = -(colCount / 2);

        for (int rowIndex = 0; rowIndex < rowCount; rowIndex++)
        {
            for (int colIndex = 0; colIndex < colCount; colIndex++)
            {           
                GameObject gm = PrefabUtility.InstantiatePrefab(tilePrefab.gameObject) as GameObject;
                float spawnPosX = transform.position.x + currentRowIndex * (gm.transform.localScale.x + gridIntervalHorz);
                float spawnPosZ = transform.position.z + currentColIndex * (gm.transform.localScale.z + gridIntervalVert);
                gm.transform.position = new Vector3(spawnPosX, transform.position.y + tileSpawnOffsetY,
                    spawnPosZ);
                gm.transform.parent = transform;
                currentColIndex++;
            }
            currentColIndex = -(colCount / 2); // reset current column index, go to next row
            currentRowIndex++;
        }
    }

    private IEnumerator Destroy(GameObject go)
    {
        yield return new WaitForSeconds(0.01f); // wait for a while to destroy GameObject at editor.
        DestroyImmediate(go);
    }

    private void OnValidate()
    {
        GenerateGridsOnGUI(); // generate grids at editor
    }
}
