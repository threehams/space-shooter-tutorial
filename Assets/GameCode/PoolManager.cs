namespace GameCode
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;


    public class PoolManager : MonoBehaviour
    {
        private static readonly Dictionary<GameObject, Pool> managedPools =
            new Dictionary<GameObject, Pool>();


        public static void Add(Pool pool)
        {
            if (pool.prefab == null)
            {
                Debug.LogError(
                    "Prefab of pool: " + pool.gameObject.name + " is empty! " +
                    "Can't add pool to managedPools Dictionary.");
                return;
            }

            if (managedPools.ContainsKey(pool.prefab))
            {
                Debug.LogError(
                    "Pool with prefab " + pool.prefab.name + " has already been added " +
                    "to managedPools Dictionary.");
                return;
            }

            managedPools.Add(pool.prefab, pool);
        }


        public static Pool CreatePool(
            GameObject prefab,
            int preLoad,
            bool limit,
            int maxCount,
            GameObject poolTarget = null)
        {
            if (managedPools.ContainsKey(prefab))
            {
                Debug.LogError("Pool Manager already contains Pool for prefab: " + prefab.name);
                return managedPools[prefab];
            }

            if (poolTarget == null)
            {
                poolTarget = new GameObject(prefab.name + " Pool");
            }

            var poolComponent = poolTarget.AddComponent<Pool>();

            poolComponent.prefab = prefab;
            poolComponent.preloadAmount = preLoad;
            poolComponent.limit = limit;
            poolComponent.maxCount = maxCount;

            poolComponent.Awake();

            return poolComponent;
        }


        public static void DeactivatePool(GameObject prefab)
        {
            if (!managedPools.ContainsKey(prefab))
            {
                Debug.LogError("PoolManager couldn't find Pool for prefab to deactivate: " + prefab.name);
                return;
            }

            var activeList = managedPools[prefab].active.ToList();
            foreach (var item in activeList)
            {
                managedPools[prefab].Despawn(item);
            }
        }


        public static void Despawn(GameObject instance)
        {
            GetPool(instance).Despawn(instance);
        }


        public static void DestroyAllInactive(bool limitToPreLoad)
        {
            foreach (var prefab in managedPools.Keys)
            {
                managedPools[prefab].DestroyUnused(limitToPreLoad);
            }
        }


        public static void DestroyAllPools()
        {
            foreach (var prefab in managedPools.Keys)
            {
                DestroyPool(managedPools[prefab].gameObject);
            }
        }


        public static void DestroyPool(GameObject prefab)
        {
            if (!managedPools.ContainsKey(prefab))
            {
                Debug.LogError("PoolManager couldn't find Pool for prefab to destroy: " + prefab.name);
                return;
            }

            Destroy(managedPools[prefab].gameObject);
            managedPools.Remove(prefab);
        }


        public static Pool GetPool(GameObject instance)
        {
            foreach (var prefab in managedPools.Keys)
            {
                if (managedPools[prefab].active.Contains(instance))
                {
                    return managedPools[prefab];
                }
            }

            Debug.LogError("PoolManager couldn't find Pool for instance: " + instance.name);
            return null;
        }


        public static GameObject Spawn(GameObject prefab)
        {
            return Spawn(prefab, Vector3.zero, Quaternion.identity);
        }


        public static GameObject Spawn(GameObject prefab, Vector3 position)
        {
            return Spawn(prefab, position, Quaternion.identity);
        }


        public static GameObject Spawn(
            GameObject prefab,
            Vector3 position,
            Quaternion rotation)
        {
            if (!managedPools.ContainsKey(prefab))
            {
                Debug.Log("New pool: " + prefab.name);
                CreatePool(prefab, 0, false, 0);
            }

            return managedPools[prefab].Spawn(position, rotation);
        }


        public void OnDestroy()
        {
            managedPools.Clear();
        }
    }
}
