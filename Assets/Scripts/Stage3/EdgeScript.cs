using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EdgeScript : MonoBehaviour
{
    public Edge currentEdge;
    private LineRenderer ln;


    private void Awake()
    {
        ln = GetComponent<LineRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        ln.positionCount = 2;
        Vector3[] points = new Vector3[2] { new Vector3(currentEdge.p1.x, currentEdge.p1.y, 1), new Vector3(currentEdge.p2.x, currentEdge.p2.y, 1) };
        ln.SetPositions(points);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
