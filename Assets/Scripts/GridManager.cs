using UnityEngine;
using System.Collections.Generic;

public class GridManager : MonoBehaviour {
    const int gridWidth = 10;
    const int gridHeight = 10;
    const float xScale = 2;
    const float zScale = 2; 
    public GameObject cellPrefab;

    public BattleshipGrid createGrid() {
        Vector3 position = new Vector3(0,0,100);
        GameObject[,] cells = createCells(position, "localplayer");
        BattleshipGrid grid = new BattleshipGrid(position, cells, gridWidth, gridHeight, zScale, xScale);

        return grid;
    }

    GameObject[,] createCells(Vector3 startPosition, string playerName){
        GameObject cellHolder = new GameObject("cells-"+playerName);
        GameObject[,] cells = new GameObject[gridWidth, gridHeight];

        for (int x = 0; x < gridWidth; x++) {
            for (int y = 0; y < gridHeight; y++) {
                Vector3 cellPosition = startPosition + new Vector3(x * xScale, 0, y * -zScale);
                GameObject cell = Instantiate(cellPrefab, cellPosition, Quaternion.identity);
                cell.transform.parent = cellHolder.transform;
                cells[x, y] = cell;
            }
        }

        return cells;
    }
}
