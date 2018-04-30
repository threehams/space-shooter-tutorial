using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace GameCode
{
    [System.Serializable]
    public class Boundary
    {
        public float xMin, xMax, zMin, zMax;
    }

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
        [SerializeField] private float speed;
        [SerializeField] private float tilt;
        [SerializeField] private Boundary boundary;
        [SerializeField] private Inventory inventory;
        [SerializeField] private WeaponDatabase weaponDatabase;

        private Rigidbody rigidBody;
        private Hardpoint[] hardpoints;

        private void Awake()
        {
            weaponDatabase.PopulateDatabase();
        }

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            hardpoints = GetComponentsInChildren<Hardpoint>();
            ReplaceWeapons();
        }

        private void Update()
        {
            if (Input.GetButton("Fire1"))
            {
                foreach (var hardpoint in hardpoints)
                {
                    hardpoint.Fire();
                }
            }
        }

        private void FixedUpdate()
        {
            rigidBody.velocity = Vector3.zero;
            // these aren't tank controls, but missiles will be.
            var moveHorizontal = Input.GetAxis("Horizontal");
            var moveVertical = Input.GetAxis("Vertical");
            
            rigidBody.velocity = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed;

            rigidBody.position = new Vector3(
                Mathf.Clamp(rigidBody.position.x, boundary.xMin, boundary.xMax),
                0.0f,
                Mathf.Clamp(rigidBody.position.z, boundary.zMin, boundary.zMax)
            );

            rigidBody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidBody.velocity.x * -tilt);
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