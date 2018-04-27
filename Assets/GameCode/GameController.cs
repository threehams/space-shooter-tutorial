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
            StartCoroutine(SpawnWaves());
            gameOver = false;
            restart = false;
            restartText.text = "";
            gameOverText.text = "";
            Pool.Spawn(player, startPosition.position, startPosition.rotation);
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
                    Pool.Spawn(hazard, spawnPosition, spawnRotation);
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
