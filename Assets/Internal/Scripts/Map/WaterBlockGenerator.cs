using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaterBlockGenerator : MapGeneratorBehaviour {

    public GameObject prefab;

    [Range(0, 1)]
    public float MaxWaterDepthPercentage;
    [Range(0, 1)]
    public float WaterProbability;

	void Start () {
	
	}

    protected override void Generate()
    {
        int mapWidth = (int)map.Size.x;
        int mapDepth = (int)map.Size.y;
        int mapHeight = (int)map.Size.z;

        int maxWaterDepth = (int)(MaxWaterDepthPercentage * mapDepth);
        int maxAreaWaterDepth, y;

        bool[,,] visited = new bool[mapWidth, maxWaterDepth+1, mapHeight];

        for (int x = 0; x < mapWidth; x++)
        {
            for (int z = 0; z < mapHeight; z++)
            {
                y = map.GetTopDepthAt(x,z);

                if (maxWaterDepth > y && y < mapDepth)
                {
                    maxAreaWaterDepth = Random.Range(y+1, maxWaterDepth);
                    CreateWaterArea(x, maxAreaWaterDepth, z, mapWidth, mapHeight, visited, WaterProbability > Random.value, false);
                }
            }
        }
    }

    protected void CreateWaterArea(int x, int y, int z, int mapWidth, int mapHeight, bool[,,] visited, bool create, bool down)
    {
        if ((visited[x, y, z] && !down) || map[x, y, z] != null) return;

        if (create)
            map[x, y, z] = CreateObject(x, y, z);
        visited[x,y,z] = true;

        if (y > 0)
            CreateWaterArea(x, y - 1, z, mapWidth, mapHeight, visited, create, true);
        if (x > 0)
            CreateWaterArea(x - 1, y, z, mapWidth, mapHeight, visited, create, false);
        if (z > 0)
            CreateWaterArea(x, y, z - 1, mapWidth, mapHeight, visited, create, false);
        if (z < mapHeight - 1)
            CreateWaterArea(x, y, z + 1, mapWidth, mapHeight, visited, create, false);
        if (x < mapWidth - 1)
            CreateWaterArea(x + 1, y, z, mapWidth, mapHeight, visited, create, false);
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
