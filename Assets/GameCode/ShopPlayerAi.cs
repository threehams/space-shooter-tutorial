using UnityEngine;

namespace GameCode
{
    public class ShopPlayerAi : MonoBehaviour
    {
        public bool fire;

        private Player player;
        private Rigidbody rigidBody;
        
        private void Awake()
        {
            enabled = false;
            player = GetComponent<Player>();
            rigidBody = GetComponent<Rigidbody>();
;        }
        
        private void OnEnable()
        {
            var shop = FindObjectOfType<Shop>();
            transform.position = shop.playerPoint.transform.position;
            transform.rotation = shop.playerPoint.transform.rotation;
            rigidBody.velocity = Vector3.zero;
        }

        private void Update()
        {
            if (fire)
            {
                player.Fire();
            }
        }
    }
}
