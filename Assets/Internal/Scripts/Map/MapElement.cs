﻿using UnityEngine;
using System.Collections;

public class MapElement : MonoBehaviour {

    public enum Directions
    {
        RIGHT = 0,
        BOTTOM = 1,
        LEFT = 2,
        TOP = 3
    }

    public Vector3 Coordinates { set; get; }

    public Directions Direction { set; get; }

}
