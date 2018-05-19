using UnityEngine;

namespace GameCode
{
    public class Weapon : MonoBehaviour
    {
        public Hardpoint.HardpointType hardpoint;
        public WeaponListData weaponListData;
        public int level;
        public int cost;
        
        [SerializeField] private float fireRate;

        private AudioSource audioSource;

        // for caching
        private WeaponPart[] weaponPartPartScripts;
        private float nextFire;
        private float timer;

        private void Start()
        {
            audioSource = GetComponent<AudioSource>();
            weaponPartPartScripts = GetComponentsInChildren<WeaponPart>();
        }

        public void Update()
        {
            timer = timer + Time.deltaTime;
        }

        public void Fire()
        {
            if (!(timer > nextFire))
            {
                return;
            }

            nextFire = timer + fireRate;

            foreach (var part in weaponPartPartScripts)
            {
                part.Fire();
            }

            audioSource.Play();

            nextFire = nextFire - timer;
            timer = 0.0f;
        }
    }
}
