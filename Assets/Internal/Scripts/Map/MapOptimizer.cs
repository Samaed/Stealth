using UnityEngine;
using System.Collections;

public class MapOptimizer : MapGeneratorBehaviour {

	void Start () {
	
	}

    protected override void Generate()
    {
        Vector3 size = map.Size;

        int mapWidth = (int)size.x;
        int mapHeight = (int)size.z;
        int mapDepth = (int)size.y;

        bool[,,] exists = new bool[(int)size.x,(int)size.y,(int)size.z];
        
        MapElement element;
        DestructibleBehaviour destructible;
        for (int x = 1; x < mapWidth-1; x++)
        {
            for (int z = 1; z < mapHeight-1; z++)
            {
                for (int y = 0; y < mapDepth-1; y++)
                {
                    element = map[x,y,z];
                    exists[x,y,z] = element != null;
                    if (element != null) {
                        destructible = element.GetComponent<DestructibleBehaviour>();
                        exists[x,y,z] &= (destructible == null || !destructible.enabled);
                    }
                }
            }
        }

        for (int x = 1; x < mapWidth-1; x++)
        {
            for (int z = 1; z < mapHeight-1; z++)
            {
                for (int y = 1; y < mapDepth-1; y++)
                {
                    if (exists[x, y, z] && exists[x + 1, y, z] && exists[x, y + 1, z] && exists[x, y, z + 1] && exists[x - 1, y, z] && exists[x, y, z - 1])
                    {
                        Destroy(map[x, y, z].gameObject);
                    }
                }
            }
        }

        exists = null;
    }

}
