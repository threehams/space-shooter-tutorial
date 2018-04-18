using System.Linq;
using UnityEngine;

public class WeaponAimController : MonoBehaviour
{
    [SerializeField] private float aimTimeout;
    [SerializeField] private float aimRadius;
    [SerializeField] private float aimSpeed;
    private float timer = 0.0f;
    private Rigidbody target;
    private AIWeaponController aiWeaponController;

    private void Start()
    {
        aiWeaponController = GetComponent<AIWeaponController>();
    }

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
        aiWeaponController.allowFire = target;
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
        var relativeVelocity = target.velocity - new Vector3(0.0f, 0.0f, 0.0f);
        // TODO hardcoded... how can I get the weapon shot velocity?
        var targetPosition = AimAhead(delta, relativeVelocity, 20.0f);

        if (!(targetPosition > 0f))
        {
            target = null;
            return;
        }

        var aimPoint = target.position + targetPosition * relativeVelocity;
        var aimRotation = Quaternion.LookRotation(aimPoint - transform.position).eulerAngles;
        var deltaRotation = aimRotation - transform.rotation.eulerAngles;
        
        var clamped = new Vector3(
            Mathf.Min(deltaRotation.x, aimSpeed),
            Mathf.Min(deltaRotation.y, aimSpeed),
            Mathf.Min(deltaRotation.z, aimSpeed)
        );
        transform.rotation = Quaternion.Euler(transform.rotation.eulerAngles + clamped);
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
}
