using UnityEngine;

public class Vegetables : MonoBehaviour
{
    [SerializeField] private GameObject _chips;

    public GameObject chips => _chips;

    private VegetablesSpawn _vegetablesSpawn;
    private UpdateCollider _updateCollider;

    public UpdateCollider updateCollider => _updateCollider;

    private void Awake()
    {
        _vegetablesSpawn = FindObjectOfType<VegetablesSpawn>();
        _updateCollider = FindObjectOfType<UpdateCollider>();
    }

    private void OnTriggerEnter(Collider other)
    {
        _vegetablesSpawn.ButtonAnim("Show");
        Destroy(gameObject);
    }

}
