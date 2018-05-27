using System.Linq;
using UnityEngine;

namespace GameCode
{
    public class WeaponAimer : MonoBehaviour, ISpawn
    {
        [SerializeField] private float aimTimeout;
        [SerializeField] private float aimRadius;
        [SerializeField] private float aimSpeed;
        // TODO hardcoded... how can I get the weapon shot velocity?
        [SerializeField] private float shotSpeed;
        private float timer;
        private Rigidbody target;

        public void OnSpawn()
        {
        }

        public void OnDespawn()
        {
            target = null;
        }
        
        private void Update()
        {
            timer = timer + Time.deltaTime;
            if (target != null && target.gameObject.activeInHierarchy)
            {
                RotateAim();
                return;
            }

            if (timer < aimTimeout)
            {
                return;
            }

            target = FindNearestEnemy();
            timer = aimTimeout;
        }

        private Rigidbody FindNearestEnemy()
        {
            var center = new Vector3(
                transform.position.x,
                transform.position.y,
                transform.position.z
            );
            var colliders = Physics.OverlapSphere(center, aimRadius);

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
            var targetPosition = AimAhead(delta, target.velocity, shotSpeed);

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
            var a = Vector3.Dot(relativeVelocity, relativeVelocity) - muzzleVelocity * muzzleVelocity;
            var b = 2f * Vector3.Dot(relativeVelocity, delta);
            var c = Vector3.Dot(delta, delta);

            var determinant = b * b - 4f * a * c;

            if (determinant > 0f)
            {
                return 2f * c / (Mathf.Sqrt(determinant) - b);
            }

            return -1f;
        }

        private float DeltaToTarget(float current, float delta)
        {
            var maxSpeed = aimSpeed * Time.deltaTime;
            return Mathf.Clamp(Mathf.DeltaAngle(current, current + delta), -maxSpeed, maxSpeed);
        }
    }
}
