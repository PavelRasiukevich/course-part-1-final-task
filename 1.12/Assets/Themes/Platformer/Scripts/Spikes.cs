using UnityEngine;

namespace UnityBase.Platformer
{
    public class Spikes : MonoBehaviour
    {
        private void OnTriggerEnter2D(Collider2D collision)
        {
            IKillable killable = collision.GetComponent<IKillable>();

            killable.Die();
        }
    }
}