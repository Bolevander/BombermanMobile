using System.Collections.Generic;
using UnityEngine;

namespace Bomberman
{
    internal sealed class BombPool : MonoBehaviour
    {
        [SerializeField] private List<Bomb> _bombs;

        public Bomb TakeOne()
        {
            for (int i = 0; i < _bombs.Count; i++)
            {
                if (_bombs[i].Exploded == false && _bombs[i].Detonating == false)
                {
                    return _bombs[i];
                }
            }

            return null;
        }

        //For bomb pickup
        public void RestoreOne()
        {
            for (int i = _bombs.Count - 1; i >= 0; i--)
            {
                if (_bombs[i].Exploded == true)
                {
                    _bombs[i].Restore();
                    return;
                }
            }
        }
    }
}
