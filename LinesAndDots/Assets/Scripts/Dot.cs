using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Dot : MonoBehaviour
{
   
    private LineRenderer lineRenderer;

    public bool isActive;

    public bool isLineOn;

    public bool isTrigger = true;

    public bool win;

    // Start is called before the first frame update
    void Start()
    {
        lineRenderer = this.GetComponent(typeof(LineRenderer)) as LineRenderer;
        lineRenderer.SetWidth(3, 3);
        isActive = false;
        isLineOn = false;
        win = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (isLineOn)
        {
            return;
        }

        if (Input.GetMouseButton(0) && !GameObject.Find("Slider").GetComponent<Timer>().isGameOver)
        {
            Vector3 pos = GetCurrentMousePosition().GetValueOrDefault();
            pos.z = 0;
            if (isActive)
            {
                lineRenderer.SetPosition(1, pos);
                if (!win)
                {
                    Scene scene = SceneManager.GetActiveScene();
                    if (scene.name == "SampleScene2")
                    {
                        GameObject slider = GameObject.Find("Slider");
                        Timer timer = slider.GetComponent<Timer>();
                        win = timer.GetGameResult();
                        if (win && timer.timeLeft > (timer.time / 3))
                        {
                            timer.GameOver();
                            win = true;
                        }
                        else if (win && timer.timeLeft > 0)
                        {
                            timer.GameOver();
                            win = true;
                        }
                    }
                }
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isActive)
        {
            return;
        }

        Vector3 pos = transform.position;
        pos.z = 0;
        lineRenderer.SetPosition(0, pos);
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