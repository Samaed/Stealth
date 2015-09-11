using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Map : MonoBehaviour
{
    public delegate void CreatedEmptyMapHandler();
    public event CreatedEmptyMapHandler CreatedEmptyMap;

    public MapElement[, ,] Elements;
    public Vector3 Size;

    void Start()
    {
        Elements = new MapElement[(int)Size.x, (int)Size.y, (int)Size.z];

        if (CreatedEmptyMap != null)
            CreatedEmptyMap();
    }

    void CheckIndexes(int x, int y, int z)
    {
        if (x < 0 || x >= (int)Size.x)
            throw new IndexOutOfRangeException(String.Format("x ({0}) should be between 0 and {1}", x, (int)Size.x));
        if (y < 0 || y >= (int)Size.y)
            throw new IndexOutOfRangeException(String.Format("y ({0}) should be between 0 and {1}", y, (int)Size.y));
        if (z < 0 || z >= (int)Size.z)
            throw new IndexOutOfRangeException(String.Format("z ({0}) should be between 0 and {1}", z, (int)Size.z));
    }

    public MapElement GetElementAt(int x, int y, int z)
    {
        CheckIndexes(x, y, z);
        return Elements[x, y, z];
    }

    public MapElement this[int x, int y, int z]
    {
        get
        {
            return GetElementAt(x, y, z);
        }
    }
}
