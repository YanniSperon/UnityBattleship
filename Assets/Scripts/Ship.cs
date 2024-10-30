using UnityEngine;
using System;

public class Ship {
    public Vector2Int position;
    public int angle;
    public int length;
    public int shipNum;
    public GameObject shipModel;
    public GameObject shipMesh;
    public string shipName; 

    private BattleshipGrid grid;

    public Ship(string shipName, int shipNum, GameObject ship, int length, BattleshipGrid grid) {
        this.shipName = shipName;
        this.shipNum = shipNum;
        this.position = new Vector2Int(0, 0);
        this.angle = 0;
        this.length = length;
        this.grid = grid;

        this.shipModel = ship;
    }

    public void rotate(int newAngle) {
        this.angle = newAngle;

        Vector3 currentRotation = this.shipModel.transform.eulerAngles;
        Vector3 newRotation = new Vector3(currentRotation.x, this.angle+90, currentRotation.z);
        this.shipModel.transform.eulerAngles = newRotation;
    }

    public void scale(GameObject cell) {
        this.shipMesh = this.shipModel.transform.Find("default").gameObject;
        // Bounds cellBounds = cell.GetComponent<Renderer>().bounds;
        // Bounds shipBounds = shipMesh.GetComponent<Renderer>().bounds;

        // Vector3 scale = new Vector3(
        //     (this.length * cellBounds.size.x) / shipBounds.size.x,
        //     24,
        //     (cellBounds.size.z) / shipBounds.size.z
        // );

        // shipMesh.transform.localScale = Vector3.Scale(shipMesh.transform.localScale, scale);
    }

    public bool place(){
        bool allCellsFree = true;
        
        this.forEachCellFrom(this.position.x, this.position.y, this.angle, (currX, currY) => {
            allCellsFree = allCellsFree && (!grid.isCellOccupied(currX, currY));
        });

        if(allCellsFree){
            this.forEachCellFrom(this.position.x, this.position.y, this.angle, (currX, currY) => {
                grid.setState(currX, currY, this.shipNum);
            });
        }

        return allCellsFree;
    }

    public bool attemptMove(int newX, int newY, int newAngle){
        bool shipCanMove = true;
        
        this.forEachCellFrom(newX, newY, newAngle, (cellX,cellY) => {
            shipCanMove = shipCanMove && grid.isInBounds(cellX, cellY); //&& (!grid.isCellOccupied(cellX, cellY));
        });

        if(shipCanMove) {
            if(newAngle != this.angle){
                this.rotate(newAngle);
            }
            this.move(newX, newY);
        }

        return shipCanMove;
    }

    public delegate void CollisionCallback(int x, int y);

    // ----------------------------------------------------------------------------------------------------------

    // calls callback on each ship cell position starting from x,y 
    private void forEachCellFrom(int x, int y, int angle, CollisionCallback callback) {
        float radians = angle * (Mathf.PI / 180);
        float directionX = Mathf.Cos(radians);
        float directionY = Mathf.Sin(radians);

        for (int i = 0; i < this.length; i++) {
            int currX = (int)Math.Round(x + directionX * i);
            int currY = (int)Math.Round(y + directionY * i);

            callback(currX, currY);
        }      
    }

    void move(int x, int y) {
        this.position.x = x;
        this.position.y = y;
        this.shipModel.transform.position = this.getPositionOnBoard();
    }

    // gets world position for ith ship given x,y grid position
    Vector3 getPositionOnBoard() {
        GameObject cellModel = this.grid.cellModels[this.position.x,this.position.y];
        Vector3 shipSize = this.shipMesh.transform.localScale;
        Vector3 posOnBoard = cellModel.transform.position + new Vector3(0, 0, 0);//new Vector3((shipSize.x-xScale)/2, 0, -(shipSize.z-zScale)/2);

        return posOnBoard;
    }
}
