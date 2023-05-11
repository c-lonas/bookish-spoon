using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Section
{
    public Vector3Int SectionPosition { get; private set; }
    public SectionTypeVariant SectionType { get; private set; }
    public List<HexCell> HexCells { get; private set; }

    public Section(Vector3Int sectionPosition, SectionTypeVariant sectionType)
    {
        SectionPosition = sectionPosition;
        SectionType = sectionType;
        HexCells = new List<HexCell>();
    }

    public void AddHexCell(HexCell hexCell)
    {
        HexCells.Add(hexCell);
    }
}

public enum SectionTypeVariant
{
    Ocean,
    EvolveTerrain,
    City
}
