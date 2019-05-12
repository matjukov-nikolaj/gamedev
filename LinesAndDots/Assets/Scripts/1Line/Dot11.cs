using System;
using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;

public class Dot11 : MonoBehaviour
{
    private LineRenderer lineRenderer;

    public bool isActive;

    // Start is called before the first frame update
    void Start()
    {
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
            if(isActive) lineRenderer.SetPosition(1, pos);
        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActive)
        {
            return;
        }
		lineRenderer.SetPosition(0, transform.position);
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