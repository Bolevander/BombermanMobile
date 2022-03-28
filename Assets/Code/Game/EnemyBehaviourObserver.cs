using System;
using System.Collections.Generic;
using UnityEngine;

namespace Bomberman
{
    internal class EnemyBehaviourObserver
    {
        private readonly List<Enemy> _enemies;

        public EnemyBehaviourObserver()
        {
            _enemies = new List<Enemy>();
            _enemies.AddRange(GameObject.FindObjectsOfType<Enemy>());
        }

        public void SubscribeEnemies(ref Action updateAction)
        {
            foreach (Enemy enemy in _enemies)
            {
                updateAction += enemy.Patrol;
            }
        }

        public void UnSubscribeEnemies(ref Action updateAction)
        {
            foreach (Enemy enemy in _enemies)
            {
                updateAction -= enemy.Patrol;
            }
        }
    }
}
