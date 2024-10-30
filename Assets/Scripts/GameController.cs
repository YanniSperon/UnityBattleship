using UnityEngine;

public class GameController : MonoBehaviour {
    private Player player1;
    private Player player2;
    private GridManager gridManager;
    private ShipManager shipManager;
    private PegManager pegManager;
    private Player player;
    Player opponent;

    BattleshipGrid currentGrid;
    // private string gameState = "PlacingPieces";

    void panCameraTo(BattleshipGrid grid) {
        for(int i = 0; i < 10; i++){
            
        }
        // lerp to this position this every tick for 2 seconds total
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
        gridManager = transform.Find("Spawners").GetComponent<GridManager>();
        shipManager = transform.Find("Spawners").GetComponent<ShipManager>();
        pegManager = transform.Find("Spawners").GetComponent<PegManager>();

        BattleshipGrid p1TopGrid = gridManager.createGrid("TopGrid", new Vector3(0,0,30));
        BattleshipGrid p1BottomGrid = gridManager.createGrid("BottomGrid", new Vector3(0,0,0));
        Ship[] p1Ships = shipManager.createShips(p1BottomGrid);
        player1 = new Player(p1Ships, p1TopGrid, p1BottomGrid, 1);

        BattleshipGrid p2TopGrid = gridManager.createGrid("TopGrid", new Vector3(50,0,30));
        BattleshipGrid p2BottomGrid = gridManager.createGrid("BottomGrid", new Vector3(50,0,0));
        Ship[] p2Ships = shipManager.createShips(p2BottomGrid);
        player2 = new Player(p2Ships, p2TopGrid, p2BottomGrid, 2);
        
        player = player1;
        opponent = player2;
        currentGrid = p1BottomGrid;
        panCameraTo(currentGrid);
    }

    void swapTurns(){
        player = player == player1 ? player2 : player1;
        opponent = player == player2 ? player1 : player2;
    }

    void Update() {

        if (Input.GetKeyDown(KeyCode.S)) {
            currentGrid = currentGrid == player.topGrid ? player.bottomGrid : player.topGrid;
            panCameraTo(currentGrid);
            Debug.Log(player.bottomGrid.isGameOver());
            Debug.Log(opponent.bottomGrid.isGameOver());
        }

        player.Update();

        if(player.state == "PlacedPiece"){
            player.numShipsPlaced++;

            if(player.numShipsPlaced < 1){
                player.shipBeingPlaced = player.ships[player.numShipsPlaced];
                player.shipBeingPlaced.attemptMove(0, 0, 0);

                player.state = "PlacingPiece";
            } else {
                player.state = "DoneSettingUp";

                swapTurns();
                panCameraTo(player.bottomGrid);
            }
        }

        if(player.state == "DoneSettingUp"){
            panCameraTo(player.topGrid);
            player.takePeg(pegManager.createPeg(player.topGrid, opponent.bottomGrid));
        }
        else if(player.state == "PlacedPeg"){
            swapTurns();
            panCameraTo(player.topGrid);
            player.takePeg(pegManager.createPeg(player.topGrid, opponent.bottomGrid));
        }


        if(player.state != "PlacingPiece" && player.bottomGrid.isGameOver()){
            print(player.playerNum + " won");
            player.state = "GameOver";
            opponent.state = "GameOver";
        } else if(opponent.state != "PlacingPiece" && opponent.bottomGrid.isGameOver()){
            print(player.playerNum + " won");
            player.state = "GameOver";
            opponent.state = "GameOver";
        }
    } 
}


