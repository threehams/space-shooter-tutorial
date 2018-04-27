using UnityEngine;

namespace GameCode
{
	public class DestroyByTime : MonoBehaviour {
		[SerializeField]
		private float lifetime;

		private void OnSpawn()
		{
			Invoke("Despawn", lifetime);
		}
		
		private void Despawn()
		{
			Pool.Despawn(gameObject);
		}

		public void OnDespawn()
		{
			CancelInvoke();
		} 
	}
}
