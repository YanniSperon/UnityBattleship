using UnityEngine;

public class Peg {
    public Vector2Int position;
    GameObject pegModel;
    BattleshipGrid guessesGrid;
    BattleshipGrid opponentShipGrid;
    Material redMaterial;
    

    public Peg(GameObject pegModel, Material red, BattleshipGrid guessesGrid, BattleshipGrid opponentShipGrid){
        this.pegModel = pegModel;
        position = new Vector2Int(0,0);
        this.guessesGrid = guessesGrid;
        this.opponentShipGrid = opponentShipGrid;
        this.redMaterial = red;
    }

    public void move(int x, int y){
        if(!guessesGrid.isInBounds(x, y)){return;}
        this.position = new Vector2Int(x,y);
        this.pegModel.transform.position = getPositionOnBoard();
    }

    public bool place(){
        bool noPegHere = guessesGrid.pegStates[this.position.x, this.position.y]==-1;

        if(noPegHere){
            // Debug.Log(opponentShipGrid.isCellOccupied(this.position.x, this.position.y));
            // opponentShipGrid.printGrid();
            if(opponentShipGrid.isCellOccupied(this.position.x, this.position.y)){
                pegModel.GetComponent<Renderer>().material = redMaterial;
                guessesGrid.pegStates[this.position.x, this.position.y] = 1;
                opponentShipGrid.cellStates[this.position.x, this.position.y] = -3;
            } else {
                guessesGrid.pegStates[this.position.x, this.position.y] = 0;
            }
        }
        
        return noPegHere;
    }

     // gets world position for peg given x,y grid position
    Vector3 getPositionOnBoard() {
        GameObject cellModel = guessesGrid.cellModels[this.position.x,this.position.y];
        Vector3 posOnBoard = cellModel.transform.position + new Vector3(0,(pegModel.transform.localScale.y/2),0);
        return posOnBoard;
    }
}