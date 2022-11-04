using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Quad3D : MonoBehaviour
{
    // square bracket operator with string argument
    public Vector3 this[string s]
    {
        get 
        {
            return (Vector3)this.GetType().GetField(s).GetValue(this);
        }
        set
        {
            this.GetType().GetField(s).SetValue(this,value);
        }
    }
    public Vector3 bottomLeftBack = new Vector3(-1, -1, 1);
    public Vector3 bottomRightBack = new Vector3(1, -1, 1);
    public Vector3 bottomRightFront = new Vector3(1, -1, -1);
    public Vector3 bottomLeftFront = new Vector3(-1, -1, -1);
    public Vector3 topleftBack = new Vector3(-1, 1, 1);
    public Vector3 topRightBack = new Vector3(1, 1, 1);
    public Vector3 topRightFront = new Vector3(1, 1, -1);
    public Vector3 topLeftFront = new Vector3(-1, 1, -1);
    private Mesh cubeMesh;
    public enum PointGroup {
        LEFT,
        BOTTOM,
        RIGHT,
        TOP,
        BACK,
        FRONT,
        LEFT_BOTTOM,
        RIGHT_BOTTOM
    }
    private Dictionary<PointGroup,string[]> pointGroups = new Dictionary<PointGroup, string[]>(){
        {PointGroup.LEFT,new string[4]{"bottomLeftBack","bottomLeftFront","topleftBack","topLeftFront"}},
        {PointGroup.BOTTOM,new string[4]{"bottomLeftBack","bottomRightBack","bottomRightFront","bottomLeftFront"}},
        {PointGroup.RIGHT,new string[4]{"bottomRightBack","bottomRightFront","topRightBack","topRightFront"}},
        {PointGroup.TOP,new string[4]{"topleftBack","topRightBack","topRightFront","topLeftFront"}},
        {PointGroup.BACK,new string[4]{"bottomLeftBack","bottomRightBack","topleftBack","topRightBack"}},
        {PointGroup.FRONT,new string[4]{"bottomRightFront","bottomLeftFront","topRightFront","topLeftFront"}},
        {PointGroup.LEFT_BOTTOM,new string[2]{"bottomLeftFront","bottomLeftBack"}},
        {PointGroup.RIGHT_BOTTOM,new string[2]{"bottomRightFront","bottomRightBack"}}
    };
    // Use this for initialization
    public void ShiftPoint(string point,Vector3 amount,bool refresh = true){
        this[point] += amount;
        if (refresh){
            Refresh();
        }
    }

    public void ShiftPoints(string[] points,Vector3 amount,bool refresh = true){
        foreach(string pt in points){
            ShiftPoint(pt,amount,false);
        }
        if (refresh){
            Refresh();
        }
    }

    public void ShiftGroup(PointGroup group,Vector3 amount,bool refresh = true){
        ShiftPoints(pointGroups[group],amount,refresh);
    }
    void Start()
    {
        Refresh();
    }

    void FixedUpdate(){
        if (Input.GetKey(KeyCode.DownArrow)){
            ShiftGroup(PointGroup.RIGHT_BOTTOM,new Vector3(0,-0.1f,0));
        }
    }

    void Refresh()
    {
        cubeMesh = GetMesh();
        cubeMesh.vertices = GetVertices();
        cubeMesh.normals = GetNormals();
        cubeMesh.uv = GetUVsMap();
        cubeMesh.triangles = GetTriangles();
        cubeMesh.RecalculateBounds();
        cubeMesh.RecalculateNormals();
        cubeMesh.Optimize();
        // ensure collider is set
        MeshCollider col = GetComponent<MeshCollider>();
        if (col){
            col.sharedMesh = cubeMesh;
        }
    }

    /// Get Vertices of the cube
    private Vector3[] GetVertices()
    {
        Vector3[] vertices = new Vector3[]
                {
				// Bottom Polygon
					bottomLeftBack, bottomRightBack, bottomRightFront, bottomLeftBack,
					
				// Left Polygon
					topLeftFront, topleftBack, bottomLeftBack, bottomLeftFront,
					
				// Front Polygon
					topleftBack, topRightBack, bottomRightBack, bottomLeftBack,
					
				// Back Polygon
					topRightFront, topLeftFront, bottomLeftFront, bottomRightFront,
					
				// Right Polygon
					topRightBack, topRightFront, bottomRightFront, bottomRightBack,
					
				// Top Polygon
					topLeftFront, topRightFront, topRightBack, topleftBack
                };

        return vertices;
    }
    /// The Cube Side Which Are Use when rendering Cube
    private Vector3[] GetNormals()
    {
        Vector3 up = Vector3.up;
        Vector3 down = Vector3.down;
        Vector3 front = Vector3.forward;
        Vector3 back = Vector3.back;
        Vector3 left = Vector3.left;
        Vector3 right = Vector3.right;

        Vector3[] normales = new Vector3[]
        {
				// Bottom Side Render
					down, down, down, down,
					
				// LEFT Side Render
					left, left, left, left,
					
				// FRONT Side Render
					front, front, front, front,
					
				// BACK Side Render
					back, back, back, back,
					
				// RIGTH Side Render
					right, right, right, right,
					
				// UP Side Render
					up, up, up, up
                };

        return normales;
    }
    /// Get the UVs Map of the cube
    private Vector2[] GetUVsMap()
    {
        Vector2 _00_CORDINATES = new Vector2(0f, 0f);
        Vector2 _10_CORDINATES = new Vector2(1f, 0f);
        Vector2 _01_CORDINATES = new Vector2(0f, 1f);
        Vector2 _11_CORDINATES = new Vector2(1f, 1f);

        Vector2[] uvs = new Vector2[]
                {
				// Bottom
					_11_CORDINATES, _01_CORDINATES, _00_CORDINATES, _10_CORDINATES,
					
				// Left
					_11_CORDINATES, _01_CORDINATES, _00_CORDINATES, _10_CORDINATES,
					
				// Front
					_11_CORDINATES, _01_CORDINATES, _00_CORDINATES, _10_CORDINATES,
					
				// Back
					_11_CORDINATES, _01_CORDINATES, _00_CORDINATES, _10_CORDINATES,
					
				// Right
					_11_CORDINATES, _01_CORDINATES, _00_CORDINATES, _10_CORDINATES,
					
				// Top
					_11_CORDINATES, _01_CORDINATES, _00_CORDINATES, _10_CORDINATES,
                };
        return uvs;
    }
    /// Get the triangle
    private int[] GetTriangles()
    {
        int[] triangles = new int[]
                {
				// Cube Bottom Side Triangles
					3, 1, 0,
                    3, 2, 1,			
					
				// Cube Left Side Triangles
					3 + 4 * 1, 1 + 4 * 1, 0 + 4 * 1,
                    3 + 4 * 1, 2 + 4 * 1, 1 + 4 * 1,
					
				// Cube Front Side Triangles
					3 + 4 * 2, 1 + 4 * 2, 0 + 4 * 2,
                    3 + 4 * 2, 2 + 4 * 2, 1 + 4 * 2,
					
				// Cube Back Side Triangles
					3 + 4 * 3, 1 + 4 * 3, 0 + 4 * 3,
                    3 + 4 * 3, 2 + 4 * 3, 1 + 4 * 3,
					
				// Cube Rigth Side Triangles
					3 + 4 * 4, 1 + 4 * 4, 0 + 4 * 4,
                    3 + 4 * 4, 2 + 4 * 4, 1 + 4 * 4,
					
				// Cube Top Side Triangles
					3 + 4 * 5, 1 + 4 * 5, 0 + 4 * 5,
                    3 + 4 * 5, 2 + 4 * 5, 1 + 4 * 5,

                };
        return triangles;
    }
    /// Gets the Mesh
    private Mesh GetMesh()
    {
        MeshFilter meshFilter = GetComponent<MeshFilter>();
        if (meshFilter == null){
            meshFilter = gameObject.AddComponent<MeshFilter>();
        }
        return meshFilter.mesh;
    }
}