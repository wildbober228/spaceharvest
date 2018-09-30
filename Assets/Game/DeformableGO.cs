using UnityEngine;

public class DeformableGO : MonoBehaviour
{
    public float maxDeformDelta = 1f;
    public float radius = 0.5f;
    MeshFilter mf;
    Vector3[] vertices;
    Transform trans;

    void Awake()
    {
        trans = GetComponent<Transform>();
        mf = gameObject.GetComponent<MeshFilter>();
        Mesh mesh = CopyMesh(mf.sharedMesh);
        mf.sharedMesh = mesh;
        vertices = mesh.vertices;
    }

    void PressMesh(Vector3 point, Vector3 dir)
    {
        var localPos = trans.InverseTransformPoint(point);

        for (int i = 0; i < vertices.Length; i++)
        {
            float distance = (localPos - vertices[i]).magnitude;

            if (distance <= radius)
            {
                vertices[i] += dir * maxDeformDelta;
            }
        }
    }

    void OnCollisionStay(Collision collision)
    {
        for (int i = 0; i < collision.contacts.Length; i++)
        {
            PressMesh(collision.contacts[i].point, collision.contacts[i].normal);
        }

        mf.sharedMesh.vertices = vertices;
        mf.sharedMesh.RecalculateNormals();
        mf.sharedMesh.RecalculateBounds();
    }

    Mesh CopyMesh(Mesh oldmesh)
    {
        Mesh newmesh = new Mesh();
        newmesh.vertices = oldmesh.vertices;
        newmesh.triangles = oldmesh.triangles;
        newmesh.uv = oldmesh.uv;
        newmesh.normals = oldmesh.normals;
        newmesh.tangents = oldmesh.tangents;
        newmesh.colors = oldmesh.colors;
        newmesh.bounds = oldmesh.bounds;
        newmesh.MarkDynamic();
        return newmesh;
    }
}