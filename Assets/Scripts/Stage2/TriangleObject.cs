using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TriangleObject : MonoBehaviour
{
    public Triangle connectedTri;
    LineRenderer lineRenderer;

    public Vector2 pos1;
    public Vector2 pos2;
    public Vector2 pos3;

    public TriangleObject(Triangle tri)
    {
        connectedTri = tri;
    }

    private void Awake()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        pos1 = connectedTri.p1;
        pos2 = connectedTri.p2;
        pos3 = connectedTri.p3;

        lineRenderer.positionCount = 3;

        Vector3[] lineRendererPoints = new Vector3[3] { new Vector3(connectedTri.p1.x, connectedTri.p1.y, 1), new Vector3(connectedTri.p2.x, connectedTri.p2.y, 1), new Vector3(connectedTri.p3.x, connectedTri.p3.y, 1) } ;
        lineRenderer.SetPositions(lineRendererPoints);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
