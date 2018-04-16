using UnityEngine;

public class EnemyWeaponController : MonoBehaviour
{
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private GameObject shot;
    [SerializeField] private Transform shotSpawn;
    [SerializeField] private float fireRate;
    [SerializeField] private float fireDelay;


    // Use this for initialization
    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire", fireDelay, fireRate);
    }

    private void Fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        audioSource.Play();
    }
}
