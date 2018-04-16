using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private GameObject[] powerups;
    public GameObject item { get; private set; }
    
    private void Start()
    {
        item = powerups[Random.Range(0, powerups.Length)]; 
    }
}
