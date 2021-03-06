﻿using UnityEngine;

namespace GameCode
{
    public class Hardpoint : MonoBehaviour
    {
        public enum HardpointType
        {
            Front,
            Rear,
            Side
        }

        public string displayName;
        public HardpointType type;
        
        public Weapon weapon;

        public void ReplaceWeapon(Weapon weaponClass)
        {
            var newWeapon = Instantiate(weaponClass.gameObject, transform.position, transform.rotation);
            newWeapon.transform.parent = gameObject.transform;
            weapon = newWeapon.GetComponent<Weapon>();
        }

        public void Fire()
        {
            if (weapon)
            {
                weapon.Fire();
            }
        }
    }
}
