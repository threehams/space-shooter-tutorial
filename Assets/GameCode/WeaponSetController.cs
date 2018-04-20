using System;
using UnityEngine;

namespace GameCode
{
    public class WeaponSetController : MonoBehaviour
    {
        private WeaponController[] weaponControllers;
        private WeaponController weapon;
        private int level = 0;

        private void Start()
        {
            weaponControllers = GetComponentsInChildren<WeaponController>();
            weapon = weaponControllers[level];
        }

        public void Fire()
        {
            weapon.Fire();
        }

        public void Upgrade()
        {
            level = Math.Min(weaponControllers.Length - 1, level + 1);
            weapon = weaponControllers[level];
        }
    }
}
