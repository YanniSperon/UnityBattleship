using UnityEngine;

public class BattleshipGrid {
    public int gridWidth;
    public int gridHeight;
    public float xScale;
    public float zScale; 
    public GameObject[,] cellModels;
    public int[,] cellStates;
    public Vector3 gridPosition;
    public int[,] pegStates;

    /*
        cell states:
        -4 = sunk
        -3 = hit
        -2 = miss 
        -1 = free
        0-4 = which is there
    */

    
    /*
    peg states: 
    -1: free
    1: red
    0: white

    */

    public bool isCellOccupied(int x, int y){
        return (cellStates[x,y] != -1);
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
        initPegStates();
    }

    public bool isGameOver(){
        bool isGameOver = true;
        for(int i = 0; i < gridWidth; i++){
            for(int j=0; j < gridHeight; j++){
                if(cellStates[i, j] >= 0){
                    isGameOver = false;
                }
            }
        }

        return isGameOver;
    }

    public void printGrid(){
        for(int i = 0; i < gridWidth; i++){
            string row = "";
            for(int j=0; j < gridHeight; j++){
                row += (", " + cellStates[i,j]); 
            }
            Debug.Log(row);
        }
    }

    private void initPegStates(){
        pegStates = new int[gridWidth, gridHeight];

        for(int i = 0; i < gridWidth; i++){
            for(int j=0; j < gridHeight; j++){
                pegStates[i, j] = -1;
            }
        }
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

  
