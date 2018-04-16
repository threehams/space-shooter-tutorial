using UnityEngine;

public class BackgroundScroller : MonoBehaviour {
    [SerializeField]
    private float scrollSpeed;
    [SerializeField]
    private float tileLength;
    private Vector3 startPosition;

    // Use this for initialization
    private void Start () {
        startPosition = transform.position;
	}
	
	// Update is called once per frame
	private void Update () {
        var newPosition = Mathf.Repeat(Time.time * scrollSpeed, tileLength);
        transform.position = startPosition + Vector3.forward * newPosition;
	}
}
