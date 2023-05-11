using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCell
{
    public Vector3Int CubeCoordinates { get; private set; }
    public float Elevation { get; set; }
    public GameObject AssociatedGameObject { get; private set; }

    public HexCell(Vector3Int cubeCoordinates, float elevation)
    {
        CubeCoordinates = cubeCoordinates;
        Elevation = elevation;
    }


    public void SetAssociatedGameObject(GameObject gameObject)
    {
        AssociatedGameObject = gameObject;
    }
}

