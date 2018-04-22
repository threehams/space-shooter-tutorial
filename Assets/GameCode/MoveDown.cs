using UnityEngine;

namespace GameCode
{
    public class MoveDown : MonoBehaviour
    {
        private Rigidbody rigidBody;
        public float speed;

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            rigidBody.velocity = transform.forward * speed;
        }
    }
}
