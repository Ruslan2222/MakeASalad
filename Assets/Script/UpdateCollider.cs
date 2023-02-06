using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class UpdateCollider : MonoBehaviour
{
    private UI _gameUI;
    private VegetablesSpawn _vegetablesSpawn;

    private MeshFilter _meshFilter;
    private MeshCollider _meshCollider;

    public MeshFilter meshFilter => _meshFilter;

    private void Awake()
    {
        _gameUI = FindObjectOfType<UI>();
        _vegetablesSpawn = FindObjectOfType<VegetablesSpawn>();
        _meshFilter = GetComponent<MeshFilter>();
        _meshCollider = GetComponent<MeshCollider>();
    }

    public void GetMesh()
    {
        _meshCollider.sharedMesh = _meshFilter.mesh;
    }

    private void OnTriggerEnter(Collider other)
    {
        _vegetablesSpawn.ButtonAnim("Show");
        Destroy(gameObject);
    }
}
