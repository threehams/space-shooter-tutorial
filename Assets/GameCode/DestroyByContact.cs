using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyByContact : MonoBehaviour {
    [SerializeField]
    private int scoreValue;
    [SerializeField]
    private GameObject explosion;
    private GameController gameController;

    private void Start()
    {
        GameObject gameControllerObject = GameObject.FindWithTag("GameController");
        if (gameControllerObject != null)
        {
            gameController = gameControllerObject.GetComponent<GameController>();
        }
        if (gameController == null)
        {
            Debug.Log("Cannot find 'GameController' script");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // we are the player
        if (GetComponent<PlayerController>() != null && other.CompareTag("Enemy"))
        {
            gameController.GameOver();
            Destroy(gameObject);
            Explode();
        }
        else if (
            CompareTag("Player") && other.CompareTag("Enemy") ||
            CompareTag("Enemy") && other.CompareTag("Player"))
        {
            Destroy(gameObject);
            Explode();
        }
        gameController.AddScore(scoreValue);
    }

    private void Explode()
    {
        if (explosion != null)
        {
            Instantiate(explosion, transform.position, transform.rotation);
        }
    }
}
