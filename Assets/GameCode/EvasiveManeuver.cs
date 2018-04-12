using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvasiveManeuver : MonoBehaviour {
    [SerializeField]
    private Vector2 startDelay;
    [SerializeField]
    private float tilt;
    [SerializeField]
    private float maneuverDistance;
    [SerializeField]
    private Vector2 maneuverTime;
    [SerializeField]
    private Vector2 maneuverWait;
    [SerializeField]
    private float smoothing;
    [SerializeField]
    private Boundary boundary;

    private float maneuverTarget;
    private Rigidbody rigidBody;

	void Start ()
    {
        rigidBody = GetComponent<Rigidbody>();
        StartCoroutine(Evade());
    }

    IEnumerator Evade()
    {
        yield return new WaitForSeconds(Random.Range(startDelay.x, startDelay.y));

        while (true)
        {
            maneuverTarget = Random.Range(1, maneuverDistance) * -Mathf.Sign(transform.position.x);
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));
            maneuverTarget = 0;
            yield return new WaitForSeconds(Random.Range(maneuverTime.x, maneuverTime.y));

        }
    }
	
	void FixedUpdate ()
    {
        float newManeuver = Mathf.MoveTowards(rigidBody.velocity.x, maneuverTarget, Time.deltaTime * smoothing);
        rigidBody.velocity = new Vector3(newManeuver, 0.0f, rigidBody.velocity.z);
        rigidBody.position = new Vector3(
            Mathf.Clamp(rigidBody.position.x, boundary.xMin, boundary.xMax),
            0.0f,
            Mathf.Clamp(rigidBody.position.z, boundary.zMin, boundary.zMax)
        );
        rigidBody.rotation = Quaternion.Euler(0.0f, 0.0f, rigidBody.velocity.x * -tilt);
    }
}
