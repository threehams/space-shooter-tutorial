using UnityEngine;

namespace GameCode
{
	public class DestroyByTime : MonoBehaviour, ISpawn {
		[SerializeField]
		private float lifetime;

		public void OnSpawn()
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
