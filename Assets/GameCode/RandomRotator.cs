using UnityEngine;

namespace GameCode
{
    public class RandomRotator : MonoBehaviour
    {
        public float tumble;
        public Rigidbody rigidBody;

        private void Start()
        {
            rigidBody = GetComponent<Rigidbody>();
            rigidBody.angularVelocity = Random.insideUnitSphere * tumble;
        }
    }
}
