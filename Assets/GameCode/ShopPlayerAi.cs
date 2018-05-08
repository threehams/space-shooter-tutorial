using UnityEngine;

namespace GameCode
{
    public class ShopPlayerAi : MonoBehaviour
    {
        private void Awake()
        {
            enabled = false;
        }
        
        private void OnEnable()
        {
            var shop = FindObjectOfType<Shop>();
            transform.position = shop.playerPoint.transform.position;
            transform.rotation = shop.playerPoint.transform.rotation;
        }
    }
}
