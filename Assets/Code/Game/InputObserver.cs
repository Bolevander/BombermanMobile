using System;
using UnityEngine;
using UnityStandardAssets.CrossPlatformInput;

namespace Bomberman
{
    internal sealed class InputObserver
    {
        private readonly Player _player;
        private Vector2 _inputAxis;

        public InputObserver(Player player)
        {
            _player = player;
        }

        public void SubscribeInput(ref Action updateAction)
        {
            updateAction += ObserveInput;
        }

        public void UnSubscribeInput(ref Action updateAction)
        {
            updateAction -= ObserveInput;
        }

        private void ObserveInput()
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
