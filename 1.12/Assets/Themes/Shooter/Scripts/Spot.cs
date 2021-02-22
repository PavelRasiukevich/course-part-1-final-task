using UnityEngine;

namespace UnityBase.Shooter
{
    public class Spot : MonoBehaviour
    {
        private new Light light;

        private void Awake()
        {
            light = GetComponent<Light>();
            light.spotAngle = 0;

        }

        private void Start()
        {

        }

        private void Update()
        {
            if (light.spotAngle < 100)
            {
                light.spotAngle += 1;
            }
        }
    }
}