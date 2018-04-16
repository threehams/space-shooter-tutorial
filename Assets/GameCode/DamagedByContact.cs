using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Destroyable))]
public class DamagedByContact : MonoBehaviour
{
    private Destroyable healthScript;
    public int damage;
    private PlayerController playerController;
    private Powerup powerup;

    private void Start()
    {
        healthScript = GetComponent<Destroyable>();
        playerController = GetComponent<PlayerController>();
        powerup = GetComponent<Powerup>();
    }

    private void OnTriggerEnter(Collider other)
    {
        var otherPlayer = other.GetComponent<PlayerController>();
        if (powerup != null)
        {
            if (otherPlayer)
            {
                otherPlayer.CollectPowerup(powerup);
            }
            else
            {
                return;
            }
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
