using UnityEngine;

public class GameInit : MonoBehaviour
{
    public Material material;
    public GameObject debugObj;

    void Start()
    {
        GameObject gameObject = new GameObject("Mesh", typeof(MeshFilter), typeof(MeshRenderer));

        Mesh mesh = new MapMeshGenerator().Generate();
        mesh.RecalculateNormals();
        mesh.RecalculateTangents();

        gameObject.GetComponent<MeshFilter>().mesh = mesh;
        gameObject.GetComponent<MeshRenderer>().material = material;
    }

}