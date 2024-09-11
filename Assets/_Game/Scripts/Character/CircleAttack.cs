using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CircleAttack : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRenderer;
    private int steps = 200;

    public void ClearCircle()
    {
        lineRenderer.positionCount = 0;
    }
    public void DrawCircle(float radius)
    {
        lineRenderer.useWorldSpace = false;
        lineRenderer.positionCount = steps;
        for (int i = 0; i < steps; i++)
        {
            float distance = (float)i / steps ;
            float currentRadian = distance * 2 * Mathf.PI;

            float xScale = Mathf.Cos(currentRadian);
            float zScale = Mathf.Sin(currentRadian);

            float x = xScale * radius;
            float z = zScale * radius;

            Vector3 currentPos = new Vector3(x, 0f, z);

            lineRenderer.SetPosition(i, currentPos);
        }
    }
}
