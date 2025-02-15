﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class MultipleTargetCamera : MonoBehaviour
{
    //public List<Transform> targets;

    public Transform[] targets = new Transform[2];

    public Vector3 offset;
    public float smoothTime = .5f;

    public float minZoom = 40f;
    public float maxZoom = 10f;
    public float zoomLimiter = 50f;

    private Vector3 velocity;
    private Camera cam;

    public bool canZoom = true;

    void Start()
    {
        cam = GetComponent<Camera>();
        canZoom = true;
    }
    Vector3 temp = new Vector3(0, 28, -80);
    void LateUpdate()
    {
        if (!canZoom)
        {
            transform.position = temp;
            GetComponent<Camera>().fieldOfView = 13;
        }


        if (targets.Length == 0)
            return;

        if(canZoom)
        {
        Move();
        Zoom();
        }
    }

    void Zoom()
    {
        float newZoom = Mathf.Lerp(maxZoom, minZoom, GetGreatestDistance() / zoomLimiter);
        if (canZoom)
            cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, newZoom, Time.deltaTime);
    }

    void Move()
    {
        Vector3 centerPoint = GetCenterPoint();

        Vector3 newPosition = centerPoint + offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref velocity, smoothTime);
    }

    float GetGreatestDistance()
    {
        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i] != null)
                bounds.Encapsulate(targets[i].position);
        }

        return bounds.size.x;
    }

    Vector3 GetCenterPoint()
    {
        if (targets.Length == 1)
        {
            return targets[0].position;
        }

        var bounds = new Bounds(targets[0].position, Vector3.zero);
        for (int i = 0; i < targets.Length; i++)
        {
            if (targets[i] != null)
                bounds.Encapsulate(targets[i].position);
        }

        return bounds.center;
    }
    
    public void SetTarget(Transform t2 = null)
    {
        targets[1] = t2;
    }
}
