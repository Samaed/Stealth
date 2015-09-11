using UnityEngine;
using System.Collections;

public class HeightMapGeneratorBehaviour : MapGeneratorBehaviour {

    public Texture2D Texture;

    public override void Fill(GameObject[, ,] objects, Vector3 size)
    {
        int mapWidth = (int)size.x;
        int mapHeight = (int)size.z;
        float maxHeight = 0;
        float[,] heights = new float[mapWidth, mapHeight];

        if (Texture != null)
        {
            float incrementX = Texture.width / size.x;
            float incrementZ = Texture.height / size.z;

            float height;
            Color currentColor;

            float xTexture = 0;
            float zTexture = 0;

            for (int xMap = 0; xMap < mapWidth; xMap++)
            {
                for (int zMap = 0; zMap < mapHeight; zMap++)
                {
                    currentColor = Texture.GetPixel((int)xTexture, (int)zTexture);

                    height = currentColor.r + currentColor.g + currentColor.b;
                    heights[xMap, zMap] = height;

                    if (height > maxHeight)
                        maxHeight = height;

                    zTexture += incrementZ;
                }
                xTexture += incrementX;
            }
        }

        int heightInt;

        for (int x = 0; x < mapWidth; x++)
        {
            for (int z = 0; z < mapHeight; z++)
            {
                heightInt = (maxHeight == 0) ? 1 : 1 + (int)(heights[x,z] / maxHeight * ((int)size.y-1));

                for (int y = 0; y < heightInt; y++) {
                    objects[x, y, z] = CreateObject(x, y, z, size);
                }
            }
        }
    }
}
