using UnityEngine;

public class BattleshipGrid {
    public int gridWidth;
    public int gridHeight;
    public float xScale;
    public float zScale; 
    public GameObject[,] cellModels;
    public int[,] cellStates;
    public Vector3 gridPosition;

    /*
        cell states:
        -4 = sunk
        -3 = hit
        -2 = miss 
        -1 = free
        0-4 = which is there
    */

    public bool isCellOccupied(int x, int y){
        bool isOccupied = cellStates[x,y] != -1;
        // Debug.Log(x + " " + y + " " + isOccupied);
        return (isOccupied);
    }

    public bool isInBounds(int x, int y){
        return x < gridHeight && y < gridHeight && x>=0 && y>=0;
    }

    public void setState(int x, int y, int newState){
        cellStates[x,y] = newState;
    }

    public BattleshipGrid(Vector3 gridPosition, GameObject[,] cellModels, int gridWidth, int gridHeight, float zScale, float xScale) {
        this.cellModels = cellModels;
        this.gridWidth = gridWidth;
        this.gridHeight = gridHeight;
        this.zScale = zScale;
        this.xScale = xScale;
        this.gridPosition = gridPosition;

        initCellStates();
    }

    private void initCellStates(){
        cellStates = new int[gridWidth, gridHeight];

        for(int i = 0; i < gridWidth; i++){
            for(int j=0; j < gridHeight; j++){
                cellStates[i, j] = -1;
            }
        }
    }
}

  
