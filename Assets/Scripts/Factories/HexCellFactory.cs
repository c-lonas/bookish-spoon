using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HexCellFactory : MonoBehaviour
{
    public GameObject hexPrefab;
    public float hexSize = 1f;

    public HexCell CreateHexCell(int q, int r, float elevation)
    {
        Vector3 position = GetWorldPosition(q, r, hexSize);
        Vector3Int cubeCoordinates = new Vector3Int(q, r, -q - r);
        HexCell hexCell = new HexCell(cubeCoordinates, elevation);
        Vector3 finalPosition = position + Vector3.up * hexCell.Elevation;

        GameObject hexInstance = Instantiate(hexPrefab, finalPosition, Quaternion.identity, this.transform);
        hexInstance.name = $"Hex({q}, {r}, {-q - r})";
        hexInstance.GetComponent<MeshRenderer>().material.SetFloat("_Elevation", hexCell.Elevation);

        hexCell.SetAssociatedGameObject(hexInstance);

        HexCellComponent hexCellComponent = hexInstance.AddComponent<HexCellComponent>();
        hexCellComponent.HexCell = hexCell;

        return hexCell;
    }

    private Vector3 GetWorldPosition(int q, int r, float hexSize)
    {
        float x = hexSize * (Mathf.Sqrt(3) * q + Mathf.Sqrt(3) / 2 * r);
        float z = hexSize * (3f / 2 * r);

        return new Vector3(x, 0f, z);
    }
}



public class HexCellComponent : MonoBehaviour
{
    public HexCell HexCell { get; set; }
}
