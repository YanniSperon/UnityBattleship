using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour {
    const int gridWidth = 10;
    const int gridHeight = 10;
    const float xScale = 2;
    const float zScale = 2; 
    public GameObject cellPrefab;

    public BattleshipGrid createGrid(string gridName, Vector3 position) {
        GameObject[,] cells = createCells(gridName, position);
        BattleshipGrid grid = new BattleshipGrid(position, cells, gridWidth, gridHeight, zScale, xScale);

        return grid;
    }

    GameObject[,] createCells(string gridName, Vector3 startPosition){
        GameObject cellHolder = new GameObject("cells-"+gridName);
        GameObject[,] cells = new GameObject[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++) {
            for (int y = 0; y < gridHeight; y++) {
                Vector3 cellPosition = new Vector3(x * xScale, 0, y * -zScale);
                GameObject cell = Instantiate(cellPrefab, cellPosition, Quaternion.identity);
                cell.transform.parent = cellHolder.transform;
                cells[x, y] = cell;
            }
        }

        cellHolder.transform.position = startPosition;

        return cells;
    }
}
