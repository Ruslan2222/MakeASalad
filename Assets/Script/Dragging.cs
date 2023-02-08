using UnityEngine;
using DG.Tweening;
using System.Collections;
using Lofelt.NiceVibrations;

public class Dragging : MonoBehaviour
{
    [SerializeField] private GameObject _tapToGrater;

    private float _dist;
    private bool _dragging = false;
    private Vector3 _offset;
    private Transform _toDrag;
    private GameObject gameObjectHit;
    public bool dragging => _dragging;

    private void Update()
    {
        Vector3 v3;

        if (Input.touchCount != 1)
        {
            _dragging = false;
            return;
        }

        Touch touch = Input.touches[0];
        Vector3 pos = touch.position;

        if (touch.phase == TouchPhase.Began)
        {
            Ray ray = Camera.main.ScreenPointToRay(pos);
            RaycastHit hit;

            if (Physics.Raycast(ray, out hit))
            {
                if (hit.collider.TryGetComponent(out Vegetables vegetables))
                {
                    gameObjectHit = hit.collider.gameObject;
                    _toDrag = hit.transform;
                    _dist = hit.transform.position.z - Camera.main.transform.position.z;
                    v3 = new Vector3(pos.x, pos.y, _dist);
                    v3 = Camera.main.ScreenToWorldPoint(v3);
                    _offset = _toDrag.position - v3;
                    _dragging = true;
                }
                else if (hit.collider.gameObject.name == "GraterCollider")
                {
                    StartCoroutine(WarningTap());
                }
            }
        }

        if (_dragging && touch.phase == TouchPhase.Moved && gameObjectHit != null )
        {
            v3 = new Vector3(Input.mousePosition.x, Input.mousePosition.y, _dist);
            v3 = Camera.main.ScreenToWorldPoint(v3);
            _toDrag.position = v3 + _offset;
        }

        if (_dragging && (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled))
        {
            _dragging = false;
        }

    }

    private IEnumerator WarningTap()
    {
        HapticPatterns.PlayPreset(HapticPatterns.PresetType.Warning);
        _tapToGrater.SetActive(true);
        Sequence sequence = DOTween.Sequence();
        sequence.Append(_tapToGrater.transform.DOLocalMoveY(0, 0.5f).From(1000));
        sequence.Insert(1, _tapToGrater.transform.DOLocalMoveY(1000, 0.5f));
        yield return new WaitForSeconds(1.3f);
        _tapToGrater.SetActive(false);
    }

}
