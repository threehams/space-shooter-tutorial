using UnityEngine;

public class MoveForward : MonoBehaviour
{
    private Rigidbody rigidBody;
    public float speed;

    private void Start()
    {
        rigidBody = GetComponent<Rigidbody>();
    }
    
    private void Update()
    {
        rigidBody.velocity = transform.forward * speed;
    }
}
