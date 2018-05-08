using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameCode
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private Transform startPosition;

        [SerializeField] private Text cashText;
        [SerializeField] private Text restartText;
        [SerializeField] private Text gameOverText;

        private int cash;
        private bool gameOver;
        private bool restart;

        private void Start()
        {
            cash = 0;
            UpdateCash();
            gameOver = false;
            restart = false;
            restartText.text = "";
            gameOverText.text = "";
            Pool.Spawn(player, startPosition.position, startPosition.rotation);
        }

        public void AddCash(int newCash)
        {
            cash += newCash;
            UpdateCash();
        }

        public void GameOver()
        {
            gameOver = true;
            gameOverText.text = "Game Over Man, Game Over";
        }

        public void Update()
        {
            if (!restart)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Main");
            }
        }

        private void UpdateCash()
        {
            cashText.text = "$" + cash;
        }
    }
}
