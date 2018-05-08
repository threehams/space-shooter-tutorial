using UnityEngine;

namespace GameCode
{
    public class EndLevel : MonoBehaviour
    {
        [SerializeField] private GameObject shopPanel;

        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<DestroyByBoundary>())
            {
                var player = FindObjectOfType<Player>();
                shopPanel.SetActive(true);
                player.enabled = false;
                player.GetComponent<ShopPlayerAi>().enabled = true;
            }
        }
    }
}
