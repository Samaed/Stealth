using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Map : MonoBehaviour
{
    private int[,] DepthTopElements;
    private MapElement[,,] Elements;
    public Vector3 Size;

    void Awake()
    {
        DepthTopElements = new int[(int)Size.x, (int)Size.z];
        Elements = new MapElement[(int)Size.x, (int)Size.y, (int)Size.z];
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

    public void SetElementAt(int x, int y, int z, MapElement element)
    {
        CheckIndexes(x, y, z);
        Elements[x, y, z] = element;

        element.BlockDestroyed += OnElementDestroyed;

        UpdateTopElements(element.Coordinates, true);
    }

    public MapElement GetElementAt(Vector3 coordinates)
    {
        return GetElementAt((int)coordinates.x, (int)coordinates.y, (int)coordinates.z);
    }

    public MapElement GetElementAt(int x, int y, int z)
    {
        CheckIndexes(x, y, z);
        return Elements[x, y, z];
    }

    public int GetTopDepthAt(int x, int z)
    {
        return DepthTopElements[x, z];
    }

    public MapElement this[Vector3 coordinates]
    {
        get
        {
            return this[(int)coordinates.x, (int)coordinates.y, (int)coordinates.z];
        }

        set
        {
            this[(int)coordinates.x, (int)coordinates.y, (int)coordinates.z] = value;
        }
    }

    public MapElement this[int x, int y, int z]
    {
        get
        {
            return GetElementAt(x, y, z);
        }

        set
        {
            SetElementAt(x, y, z, value);
        }
    }

    public void OnElementDestroyed(MapElement element)
    {
        UpdateTopElements(element.Coordinates, false);
    }

    public void UpdateTopElements(Vector3 coordinates, bool creation)
    {
        int x = (int)coordinates.x;
        int y = (int)coordinates.y;
        int z = (int)coordinates.z;

        MapElement behind = null;
        if (y > 0)
            behind = GetElementAt(x, y - 1, z);

        if (creation)
        {
            if (y != ((int)Size.y) - 1 && Elements[x, y + 1, z] == null)
            {
                if (behind != null)
                    DepthTopElements[x, z] = y;
            }
        }
        else
        {
            if (behind != null)
                DepthTopElements[x, z] = y-1;
        }
    }
}
