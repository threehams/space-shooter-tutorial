using UnityEngine;

namespace GameCode
{
    public class DamagedByContact : MonoBehaviour
    {
        private Destroyable destroyable;
        public int damage;

        private void Start()
        {
            destroyable = GetComponent<Destroyable>();
        }

        private void OnTriggerEnter(Collider other)
        {
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
