﻿using System.Linq;
using UnityEngine;

namespace GameCode
{   
    [CreateAssetMenu]
    public class WeaponDatabase : ScriptableObject
    {
        public WeaponListData[] weapons;

        public int TotalCost(WeaponListData weaponListData, int weaponLevel)
        {
            var databaseList = weapons.First(weapon => weapon == weaponListData);
            return databaseList
                .weaponLevels.Where(level => level.level <= weaponLevel)
                .Sum(level => level.weapon.cost);
        }

        public int NextCost(Weapon weapon)
        {
            var nextWeapon = weapon.weaponListData.weaponLevels[weapon.level + 1];
            return nextWeapon == null ? int.MaxValue : nextWeapon.cost;
        }
    }
}
