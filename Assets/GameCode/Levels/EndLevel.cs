using UnityEngine;

namespace GameCode
{
    public class EndLevel : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.GetComponent<DestroyByBoundary>())
            {
                var game = FindObjectOfType<Game>();
                game.EndLevel();
            }
        }
    }
}
