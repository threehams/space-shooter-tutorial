using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour {
    [SerializeField]
    private GameObject explosion;

    private void OnDestroy()
    {
        Instantiate(explosion, transform.position, transform.rotation);
    }
}
