using System.Collections.Generic;
using UnityEngine;

namespace Bomberman
{
    [RequireComponent(typeof(Rigidbody2D))]
    internal abstract class Enemy : MonoBehaviour
    {
        [SerializeField] protected float _moveSpeed;
        [SerializeField] protected Rigidbody2D _body;
        [SerializeField] protected List<WayPoint> _waypoints;        
        [SerializeField] protected WayPoint _startWaypoint;
        
        protected IPatrolBehaviour _patrolBehaviour;
        protected bool _isDead;

        protected void Start()
        {
            transform.position = _startWaypoint.transform.position;
        }

        protected void OnTriggerEnter2D(Collider2D collision)
        {
            if (_isDead == false && collision.TryGetComponent(out Explosion explosion))
            {
                _isDead = true;
                gameObject.SetActive(false);
            }
        }

        protected abstract void Patrol();
    }
}
