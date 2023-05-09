
using UnityEngine;
using System.Collections.Generic;


public enum NoiseType
{
    Perlin,
    Simplex
}


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
                // Calculate the world position for the hex cell
                Vector3 position = GetWorldPosition(q, r, hexSize);

                // Create a new HexCell with the cube coordinates and initial elevation
                Vector3Int cubeCoordinates = new Vector3Int(q, r, -q - r);
                HexCell hexCell = new HexCell(cubeCoordinates, initialElevation, Biome.Water);

                // Instantiate the hex-shaped prefab and position it according to the calculated position
                Vector3 finalPosition = position + Vector3.up * hexCell.Elevation;
                GameObject hexInstance = Instantiate(hexPrefab, finalPosition, Quaternion.identity, this.transform);
                hexInstance.name = $"Hex({q}, {r}, {-q - r})";
                hexInstance.GetComponent<MeshRenderer>().material.SetFloat("_Elevation", hexCell.Elevation);


                // Store the HexCell in the hexCells dictionary
                hexCells.Add(cubeCoordinates, hexCell);
            }
        }
    }

    // GenerateGrid Method with Random Elevation
    public void GenerateGrid(int gridSize, float initialElevation, float minRandomElevation, float maxRandomElevation)
    {
        hexCells = new Dictionary<Vector3Int, HexCell>();

        for (int q = -gridSize; q <= gridSize; q++)
        {
            for (int r = Mathf.Max(-gridSize, -q - gridSize); r <= Mathf.Min(gridSize, -q + gridSize); r++)
            {
                // Calculate the world position for the hex cell
                Vector3 position = GetWorldPosition(q, r, hexSize);

                // Create a new HexCell with the cube coordinates and random elevation
                Vector3Int cubeCoordinates = new Vector3Int(q, r, -q - r);
                float randomElevation = Random.Range(minRandomElevation, maxRandomElevation);
                HexCell hexCell = new HexCell(cubeCoordinates, initialElevation + randomElevation, Biome.Water);

                // Instantiate the hex-shaped prefab and position it according to the calculated position
                Vector3 finalPosition = position + Vector3.up * hexCell.Elevation;
                GameObject hexInstance = Instantiate(hexPrefab, finalPosition, Quaternion.identity, this.transform);
                hexInstance.name = $"Hex({q}, {r}, {-q - r})";
                hexInstance.GetComponent<MeshRenderer>().material.SetFloat("_Elevation", hexCell.Elevation);


                // Store the HexCell in the hexCells dictionary
                hexCells.Add(cubeCoordinates, hexCell);
            }
        }
    }

    // GenerateGrid Method with Noise
    public void GenerateGrid(int gridSize, float initialElevation, NoiseType noiseType, float noiseScale, float noiseAmplitude)
    {
        hexCells = new Dictionary<Vector3Int, HexCell>();

        for (int q = -gridSize; q <= gridSize; q++)
        {
            for (int r = Mathf.Max(-gridSize, -q - gridSize); r <= Mathf.Min(gridSize, -q + gridSize); r++)
            {
                // Calculate the world position for the hex cell
                Vector3 position = GetWorldPosition(q, r, hexSize);

                // Create a new HexCell with the cube coordinates and elevation from the noise function
                Vector3Int cubeCoordinates = new Vector3Int(q, r, -q - r);
                float noiseValue = GetNoiseValue(q, r, noiseType, noiseScale, noiseAmplitude);
                HexCell hexCell = new HexCell(cubeCoordinates, initialElevation + noiseValue, Biome.Water);

                // Instantiate the hex-shaped prefab and position it according to the calculated position
                Vector3 finalPosition = position + Vector3.up * hexCell.Elevation;
                GameObject hexInstance = Instantiate(hexPrefab, finalPosition, Quaternion.identity, this.transform);
                hexInstance.name = $"Hex({q}, {r}, {-q - r})";
                hexInstance.GetComponent<MeshRenderer>().material.SetFloat("_Elevation", hexCell.Elevation);


                // Store the HexCell in the hexCells dictionary
                hexCells.Add(cubeCoordinates, hexCell);
            }
        }

    }

    private float GetNoiseValue(int q, int r, NoiseType noiseType, float noiseScale, float noiseAmplitude)
    {
        // Convert axial coordinates (q, r) to continuous coordinates (x, y)
        float x = (Mathf.Sqrt(3) * q + Mathf.Sqrt(3) / 2 * r) * noiseScale;
        float y = (3f / 2 * r) * noiseScale;

        float noiseValue = 0f;

        switch (noiseType)
        {
            case NoiseType.Perlin:
                noiseValue = Mathf.PerlinNoise(x, y) * noiseAmplitude;
                break;
            case NoiseType.Simplex:
                noiseValue = SimplexNoiseFunction(x, y) * noiseAmplitude;
                break;
        }

        return noiseValue;
    }



    // Replace this with the Simplex noise function
    private float SimplexNoiseFunction(float x, float y)
    {
        FastNoiseLite noise = new FastNoiseLite();
        noise.SetNoiseType(FastNoiseLite.NoiseType.OpenSimplex2);
        return (noise.GetNoise(x, y) + 1) / 2; // Convert the noise value to a range between 0 and 1
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