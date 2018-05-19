using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameCode
{
    [System.Serializable]
    public class WeaponLevels
    {
        public WeaponListData weaponListData;
        public int currentLevel;
        public Hardpoint hardpoint;

        public WeaponLevels(WeaponListData weaponListData, int currentLevel, Hardpoint hardpoint)
        {
            this.weaponListData = weaponListData;
            this.currentLevel = currentLevel;
            this.hardpoint = hardpoint;
        }
    }

    [System.Serializable]
    public class Inventory
    {
        public List<WeaponLevels> weaponLevels;
    }

    public class Player : MonoBehaviour
    {
        public Hardpoint[] hardpoints;
        public Inventory inventory;

        private void Awake()
        {
            hardpoints = GetComponentsInChildren<Hardpoint>();
            ReplaceWeapons();
        }

        public void Fire()
        {
            foreach (var hardpoint in hardpoints)
            {
                hardpoint.Fire();
            }
        }

        public void ReplaceWeapons()
        {
            foreach (var weaponLevel in inventory.weaponLevels)
            {
                var currentLevel = weaponLevel.currentLevel;
                var weaponCollection = weaponLevel.weaponListData;
                var weapon = weaponCollection
                    .weaponLevels
                    .Where(weaponData => weaponData.level == currentLevel)
                    .Select(weaponData => weaponData.weapon)
                    .First();
                foreach (var hardpoint in hardpoints)
                {
                    if (hardpoint.type == weaponCollection.hardpoint)
                    {
                        hardpoint.ReplaceWeapon(weapon);
                    }
                }
            }
        }
    }
}