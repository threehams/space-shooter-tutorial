using GameCode;
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

    private Rigidbody rigidBody;
    private Hardpoint[] hardpoints;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        hardpoints = GetComponentsInChildren<Hardpoint>();
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
        hardpoints[0].Upgrade();
    }
}
