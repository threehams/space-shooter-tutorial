using UnityEngine;

namespace GameCode
{
	public class Spawner : MonoBehaviour
	{
		[SerializeField] private GameObject item;
		[SerializeField] private float moveSpeed;

		private void OnTriggerEnter (Collider other) {
			var spawned = Pool.Spawn(item, transform.position, transform.rotation);
			if (moveSpeed > 0)
			{
				spawned.GetComponent<MoveDown>().speed = moveSpeed;
			}
		}
	}
}
