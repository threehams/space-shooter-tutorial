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
                var playerInput = FindObjectOfType<PlayerInput>();
                shopPanel.SetActive(true);
                playerInput.enabled = false;
                playerInput.GetComponent<ShopPlayerAi>().enabled = true;
            }
        }
    }
}
