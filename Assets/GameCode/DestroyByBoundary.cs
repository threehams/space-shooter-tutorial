using UnityEngine;

namespace GameCode
{
    public class DestroyByBoundary : MonoBehaviour {
        private void OnTriggerExit(Collider other)
        {
            Pool.Despawn(other.gameObject);
        }
    }
}
