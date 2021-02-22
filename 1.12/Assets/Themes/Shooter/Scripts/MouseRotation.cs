using UnityEngine;

namespace UnityBase.Shooter
{
    public class MouseRotation : MonoBehaviour
    {
        private float x;
        private float y;

        [SerializeField] int rotationSpeed = 0, clampValue = 0;

        private void Start()
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }

        private void Update()
        {
            if (!GameManager.IsGameOver)
            {
                if (!GameManager.StopRotation)
                    RotateCamera();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameManager.StopRotation = !GameManager.StopRotation;

                if (GameManager.StopRotation)
                {
                    Cursor.lockState = CursorLockMode.None;
                    Cursor.visible = true;
                }
                else
                {
                    Cursor.lockState = CursorLockMode.Locked;
                    Cursor.visible = false;
                }
            }
        }

        private void RotateCamera()
        {
            y -= Input.GetAxis("Mouse Y") * rotationSpeed;

            x += Input.GetAxis("Mouse X") * rotationSpeed;

            y = Mathf.Clamp(y, -clampValue, clampValue);

            Vector3 rotation = new Vector3(y, x, 0.0f);

            transform.eulerAngles = rotation;
        }

        void OnGUI()
        {
            GUI.Label(new Rect(Screen.width / 2, Screen.height / 2, 100, 100), "+");
        }
    }
}