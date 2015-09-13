using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class MapGenerationPipeline : MonoBehaviour {

    protected Map map;

    [Serializable]
    public class Step
    {
        public string Name;
        public bool Active;
    }
    
    [HideInInspector]
    public List<Step> Steps = new List<Step> { new Step { Name = "Inactive" } };

    private List<MapGeneratorBehaviour>[] behaviours;

    void Awake() {
        map = GetComponent<Map>();
        enabled &= map != null;

        behaviours = new List<MapGeneratorBehaviour>[Steps.Count];

        MapGeneratorBehaviour[] generators = GetComponents<MapGeneratorBehaviour>();
        foreach (var generator in generators) {
            if (behaviours[generator.stepIndex] == null)
                behaviours[generator.stepIndex] = new List<MapGeneratorBehaviour>();
            behaviours[generator.stepIndex].Add(generator);
        }
    }

    void Start()
    {
        int stepCount = Steps.Count;
        for (int i = 0; i < stepCount; i++)
        {
            if (Steps[i].Active && behaviours[i] != null)
                foreach (var behaviour in behaviours[i])
                {
                    behaviour.Generation(map);
                }
        }
    }

}
