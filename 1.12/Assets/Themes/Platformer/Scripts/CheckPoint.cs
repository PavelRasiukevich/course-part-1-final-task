using UnityBase.Platformer;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        Hero hero = other.GetComponent<Hero>();

        if (hero != null)
        {
            hero.PositionToRespawn = transform.position;
        }
    }
}
