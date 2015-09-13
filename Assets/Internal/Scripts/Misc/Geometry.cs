using UnityEngine;
using System.Collections;

public static class Geometry {

    public static Vector3 PrefabMeshSize(GameObject prefab)
    {
        Vector3 groundSize = prefab.GetComponent<MeshFilter>().sharedMesh.bounds.size;
        Vector3 scale = prefab.transform.lossyScale;
        groundSize.x *= scale.x;
        groundSize.y *= scale.y;
        groundSize.z *= scale.z;
        return groundSize;
    }

}
