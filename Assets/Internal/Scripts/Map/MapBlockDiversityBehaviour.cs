using UnityEngine;
using System.Collections;
using System;

public class MapBlockDiversityBehaviour : MapGeneratorBehaviour
{
    [Serializable]
    public class BlockDistribution
    {
        public Material Material;
        public AnimationCurve Curve;
    }

    public BlockDistribution[] distributions;

    void Start()
    {

    }

    protected override void Generate()
    {
        Vector3 size = map.Size;

        int mapWidth = (int)size.x;
        int mapDepth = (int)size.y;
        int mapHeight = (int)size.z;

        for (int x = 0; x < mapWidth; x++)
        {
            for (int y = 0; y < mapDepth; y++)
            {
                for (int z = 0; z < mapHeight; z++)
                {
                    CustomizeBlock(map[x,y,z]);
                }
            }
        }
    }

    public BlockDistribution GetBlock(float value)
    {
        int distributionCount = distributions.Length;
        float[] probabilities = new float[distributionCount];
        float totalProbability = 0;

        for (int ind = 0; ind < distributionCount; ind++) {
            probabilities[ind] = distributions[ind].Curve.Evaluate(value);
            totalProbability += probabilities[ind];
        }

        float rand = UnityEngine.Random.value * totalProbability;
        int index = -1;
        float randOffset = 0;

        int i = 0;
        while (index == -1 && i < distributionCount)
        {
            if (rand < randOffset + probabilities[i])
            {
                index = i;
            }
            randOffset += probabilities[i];
            i++;
        }

        return (index == -1) ? null : distributions[index];
    }

    public void CustomizeBlock(MapElement block)
    {
        if (block == null) return;

        MapBlockDiversityBehaviour blockDiversity = GetComponent<MapBlockDiversityBehaviour>();
        if (blockDiversity != null)
        {
            BlockDistribution dist = blockDiversity.GetBlock(block.Coordinates.y / (int)map.Size.y);
            MeshRenderer renderer = block.GetComponent<MeshRenderer>();
            if (block != null && renderer != null && dist != null)
                renderer.material = dist.Material;
        }
    }
}
