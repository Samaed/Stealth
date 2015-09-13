using UnityEngine;
using System.Collections;

public abstract class GroundGeneratorBehaviour : MapGeneratorBehaviour {

    public GameObject prefab;

    void Start()
    {

    }

    protected MapElement CreateObject(int x, int y, int z)
    {
        Vector3 prefabMeshSize = Geometry.PrefabMeshSize(prefab);

        GameObject mapElementGameObject = (GameObject)Instantiate(prefab, new Vector3(prefabMeshSize.x * x, prefabMeshSize.y * y, prefabMeshSize.z * z), Quaternion.identity);
        mapElementGameObject.name = string.Format("{0},{1},{2}", x, y, z);
        MapElement mapElement = mapElementGameObject.GetComponent<MapElement>();
        mapElement.Coordinates = new Vector3(x, y, z);
        mapElementGameObject.transform.parent = transform;

        return mapElement;
    }

}
