using UnityEngine;
using System.Collections.Generic;

public class HexCoordinates
{
    public int Q { get; private set; }
    public int R { get; private set; }
    public int S { get; private set; }

    public HexCoordinates(int q, int r, int s)
    {
        Q = q;
        R = r;
        S = s;
    }

    public HexCoordinates Add(HexCoordinates other)
    {
        return new HexCoordinates(Q + other.Q, R + other.R, S + other.S);
    }

    public HexCoordinates Multiply(int factor)
    {
        return new HexCoordinates(Q * factor, R * factor, S * factor);
    }

    public static HexCoordinates Direction(int direction)
    {
        // Hexagonal directions in cube coordinates
        HexCoordinates[] directions = {
            new HexCoordinates(+1, -1, 0), new HexCoordinates(+1, 0, -1), new HexCoordinates(0, +1, -1),
            new HexCoordinates(-1, +1, 0), new HexCoordinates(-1, 0, +1), new HexCoordinates(0, -1, +1)
        };

        return directions[direction % 6];
    }

    public Vector3Int ToVector3Int()
    {
        return new Vector3Int(Q, R, S);
    }
}
