namespace GameCode
{
    using System.Collections.Generic;
    using UnityEngine;

    interface ISpawn
    {
        void OnSpawn();
        void OnDespawn();
    }

    public static class Pool
    {
        private static Dictionary<GameObject, Stack<GameObject>> inactive =
            new Dictionary<GameObject, Stack<GameObject>>();

        private static Dictionary<GameObject, GameObject> active =
            new Dictionary<GameObject, GameObject>();

        public static GameObject Spawn(
            GameObject prefab,
            Vector3 position,
            Quaternion rotation
        )
        {
            var pool = GetOrCreatePool(prefab);
            var item = pool.Count > 0 ? pool.Pop() : Object.Instantiate(prefab);

            item.SetActive(true);
            var interfaces = item.GetComponents<ISpawn>();
            foreach (var spawn in interfaces)
            {
                spawn.OnSpawn();
            }
            item.transform.position = position;
            item.transform.rotation = rotation;
            active[item] = prefab;
            return item;
        }

        private static Stack<GameObject> GetOrCreatePool(GameObject key)
        {
            if (inactive.ContainsKey(key))
            {
                return inactive[key];
            }
            else
            {
                inactive[key] = new Stack<GameObject>();
                return inactive[key];
            }
        }

        public static void Despawn(GameObject instance)
        {
            var interfaces = instance.GetComponents<ISpawn>();
            foreach (var spawn in interfaces)
            {
                spawn.OnDespawn();
            }
            instance.SetActive(false);
            var key = active[instance];
            inactive[key].Push(instance);
            active.Remove(instance);
        }
    }
}
