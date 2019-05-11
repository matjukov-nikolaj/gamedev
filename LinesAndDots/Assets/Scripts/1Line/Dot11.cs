using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Dot11 : MonoBehaviour
{
    private LineRenderer lineRenderer;

    private bool isActive;

    private Vector3 prevPos;

    private Vector3 currPos;

    // Start is called before the first frame update
    void Start()
    {
        prevPos = transform.position;
        lineRenderer = this.GetComponent(typeof(LineRenderer)) as LineRenderer;
        lineRenderer.SetWidth(3, 3);
        isActive = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 pos = GetCurrentMousePosition().GetValueOrDefault();
            pos.z = 0;
            currPos = pos;
            lineRenderer.SetPosition(1, currPos);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.position != transform.position)
        {
            
        }

        if (isActive)
        {
            return;
        }
        prevPos = other.transform.position;

        prevPos = currPos;
        isActive = true;
    }

    private Vector3? GetCurrentMousePosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var plane = new Plane(Vector3.forward, Vector3.zero);

        float rayDistance;
        if (plane.Raycast(ray, out rayDistance))
        {
            Vector3 pos = ray.GetPoint(rayDistance);
            pos.z = 0;
            return pos;
        }

        return null;
    }
}