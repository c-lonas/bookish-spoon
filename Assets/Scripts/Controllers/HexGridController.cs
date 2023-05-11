using UnityEngine;
using System.Collections.Generic;

public class HexGridController : MonoBehaviour
{
    // public GameObject hexPrefab; // Assign a hex-shaped prefab in the Unity Inspector
    // public float hexSize = 1f; // The size of a single hexagon, modify as needed

    public HexCellFactory hexCellFactory;
    public int gridSize;

    private Dictionary<Vector3Int, HexCell> hexCells;

    // Expose the hexCells dictionary to other classes
    public Dictionary<Vector3Int, HexCell> HexCells { get { return hexCells; } }

    // Create Main Grid
    public void GenerateGrid(int gridSize, float initialElevation)
    {
        hexCells = new Dictionary<Vector3Int, HexCell>();

        for (int q = -gridSize; q <= gridSize; q++)
        {
            for (int r = Mathf.Max(-gridSize, -q - gridSize); r <= Mathf.Min(gridSize, -q + gridSize); r++)
            {
                HexCell hexCell = hexCellFactory.CreateHexCell(q, r, initialElevation);
                hexCells.Add(hexCell.CubeCoordinates, hexCell);
            }
        }
    }

    // private void GenerateHexCell(int q, int r, float elevation)
    // {
    //     // Calculate the world position for the hex cell
    //     Vector3 position = GetWorldPosition(q, r, hexSize);

    //     // Create a new HexCell with the cube coordinates and elevation from the noise function
    //     Vector3Int cubeCoordinates = new Vector3Int(q, r, -q - r);
    //     HexCell hexCell = new HexCell(cubeCoordinates, elevation);

    //     // Instantiate the hex-shaped prefab and position it according to the calculated position
    //     Vector3 finalPosition = position + Vector3.up * hexCell.Elevation;
    //     GameObject hexInstance = Instantiate(hexPrefab, finalPosition, Quaternion.identity, this.transform);
    //     hexInstance.name = $"Hex({q}, {r}, {-q - r})";
    //     hexInstance.GetComponent<MeshRenderer>().material.SetFloat("_Elevation", hexCell.Elevation);

    //     // Set the AssociatedGameObject of the HexCell
    //     hexCell.SetAssociatedGameObject(hexInstance);

    //     // Store the HexCell in the hexCells dictionary
    //     hexCells.Add(cubeCoordinates, hexCell);
    // }


    public List<HexCoordinates> GetHexesInRange(HexCoordinates center, int range)
    {
        var results = new List<HexCoordinates>();

        for (int q = -range; q <= range; q++)
        {
            int r1 = Mathf.Max(-range, -q - range);
            int r2 = Mathf.Min(range, -q + range);
            for (int r = r1; r <= r2; r++)
            {
                results.Add(center.Add(new HexCoordinates(q, r, -q - r)));
            }
        }

        return results;
    }


    private Vector3 GetWorldPosition(int q, int r, float hexSize)
    {
        float x = hexSize * (Mathf.Sqrt(3) * q + Mathf.Sqrt(3) / 2 * r);
        float z = hexSize * (3f / 2 * r);

        return new Vector3(x, 0f, z);
    }

    public void ClearGrid()
    {
        // Collect all hex instances in a list
        List<GameObject> hexInstances = new List<GameObject>();
        foreach (HexCell hexCell in hexCells.Values)
        {
            hexInstances.Add(hexCell.AssociatedGameObject);
        }

        // Destroy the collected hex instances
        foreach (GameObject hexInstance in hexInstances)
        {
            DestroyImmediate(hexInstance);
        }

        // Clear the hexCells dictionary
        hexCells.Clear();
    }

}
