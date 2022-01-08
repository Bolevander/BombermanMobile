using UnityEngine;

namespace Bomberman
{
    internal sealed class Bomb : MonoBehaviour
    {
        public AudioClip explosionSound;
        public Explosion explosionPrefab;
        public LayerMask undestroyableMask;
        private readonly float _timeToDetonate = 3f;
        private readonly float _explosionsCount = 3f;
        private readonly float _explodeOffset = 0f;

        public bool Detonating { get; private set; }
        public bool Exploded { get; private set; }

        private void OnEnable()
        {
            Detonating = true;
            Invoke(nameof(Detonate), _timeToDetonate);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (Exploded == false && collision.TryGetComponent(out Explosion explosion))
            {
                CancelInvoke(nameof(Detonate));
                Detonate();
            }
        }

        public void Restore()
        {
            Exploded = false;
        }

        private void Detonate()
        {
            if (explosionSound != null)
            {
                AudioSource.PlayClipAtPoint(explosionSound, transform.position);
            }

            Instantiate(explosionPrefab, transform.position, Quaternion.identity);

            CreateExplosions(Vector2.up);
            CreateExplosions(Vector2.right);
            CreateExplosions(Vector2.down);
            CreateExplosions(Vector2.left);

            Exploded = true;
            gameObject.SetActive(false);
        }

        private void CreateExplosions(Vector2 direction)
        {
            for (int i = 1; i < _explosionsCount; i++)
            {
                Vector2 position = new Vector2(transform.position.x, transform.position.y);
                RaycastHit2D hit = Physics2D.Raycast(position, direction, i + _explodeOffset, undestroyableMask);
                if (hit.collider == false)
                {
                    Instantiate(explosionPrefab, position + (i + _explodeOffset) * direction, Quaternion.identity);
                }
                else
                {
                    break;
                }
            }
        }
    }
}
