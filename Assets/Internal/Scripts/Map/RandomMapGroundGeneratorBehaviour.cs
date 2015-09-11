using UnityEngine;
using System.Collections;

public class RandomMapGroundGeneratorBehaviour : GroundGeneratorBehaviour {

    public override void Fill() {

        MapElement[, ,] objects = map.Elements;
        Vector3 size = map.Size;

        int x = (int)size.x;
        int y = (int)size.y;
        int z = (int)size.z;

        int height;

        for (int i = 0; i < x; i++)
        {
            for (int k = 0; k < z; k++)
            {
                height = UnityEngine.Random.Range(0, y);
                for (int j = 0; j <= height; j++)
                {
                    objects[i,j,k] = CreateObject(i, j, k, size);
                }
            }
        }
    }
}
