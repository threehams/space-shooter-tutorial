using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponController : MonoBehaviour {
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private GameObject shot;
    [SerializeField]
    private Transform shotSpawn;
    [SerializeField]
    private float fireRate;
    [SerializeField]
    private float fireDelay;


    // Use this for initialization
    void Start () {
        audioSource = GetComponent<AudioSource>();
        InvokeRepeating("Fire", fireDelay, fireRate);
	}

    void Fire()
    {
        Instantiate(shot, shotSpawn.position, shotSpawn.rotation);
        audioSource.Play();
    }
}
