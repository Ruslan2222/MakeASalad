using System.Collections;
using UnityEngine;
using MeshMakerNamespace;
using Lofelt.NiceVibrations;

[RequireComponent(typeof(Rigidbody))]
public class Rubbing : MonoBehaviour
{
    [Header("Chips")]
    [Space]
    [SerializeField] private GameObject[] _chips;

    private UpdateCollider _updateCollider;
    private Dragging _dragging;
    private RandomGoal _randomGoal;

    private void Awake()
    {
        _dragging = FindObjectOfType<Dragging>();
        _randomGoal = FindObjectOfType<RandomGoal>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Carrot carrot))
        {
            _updateCollider = carrot.updateCollider;

            StartCoroutine(Sub(collision.gameObject, _updateCollider.meshFilter, _updateCollider, 0));
        }
        else if (collision.gameObject.TryGetComponent(out Tomato tomato))
        {
            _updateCollider = tomato.updateCollider;
            StartCoroutine(Sub(collision.gameObject, _updateCollider.meshFilter, _updateCollider, 1));
        }
        else if (collision.gameObject.TryGetComponent(out Cucumber cucumber))
        {
            _updateCollider = cucumber.updateCollider;
            StartCoroutine(Sub(collision.gameObject, _updateCollider.meshFilter, _updateCollider, 2));
        }
    }

    private IEnumerator Sub(GameObject subObject, MeshFilter objectMesh, UpdateCollider updateCollider, int chipsType)
    {

        if (_dragging.dragging && subObject.activeInHierarchy)
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);

            Instantiate(_chips[chipsType], new Vector3(0.9f, 1.95f, -2.39f), Quaternion.identity);

            objectMesh.mesh = CSG.Subtract(subObject, gameObject, true, true);

            subObject.GetComponent<MeshFilter>().sharedMesh = objectMesh.mesh;

            yield return new WaitForSeconds(0.5f);

            if (subObject != null)
            {
                updateCollider.GetMesh();
            }

        }
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.TryGetComponent(out Carrot carrot))
        {
            _randomGoal.CheckGoal("Carrot");
        }
        else if (other.gameObject.TryGetComponent(out Cucumber cucumber))
        {
            _randomGoal.CheckGoal("Cucumber");
        }
        else if (other.gameObject.TryGetComponent(out Tomato tomato))
        {
            _randomGoal.CheckGoal("Tomato");
        }

        HapticPatterns.PlayPreset(HapticPatterns.PresetType.SoftImpact);

    }

}
