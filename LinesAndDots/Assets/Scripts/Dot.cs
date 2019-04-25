using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dot : MonoBehaviour
{
    private LineRenderer lineRenderer;
    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = this.GetComponent(typeof(LineRenderer)) as LineRenderer;
        lineRenderer.SetWidth(3, 3);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            Vector3 pos = GetCurrentMousePosition().GetValueOrDefault();
            pos.z = 0;
            lineRenderer.SetPosition(1, pos);
        }
    }

    void OnMouseDown()
    {
        Vector3 newPos = transform.position;
        newPos.z = 0;
        lineRenderer.SetPosition(0, newPos);
    }

    private Vector3? GetCurrentMousePosition()
    {
        var ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        var plane = new Plane(Vector3.forward, Vector3.zero);

        float rayDistance;
        if (plane.Raycast(ray, out rayDistance))
        {
            return ray.GetPoint(rayDistance);

        }

        return null;
    }
}