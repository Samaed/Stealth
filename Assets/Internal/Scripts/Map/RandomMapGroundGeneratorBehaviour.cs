using UnityEngine;
using System.Collections;

public class RandomMapGroundGeneratorBehaviour : GroundGeneratorBehaviour
{

    public override void Fill()
    {
        Vector3 size = map.Size;

        int mapWidth = (int)size.x;
        int mapDepth = (int)size.y;
        int mapHeight = (int)size.z;

        int height;

        for (int x = 0; x < mapWidth; x++)
        {
            for (int z = 0; z < mapHeight; z++)
            {
                height = UnityEngine.Random.Range(0, mapDepth);
                for (int y = 0; y <= height; y++)
                {
                    map.SetElementAt(x, y, z, CreateObject(x, y, z));
                }
            }
        }
    }
}
