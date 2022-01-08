using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Bomberman
{
    internal sealed class InputManager
    {
        private readonly Player _player;
        private Vector2 _inputAxis;

        public InputManager(Player player)
        {
            Root.OnUpdate += Execute;
            _player = player;
        }

        public void Execute()
        {
            _inputAxis.x = CrossPlatformInputManager.GetAxis("Horizontal");
            _inputAxis.y = CrossPlatformInputManager.GetAxis("Vertical");

            if (_inputAxis.x != 0 || _inputAxis.y != 0)
            {
                _player.Move(_inputAxis);
            }

            if (Input.GetButtonUp("Bomb"))
            {
                _player.PlaceBomb();
            }
        }
    }
}
