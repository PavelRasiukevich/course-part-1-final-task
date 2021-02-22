using UnityEngine;

namespace UnityBase.Shooter
{
    public class Projectile : MonoBehaviour
    {
        private Rigidbody rb;

        [SerializeField]
        float force = 0;

        private void Start()
        {
            rb = GetComponent<Rigidbody>();
            Destroy(gameObject, 5.0f);
        }

        private void Update()
        {
            rb.AddForce(transform.forward * force, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision collision)
        {
            Destroy(gameObject);
        }

    }
}