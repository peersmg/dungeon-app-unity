using UnityEngine;

struct VertexPackage
{
    public int[, ] vertexIndexMap;
    public Vector3[] verteces;
    public VertexPackage(int[, ] vertexIndexMap, Vector3[] verteces)
    {
        this.vertexIndexMap = vertexIndexMap;
        this.verteces = verteces;
    }
}

public class MapMeshGenerator
{
    readonly int[, ] map = new int[, ] { { 1, 1, 1, 1, 1 }, { 1, 0, 1, 0, 1 }, { 1, 0, 0, 0, 1 }, { 1, 1, 1, 1, 1 } };
    public Mesh Generate()
    {
        return generateMeshMap();
    }

    private Mesh generateMeshMap()
    {
        int mapWidth = map.GetLength(1);
        int mapHeight = map.GetLength(0);

        int[, ] vertexMap = buildVertexMap(map, mapWidth, mapHeight);

        int vertexCount = sumArray(vertexMap);

        VertexPackage vertexPackage = generateVertices(vertexMap, mapWidth, mapHeight);

        Vector3[] meshVertices = vertexPackage.verteces;
        Vector2[] meshUvs = generateUvs(vertexPackage.verteces);
        int[] meshTriangles = generateTriangles(vertexPackage.vertexIndexMap);

        Mesh mesh = new Mesh();

        mesh.vertices = meshVertices;
        mesh.uv = meshUvs;
        mesh.triangles = meshTriangles;

        return mesh;
    }

    private VertexPackage generateVertices(int[, ] vertexGrid, int mapWidth, int mapHeight)
    {
        int vertexCount = sumArray(vertexGrid);
        Vector3[] meshVertices = new Vector3[vertexCount];
        int[, ] vertexIndexMap = new int[mapHeight + 1, mapWidth + 1];

        int vertexIndex = 0;

        for (int row = 0; row < mapHeight; row++)
        {
            for (int col = 0; col < mapWidth; col++)
            {
                if (vertexGrid[row, col] == 1)
                {
                    meshVertices[vertexIndex] = new Vector3(col, row);
                    vertexGrid[row, col] = 0;
                    vertexIndexMap[row, col] = vertexIndex;

                    vertexIndex++;
                }

                if (vertexGrid[row + 1, col] == 1)
                {
                    meshVertices[vertexIndex] = new Vector3(col, row + 1);
                    vertexGrid[row + 1, col] = 0;
                    vertexIndexMap[row + 1, col] = vertexIndex;
                    vertexIndex++;
                }

                if (vertexGrid[row, col + 1] == 1)
                {
                    meshVertices[vertexIndex] = new Vector3(col + 1, row);
                    vertexGrid[row, col + 1] = 0;
                    vertexIndexMap[row, col + 1] = vertexIndex;
                    vertexIndex++;
                }

                if (vertexGrid[row + 1, col + 1] == 1)
                {
                    meshVertices[vertexIndex] = new Vector3(col + 1, row + 1);
                    vertexGrid[row + 1, col + 1] = 0;
                    vertexIndexMap[row + 1, col + 1] = vertexIndex;
                    vertexIndex++;
                }
            }
        }

        return new VertexPackage(vertexIndexMap, meshVertices);
    }

    private Vector2[] generateUvs(Vector3[] meshVertices)
    {
        Vector2[] meshUvs = new Vector2[meshVertices.Length];

        for (int i = 0; i < meshUvs.Length; i++)
        {
            meshUvs[i] = new Vector2(meshVertices[i].x, meshVertices[i].z);
        }

        return meshUvs;
    }

    private int[] generateTriangles(int[, ] vertexIndexMap)
    {
        int triCount = sumArray(map) * 6;
        int[] triangles = new int[triCount];
        int triIndex = 0;
        for (int row = 0; row < map.GetLength(0); row++)
        {
            for (int col = 0; col < map.GetLength(1); col++)
            {
                if (map[row, col] == 1)
                {
                    triangles[triIndex] = vertexIndexMap[row, col];
                    triangles[triIndex + 1] = vertexIndexMap[row + 1, col];
                    triangles[triIndex + 2] = vertexIndexMap[row, col + 1];

                    triangles[triIndex + 3] = vertexIndexMap[row + 1, col];
                    triangles[triIndex + 4] = vertexIndexMap[row + 1, col + 1];
                    triangles[triIndex + 5] = vertexIndexMap[row, col + 1];

                    triIndex += 6;
                }
            }
        }

        return triangles;
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

    private int[, ] buildVertexMap(int[, ] map, int mapWidth, int mapHeight)
    {
        int[, ] vertexMap = new int[mapHeight + 1, mapWidth + 1];

        for (int i = 0; i < mapHeight; i++)
        {
            for (int j = 0; j < mapWidth; j++)
            {
                bool topEdge = (i) + 1 > mapHeight + 1;
                bool rightEdge = (j) + 1 > mapWidth + 1;

                vertexMap[i, j] = vertexMap[i, j] | map[i, j];

                if (!topEdge)
                {
                    vertexMap[(i) + 1, j] = vertexMap[(i) + 1, j] | map[i, j];
                }

                if (!rightEdge)
                {
                    vertexMap[i, (j) + 1] = vertexMap[i, (j) + 1] | map[i, j];
                }

                if (!rightEdge && !topEdge)
                {

                    vertexMap[(i) + 1, (j) + 1] = vertexMap[(i) + 1, (j) + 1] | map[i, j];
                }
            }
        }

        return vertexMap;
    }

    public Mesh generateBasicMesh()
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