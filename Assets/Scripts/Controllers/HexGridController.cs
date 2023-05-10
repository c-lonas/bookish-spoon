using UnityEngine;
using System.Collections.Generic;

public class HexGridController : MonoBehaviour
{
    public GameObject hexPrefab; // Assign a hex-shaped prefab in the Unity Inspector
    public float hexSize = 1f; // The size of a single hexagon, modify as needed

    private Dictionary<Vector3Int, HexCell> hexCells;

    // GenerateGrid Default Method
    public void GenerateGrid(int gridSize, float initialElevation)
    {
        hexCells = new Dictionary<Vector3Int, HexCell>();

        for (int q = -gridSize; q <= gridSize; q++)
        {
            for (int r = Mathf.Max(-gridSize, -q - gridSize); r <= Mathf.Min(gridSize, -q + gridSize); r++)
            {
                GenerateHexCell(q, r, initialElevation);
            }
        }
    }

    // GenerateGrid with Satellites
    public void GenerateGrid(int gridSize, float initialElevation, int numberOfSatellites, int satelliteSize)
    {
        hexCells = new Dictionary<Vector3Int, HexCell>();

        for (int q = -gridSize; q <= gridSize; q++)
        {
            for (int r = Mathf.Max(-gridSize, -q - gridSize); r <= Mathf.Min(gridSize, -q + gridSize); r++)
            {
                GenerateHexCell(q, r, initialElevation);
            }
        }
    }

    private void GenerateHexCell(int q, int r, float elevation)
    {
        // Calculate the world position for the hex cell
        Vector3 position = GetWorldPosition(q, r, hexSize);

        // Create a new HexCell with the cube coordinates and elevation from the noise function
        Vector3Int cubeCoordinates = new Vector3Int(q, r, -q - r);
        HexCell hexCell = new HexCell(cubeCoordinates, elevation, Biome.Water);

        // Instantiate the hex-shaped prefab and position it according to the calculated position
        Vector3 finalPosition = position + Vector3.up * hexCell.Elevation;
        GameObject hexInstance = Instantiate(hexPrefab, finalPosition, Quaternion.identity, this.transform);
        hexInstance.name = $"Hex({q}, {r}, {-q - r})";
        hexInstance.GetComponent<MeshRenderer>().material.SetFloat("_Elevation", hexCell.Elevation);

        // Store the HexCell in the hexCells dictionary
        hexCells.Add(cubeCoordinates, hexCell);
    }

    private Vector3 GetWorldPosition(int q, int r, float hexSize)
    {
        float x = hexSize * (Mathf.Sqrt(3) * q + Mathf.Sqrt(3) / 2 * r);
        float z = hexSize * (3f / 2 * r);

        return new Vector3(x, 0f, z);
    }

    public void ClearGrid()
    {
        // Collect all child objects (hex instances) in a list
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in transform)
        {
            children.Add(child.gameObject);
        }

        // Destroy the collected child objects
        foreach (GameObject child in children)
        {
            DestroyImmediate(child);
        }

        // Clear the hexCells dictionary
        hexCells.Clear();
    }
}
