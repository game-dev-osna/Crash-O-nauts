//
//  ProceduralCylinder.cs
//
//  Based on https://github.com/doukasd/Unity-Components/blob/master/ProceduralCylinder/Assets/Scripts/Procedural/ProceduralCylinder.cs by Dimitris Doukas.
//

using UnityEngine;
using System.Collections;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]


public class ProceduralCylinder : MonoBehaviour
{

    //constants
    private const int DEFAULT_RADIAL_SEGMENTS = 512;
    private const int DEFAULT_HEIGHT_SEGMENTS = 1;
    private const int MIN_RADIAL_SEGMENTS = 3;
    private const int MIN_HEIGHT_SEGMENTS = 1;
    private const float DEFAULT_HEIGHT = 10f;

    //public variables
    [SerializeField]
    private int radialSegments = DEFAULT_RADIAL_SEGMENTS;

    [SerializeField]
    private int heightSegments = DEFAULT_HEIGHT_SEGMENTS;

    [SerializeField]
    public float maxRadius = 100;
    [SerializeField]
    public float minRadius = 75;

    //private variables
    private Mesh modelMesh;
    private MeshFilter meshFilter;
    private int numVertexRows;    //columns and rows of vertices
    private float length = DEFAULT_HEIGHT;

    public void AssignDefaultShader()
    {
        //assign it a white Diffuse shader, it's better than the default magenta
        MeshRenderer meshRenderer = (MeshRenderer)gameObject.GetComponent<MeshRenderer>();
        meshRenderer.sharedMaterial = new Material(Shader.Find("Diffuse"));
        meshRenderer.sharedMaterial.color = Color.green;
    }

    public void Rebuild()
    {
        //create the mesh
        modelMesh = new Mesh();
        modelMesh.name = "ProceduralPlanet";
        meshFilter = (MeshFilter)gameObject.GetComponent<MeshFilter>();
        meshFilter.mesh = modelMesh;
        SetColliderMesh();

        //sanity check
        if (radialSegments < MIN_RADIAL_SEGMENTS) radialSegments = MIN_RADIAL_SEGMENTS;
        if (heightSegments < MIN_HEIGHT_SEGMENTS) heightSegments = MIN_HEIGHT_SEGMENTS;

        //calculate how many vertices we need
        numVertexRows = heightSegments + 1;

        //calculate sizes
        int numVertices = radialSegments * numVertexRows + 1;
        int numUVs = numVertices;                                   //always
        int numSideTris = radialSegments * heightSegments * 2;      //for one cap
        int numCapTris = radialSegments;                        //fact
        int trisArrayLength = (numSideTris + numCapTris) * 3;   //3 places in the array for each tri

        //optional: log the number of tris
        //Debug.Log ("CustomCylinder has " + trisArrayLength/3 + " tris");

        //initialize arrays
        Vector3[] Vertices = new Vector3[numVertices];
        Vector2[] UVs = new Vector2[numUVs];
        int[] Tris = new int[trisArrayLength];

        //precalculate increments to improve performance
        float heightStep = length / heightSegments;
        float angleStep = 2 * Mathf.PI / radialSegments;
        float uvStepH = 1.0f / radialSegments;
        float uvStepV = 1.0f / heightSegments;

        float radiusDiff = maxRadius - minRadius;

        float PerlinRow = Random.Range(0f, 1f);
        float SecondRow = Random.Range(0f, 1f);
        float ThirdRow = Random.Range(0f, 1f);
        float FourthRow = Random.Range(0f, 1f);

        float max;
        float min = max = Mathf.PerlinNoise(0f, PerlinRow);

        float[] radians = new float[radialSegments];

        for (int i = 0; i < radialSegments; i++)
        {
            radians[i] = Mathf.PerlinNoise(i * (1f / radialSegments), PerlinRow);
            radians[i] += Mathf.PerlinNoise((i * (3f / radialSegments)%1), SecondRow);
            radians[i] += Mathf.PerlinNoise((i * (10f / radialSegments)%1), ThirdRow);
            radians[i] += Mathf.PerlinNoise((i * (17f / radialSegments)%1), FourthRow);

            min = Mathf.Min(min, radians[i]);
            max = Mathf.Max(max, radians[i]);
        }

        float realMax = minRadius + radiusDiff * max;

        for (int j = 0; j < numVertexRows; j++)
        {
            for (int i = 0; i < radialSegments; i++)
            {
                float radius = minRadius + radiusDiff * radians[i];

                if (j == 0)
                {

                    float angle = i * angleStep;
                    Vertices[j * radialSegments + i] = new Vector3(radius * Mathf.Cos(angle), j * heightStep-heightStep/2, radius * Mathf.Sin(angle));
                }
                else
                    Vertices[j * radialSegments + i] = Vertices[i] + new Vector3(0, j * heightStep, 0);

                //calculate UVs
                UVs[j * radialSegments + i] = new Vector2(radius / realMax, radius / realMax);

                if(j>0)
                {
                    //create 2 tris below each vertex
                    //6 seems like a magic number. For every vertex we draw 2 tris in this for-loop, therefore we need 2*3=6 indices in the Tris array
                    //offset the base by the number of slots we need for the bottom cap tris. Those will be populated once we draw the cap
                    int baseIndex = numCapTris * 3 + (j - 1) * radialSegments * 6 + i * 6;

                    //1st tri - below and in front
                    Tris[baseIndex + 0] = j * radialSegments + i;
                    Tris[baseIndex + 2] = (j - 1) * radialSegments + i;

                    //2nd tri - the one it doesn't touch
                    Tris[baseIndex + 3] = (j - 1) * radialSegments + i;
                    

                    if (i == radialSegments - 1)
                    {
                        Tris[baseIndex + 1] = j * radialSegments;

                        Tris[baseIndex + 4] = j * radialSegments;
                        Tris[baseIndex + 5] = (j - 1) * radialSegments;
                    }
                    else
                    {
                        Tris[baseIndex + 1] = j * radialSegments + i + 1;

                        Tris[baseIndex + 4] = j * radialSegments + i + 1;
                        Tris[baseIndex + 5] = (j - 1) * radialSegments + i + 1;
                    }
                }
                
            }
        }

        Vertices[numVertices-1] = Vector3.zero + new Vector3(0, (numVertexRows-1) * heightStep - heightStep / 2, 0);
        UVs[numVertices - 1] = Vector2.zero;


        //draw caps
        int topCapVertexOffset = numVertices - radialSegments;
        for (int i = 0; i < numCapTris; i++)
        {
            int bottomCapBaseIndex = i * 3;
            //int topCapBaseIndex = (numCapTris + numSideTris) * 3 + i * 3;

            //assign bottom tris
            //Tris[bottomCapBaseIndex + 0] = i;
            //Tris[bottomCapBaseIndex + 1] = i == numCapTris-1 ? 0 :  i +1 ;
            //Tris[bottomCapBaseIndex + 2] = numVertices - 1;

            //assign top tris
            Tris[bottomCapBaseIndex + 0] = i == numCapTris - 1 ? radialSegments : radialSegments + i + 1;
            Tris[bottomCapBaseIndex + 1] = radialSegments + i;
            Tris[bottomCapBaseIndex + 2] = numVertices - 1;
        }

        //assign vertices, uvs and tris
        modelMesh.vertices = Vertices;
        modelMesh.uv = UVs;
        modelMesh.triangles = Tris;

        modelMesh.RecalculateNormals();
        modelMesh.RecalculateBounds();
        //calculateMeshTangents(modelMesh);
    }

    // Recalculate mesh tangents
    // I found this on the internet (Unity forums?), I don't take credit for it.

    void calculateMeshTangents(Mesh mesh)
    {

        //speed up math by copying the mesh arrays
        int[] triangles = mesh.triangles;
        Vector3[] vertices = mesh.vertices;
        Vector2[] uv = mesh.uv;
        Vector3[] normals = mesh.normals;

        //variable definitions
        int triangleCount = triangles.Length;
        int vertexCount = vertices.Length;

        Vector3[] tan1 = new Vector3[vertexCount];
        Vector3[] tan2 = new Vector3[vertexCount];

        Vector4[] tangents = new Vector4[vertexCount];

        for (long a = 0; a < triangleCount; a += 3)
        {
            long i1 = triangles[a + 0];
            long i2 = triangles[a + 1];
            long i3 = triangles[a + 2];

            Vector3 v1 = vertices[i1];
            Vector3 v2 = vertices[i2];
            Vector3 v3 = vertices[i3];

            Vector2 w1 = uv[i1];
            Vector2 w2 = uv[i2];
            Vector2 w3 = uv[i3];

            float x1 = v2.x - v1.x;
            float x2 = v3.x - v1.x;
            float y1 = v2.y - v1.y;
            float y2 = v3.y - v1.y;
            float z1 = v2.z - v1.z;
            float z2 = v3.z - v1.z;

            float s1 = w2.x - w1.x;
            float s2 = w3.x - w1.x;
            float t1 = w2.y - w1.y;
            float t2 = w3.y - w1.y;

            float r = 1.0f / (s1 * t2 - s2 * t1);

            Vector3 sdir = new Vector3((t2 * x1 - t1 * x2) * r, (t2 * y1 - t1 * y2) * r, (t2 * z1 - t1 * z2) * r);
            Vector3 tdir = new Vector3((s1 * x2 - s2 * x1) * r, (s1 * y2 - s2 * y1) * r, (s1 * z2 - s2 * z1) * r);

            tan1[i1] += sdir;
            tan1[i2] += sdir;
            tan1[i3] += sdir;

            tan2[i1] += tdir;
            tan2[i2] += tdir;
            tan2[i3] += tdir;
        }

        for (long a = 0; a < vertexCount; ++a)
        {
            Vector3 n = normals[a];
            Vector3 t = tan1[a];

            //Vector3 tmp = (t - n * Vector3.Dot(n, t)).normalized;
            //tangents[a] = new Vector4(tmp.x, tmp.y, tmp.z);
            Vector3.OrthoNormalize(ref n, ref t);
            tangents[a].x = t.x;
            tangents[a].y = t.y;
            tangents[a].z = t.z;

            tangents[a].w = (Vector3.Dot(Vector3.Cross(n, t), tan2[a]) < 0.0f) ? -1.0f : 1.0f;
        }

        mesh.tangents = tangents;
    }

    void SetColliderMesh()
    {
        MeshCollider meshCollider = gameObject.GetComponent<MeshCollider>();
        if (meshCollider != null)
            meshCollider.sharedMesh = modelMesh;
    }

    void Awake()
    {
    }
    public void Init()
    {
        Rebuild();
        SetColliderMesh();

        PlanetFiller pf = GetComponent<PlanetFiller>();

        if (pf)
            pf.FillPlanet();

    }

}