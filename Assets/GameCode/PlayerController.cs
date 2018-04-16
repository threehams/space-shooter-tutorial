using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float tilt;
    [SerializeField] private Boundary boundary;
    [SerializeField] private GameObject startingWeapon;
    [SerializeField] private Transform hardpoint1;
    private WeaponController weapon;

    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        ReplaceWeapon(startingWeapon, hardpoint1);
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            weapon.Fire();
        }
    }

    private void FixedUpdate()
    {
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

    private void ReplaceWeapon(GameObject weaponClass, Transform hardpoint)
    {
        var newWeapon = Instantiate(weaponClass, hardpoint.position, hardpoint.rotation);
        newWeapon.transform.parent = gameObject.transform;
        weapon = newWeapon.GetComponent<WeaponController>();
    }

    public void CollectPowerup(Powerup powerup)
    {
        ReplaceWeapon(powerup.item, hardpoint1);
    }
}
