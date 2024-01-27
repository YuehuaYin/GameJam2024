using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer : MonoBehaviour
{
    [SerializeField]
    private MeshFilter meshFilter;
    private Mesh mesh;

    public int targetLiquid = 30;
    public int currectLiquid;
    
    void Start()
    {
        mesh = new Mesh();
        
        Vector3[] verticies = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        verticies[0] = new Vector3(-0.283f, -0.49f);
        verticies[1] = new Vector3(-0.283f, -0.49f);
        verticies[2] = new Vector3(0.283f, -0.49f);
        verticies[3] = new Vector3(0.283f, -0.49f);

        uv[0] = new Vector2(0, 0);
        uv[1] = new Vector2(0, 1);
        uv[2] = new Vector2(1, 1);
        uv[3] = new Vector2(1, 0);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;

        triangles[3] = 0;
        triangles[4] = 2;
        triangles[5] = 3;

        mesh.vertices = verticies;
        mesh.uv = uv;
        mesh.triangles = triangles;

        meshFilter.mesh = mesh;

        StartCoroutine(Test());
    }

    private void addLiquid()
    {
        float x = (currectLiquid / targetLiquid) * (0.45f - 0.283f);
        float y = (currectLiquid / targetLiquid) *(0.26f + 0.49f);

        Vector3[] vertices = new Vector3[4];
        
        vertices[0] = new Vector3(-0.283f, -0.49f);
        vertices[1] = new Vector3(-x, y);
        vertices[2] = new Vector3(x, y);
        vertices[3] = new Vector3(0.283f, -0.49f);

        mesh.vertices = vertices;
        
        meshFilter.mesh = mesh;
        
        Debug.Log(x + "," + y);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Test()
    {
        while (currectLiquid < targetLiquid)
        {
            currectLiquid++;
            addLiquid();
            Debug.Log(currectLiquid);

            yield return null;
        }
    }
}
