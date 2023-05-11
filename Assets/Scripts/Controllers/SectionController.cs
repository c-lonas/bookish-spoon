using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SectionController : MonoBehaviour
{
    public HexGridController hexGridController; // Assign in the Unity Inspector
    public float sectionSize = 5f; // The size of a single section in hexes, modify as needed

    private Dictionary<Vector3Int, Section> sections;

    private OceanRing oceanRing;
    private List<City> cities;
 
    public void ParcelGridIntoSections(int numberOfCities, int oceanRingRadius, float averageTerrainSectionSize, float terrainSectionVariation)
    {
        sections = new Dictionary<Vector3Int, Section>();

        // First, create the Ocean Ring around the perimeter of the grid
        CreateOceanRing(hexGridController.gridSize, oceanRingRadius);

        // Second, parcel out the cities
        for (int i = 0; i < numberOfCities; i++)
        {
            CitySize citySize = (CitySize)Random.Range(0, 3);  // Randomly decide the size of the city
            CreateCity(citySize);
        }

        // Third, parcel out the terrain evolution from the remaining hexes in the grid
    }

    public void ClearSections()
    {
        // Check if the ocean ring exists
        if (oceanRing != null)
        {
            // Clear the ocean ring
            List<GameObject> hexesToClear = oceanRing.Clear();

            // Iterate over each ocean hex
            foreach (GameObject hexGameObject in hexesToClear)
            {
                // Check if the "ocean_hex" component exists
                ocean_hex component = hexGameObject.GetComponent<ocean_hex>();
                if (component != null)
                {
                    // Remove the "ocean_hex" component
                    DestroyImmediate(component, true);
                }
            }
        }

        sections.Clear();
    }


    /////// Helper Functions //////


    // Ocean Ring Helper Functions
    public void CreateOceanRing(int gridRadius, int oceanRingRadius)
    {
        List<HexCell> oceanRingHexes = new List<HexCell>();

        HexCoordinates center = new HexCoordinates(0, 0, 0);
        oceanRing = new OceanRing();

        // Loop for each radius from the gridRadius down to (gridRadius - oceanRingRadius + 1)
        for (int radius = gridRadius; radius > gridRadius - oceanRingRadius; radius--)
        {
            // Start from a hex in the direction 4 (upper right) at a distance of radius
            HexCoordinates startHex = center.Add(HexCoordinates.Direction(4).Multiply(radius));

            // Loop around the 6 directions
            for (int direction = 0; direction < 6; direction++)
            {
                // Move radius steps in each direction
                for (int step = 0; step < radius; step++)
                {
                    // If the hex exists in the grid, add it to the ocean ring
                    if (hexGridController.HexCells.TryGetValue(startHex.ToVector3Int(), out HexCell hexCell))
                    {
                        if(hexCell != null) // check if hexCell is not null
                        {
                            oceanRingHexes.Add(hexCell);
                        }
                        else
                        {
                            Debug.LogError($"HexCell at {startHex.ToVector3Int()} is null");
                        }
                    }

                    // Move to the next hex in the current direction
                    startHex = startHex.Add(HexCoordinates.Direction(direction));
                }
            }
        }

        foreach (HexCell hexCell in oceanRingHexes)
        {
            if(hexCell != null)
            {
                // Get the game object associated with the hex cell
                GameObject hexGameObject = hexCell.AssociatedGameObject;
                if(hexGameObject != null)
                {
                    // Add the hex GameObject to the oceanRing
                    oceanRing.AddHex(hexGameObject);
                }
                else
                {
                    Debug.LogError($"AssociatedGameObject for HexCell at {hexCell.CubeCoordinates} is null");
                }
            }
        }
    }

    // City Helper Functions
    public void CreateCity(CitySize size)
    {
        HexCoordinates location;

        do
        {
            location = new HexCoordinates(Random.Range(-hexGridController.gridSize, hexGridController.gridSize), Random.Range(-hexGridController.gridSize, hexGridController.gridSize), Random.Range(-hexGridController.gridSize, hexGridController.gridSize));

        } while (!IsLocationValidForCity(location, size));

        cities.Add(new City(size, location));
    }

    public bool IsLocationValidForCity(HexCoordinates location, CitySize size)
    {
        // Get the hexes within the city's radius
        List<HexCoordinates> cityHexes = hexGridController.GetHexesInRange(location, (int)size);

        foreach (HexCoordinates cityHex in cityHexes)
        {
            // If the hex contains the ocean_hex component, the location is not valid
            // If the hex is already part of a city (contains the city_hex component), the location is not valid
            foreach (City city in cities)
            {
                // implement check for ocean_hex component
                // implement check for city_hex component
                    return false;
            }
        }

        // If none of the hexes in the city intersect with the ocean or an existing city, the location is valid
        return true;
    }

}
