using UnityEngine;

public class ShipManager : MonoBehaviour {
    public GameObject[] shipPrefabs;

    public Ship[] createShips(BattleshipGrid grid){
        Ship[] ships = new Ship[5];
        ships[0] = createShip("Aircraft Carrier", 0, 5, grid);
        ships[1] = createShip("Battleship", 1, 4, grid);
        ships[2] = createShip("Cruiser", 2, 3, grid);
        ships[3] = createShip("Destroyer", 3, 2, grid);
        ships[4] = createShip("Submarine", 4, 3, grid);

        return ships;
    }
    
    Ship createShip(string shipName, int shipNum, int length, BattleshipGrid grid) {
        GameObject shipModel = createModel(shipPrefabs[shipNum]);
        Ship ship = new Ship(shipName, shipNum, shipModel, length, grid);
        ship.scale(grid.cellModels[0,0]);
        
        return ship;
    }

    GameObject createModel(GameObject shipPrefab) {
        GameObject shipHolder = GameObject.Find("ShipModels") ?? new GameObject("ShipModels");
        GameObject shipModel = Instantiate(shipPrefab);
        shipModel.transform.parent = shipHolder.transform;
        shipModel.transform.Rotate(180, -90, 0);
        return shipModel;
    }

    void Start(){   
    }
}
