using UnityEngine;
using System.Collections;

public class MapElement : MonoBehaviour {

    // TODO maybe the element will be deleted too fast
    public delegate void BlockDestroyedHandler(MapElement element);
    public event BlockDestroyedHandler BlockDestroyed;

    public enum Directions
    {
        RIGHT = 0,
        BOTTOM = 1,
        LEFT = 2,
        TOP = 3
    }

    public Vector3 Coordinates { set; get; }

    public Directions Direction { set; get; }

    void OnDestroy()
    {
        if (BlockDestroyed != null)
            BlockDestroyed(this);
    }

}
