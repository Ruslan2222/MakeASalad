using UnityEngine;

public class Cucumber : MonoBehaviour
{
    private UpdateCollider _updateCollider;
    private Dragging _dragging;

    public UpdateCollider updateCollider => _updateCollider;
    public Dragging dragging => _dragging;

    private void Awake()
    {
        _updateCollider = GetComponent<UpdateCollider>();
        _dragging = GetComponent<Dragging>();
    }
}
