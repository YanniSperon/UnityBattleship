// using UnityEngine;

// public class GridSpawner : MonoBehaviour
// {

//     public GameObject cell; 
//     public GameObject partPrefab;
//     float xScale;
//     float zScale; 
//     public int gridWidth = 10;
//     public int gridHeight = 10;
//     public float padding = 10f;

//     private GameObject[,] cells;
//     public Camera mainCamera;
//     GameObject cellHolder;

//     void Start() {
//         xScale = partPrefab.transform.localScale.x;
//         zScale = partPrefab.transform.localScale.z;
//         print(xScale + " " + zScale);
//         cellHolder = new GameObject("CellHolder");

//         for (int x = 0; x < gridWidth; x++) {
//             for (int y = 0; y < gridHeight; y++) {
//                 // Vector3 position = new Vector3(x*xScale*padding, 0, y*zScale*padding);
//                 // cell = Instantiate(partPrefab, position, Quaternion.identity);
//                 // cells[x, y] = cell;
//                 // cell.transform.parent = cellHolder.transform;
//             }
//         }

//         PositionCamera();
//     }

//     void PositionCamera() {
//         if (mainCamera != null && partPrefab != null) {
//             Vector3 centerPosition = new Vector3(((gridWidth-1)*xScale*padding) / 2, 0, ((gridHeight-1)*zScale*padding) / 2);
//             mainCamera.transform.position = new Vector3(centerPosition.x, 20.0f, centerPosition.z);
//             mainCamera.transform.LookAt(centerPosition);
//         }
//     }
// }
