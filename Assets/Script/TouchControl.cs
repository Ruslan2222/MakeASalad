using System.Collections;
using UnityEngine;
using DG.Tweening;
using Lofelt.NiceVibrations;

public class TouchControl : MonoBehaviour
{
    [SerializeField] private MeshCollider _graterCollider;

    public bool dragging;
    public bool isMoving;

    private Vector3 _fp;
    private Vector3 _lp;
    private float _dragDistance;

    private GameObject _vegan;

    private void Start()
    {
        _dragDistance = Screen.height * 2 / 100;
        DOTween.Init(false, false, LogBehaviour.Verbose).SetCapacity(5000, 10);
    }

    private void Update()
    {
        if (Input.touchCount > 0)
        {
            Touch touch = Input.touches[0];
            dragging = true;

            _vegan = GameObject.FindGameObjectWithTag("Vegan");

            if (touch.phase == TouchPhase.Began)
            {
                _fp = touch.position;
                _lp = touch.position;

                if (_vegan != null)
                {
                    MeshCollider veganColider = _vegan.GetComponent<MeshCollider>();
                    Vector3 veganTransform = _vegan.transform.position;
                    Vector3 closestPointVegan = veganColider.ClosestPoint(veganColider.bounds.extents);
                    Vector3 closestPointGrater = _graterCollider.ClosestPoint(_graterCollider.bounds.extents);
                    Vector3 distance = closestPointGrater - closestPointVegan;
                    _vegan = GameObject.FindGameObjectWithTag("Vegan");
                    _vegan.transform.DOMove(new Vector3(veganTransform.x + distance.x + 0.2f, veganTransform.y + distance.y + 0.5f, veganTransform.z + distance.z - 0.25f), 1f);
                }
            }

            if (touch.phase == TouchPhase.Moved)
            {
                _lp = touch.position;

                if (_vegan != null)
                {
                    if (Mathf.Abs(_lp.x - _fp.x) > _dragDistance || Mathf.Abs(_lp.y - _fp.y) > _dragDistance && dragging)
                    {
                        if (_lp.y > _fp.y)
                        {
                            isMoving = true;
                            _vegan.transform.DOKill();
                            _vegan.transform.DOMove(new Vector3(_vegan.transform.position.x + 0.05f, 3, _vegan.transform.position.z + 0.03f), 0.5f);
                        }
                        else
                        {
                            isMoving = true;
                            _vegan.transform.DOKill();
                            _vegan.transform.DOMove(new Vector3(_vegan.transform.position.x + 0.05f, 1.3f, _vegan.transform.position.z + 0.03f), 0.5f);
                        }
                    }
                }
            }

            if (touch.phase == TouchPhase.Stationary)
            {
                isMoving = false;
            }

            if (touch.phase == TouchPhase.Ended)
            {
                dragging = false;
                if (_vegan != null)
                {
                    _vegan.transform.DOKill();
                    _vegan.transform.DOMove(new Vector3(-0.338f, 2.4f, -2.89f), 0.6f);
                }
            }

        }
    }

}
