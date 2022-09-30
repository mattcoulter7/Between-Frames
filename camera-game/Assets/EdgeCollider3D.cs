using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class EdgeCollider3D : MonoBehaviour
{
    public float depth = 10f;
    Vector2[] points;
    // Start is called before the first frame update
    void Awake()
    {
        EdgeCollider2D edgeCollider2D = gameObject.GetComponent<EdgeCollider2D>();
        if (edgeCollider2D == null) return;
        points = edgeCollider2D.points;
        DestroyImmediate(edgeCollider2D);

        MeshCollider meshCollider = gameObject.AddComponent<MeshCollider>();
        meshCollider.convex = true;
        meshCollider.isTrigger = true;

        List<Vector3> vertices = new List<Vector3>();
        foreach (Vector2 pt in points)
        {
            vertices.Add(new Vector3(pt.x, pt.y, 0));
            vertices.Add(new Vector3(pt.x, pt.y, depth));
        }
        List<int> triangles = new List<int>();
        for (int i = 0; i < points.Length; i++)
        {
            triangles.Add(i + 0);
            triangles.Add(i + 1);
            triangles.Add(i + 2);
        }

        Mesh mesh = new Mesh();
        mesh.vertices = vertices.ToArray();
        mesh.triangles = triangles.ToArray();
        mesh.RecalculateBounds();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        meshCollider.sharedMesh = mesh;
    }
}
