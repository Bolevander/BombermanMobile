using UnityEngine;

namespace Bomberman
{
    internal sealed class Explosion : MonoBehaviour
    {
        public float Delay = 0.5f;

        private void Start()
        {
            Destroy(gameObject, Delay);
        }
    }
}
