using System;
using UnityEngine;

namespace Bomberman
{
    internal sealed class Root : MonoBehaviour
    {
        public static event Action OnUpdate;

        private Player _player;
        private InputManager _inputManager;
        private GameStateObserver _gameStateObserver;

        private void Start()
        {
            _player = GameObject.FindObjectOfType<Player>();
            _inputManager = new InputManager(_player);
            _gameStateObserver = new GameStateObserver(_player);
        }

        private void Update()
        {
            OnUpdate?.Invoke();
        }
    }
}
