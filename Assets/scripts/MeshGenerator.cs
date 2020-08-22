using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    public Material material;

    void Start()
    {
        int[][] map = new int[5][];
        map[0] = new int[] { 1, 1, 1, 1, 1 };
        map[1] = new int[] { 1, 0, 0, 0, 1 };
        map[2] = new int[] { 1, 0, 0, 0, 1 };
        map[3] = new int[] { 1, 0, 0, 0, 1 };
        map[4] = new int[] { 1, 1, 1, 1, 1 };

        Mesh mesh = generateMesh(map);

        GameObject gameObject = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));

        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.GetComponent<MeshRenderer>().material = material;
    }

    Mesh generateMesh(int[][] map)
    {
        Vector3[] vertices = new Vector3[4];
        Vector2[] uv = new Vector2[4];
        int[] triangles = new int[6];

        vertices[0] = new Vector3(0, 1);
        vertices[1] = new Vector3(1, 1);
        vertices[2] = new Vector3(0, 0);
        vertices[3] = new Vector3(1, 0);

        uv[0] = new Vector2(0, 1);
        uv[1] = new Vector2(1, 1);
        uv[2] = new Vector2(0, 0);
        uv[3] = new Vector2(1, 0);

        triangles[0] = 0;
        triangles[1] = 1;
        triangles[2] = 2;
        triangles[3] = 2;
        triangles[4] = 1;
        triangles[5] = 3;

        Mesh mesh = new Mesh();

        mesh.vertices = vertices;
        mesh.uv = uv;
        mesh.triangles = triangles;

        return mesh;
    }
}