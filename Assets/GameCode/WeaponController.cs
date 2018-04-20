using UnityEngine;
using GameCode;


public class WeaponController : MonoBehaviour
{
    public Hardpoint.HardpointType hardpoint;
    [SerializeField] private float fireRate;
    private AudioSource audioSource;

    // for caching
    private Weapon[] weaponPartScripts;
    private float nextFire = 0.0f;
    private float timer = 0.0f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        weaponPartScripts = GetComponentsInChildren<Weapon>();
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

        foreach (var part in weaponPartScripts)
        {
            part.Fire();
        }

        audioSource.Play();

        nextFire = nextFire - timer;
        timer = 0.0f;
    }
}
