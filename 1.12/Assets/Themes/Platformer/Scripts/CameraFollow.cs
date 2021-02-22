using UnityEngine;


namespace UnityBase.Platformer
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] float followSpeed;
        [SerializeField] Vector3 offset;
        [SerializeField] float minClamp, maxClamp;

        private Transform target;

        private void Start()
        {
            target = FindObjectOfType<Hero>().transform;
        }

        private void LateUpdate()
        {
            Vector3 smoothPosition = Vector3.Lerp(transform.position, target.position + offset, followSpeed * Time.deltaTime);
            smoothPosition.x = Mathf.Clamp(smoothPosition.x, minClamp, maxClamp);
            transform.position = smoothPosition;
        }
    }
}