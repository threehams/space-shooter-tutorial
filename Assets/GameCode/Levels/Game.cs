using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;

namespace GameCode
{
    public class Game : MonoBehaviour
    {
        [SerializeField] private GameObject player;
        [SerializeField] private Transform startPosition;

        [SerializeField] private GameObject shopPanel;
        [SerializeField] private GameObject[] levels;

        private int currentLevel;

        public ReactiveProperty<int> cash = new ReactiveProperty<int>(0);
        public ReactiveProperty<bool> gameOver = new ReactiveProperty<bool>(false);
        public ReactiveProperty<bool> restart = new ReactiveProperty<bool>(false);

        private void Start()
        {
            cash.Value = 0;
            gameOver.Value = false;
            restart.Value = false;
            foreach (var level in levels)
            {
                level.SetActive(false);
            }

            levels[0].SetActive(true);
            Pool.Spawn(player, startPosition.position, startPosition.rotation);
        }

        public void AddCash(int newCash)
        {
            cash.Value += newCash;
        }

        public void RemoveCash(int newCash)
        {
            cash.Value -= newCash;
        }

        public void GameOver()
        {
            gameOver.Value = true;
        }

        public void Update()
        {
            if (!restart.Value)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("Main");
            }
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
