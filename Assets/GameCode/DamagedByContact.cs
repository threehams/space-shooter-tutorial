using UnityEngine;

// OnValidate?

namespace GameCode
{
    public class DamagedByContact : MonoBehaviour
    {
        private Destroyable destroyable;
        public int damage;
        private Powerup powerup;

        private void Start()
        {
            destroyable = GetComponent<Destroyable>();
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

            if (CompareTag("Player") && other.CompareTag("Player") ||
                CompareTag("Enemy") && other.CompareTag("Enemy"))
            {
                return;
            }

            var damageScript = other.gameObject.GetComponent<DamagedByContact>();
            if (!damageScript || damageScript.damage <= 0)
            {
                return;
            }

            destroyable.RemoveHealth(damageScript.damage);
        }
    }
}
