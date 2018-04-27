namespace GameCode
{
    using System.Collections.Generic;
    using UnityEngine;

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
            GameObject item;
            if (pool.Count > 0)
            {
                item = pool.Pop();
            }
            else
            {
                item = Object.Instantiate(prefab) as GameObject;
            }

            item.SetActive(true);
            item.SendMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);
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
            instance.SendMessage("OnDespawn", SendMessageOptions.DontRequireReceiver);
            instance.SetActive(false);
            var key = active[instance];
            inactive[key].Push(instance);
            active.Remove(instance);
        }
    }
}
