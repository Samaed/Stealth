using UnityEngine;
using System.Collections;
using System;

public class OrbitBehaviour : MonoBehaviour {

    public GameObject target;
    public AnimationCurve distanceOverOrbitDuration = AnimationCurve.Linear(0, 1, 1, 1);
    public Vector3 axis;
    public float speed = 1;
    public float time = 0;
    public bool lookAtTarget;
    private float magnitude;
    private Vector3 initialPosition;

    void Awake()
    {
        initialPosition = transform.position - target.transform.position;
        RotateOfAngle(time * 360);
    }

    void Update()
    {
        RotateOfAngle(Time.deltaTime);
	}

    private void RotateOfAngle(float angle)
    {
        transform.RotateAround(target.transform.position, axis, speed * angle);
        if (lookAtTarget)
            transform.LookAt(target.transform.position);
        transform.position = (transform.position - target.transform.position).normalized * distanceOverOrbitDuration.Evaluate(time) * initialPosition.magnitude + target.transform.position;
        time += speed * angle / 360;
        time %= 1;
    }
}
