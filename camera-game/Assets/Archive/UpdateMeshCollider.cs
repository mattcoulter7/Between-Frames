using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[RequireComponent(typeof(MeshCollider))]
public class UpdateMeshCollider : MonoBehaviour
{
    public SkinnedMeshRenderer meshRenderer;
    private MeshCollider meshCollider;
    private Mesh colliderMesh;
    private void Start()
    {
        colliderMesh = new Mesh();
        meshCollider = GetComponent<MeshCollider>();
        if (meshRenderer == null)
        {
            meshRenderer = GetComponentInChildren<SkinnedMeshRenderer>();
        }
    }
    private void Update()
    {
        meshRenderer.BakeMesh(colliderMesh);
        meshCollider.sharedMesh = colliderMesh;
    }
}
