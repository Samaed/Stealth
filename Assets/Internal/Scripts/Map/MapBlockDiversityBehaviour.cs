using UnityEngine;
using System.Collections;
using System;

public class MapBlockDiversityBehaviour : MonoBehaviour
{

    public BlockDistribution[] distributions;

    void Awake()
    {
        GroundGeneratorBehaviour[] behaviours = GetComponents<GroundGeneratorBehaviour>();

        if (enabled)
            foreach (GroundGeneratorBehaviour behaviour in behaviours)
                behaviour.CreatedBlock += CustomizeBlock;
    }

    void Start()
    {

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

    public void CustomizeBlock(Map map, MapElement block)
    {
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
