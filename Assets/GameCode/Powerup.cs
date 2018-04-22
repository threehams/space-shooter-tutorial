using UnityEngine;

public class Powerup : MonoBehaviour
{
    [SerializeField] private string[] ids;
    public string id { get; private set; }
    
    private void Start()
    {
        id = ids[Random.Range(0, ids.Length)]; 
    }
}
