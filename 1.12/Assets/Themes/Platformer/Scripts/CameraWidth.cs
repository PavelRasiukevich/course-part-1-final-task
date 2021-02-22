using UnityEngine;

namespace UnityBase.Platformer
{

    [ExecuteAlways]
    public class CameraWidth : MonoBehaviour
    {
        [SerializeField] float width;

        Camera targetCamera;

        private void OnEnable()
        {
            targetCamera = GetComponent<Camera>();
        }

        private void Update()
        {
            CheckAndCorrect();
        }

        private void CheckAndCorrect()
        {
            float cameraSize = width / Screen.width * Screen.height;
            targetCamera.orthographicSize = cameraSize;
        }
    }
}
