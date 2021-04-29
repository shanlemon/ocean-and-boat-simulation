using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
public class PlaneGeneration : MonoBehaviour
{

    public int xSize = 20;

    public int zSize = 20;

    public float scale = 1.0f;


    private Mesh mesh;
    private Vector3[] vertices;
    private int[] triangles;
    private Vector2[] uvs;
    private int verticiesLength = 0;

    [SerializeField] private Transform debugSphere;
    [SerializeField] private bool isUpdatingOnCPU = false;


    // Start is called before the first frame update
    void Start()
    {
        mesh = new Mesh();
        GetComponent<MeshFilter>().mesh = mesh;
        verticiesLength = (xSize + 1) * (zSize + 1);

        UpdatePlaneVerticies();
        UpdateMesh();
    }

    private void FixedUpdate() {
        if (isUpdatingOnCPU)
        {
            UpdatePlaneVerticies();
            UpdateMesh();
        }

        if (debugSphere != null) 
        {
            Vector3 newPos = debugSphere.position;
            newPos.y = WaterController.current.getHeightAtPosition(newPos) + transform.position.y;
            debugSphere.position = newPos;
        }
    }

    void UpdatePlaneVerticies()
    {
        vertices = new Vector3[verticiesLength];
        uvs = new Vector2[vertices.Length];

        float halfSizeX = (scale * xSize) / 2;
        float halfSizeZ = (scale * zSize) / 2;

		int i = 0;
		for (int z = 0; z <= zSize; z++) 
        {
			for (int x = 0; x <= xSize; x++) 
            {
                float xPos = (x * scale) - halfSizeX;
                float zPos = (z * scale) - halfSizeZ;
                float yPos = 0;

				vertices[i] = new Vector3(xPos, yPos, zPos);
                
                if (isUpdatingOnCPU)
                    vertices[i] += WaterController.current.GetWaveAddition(vertices[i] + transform.position, Time.timeSinceLevelLoad);
				
                uvs[i] = new Vector2(vertices[i].x, vertices[i].z);
				i++;
			}
		}

        triangles = new int[xSize * zSize * 6];

		int vert = 0;
        int tris = 0;

		for (int z = 0; z < zSize; z++) 
        {
			for (int x = 0; x < xSize; x++) 
            {
				triangles[tris + 0] = vert + 0;
				triangles[tris + 1] = vert + xSize + 1;
				triangles[tris + 2] = vert + 1;
				triangles[tris + 3] = vert + 1;
				triangles[tris + 4] = vert + xSize + 1;
				triangles[tris + 5] = vert + xSize + 2;

				vert++;
				tris += 6;
			}
			vert++;
		}
    }

    void UpdateMesh()
    {
        mesh.Clear();
        mesh.vertices = vertices;
        mesh.triangles = triangles;
        mesh.uv = uvs;
        mesh.RecalculateNormals();
    }

}
