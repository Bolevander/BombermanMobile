using UnityEngine.SceneManagement;

namespace Bomberman
{
    internal sealed class GameStateObserver
    {
        private Player _player;

        public GameStateObserver(Player player)
        {
            _player = player;           
        }

        public void SubscribeOnDied()
        {
            _player.OnDied += ResetGame;
        }
        
        public void UnSubscribeOnDied()
        {
            _player.OnDied -= ResetGame;
        }

        private void ResetGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
