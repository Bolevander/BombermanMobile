using System.Collections.Generic;
using UnityEngine;

namespace Bomberman
{
    internal class BasePatrol : IPatrolBehaviour
    {
        private readonly float _approximationThreshold = 0.1f;
        private readonly float _moveSpeed;
        private Rigidbody2D _body;
        private WayPoint _lastWaypoint;
        private WayPoint _currentWaypoint;

        public BasePatrol(WayPoint startWaypoint, float speed, Rigidbody2D body)
        {
            _currentWaypoint = startWaypoint;
            _moveSpeed = speed;
            _body = body;
        }

        public void Patrol()
        {
            if (_currentWaypoint != null)
            {
                Vector3 direction = _currentWaypoint.transform.position - _body.transform.position;
                if (direction.sqrMagnitude < _approximationThreshold)
                {
                    _currentWaypoint = RollWaypoint();
                }

                _body.velocity = Navigate(direction);
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
                    _body.transform.rotation = Quaternion.Euler(new Vector3(0, 0, 0));
                    rotate = new Vector2(_moveSpeed, _body.velocity.y);
                }
                else
                {
                    _body.transform.rotation = Quaternion.Euler(new Vector3(0, 180, 0));
                    rotate = new Vector2(-_moveSpeed, _body.velocity.y);
                }
            }

            return rotate;
        }

        private WayPoint RollWaypoint()
        {
            List<WayPoint> tempPool = new List<WayPoint>();
            WayPoint[] tempArray = new WayPoint[_currentWaypoint.borderWaypoints.Count];
            _currentWaypoint.borderWaypoints.CopyTo(tempArray);
            tempPool.AddRange(tempArray);
            int index = Mathf.RoundToInt(Random.Range(0, tempPool.Count - 1));
            WayPoint newWaypoint = tempPool[index];
            if (newWaypoint == _lastWaypoint)
            {
                tempPool.Remove(_lastWaypoint);
                index = Mathf.RoundToInt(Random.Range(0, tempPool.Count - 1));
                newWaypoint = tempPool[index];
            }

            _lastWaypoint = _currentWaypoint;
            return newWaypoint;
        }
    }
}
