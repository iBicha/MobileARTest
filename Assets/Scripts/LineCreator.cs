using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineCreator : MonoBehaviour
{
    public GameObject LinePrefab;

    public float simplifyTolerence = 0.001f;
    public float cameraToPointDistance = 0.2f;
    
    private Camera mainCamera;
    private LineRenderer currentLine;
    // Use this for initialization
    void Start()
    {
        Input.simulateMouseWithTouches = true;
        mainCamera = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var lineObject = Instantiate(LinePrefab, transform.parent);
            currentLine = lineObject.GetComponent<LineRenderer>();
            AddPointToCurrentLine(Input.mousePosition);
        }
        else if (Input.GetMouseButton(0))
        {
            AddPointToCurrentLine(Input.mousePosition);
        }
        else if (Input.GetMouseButtonUp(0))
        {
            currentLine.Simplify(simplifyTolerence);
        }
    }

    void AddPointToCurrentLine(Vector3 screenPoint)
    {
        var ray = mainCamera.ScreenPointToRay(screenPoint);
        var point = ray.GetPoint(cameraToPointDistance);
        currentLine.positionCount++;
        currentLine.SetPosition(currentLine.positionCount - 1, point);
    }
}