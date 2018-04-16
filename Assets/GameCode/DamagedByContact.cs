using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Destroyable))]
public class DamagedByContact : MonoBehaviour
{
    private Destroyable healthScript;
    public int damage;
    private PlayerController playerController;

    private void Start()
    {
        healthScript = GetComponent<Destroyable>();
        playerController = GetComponent<PlayerController>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var powerup = other.GetComponent<Powerup>();

        if (playerController != null && powerup != null)
        {
            playerController.CollectPowerup(powerup);
        }

        if ((CompareTag("Player") && other.CompareTag("Player")) ||
            (CompareTag("Enemy") && other.CompareTag("Enemy")))
        {
            return;
        }

        var damageScript = other.gameObject.GetComponent<DamagedByContact>();
        if (!damageScript || damageScript.damage <= 0)
        {
            return;
        }

        healthScript.RemoveHealth(damageScript.damage);
    }
}
