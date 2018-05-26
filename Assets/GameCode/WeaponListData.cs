using UnityEngine;

namespace GameCode
{
    [CreateAssetMenu]
    public class WeaponListData : ScriptableObject
    {
        public string id;
        public string displayName;
        public Hardpoint.HardpointType hardpoint;
        public Weapon[] weaponLevels;
    }
}
