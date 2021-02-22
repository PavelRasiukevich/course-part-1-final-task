using UnityEngine;

namespace UnityBase.Platformer
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Enemy : MonoBehaviour
    {
        private Rigidbody2D enemyRb2D;
        private Animator enemyAnimator;
        private SpriteRenderer enemyRenderer;

        public Rigidbody2D EnemyRb2D { get => enemyRb2D; }


        private void Awake()
        {
            enemyRb2D = GetComponent<Rigidbody2D>();
            enemyRb2D.gravityScale = 0;
            enemyAnimator = GetComponent<Animator>();
            enemyRenderer = GetComponent<SpriteRenderer>();
        }

        private void Update()
        {
            if (enemyRb2D.velocity.x > 0)
                enemyRenderer.flipX = true;
            else
                enemyRenderer.flipX = false;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            IKillable killable = collision.GetComponent<IKillable>();

            killable.Die();
        }
    }
}