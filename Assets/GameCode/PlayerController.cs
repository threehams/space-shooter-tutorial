using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Boundary
{
    public float xMin, xMax, zMin, zMax;
}

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float tilt;
    public float fireRate;
    public Boundary boundary;
    public GameObject shot;
    public Transform hardpoint1;
    public Transform hardpoint2;
    public Transform hardpoint3;

    private AudioSource audioSource;
    private Rigidbody rigidBody;
    private float nextFire = 0.5f;
    private float myTime = 0.0f;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        myTime = myTime + Time.deltaTime;

        if (Input.GetButton("Fire1") && myTime > nextFire)
        {
            nextFire = myTime + fireRate;
            Instantiate(shot, hardpoint1.position, hardpoint1.rotation);
            Instantiate(shot, hardpoint2.position, hardpoint2.rotation);
            Instantiate(shot, hardpoint3.position, hardpoint3.rotation);
            audioSource.Play();

            nextFire = nextFire - myTime;
            myTime = 0.0f;
        }
    }

    private void FixedUpdate()
    {
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        rigidBody.velocity = new Vector3(moveHorizontal, 0.0f, moveVertical) * speed;

        rigidBody.position = new Vector3(
            Mathf.Clamp(rigidBody.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rigidBody.position.z, boundary.zMin, boundary.zMax)
        );

        rigidBody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidBody.velocity.x * -tilt);
    }
}
