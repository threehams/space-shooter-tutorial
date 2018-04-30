using UnityEngine;

namespace GameCode
{
	public class Spawner : MonoBehaviour
	{
		[SerializeField] private GameObject item;

		private void OnTriggerEnter (Collider other) {
			Pool.Spawn(item, transform.position, transform.rotation);
		}
	}
}
