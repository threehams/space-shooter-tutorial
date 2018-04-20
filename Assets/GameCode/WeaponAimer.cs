using System.Linq;
using UnityEngine;

public class WeaponAimer : MonoBehaviour
{
    [SerializeField] private float aimTimeout;
    [SerializeField] private float aimRadius;
    [SerializeField] private float aimSpeed;
    private float timer;
    private Rigidbody target;

    private void Update()
    {
        timer = timer + Time.deltaTime;
        if (target)
        {
            RotateAim();
            return;
        }

        if (timer < aimTimeout)
        {
            return;
        }

        target = FindNearestEnemy();
        timer = 0.0f;
    }

    private Rigidbody FindNearestEnemy()
    {
        var center = new Vector3(
            transform.position.x,
            transform.position.y,
            transform.position.z
        );
        var dimensions = new Vector3(aimRadius, aimRadius, aimRadius);
        var colliders = Physics.OverlapBox(center, dimensions);

        return (
            from col in colliders
            // TODO custom layerMask would help avoid magic number here
            where col.CompareTag("Enemy") && col.gameObject.layer == LayerMask.NameToLayer("Ship")
            select col.gameObject.GetComponent<Rigidbody>()
        ).FirstOrDefault();
    }

    private void RotateAim()
    {
        var delta = target.position - transform.position;
        // TODO hardcoded... how can I get the weapon shot velocity?
        var targetPosition = AimAhead(delta, target.velocity, 20.0f);

        if (!(targetPosition > 0f))
        {
            target = null;
            return;
        }

        var current = transform.rotation.eulerAngles;
        var aimPoint = target.position + targetPosition * target.velocity;
        var aimRotation = Quaternion.LookRotation(aimPoint - transform.position).eulerAngles;
        var deltaRotation = aimRotation - current;
        
        var clamped = new Vector3(
            0.0f,
            DeltaToTarget(current.y, deltaRotation.y),
            0.0f
        );
        transform.rotation = Quaternion.Euler(current + clamped);
    }

    // scriptable objects... like JSON but not JSON
    // could be used to keep track of weapon shot speed
    private static float AimAhead(Vector3 delta, Vector3 relativeVelocity, float muzzleVelocity)
    {
        // http://howlingmoonsoftware.com/wordpress/leading-a-target/
        // Quaternion can convert to angles

        var a = Vector3.Dot(relativeVelocity, relativeVelocity) - muzzleVelocity * muzzleVelocity;
        var b = 2f * Vector3.Dot(relativeVelocity, delta);
        var c = Vector3.Dot(delta, delta);

        var determinant = b * b - 4f * a * c;

        if (determinant > 0f)
        {
            return 2f * c / (Mathf.Sqrt(determinant) - b);
        }
        else
        {
            return -1f;
        }
    }

    private float DeltaToTarget(float current, float delta)
    {
        var maxSpeed = aimSpeed * Time.deltaTime;
        return Mathf.Clamp(Mathf.DeltaAngle(current, current + delta), -maxSpeed, maxSpeed);
    }
}
