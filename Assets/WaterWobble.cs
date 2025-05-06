using UnityEngine;

public class WaterWobble : MonoBehaviour
{
    public float waveHeight = 0.1f;
    public float waveFrequency = 1f;

    private Vector3[] baseVertices;
    private Mesh mesh;

    void Start()
    {
        mesh = GetComponent<MeshFilter>().mesh;
        baseVertices = mesh.vertices;
    }

    void Update()
    {
        Vector3[] vertices = new Vector3[baseVertices.Length];
        for (int i = 0; i < vertices.Length; i++)
        {
            Vector3 vertex = baseVertices[i];
            vertex.y += Mathf.Sin(Time.time * waveFrequency + vertex.x + vertex.z) * waveHeight;
            vertices[i] = vertex;
        }
        mesh.vertices = vertices;
        mesh.RecalculateNormals();
    }
}
