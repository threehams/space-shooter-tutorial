using UnityEngine;

namespace GameCode
{
    public class Powerup : MonoBehaviour
    {
        [SerializeField] private string[] ids;
        public string Id { get; private set; }
    
        private void Start()
        {
            Id = ids[Random.Range(0, ids.Length)]; 
        }
    }
}
