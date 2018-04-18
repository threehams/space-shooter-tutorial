using UnityEngine;

public class AIWeaponController : MonoBehaviour
{
    [SerializeField] private WeaponController startingWeapon;
    [SerializeField] private float fireDelay;
    public bool allowFire = true;

    private float timer = 0.0f;
    private WeaponController weapon;

    private void Start()
    {
        var newWeapon = Instantiate(startingWeapon, transform.position, transform.rotation);
        newWeapon.transform.parent = gameObject.transform;
        weapon = newWeapon.GetComponent<WeaponController>();
    }
    
    private void Update()
    {
        timer = timer + Time.deltaTime;
        if (timer > fireDelay && allowFire)
        {
            weapon.Fire();
        }
    }
}
