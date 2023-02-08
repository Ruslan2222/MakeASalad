using System.Collections;
using UnityEngine;
using MeshMakerNamespace;
using Lofelt.NiceVibrations;

[RequireComponent(typeof(Rigidbody))]
public class Rubbing : MonoBehaviour
{
    private UpdateCollider _updateCollider;
    private TouchControl _touchControl;
    private Levels _levels;
    private Vegetables _vegetables;

    private bool _isRubbing;

    private void Awake()
    {
        _touchControl = FindObjectOfType<TouchControl>();
        _levels = FindObjectOfType<Levels>();
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.TryGetComponent(out Vegetables vegetables) && _touchControl.isMoving && !_isRubbing)
        {
            _updateCollider = vegetables.updateCollider;
            _vegetables = vegetables;
            StartCoroutine(Sub(collision.gameObject, _updateCollider.meshFilter, _updateCollider.meshCollider, _vegetables.chips));
            _isRubbing = true;
        }
    }

    public IEnumerator Sub(GameObject subObject, MeshFilter objectMesh, MeshCollider meshCollider, GameObject chips)
    {

        if (_touchControl.dragging && subObject.activeInHierarchy)
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.Selection);

            Instantiate(chips, new Vector3(0.9f, 1.95f, -2.39f), Quaternion.identity);

            Mesh mesh = CSG.Subtract(_vegetables.gameObject, gameObject, true, true);
            objectMesh.sharedMesh = mesh;
            if (_vegetables != null)
            {
                meshCollider.sharedMesh = objectMesh.mesh;
            }

            yield return new WaitForSeconds(0.5f);

            _isRubbing = false;

        }

    }

    private void OnTriggerEnter(Collider other)
    {

        if (other.gameObject.TryGetComponent(out Vegetables vegetables))
        {
            HapticPatterns.PlayPreset(HapticPatterns.PresetType.SoftImpact);
            _levels.CheckGoal(vegetables.name);
        }

    }

}
