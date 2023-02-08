using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UpdateCollider : MonoBehaviour
{

    private MeshFilter _meshFilter;
    private MeshCollider _meshCollider;
    private BoxCollider _boxCollider;

    public MeshCollider meshCollider => _meshCollider;
    public MeshFilter meshFilter => _meshFilter;
    public BoxCollider boxCollider => _boxCollider;

    private void Awake()
    {
        _meshFilter = GetComponent<MeshFilter>();
        _meshCollider = GetComponent<MeshCollider>();
        _boxCollider = GetComponent<BoxCollider>();
    }

    public void GetMesh()
    {
        _meshCollider.sharedMesh = _meshFilter.mesh;
    }

}
