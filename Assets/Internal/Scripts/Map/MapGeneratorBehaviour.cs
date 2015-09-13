using UnityEngine;
using System.Collections;
using System;

public abstract class MapGeneratorBehaviour : MonoBehaviour {

    protected Map map;

    [HideInInspector]
    public int stepIndex;

    void Awake()
    {
    }

    public void Generation(Map map)
    {
        this.map = map;
        Generate();
    }

    protected abstract void Generate();

}
