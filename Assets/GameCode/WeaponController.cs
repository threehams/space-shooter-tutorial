using UnityEngine;


public class WeaponController : MonoBehaviour
{
    public GameObject nextWeapon;
    
    [SerializeField] private float fireRate;
    [SerializeField] private GameObject[] weaponParts;
    [SerializeField] private AudioSource audioSource;

    // for caching
    private Weapon[] weaponPartScripts;
    private float nextFire = 0.0f;
    private float myTime = 0.0f;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        weaponPartScripts = new Weapon[weaponParts.Length];
        for (var i = 0; i < weaponParts.Length; i++)
        {
            weaponPartScripts[i] = weaponParts[i].GetComponent<Weapon>();
        }
    }

    public void Update()
    {
        myTime = myTime + Time.deltaTime;
    }
    
    public void Fire()
    {
        if (!(myTime > nextFire))
        {
            return;
        }
        nextFire = myTime + fireRate;

        foreach (var part in weaponPartScripts)
        {
            part.Fire();
        }

        audioSource.Play();

        nextFire = nextFire - myTime;
        myTime = 0.0f;

    }
}
