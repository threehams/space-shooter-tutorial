using UnityEngine;
using Random = UnityEngine.Random;

namespace GameCode
{
    public class Destroyable : MonoBehaviour
    {
        [SerializeField] private int scoreValue;
        [SerializeField] private GameObject explosion;
        [SerializeField] private int maxHealth;
        [SerializeField] private GameObject powerup;
        [SerializeField] private float powerupChance;
        private int currentHealth;
        private bool isPlayer;

        private GameController gameController;

        private void Start()
        {
            currentHealth = maxHealth;
            var gameControllerObject = GameObject.FindWithTag("GameController");
            gameController = gameControllerObject.GetComponent<GameController>();
            isPlayer = GetComponent<PlayerController>();
        }

        public void RemoveHealth(int value)
        {
            if (currentHealth <= 0)
            {
                return;
            }
            currentHealth -= value;
            if (currentHealth > 0) return;
            if (isPlayer)
            {
                gameController.GameOver();
            
            }

            if (explosion != null)
            {
                Instantiate(explosion, transform.position, transform.rotation);
            }

            if (powerup && Random.value < powerupChance)
            {
                Instantiate(powerup, transform.position, Quaternion.identity);
            }

            gameController.AddScore(scoreValue);
            Destroy(gameObject);
        }
    }
}
