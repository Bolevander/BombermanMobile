using System;
using UnityEngine;

namespace Bomberman
{
    internal sealed class Root : MonoBehaviour
    {
        private event Action OnUpdate;

        private Player _player;
        private InputObserver _inputManager;
        private GameStateObserver _gameStateObserver;
        private EnemyBehaviourObserver _enemyUpdateController;

        private void Awake()
        {
            _player = GameObject.FindObjectOfType<Player>();
            _inputManager = new InputObserver(_player);
            _gameStateObserver = new GameStateObserver(_player);
            _enemyUpdateController = new EnemyBehaviourObserver();
        }

        private void Start()
        {
            _inputManager.SubscribeInput(ref OnUpdate);
            _gameStateObserver.SubscribeOnDied();
            _enemyUpdateController.SubscribeEnemies(ref OnUpdate);
        }

        private void OnDestroy()
        {
            _inputManager.UnSubscribeInput(ref OnUpdate);
            _gameStateObserver.UnSubscribeOnDied();
            _enemyUpdateController.UnSubscribeEnemies(ref OnUpdate);
        }

        private void Update()
        {
            OnUpdate?.Invoke();
        }
    }
}
