using System;
using UnityEngine;

namespace GameCode
{
    public class WeaponSetController : MonoBehaviour
    {
        private Weapon[] weapons;
        private Weapon weapon;
        private int level = 0;

        private void Start()
        {
            weapons = GetComponentsInChildren<Weapon>();
            weapon = weapons[level];
        }

        public void Fire()
        {
            weapon.Fire();
        }

        public void Upgrade()
        {
            level = Math.Min(weapons.Length - 1, level + 1);
            weapon = weapons[level];
        }
    }
}
