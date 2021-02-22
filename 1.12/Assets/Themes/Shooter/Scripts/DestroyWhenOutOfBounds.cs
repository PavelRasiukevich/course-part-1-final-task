using UnityEngine;

namespace UnityBase.Shooter
{
    public class DestroyWhenOutOfBounds : MonoBehaviour
    {
        private void OnTriggerExit(Collider other)
        {
            if (other.GetComponent<Figure>())
            {
                Destroy(other.gameObject, 2.5f);
            }
        }
    }
}