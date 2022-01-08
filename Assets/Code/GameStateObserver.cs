using UnityEngine.SceneManagement;

namespace Bomberman
{
    internal sealed class GameStateObserver
    {
        private Player _player;

        public GameStateObserver(Player player)
        {
            _player = player;
            _player.OnDied += ResetGame;
        }

        private void ResetGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }
}
