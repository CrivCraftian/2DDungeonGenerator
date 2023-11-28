using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public struct Triangle
{
    public Triangle(Vector2 Point1, Vector2 Point2, Vector2 Point3)
    {
        p1 = Point1;
        p2 = Point2;
        p3 = Point3;

        edges = new Edge[3] { new Edge(p3, p1), new Edge(p1, p2), new Edge(p2, p3)};
    }

    public Vector2 p1 { get; set; }
    public Vector2 p2 { get; set; }
    public Vector2 p3 { get; set; }

    public Edge[] edges;
}

public struct Edge
{
    public Edge(Vector2 point1, Vector2 point2)
    {
        p1 = point1;
        p2 = point2;
    }

    public Vector2 p1 { get; set; }
    public Vector2 p2 { get; set; }

    public static bool IsEqual(Edge e1, Edge e2)
    {
        if (e1.p1 == e2.p1 && e1.p2 == e2.p2 || e1.p1 == e2.p2 && e1.p2 == e2.p1)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

public static class DelaunayTriangulation
{
    public static Vector2[] RoomsToPoints(List<Room> rooms)
    {
        Vector2[] Points = new Vector2[rooms.Count];

        for (int i = 0; i < rooms.Count; i++)
        {
            Points[i] = rooms[i].roomPosition;
        }

        return Points;
    }

    public static List<Triangle> Triangulate(Vector2[] Points)
    {
        Triangle supraTriangle = CreateSupraTriangle(Points);

        HashSet<Triangle> triangles = new HashSet<Triangle>();
        List<Triangle> triToTrim = new List<Triangle>();
        triangles.Add(supraTriangle);

        foreach (Vector2 point in Points)
        {
            List<Triangle> badTriangles = new List<Triangle>();
            List<Edge> currentEdges=new List<Edge>();
            List<Edge> polygon = new List<Edge>();

            foreach(Triangle triangle in triangles)
            {
                Vector2 currentCircumCirclePoint = CalculateCircumcenter(triangle);
                float radius = CalculateCircumRadius(triangle, currentCircumCirclePoint);

                float distanceFromCircumcircle = Vector2.Distance(point, currentCircumCirclePoint);
                if (distanceFromCircumcircle < radius)
                {
                    badTriangles.Add(triangle);
                }
            }

            foreach(Triangle tri in badTriangles)
            {
                currentEdges.AddRange(tri.edges);
            }

            foreach(Triangle triangle in badTriangles)
            {
                foreach(Edge triEdge in triangle.edges)
                {
                    int appearanceCount = 0;
                    for(int i = 0; i < currentEdges.Count; i++)
                    {
                        if (Edge.IsEqual(currentEdges[i], triEdge))
                        {
                            appearanceCount++;
                        }
                    }
                    if(appearanceCount < 2) 
                    {
                        polygon.Add(triEdge);
                    }
                }
            }

            foreach(Triangle tri in badTriangles)
            {
                triangles.Remove(tri);
            }

            foreach(Edge edge in polygon)
            {
                Triangle newTri = new Triangle(edge.p1, point, edge.p2);
                triangles.Add(newTri);
            }

        }

        foreach (Triangle tri in triangles)
        {
            if (tri.p1 == supraTriangle.p1 || tri.p2 == supraTriangle.p1 || tri.p3 == supraTriangle.p1)
            {
                triToTrim.Add(tri);
            }
            if (tri.p1 == supraTriangle.p2 || tri.p2 == supraTriangle.p2 || tri.p3 == supraTriangle.p2)
            {
                triToTrim.Add(tri);
            }
            if (tri.p1 == supraTriangle.p3 || tri.p2 == supraTriangle.p3 || tri.p3 == supraTriangle.p3)
            {
                triToTrim.Add(tri);
            }
        }

        foreach(Triangle tri in triToTrim)
        {
            triangles.Remove(tri);
        }

        return triangles.ConvertTo<List<Triangle>>();
    }

    static float CalculateCircumRadius(Triangle triangle, Vector2 CircumCircle)
    {
        return Vector2.Distance(triangle.p1, CircumCircle);
    }

    static Vector2 CalculateCircumcenter(Triangle triangle)
    {
        float x1 = triangle.p1.x;
        float y1 = triangle.p1.y;
        float x2 = triangle.p2.x;
        float y2 = triangle.p2.y;
        float x3 = triangle.p3.x;
        float y3 = triangle.p3.y;

        float D = 2 * (x1 * (y2 - y3) + x2 * (y3 - y1) + x3 * (y1 - y2));

        float Ux = ((x1 * x1 + y1 * y1) * (y2 - y3) + (x2 * x2 + y2 * y2) * (y3 - y1) + (x3 * x3 + y3 * y3) * (y1 - y2)) / D;

        float Uy = ((x1 * x1 + y1 * y1) * (x3 - x2) + (x2 * x2 + y2 * y2) * (x1 - x3) + (x3 * x3 + y3 * y3) * (x2 - x1)) / D;

        return new Vector2((float)Ux, (float)Uy);
    }

    static Triangle CreateSupraTriangle(Vector2[] Points)
    {
        Bounds boundBox = new Bounds();

        Vector2 min = new Vector2(Mathf.Infinity, Mathf.Infinity);
        Vector2 max = new Vector2(Mathf.NegativeInfinity, Mathf.NegativeInfinity);

        foreach(Vector2 point in Points)
        {
            if(point.x > max.x)
            {
                max.x = point.x;
            }
            if (point.y > max.y)
            {
                max.y = point.y;
            }
            if (point.x < min.x)
            {
                min.x = point.x;
            }
            if (point.y < min.y)
            {
                min.y = point.y;
            }
        }
        boundBox.Encapsulate(new Vector3(max.x, max.y, 1));

        return new Triangle(new Vector2(boundBox.center.x - boundBox.extents.x - boundBox.size.x, boundBox.center.y - boundBox.extents.y),
            new Vector2(boundBox.center.x + boundBox.extents.x + boundBox.size.x, boundBox.center.y - boundBox.extents.y),
            new Vector2(boundBox.center.x, boundBox.center.y + boundBox.center.y + boundBox.extents.y));
    }
}
