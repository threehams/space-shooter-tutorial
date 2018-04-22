using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace GameCode
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameObject[] hazards;
        [SerializeField] private Vector3 spawnValues;
        [SerializeField] private int hazardCount;
        [SerializeField] private float spawnWait;
        [SerializeField] private float startWait;
        [SerializeField] private float waveWait;

        [SerializeField] private Text scoreText;
        [SerializeField] private Text restartText;
        [SerializeField] private Text gameOverText;

        private int score;
        private bool gameOver;
        private bool restart;

        private void Start()
        {
            score = 0;
            UpdateScore();
            StartCoroutine(SpawnWaves());
            gameOver = false;
            restart = false;
            restartText.text = "";
            gameOverText.text = "";
        }

        private IEnumerator SpawnWaves()
        {
            yield return new WaitForSeconds(startWait);
            while (true)
            {
                for (var i = 0; i < hazardCount; i++)
                {
                    var hazard = hazards[Random.Range(0, hazards.Length)];
                    var spawnPosition = new Vector3(Random.Range(-spawnValues.x, spawnValues.x), spawnValues.y,
                        spawnValues.z);
                    var spawnRotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                    Instantiate(hazard, spawnPosition, spawnRotation);
                    yield return new WaitForSeconds(spawnWait);
                }

                yield return new WaitForSeconds(waveWait);
                if (!gameOver)
                {
                    continue;
                }

                restartText.text = "Press 'R' to restart";
                restart = true;
                break;
            }
        }

        public void AddScore(int newScore)
        {
            score += newScore;
            UpdateScore();
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

        private void UpdateScore()
        {
            scoreText.text = "Score: " + score;
        }
    }
}
