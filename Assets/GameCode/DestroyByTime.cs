using UnityEngine;

public class DestroyByTime : MonoBehaviour {
    [SerializeField]
    private float lifetime;

	// Use this for initialization
	private void Start () {
        Destroy(gameObject, lifetime);
	}
}
