using UnityEngine;

public class Bowl : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.parent = transform;
    }
}
