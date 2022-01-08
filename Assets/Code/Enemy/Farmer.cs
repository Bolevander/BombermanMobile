﻿using System.Collections.Generic;
using UnityEngine;

namespace Bomberman
{
    internal sealed class Farmer : Enemy
    {
        private new void Start()
        {
            base.Start();
            _patrolBehaviour = new BasePatrol(_startWaypoint, _moveSpeed, _body);
            Root.OnUpdate += Patrol;
        }

        protected override void Patrol()
        {
            _patrolBehaviour.Patrol();
        }        
    }
}
