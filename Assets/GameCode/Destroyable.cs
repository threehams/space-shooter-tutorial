using UnityEngine;
using Random = UnityEngine.Random;

namespace GameCode
{
    public class Destroyable : MonoBehaviour, ISpawn
    {
        [SerializeField] private int scoreValue;
        [SerializeField] private GameObject explosion;
        [SerializeField] private int maxHealth;
        [SerializeField] private GameObject powerup;
        [SerializeField] private float powerupChance;
        private int currentHealth;
        private bool isPlayer;

        private Game game;

        private void Start()
        {
            // TODO replace with event
            var gameObj = GameObject.FindWithTag("GameController");
            game = gameObj.GetComponent<Game>();
            isPlayer = GetComponent<Player>();
            OnDespawn();
        }

        public void OnSpawn()
        {
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
                game.GameOver();
            }

            if (explosion != null)
            {
                Pool.Spawn(explosion, transform.position, transform.rotation);
            }

            if (powerup && Random.value < powerupChance)
            {
                Pool.Spawn(powerup, transform.position, Quaternion.identity);
            }

            game.AddCash(scoreValue);
            Pool.Despawn(gameObject);
        }
    }
}
