using UnityEngine;

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

    private void Update()
    {
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
            Instantiate(explosion, transform.position, transform.rotation);
        }

        if (powerup && Random.value < powerupChance)
        {
            Instantiate(powerup, transform.position, Quaternion.identity);
        }

        gameController.AddScore(scoreValue);
        Destroy(gameObject);
    }

    public void AddHealth(int value)
    {
        currentHealth += value;
    }

    public void RemoveHealth(int value)
    {
        currentHealth -= value;
    }
}
