using UnityEngine;

namespace GameCode
{
	public class BackgroundScroller : MonoBehaviour {
		[SerializeField]
		private float scrollSpeed;
		[SerializeField]
		private float tileLength;
		private Vector3 startPosition;

		private void Start () {
			startPosition = transform.position;
		}
	
		private void Update () {
			var newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileLength);
			transform.position = startPosition + Vector3.forward * newPosition;
		}
	}
}
