using UnityEngine;

namespace GameCode
{
    public class AiWeaponController : MonoBehaviour
    {
        [SerializeField] private Weapon startingWeapon;
        [SerializeField] private float fireDelay;
        public bool allowFire = true;

        private float timer;
        private Weapon weapon;

        private void Start()
        {
            var newWeapon = Instantiate(startingWeapon, transform.position, transform.rotation);
            newWeapon.transform.parent = gameObject.transform;
            weapon = newWeapon.GetComponent<Weapon>();
        }
    
        private void Update()
        {
            timer = timer + Time.deltaTime;
            if (timer > fireDelay && allowFire)
            {
                weapon.Fire();
            }
        }
    }
}
