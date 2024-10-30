using UnityEngine;

public class GameController : MonoBehaviour {
    private Player player;
    private GridManager gridManager;
    private ShipManager shipManager;
    // private string gameState = "PlacingPieces";

    void panCameraTo(BattleshipGrid grid) {
        Vector3 centerPosition = grid.gridPosition
        + new Vector3(
            ((grid.gridWidth - 1) * grid.xScale) / 2,
            0, 
            ((grid.gridHeight - 1) * -grid.zScale) / 2
        );

        Camera.main.transform.position = new Vector3(centerPosition.x, 20.0f, centerPosition.z);
        Camera.main.transform.LookAt(centerPosition);
    }

    void Start() {
        gridManager = transform.Find("GridManager").GetComponent<GridManager>();
        shipManager = transform.Find("ShipManager").GetComponent<ShipManager>();
        BattleshipGrid grid = gridManager.createGrid();
        Ship[] ships = shipManager.createShips(grid);

        player = new Player(ships);
        panCameraTo(grid);
    }

    void Update() {
        player.Update();
        
        if(player.state == "PlacedPiece"){
            player.numShipsPlaced++;

            if(player.numShipsPlaced < 5){
                player.shipBeingPlaced = player.ships[player.numShipsPlaced];
                player.shipBeingPlaced.attemptMove(0, 0, 0);

                player.state = "PlacingPiece";
            } else {
                print("done placing");
                player.state = "WaitingForTurn";
            }
        }
    } 
}


