using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell
{
    public Vector3Int CubeCoordinates { get; private set; }
    public float Elevation { get; set; }
    public Biome Biome { get; set; }

    public HexCell(Vector3Int cubeCoordinates, float elevation, Biome biome)
    {
        CubeCoordinates = cubeCoordinates;
        Elevation = elevation;
        Biome = biome;
    }
}

public enum Biome
{
    Water,
    Sand,
    Grassland,
    Forest,
    // Add more biomes as needed
}

