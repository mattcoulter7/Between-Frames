using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateMeshCollider : MonoBehaviour
{
    private void unscaleMesh(Mesh mesh)
    {

        Vector3[] baseVertices = mesh.vertices;

        var vertices = new Vector3[baseVertices.Length];

        for (var i = 0; i < vertices.Length; i++)
        {
            var vertex = baseVertices[i];
            vertex.x = vertex.x * 1 / transform.localScale.x;
            vertex.y = vertex.y * 1 / transform.localScale.y;
            vertex.z = vertex.z * 1 / transform.localScale.z;

            vertices[i] = vertex;
        }

        mesh.vertices = vertices;

        mesh.RecalculateNormals();
        mesh.RecalculateBounds();
    }
    void Update()
    {
        SkinnedMeshRenderer skinnedMeshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        MeshCollider meshCollider = GetComponent<MeshCollider>();
        if (skinnedMeshRenderer && meshCollider)
        {
            Mesh colliderMesh = new Mesh();
            skinnedMeshRenderer.BakeMesh(colliderMesh);

            unscaleMesh(colliderMesh);

            meshCollider.sharedMesh = null;
            meshCollider.sharedMesh = colliderMesh;
        }
    }
}
