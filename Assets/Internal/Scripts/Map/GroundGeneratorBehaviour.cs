using UnityEngine;
using System.Collections;

public abstract class GroundGeneratorBehaviour : MonoBehaviour {

    public GameObject prefab;
    protected Map map;

    public delegate void CreatedBlockHandler(Map map, MapElement block);
    public event CreatedBlockHandler CreatedBlock;

    public delegate void CreatedGroundHandler(Map map);
    public event CreatedGroundHandler CreatedGround;

    void Start()
    {

    }

    void Awake()
    {
        map = GetComponent<Map>();
        if (map != null && enabled)
            GetComponent<Map>().CreatedEmptyMap += GenerateGround;
    }

    protected Vector3 PrefabMeshSize()
    {
        Vector3 groundSize = prefab.GetComponent<MeshFilter>().sharedMesh.bounds.size;
        Vector3 scale = prefab.transform.lossyScale;
        groundSize.x *= scale.x;
        groundSize.y *= scale.y;
        groundSize.z *= scale.z;
        return groundSize;
    }

    protected MapElement CreateObject(int x, int y, int z)
    {
        Vector3 prefabMeshSize = PrefabMeshSize();

        GameObject mapElementGameObject = (GameObject)Instantiate(prefab, new Vector3(prefabMeshSize.x * x, prefabMeshSize.y * y, prefabMeshSize.z * z), Quaternion.identity);
        mapElementGameObject.name = string.Format("{0},{1},{2}", x, y, z);
        MapElement mapElement = mapElementGameObject.GetComponent<MapElement>();
        mapElement.Coordinates = new Vector3(x, y, z);
        mapElementGameObject.transform.parent = transform;

        if (CreatedBlock != null)
            CreatedBlock(map, mapElement);

        return mapElement;
    }

    private void GenerateGround()
    {
        Fill();

        if (CreatedGround != null)
            CreatedGround(map);
    }

    public abstract void Fill();

}
