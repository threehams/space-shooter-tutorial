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
    [SerializeField] private GameObject startingSidekickLeft;
    [SerializeField] private GameObject startingSidekickRight;
    [SerializeField] private Transform frontHardpoint;
    [SerializeField] private Transform sidekickLeftHardpoint;
    [SerializeField] private Transform sidekickRightHardpoint;
    private WeaponController frontWeapon;
    private WeaponController sidekickLeft;
    private WeaponController sidekickRight;

    private Rigidbody rigidBody;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        frontWeapon = ReplaceWeapon(startingWeapon, frontHardpoint);
        if (startingSidekickLeft)
        {
            sidekickLeft = ReplaceWeapon(startingSidekickLeft, sidekickLeftHardpoint);
        }
        if (startingSidekickRight)
        {
            sidekickRight = ReplaceWeapon(startingSidekickRight, sidekickRightHardpoint);
        }
    }

    private void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            frontWeapon.Fire();
        }
    }

    private void FixedUpdate()
    {
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

    private WeaponController ReplaceWeapon(GameObject weaponClass, Transform hardpoint)
    {
        var newWeapon = Instantiate(weaponClass, hardpoint.position, hardpoint.rotation);
        newWeapon.transform.parent = gameObject.transform;
        return newWeapon.GetComponent<WeaponController>();
    }

    public void CollectPowerup(Powerup powerup)
    {
        if (frontWeapon.nextWeapon)
        {
            frontWeapon = ReplaceWeapon(frontWeapon.nextWeapon, frontHardpoint);
        }
    }
}
