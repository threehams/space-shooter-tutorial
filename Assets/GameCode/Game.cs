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
        [SerializeField] private GameObject shopPanel;
        [SerializeField] private GameObject[] levels;

        private int cash;
        private int currentLevel;
        
        public int Cash
        {
            get { return cash; }
        }

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
            foreach (var level in levels)
            {
                level.SetActive(false);
            }
            levels[0].SetActive(true);
            Pool.Spawn(player, startPosition.position, startPosition.rotation);
        }

        public void AddCash(int newCash)
        {
            cash += newCash;
            UpdateCash();
        }

        public void RemoveCash(int newCash)
        {
            cash -= newCash;
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

        public void EndLevel()
        {
            levels[currentLevel].SetActive(false);
            var playerInput = FindObjectOfType<PlayerInput>();
            shopPanel.SetActive(true);
            playerInput.enabled = false;
            playerInput.GetComponent<ShopPlayerAi>().enabled = true;
        }

        public void StartNextLevel()
        {
            var playerInput = FindObjectOfType<PlayerInput>();
            shopPanel.SetActive(false);
            playerInput.enabled = true;
            playerInput.GetComponent<ShopPlayerAi>().enabled = false;

            currentLevel++;
            levels[currentLevel].SetActive(true);
        }
    }
}
