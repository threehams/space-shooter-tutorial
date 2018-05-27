using UniRx;
using UnityEngine;
using UnityEngine.UI;

namespace GameCode
{
    public class Hud : MonoBehaviour
    {
        [SerializeField] private Game game;
        [SerializeField] private Text cashText;
        [SerializeField] private Text restartText;
        [SerializeField] private Text gameOverText;

        private void Start()
        {
            game.gameOver
                .Select(flag => flag ? "Game Over Man, Game Over" : "")
                .Subscribe(text => gameOverText.text = text);
            game.cash
                .Select(cash => string.Format("${0:n}", cash))
                .Subscribe(text => cashText.text = text);
            game.restart
                .Select(flag => flag ? "Press R to restart" : "")
                .Subscribe(text => restartText.text = text);

        }
    }
}
