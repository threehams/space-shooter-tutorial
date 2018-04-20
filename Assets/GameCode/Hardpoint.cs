using UnityEngine;

namespace GameCode
{
    public class Hardpoint : MonoBehaviour
    {
        public enum HardpointType
        {
            Front,
            Rear,
            Left,
            Right
        };

        [SerializeField] private HardpointType type;
        [SerializeField] private WeaponSetController startingWeapon;
        
        private WeaponSetController weapon;

        private void Start()
        {
            if (startingWeapon)
            {
                ReplaceWeapon(startingWeapon);            
            }
        }
        
        private void ReplaceWeapon(WeaponSetController weaponClass)
        {
            var newWeapon = Instantiate(weaponClass.gameObject, transform.position, transform.rotation);
            newWeapon.transform.parent = gameObject.transform;
            weapon = newWeapon.GetComponent<WeaponSetController>();
        }

        public void Fire()
        {
            if (weapon)
            {
                weapon.Fire();
            }
        }

        public void Upgrade()
        {
            weapon.Upgrade();
        }
    }
}
