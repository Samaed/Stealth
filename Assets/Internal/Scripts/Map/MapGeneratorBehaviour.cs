using UnityEngine;
using System.Collections;

public abstract class MapGeneratorBehaviour : MonoBehaviour {

    public GameObject prefab;
    void Start()
    {

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

    protected GameObject CreateObject(int x, int y, int z, Vector3 size)
    {
        Vector3 prefabMeshSize = PrefabMeshSize();

        GameObject mapElementGameObject = (GameObject)Instantiate(prefab, new Vector3(prefabMeshSize.x * x, prefabMeshSize.y * y, prefabMeshSize.z * z), Quaternion.identity);
        mapElementGameObject.name = string.Format("{0},{1},{2}", x, y, z);
        MapElement mapElement = mapElementGameObject.GetComponent<MapElement>();
        mapElement.Coordinates = new Vector3(x, y, z);
        mapElementGameObject.transform.parent = transform;

        MapBlockDiversityBehaviour behaviour = GetComponent<MapBlockDiversityBehaviour>();
        if (behaviour != null && behaviour.enabled)
        {
            behaviour.CustomizeBlock(mapElement, size);
        }

        return mapElementGameObject;
    }

    public abstract void Fill(GameObject[, ,] objects, Vector3 size);

}
