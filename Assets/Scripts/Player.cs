using UnityEngine;

public class Player {
    public Ship[] ships;
    public Ship shipBeingPlaced;
    public int numShipsPlaced = 0;
    public string state = "PlacingPiece";

    public Player(Ship[] ships) {
        this.ships = ships;
        shipBeingPlaced = ships[0];
        shipBeingPlaced.attemptMove(0, 0, 0);
    }

    public void Update() {
        if(state == "PlacingPiece"){
            checkForMovement();

            if (Input.GetKeyDown(KeyCode.Return)) { 
                bool wasSuccessful = shipBeingPlaced.place();

                if(wasSuccessful)
                    state = "PlacedPiece";
            } 
        }
    }

    void checkForMovement(){
        int newX = shipBeingPlaced.position.x;
        int newY = shipBeingPlaced.position.y;
        int newAngle = shipBeingPlaced.angle;

        if (Input.GetKeyDown(KeyCode.RightArrow)){ newX++; } 
        if (Input.GetKeyDown(KeyCode.LeftArrow)){ newX--; }
        if (Input.GetKeyDown(KeyCode.DownArrow)){ newY++; }
        if (Input.GetKeyDown(KeyCode.UpArrow)){ newY--; }
        if (Input.GetKeyDown(KeyCode.R)) { newAngle = (newAngle + 90)%360;}

        bool attemptedToMove = newX != shipBeingPlaced.position.x || newY != shipBeingPlaced.position.y;
        bool attemptedRotation = newAngle != shipBeingPlaced.angle;

        // exit out if we're not actually moving anywhere
        if(!attemptedRotation && !attemptedToMove){ return; }
        bool moved = shipBeingPlaced.attemptMove(newX, newY, newAngle);

        // if we tried rotating and we weren't able to rotate, keep rotating untill successful or we tried all our rotations
        if(!moved && attemptedRotation) {
            int i = 0;
            while(i<3 && !moved){
                i++;
                newAngle += 90;
                newAngle %= 360;
                moved = shipBeingPlaced.attemptMove(newX, newY, newAngle);
            }
        }
    }
}