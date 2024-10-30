using System;
using System.Collections.Generic;
using UnityEngine;

public class DataManager : MonoBehaviour
{
    [Flags]
    [Serializable]
    public enum ShipType
    {
        None = 0,
        Destroyer = 1, // 2 space ship
        Submarine = 2, // 3 space ship
        Cruiser = 4, // 3 space ship
        Battleship = 8, // 4 space ship
        Carrier = 16, // 5 space ship
    }
    [Flags]
    [Serializable]
    public enum GridState
    {
        Hit = 32, // Values 1-16 reserved for ship types
        Miss = 64,
    }
    [Flags]
    [Serializable]
    public enum ShipDirection
    {
        Vertical = 128, // Values 1-64 reserves for ship types and grid state
        Horizontal = 256
    }

    private Dictionary<ShipType, int> shipSizes = new Dictionary<ShipType, int>();
    private Dictionary<ShipType, GameObject> shipPrefabs = new Dictionary<ShipType, GameObject>();
    private Dictionary<ShipDirection, Quaternion> shipRotations = new Dictionary<ShipDirection, Quaternion>();
    [Serializable]
    private class ShipPrefabMapKVP
    {
        public ShipType type;
        public GameObject prefab;
    }
    [SerializeField]
    private List<ShipPrefabMapKVP> shipPrefabsList = new List<ShipPrefabMapKVP>();
    [Serializable]
    private class ShipRotationsMapKVP
    {
        public ShipDirection direction;
        public Quaternion quaternion;
    }
    [SerializeField]
    private List<ShipRotationsMapKVP> shipRotationsList = new List<ShipRotationsMapKVP>();

    public int GridWidth = 10;
    public int GridDepth = 10;

    private int[,] PlayerBoard = null;
    private int[,] EnemyBoard = null;

    public Vector3 PlayerBoardTopLeft = Vector3.zero;
    public Vector3 PlayerBoardHorizontalStep = Vector3.zero;
    public Vector3 PlayerBoardVerticalStep = Vector3.zero;

    public Vector3 EnemyBoardTopLeft = Vector3.zero;
    public Vector3 EnemyBoardHorizontalStep = Vector3.zero;
    public Vector3 EnemyBoardVerticalStep = Vector3.zero;

    private void Awake()
    {
        ClearBoard();

        shipSizes.Clear();
        shipSizes.Add(ShipType.Destroyer, 2);
        shipSizes.Add(ShipType.Submarine, 3);
        shipSizes.Add(ShipType.Cruiser, 3);
        shipSizes.Add(ShipType.Battleship, 4);
        shipSizes.Add(ShipType.Carrier, 5);

        shipPrefabs.Clear();
        for (int i = 0; i < shipPrefabsList.Count; ++i)
        {
            shipPrefabs.Add(shipPrefabsList[i].type, shipPrefabsList[i].prefab);
        }

        shipRotations.Clear();
        for (int i = 0; i < shipRotationsList.Count; ++i)
        {
            shipRotations.Add(shipRotationsList[i].direction, shipRotationsList[i].quaternion);
        }
    }

    public int GetShipSize(ShipType s)
    {
        return shipSizes[s];
    }

    public GameObject SpawnShipAt(ShipType type, Vector3 location, Quaternion rotation)
    {
        GameObject prefab = null;
        if (shipPrefabs.TryGetValue(type, out prefab))
        {
            GameObject output = Instantiate(prefab);
            output.transform.position = location;
            output.transform.localRotation = rotation;
            return output;
        }
        return null;
    }

    public Quaternion GetRotationFromDirection(ShipDirection d)
    {
        if (shipRotations.TryGetValue(d, out var rotation))
        {
            return rotation;
        }
        return Quaternion.identity;
    }

    public ShipType GetShipOnSpace(int val)
    {
        ShipType t = (ShipType)val;
        if ((t & ShipType.Destroyer) != 0)
        {
            return ShipType.Destroyer;
        }
        else if ((t & ShipType.Submarine) != 0)
        {
            return ShipType.Submarine;
        }
        else if ((t & ShipType.Cruiser) != 0)
        {
            return ShipType.Cruiser;
        }
        else if ((t & ShipType.Battleship) != 0)
        {
            return ShipType.Battleship;
        }
        else if ((t & ShipType.Carrier) != 0)
        {
            return ShipType.Carrier;
        }
        else
        {
            return ShipType.None;
        }
    }

    public Boolean DoesSpaceHaveShip(int val)
    {
        ShipType t = (ShipType)val;
        return ((t & ShipType.Destroyer) | (t & ShipType.Submarine) | (t & ShipType.Cruiser) | (t & ShipType.Battleship) | (t & ShipType.Carrier)) != 0;
    }

    public Boolean WasSpaceHit(int val)
    {
        GridState s = (GridState)val;
        return ((s & GridState.Hit)) != 0;
    }

    public Boolean HasSpaceBeenAttacked(int val)
    {
        GridState s = (GridState)val;
        return ((s & GridState.Hit) | (s & GridState.Miss)) != 0;
    }

    public Vector3 PlayerBoardPositionToWorldLocation(int y, int x)
    {
        return PlayerBoardTopLeft + (PlayerBoardVerticalStep * y) + (PlayerBoardHorizontalStep * x);
    }

    public Vector3 EnemyBoardPositionToWorldLocation(int y, int x)
    {
        return EnemyBoardTopLeft + (EnemyBoardVerticalStep * y) + (EnemyBoardHorizontalStep * x);
    }

    private GameObject PlaceShipAt(int y, int x, ShipType t, ShipDirection d, Boolean isEnemy)
    {
        int end;
        int[,] board;
        if (isEnemy)
        {
            board = EnemyBoard;
        }
        else
        {
            board = PlayerBoard;
        }

        List<Tuple<int, int>> positionsToMarkAsPlaced = new List<Tuple<int, int>>();

        if (d == ShipDirection.Vertical)
        {
            end = y + GetShipSize(t);
            for (int i = y; i < end; ++i)
            {
                if (DoesSpaceHaveShip(board[i, x]))
                {
                    return null;
                }
                else
                {
                    positionsToMarkAsPlaced.Add(Tuple.Create(i, x));
                }
            }
            // No ships in the way
        }
        else if (d == ShipDirection.Horizontal)
        {
            end = x + GetShipSize(t);
            for (int i = x; i < end; ++i)
            {
                if (DoesSpaceHaveShip(board[y, i]))
                {
                    return null;
                }
                else
                {
                    positionsToMarkAsPlaced.Add(Tuple.Create(y, i));
                }
            }
            // No ships in the way
        }
        else
        {
            // Unknown direction? Direction added without expanding support
            return null;
        }
        Vector3 pos;
        if (isEnemy)
        {
            pos = EnemyBoardPositionToWorldLocation(y, x);
        }
        else
        {
            pos = PlayerBoardPositionToWorldLocation(y, x);
        }

        for (int i = 0; i < positionsToMarkAsPlaced.Count; ++i)
        {
            if (isEnemy)
            {
                EnemyBoard[positionsToMarkAsPlaced[i].Item1, positionsToMarkAsPlaced[i].Item2] |= ((int)t);
            }
            else
            {
                PlayerBoard[positionsToMarkAsPlaced[i].Item1, positionsToMarkAsPlaced[i].Item2] |= ((int)t);
            }
        }

        return SpawnShipAt(t, pos, GetRotationFromDirection(d));
    }

    public GameObject PlacePlayerShipAt(int y, int x, ShipType t, ShipDirection d)
    {
        return PlaceShipAt(y, x, t, d, false);
    }

    public GameObject PlaceEnemyShipAt(int y, int x, ShipType t, ShipDirection d)
    {
        return PlaceShipAt(y, x, t, d, true);
    }

    public Dictionary<ShipType, List<Tuple<int, int>>> GetShipHits(Boolean forEnemy)
    {
        int[,] board;
        if (forEnemy)
        {
            board = EnemyBoard;
        }
        else
        {
            board = PlayerBoard;
        }

        Dictionary<ShipType, List<Tuple<int, int>>> output = new Dictionary<ShipType, List<Tuple<int, int>>>();

        for (int y = 0; y < board.GetLength(0); ++y)
        {
            for (int x = 0; x < board.GetLength(1); ++x)
            {
                ShipType t = GetShipOnSpace(board[y, x]);
                if (t != ShipType.None && WasSpaceHit(board[y, x]))
                {
                    if (output.TryGetValue(t, out var val))
                    {
                        val.Add(new Tuple<int, int>(y, x));
                    }
                    else
                    {
                        var newList = new List<Tuple<int, int>>
                        {
                            new Tuple<int, int>(y, x)
                        };
                        output.Add(t, newList);
                    }
                }
            }
        }

        return output;
    }

    public Dictionary<ShipType, List<Tuple<int, int>>> GetSunkenShips(Boolean forEnemy)
    {
        Dictionary<ShipType, List<Tuple<int, int>>> output = new Dictionary<ShipType, List<Tuple<int, int>>>();

        Dictionary<ShipType, List<Tuple<int, int>>> hitShips = GetShipHits(forEnemy);
        foreach (KeyValuePair<ShipType, List<Tuple<int, int>>> entry in hitShips)
        {
            int expectedSize = GetShipSize(entry.Key);
            if (entry.Value.Count == expectedSize)
            {
                output.Add(entry.Key, entry.Value);
            }
        }

        return output;
    }

    public int CountTotalHitsOnBoard(Boolean forEnemy)
    {
        int output = 0;
        Dictionary<ShipType, List<Tuple<int, int>>> hitShips = GetShipHits(forEnemy);
        foreach (KeyValuePair<ShipType, List<Tuple<int, int>>> entry in hitShips)
        {
            output += entry.Value.Count;
        }
        return output;
    }

    public void ClearBoard()
    {
        PlayerBoard = new int[GridWidth, GridDepth];
        EnemyBoard = new int[GridWidth, GridDepth];
    }
}
