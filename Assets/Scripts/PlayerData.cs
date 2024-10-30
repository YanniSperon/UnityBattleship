using NUnit.Framework;
using System;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using static DataManager;

public class PlayerData : MonoBehaviour
{
    public NetworkVariable<int> xp = new NetworkVariable<int>();

    // Destroyer, Submarine, Cruiser, Battleship, Carrier
    public NetworkVariable<List<List<Vector2Int>>> shipPositions;
}
