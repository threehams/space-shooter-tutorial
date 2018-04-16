using UnityEngine;

public class Weapon : MonoBehaviour
{
    [SerializeField] private GameObject shot;
    
    public void Fire()
    {
        Instantiate(shot, transform.position, transform.rotation);
    }
}
