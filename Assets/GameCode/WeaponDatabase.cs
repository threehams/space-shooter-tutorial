using System.Collections.Generic;
using UnityEngine;

namespace GameCode
{
    [CreateAssetMenu]
    public class WeaponDatabase : ScriptableObject
    {
        [SerializeField] private WeaponListData[] weapons;

        private Dictionary<string, WeaponListData> database;

        public void PopulateDatabase()
        {
            database = new Dictionary<string, WeaponListData>();
            foreach (var entry in weapons)
            {
                database.Add(entry.id, entry);
            }
        }


        public WeaponListData GetWeaponListData(string id)
        {
            return database[id];
        }
        
        public IEnumerator<WeaponListData> GetEnumerator()
        {
            return database.Values.GetEnumerator();
        }

    }
}
