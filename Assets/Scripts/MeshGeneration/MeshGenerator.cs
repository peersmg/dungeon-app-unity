using UnityEngine;

public class MeshGenerator : MonoBehaviour
{
    public Material material;

    void Start()
    {
        int[][] map = new int[4][];
        map[0] = new int[] { 1, 1, 1, 1 };
        map[1] = new int[] { 1, 0, 0, 1 };
        map[2] = new int[] { 1, 0, 0, 1 };
        map[3] = new int[] { 1, 1, 1, 1 };

        Mesh mesh = generateMeshMap(map, 4, 4);

        GameObject gameObject = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));

        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.GetComponent<MeshRenderer>().material = material;
    }

    private Mesh generateMeshMap(int[][] map, int mapWidth, int mapHeight)
    {
        int[, ] vertexMap = buildVertexMap(map, mapWidth, mapHeight);

        int vertexCount = sumArray(vertexMap);

        Vector3[] meshVertices = new Vector3[vertexCount];
        Vector2[] meshUvs = new Vector2[vertexCount];
        int[] meshTriangles = new int[(vertexCount / 4) * 6];
        printArray(vertexMap);

        Mesh mesh = new Mesh();

        mesh.vertices = meshVertices;
        mesh.uv = meshUvs;
        mesh.triangles = meshTriangles;

        return mesh;
    }

    private int sumArray(int[, ] arr)
    {
        var rowCount = arr.GetLength(0);
        var colCount = arr.GetLength(1);

        int total = 0;
        for (int row = 0; row < rowCount; row++)
        {
            for (int column = 0; column < colCount; column++)
            {
                total += arr[row, column];
            }
        }
        return total;
    }

    private int[, ] buildVertexMap(int[][] map, int mapWidth, int mapHeight)
    {
        int[, ] vertexMap = new int[mapHeight + 1, mapWidth + 1];

        for (int i = 0; i < mapHeight; i++)
        {
            for (int j = 0; j < mapWidth; j++)
            {
                bool topEdge = (i) + 1 > mapHeight + 1;
                bool rightEdge = (j) + 1 > mapWidth + 1;

                vertexMap[i, j] = vertexMap[i, j] | map[i][j];

                if (!topEdge)
                {
                    vertexMap[(i) + 1, j] = vertexMap[(i) + 1, j] | map[i][j];
                }

                if (!rightEdge)
                {
                    vertexMap[i, (j) + 1] = vertexMap[i, (j) + 1] | map[i][j];
                }

                if (!rightEdge && !topEdge)
                {

                    vertexMap[(i) + 1, (j) + 1] = vertexMap[(i) + 1, (j) + 1] | map[i][j];
                }
            }
        }

        return vertexMap;
    }

    private void printArray(int[, ] arr)
    {
        var rowCount = arr.GetLength(0);
        var colCount = arr.GetLength(1);
        for (int row = 0; row < rowCount; row++)
        {
            Debug.Log(string.Format("{0},{1},{2},{3},{4}\t", arr[row, 0], arr[row, 1], arr[row, 2], arr[row, 3], arr[row, 4]));
        }
    }

    private Mesh generateBasicMesh()
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