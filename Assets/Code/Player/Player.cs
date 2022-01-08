using System;
using UnityEngine;

namespace Bomberman
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal sealed class Player : MonoBehaviour
    {
        public event Action OnDied;

        [SerializeField] private BombPool _bombsPool;
        [SerializeField] private Rigidbody2D _body;
        [SerializeField] private float _moveSpeed;

        private readonly float _bombYOffset = 0.3f;
        private bool _isDead;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (_isDead == false && (collision.TryGetComponent(out Explosion explosion) ||
                collision.TryGetComponent(out Enemy enemy)))
            {
                _isDead = true;
                OnDied?.Invoke();
            }
        }

        public void Move(Vector2 direction)
        {
            if (_body != null)
            {
                _body.velocity = Navigate(direction);
            }
        }

        public void PlaceBomb()
        {
            Bomb bomb = _bombsPool.TakeOne();
            if (bomb != null)
            {
                Vector3 position = new Vector3(transform.position.x, transform.position.y + _bombYOffset);
                bomb.transform.position = position;
                bomb.gameObject.SetActive(true);
            }
        }

        private Vector2 Navigate(Vector2 direction)
        {
            Vector2 rotate;
            if (Mathf.Abs(direction.y) >= Mathf.Abs(direction.x))
            {
                if (direction.y > 0)
                {
                    rotate = new Vector2(_body.velocity.x, _moveSpeed);
                }
                else
                {
                    rotate = new Vector2(_body.velocity.x, -_moveSpeed);
                }
            }
            else
            {
                if (direction.x > 0)
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    rotate = new Vector2(_moveSpeed, _body.velocity.y);
                }
                else
                {
                    transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    rotate = new Vector2(-_moveSpeed, _body.velocity.y);
                }
            }

            return rotate;
        }
    }
}
