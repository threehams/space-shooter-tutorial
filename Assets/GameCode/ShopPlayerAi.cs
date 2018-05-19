using UnityEngine;

namespace GameCode
{
    public class ShopPlayerAi : MonoBehaviour
    {
        private Player player;
        public bool fire;
        
        private void Awake()
        {
            enabled = false;
            player = GetComponent<Player>();
        }
        
        private void OnEnable()
        {
            var shop = FindObjectOfType<Shop>();
            transform.position = shop.playerPoint.transform.position;
            transform.rotation = shop.playerPoint.transform.rotation;
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
