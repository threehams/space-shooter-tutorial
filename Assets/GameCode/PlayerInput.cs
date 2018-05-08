using UnityEngine;

namespace GameCode
{
    [System.Serializable]
    public class Boundary
    {
        public float xMin, xMax, zMin, zMax;
    }

    public class PlayerInput : MonoBehaviour
    {
        [SerializeField] private Boundary boundary;
        [SerializeField] private float speed;
        [SerializeField] private float tilt;

        private Rigidbody rigidBody;
        private Player player;

        private void Awake()
        {
            rigidBody = GetComponent<Rigidbody>();
            player = GetComponent<Player>();
        }
        
        private void Update()
        {
            if (Input.GetButton("Fire1"))
            {
                player.Fire();
            }
        }

        private void FixedUpdate()
        {
            rigidBody.velocity = Vector3.zero;
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
    }
}
