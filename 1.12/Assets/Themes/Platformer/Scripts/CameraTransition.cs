using UnityEngine;

public class CameraTransition : MonoBehaviour
{

    private Vector2 bounds;
    private BoxCollider2D[] colliders;

    private void Awake()
    {
        colliders = new BoxCollider2D[2];

        colliders = GetComponents<BoxCollider2D>();

        foreach (var item in colliders)
        {
            if (item.isTrigger)
            {
                bounds = item.bounds.size;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        IKillable _obj = other.GetComponent<IKillable>();

        if (_obj != null)
            transform.position = new Vector3(transform.position.x + bounds.x, transform.position.y, transform.position.z);
    }
}
