using UnityEngine;

namespace GameCode
{
    public class WeaponPart : MonoBehaviour
    {
        [SerializeField] private GameObject shot;
    
        public void Fire()
        {
            Pool.Spawn(shot, transform.position, transform.rotation);
        }
    }
}
