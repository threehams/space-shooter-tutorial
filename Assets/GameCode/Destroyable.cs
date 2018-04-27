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
            var gameControllerObject = GameObject.FindWithTag("GameController");
            gameController = gameControllerObject.GetComponent<GameController>();
            isPlayer = GetComponent<PlayerController>();
            OnDespawn();
        }

        public void OnDespawn()
        {
            currentHealth = maxHealth;
        }

        public void RemoveHealth(int value)
        {
            if (currentHealth <= 0)
            {
                return;
            }

            currentHealth -= value;
            if (currentHealth > 0)
            {
                return;
            }

            if (isPlayer)
            {
                gameController.GameOver();
            }

            if (explosion != null)
            {
                Pool.Spawn(explosion, transform.position, transform.rotation);
            }

            if (powerup && Random.value < powerupChance)
            {
                Pool.Spawn(powerup, transform.position, Quaternion.identity);
            }

            gameController.AddCash(scoreValue);
            Pool.Despawn(gameObject);
        }
    }
}
