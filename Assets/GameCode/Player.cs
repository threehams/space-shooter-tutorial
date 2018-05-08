using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameCode
{
    [System.Serializable]
    public class WeaponLevels
    {
        public string id;
        public int currentLevel;
        public bool active;

        public WeaponLevels(string id, int currentLevel, bool active)
        {
            this.id = id;
            this.currentLevel = currentLevel;
            this.active = active;
        }
    }

    [System.Serializable]
    public class Inventory
    {
        public List<WeaponLevels> weaponLevels;
    }

    public class Player : MonoBehaviour
    {
        [SerializeField] private WeaponDatabase weaponDatabase;

        private Hardpoint[] hardpoints;

        public Inventory inventory;

        private void Awake()
        {
            weaponDatabase.PopulateDatabase();
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

        public void CollectPowerup(Powerup powerup)
        {
            var currentWeapon = inventory.weaponLevels.FirstOrDefault(level => level.id == powerup.Id);
            var maxLevel = weaponDatabase.GetWeaponListData(powerup.Id).weaponLevels.Length - 1;
            if (currentWeapon == null)
            {
                DeactivateOtherWeapons(powerup.Id);
                inventory.weaponLevels.Add(new WeaponLevels(powerup.Id, 1, true));
            }
            else if (currentWeapon.active == false)
            {
                DeactivateOtherWeapons(powerup.Id);
                currentWeapon.active = true;
            }
            else if (currentWeapon.currentLevel <= maxLevel)
            {
                currentWeapon.currentLevel++;
            }

            ReplaceWeapons();
        }

        private void DeactivateOtherWeapons(string id)
        {
            foreach (var weaponLevel in inventory.weaponLevels.Where(level => level.id != id))
            {
                weaponLevel.active = false;
            }
        }

        private void ReplaceWeapons()
        {
            foreach (var weaponLevel in inventory.weaponLevels)
            {
                var currentLevel = weaponLevel.currentLevel;
                var weaponCollection = weaponDatabase.GetWeaponListData(weaponLevel.id);
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