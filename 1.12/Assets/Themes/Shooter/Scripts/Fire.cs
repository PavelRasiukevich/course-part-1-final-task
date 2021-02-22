using UnityEngine;

namespace UnityBase.Shooter
{
    public class Fire : MonoBehaviour
    {
        private new Camera camera;

        [SerializeField]
        GameObject projectilePrefab = null;

        private void Start()
        {
            camera = FindObjectOfType<Camera>();
        }

        private void Update()
        {

            if (!GameManager.IsGameOver)
                if (!GameManager.StopRotation)
                    if (Input.GetKeyDown(KeyCode.Mouse0))
                        FireProjectile();

        }

        private void FireProjectile()
        {
            Instantiate(projectilePrefab, camera.transform.GetChild(0).position, camera.transform.rotation);
        }
    }
}