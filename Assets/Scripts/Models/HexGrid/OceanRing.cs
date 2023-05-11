using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class OceanRing
{
    public List<GameObject> oceanHexes;

    public OceanRing()
    {
        oceanHexes = new List<GameObject>();
    }

    public void AddHex(GameObject hexGameObject)
    {
        oceanHexes.Add(hexGameObject);

        hexGameObject.AddComponent<ocean_hex>();
    }

    public bool ContainsHex(HexCoordinates hex)
    {
        // Check if the hex is in the ocean ring
        return oceanHexes.Any(oceanHexGameObject => 
        {
            HexCellComponent hexCellComponent = oceanHexGameObject.GetComponent<HexCellComponent>();
            if(hexCellComponent != null && hexCellComponent.HexCell != null)
            {
                return hexCellComponent.HexCell.CubeCoordinates.Equals(hex);
            }
            else
            {
                Debug.LogError($"HexCellComponent or HexCell is missing from GameObject at {hex.ToVector3Int()}");
                return false;
            }
        });
    }


    public List<GameObject> Clear()
    {
        List<GameObject> hexesToClear = new List<GameObject>(oceanHexes);

        // Clear the oceanHexes list
        oceanHexes.Clear();

        return hexesToClear;
    }

}
