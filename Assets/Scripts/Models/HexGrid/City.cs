using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CitySize
{
    Small,
    Medium,
    Large
}

public class City
{
    public CitySize Size { get; private set; }
    public HexCoordinates Location { get; private set; }

    public City(CitySize size, HexCoordinates location)
    {
        this.Size = size;
        this.Location = location;
    }
}