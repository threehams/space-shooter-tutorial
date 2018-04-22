namespace GameCode
{
    using System.Collections.Generic;
    using UnityEngine;


    public class Pool : MonoBehaviour
    {
        public GameObject prefab;
        public int preloadAmount;
        public bool limit;
        public int maxCount;
        public List<GameObject> active = new List<GameObject>();
        public List<GameObject> inactive = new List<GameObject>();

        private int TotalCount
        {
            get
            {
                return active.Count + inactive.Count;
            }
        }

        public void Awake()
        {
            if (prefab == null)
            {
                return;
            }

            PoolManager.Add(this);
            Preload();
        }


        public void Despawn(GameObject instance)
        {
            if (!active.Contains(instance))
            {
                Debug.LogWarning(
                    "Can't despawn - Instance not found: "
                    + instance.name + " in Pool " + name);
                return;
            }

            if (instance.transform.parent != transform)
            {
                instance.transform.parent = transform;
            }

            active.Remove(instance);
            inactive.Add(instance);

            instance.BroadcastMessage("OnDespawn", SendMessageOptions.DontRequireReceiver);

            instance.SetActive(false);
        }


        public void DespawnAll()
        {
            PoolManager.DeactivatePool(prefab);
        }


        public void DestroyCount(int count)
        {
            if (count > inactive.Count)
            {
                Debug.LogWarning(
                    "Destroy Count value: " + count + " is greater than inactive Count: "
                    + inactive.Count + ". Destroying all available inactive objects of type: " +
                    prefab.name + "." +
                    "Use DestroyUnused(false) instead to achieve the same.");
                DestroyUnused(false);
                return;
            }

            for (var i = inactive.Count - 1; i >= inactive.Count - count; i--)
            {
                Destroy(inactive[i]);
            }

            inactive.RemoveRange(inactive.Count - count, count);
        }


        public void DestroyUnused(bool preloadLimit)
        {
            if (preloadLimit)
            {
                for (var i = inactive.Count - 1; i >= preloadAmount; i--)
                {
                    Destroy(inactive[i]);
                }

                if (inactive.Count > preloadAmount)
                {
                    inactive.RemoveRange(preloadAmount, inactive.Count - preloadAmount);
                }
            }
            else
            {
                foreach (var item in inactive)
                {
                    Destroy(item);
                }

                inactive.Clear();
            }
        }


        public void OnDestroy()
        {
            active.Clear();
            inactive.Clear();
        }


        public void Preload()
        {
            if (prefab == null)
            {
                Debug.LogWarning("Cannot preload empty prefab.");
                return;
            }

            for (var i = TotalCount; i < preloadAmount; i++)
            {
                var poolObject = Instantiate(prefab, Vector3.zero, Quaternion.identity);
                poolObject.transform.SetParent(transform);

                Rename(poolObject.transform);
                poolObject.SetActive(false);
                inactive.Add(poolObject);
            }
        }


        public GameObject Spawn(Vector3 position, Quaternion rotation)
        {
            GameObject poolObject;
            Transform poolTransform;

            if (inactive.Count > 0)
            {
                poolObject = inactive[0];
                inactive.RemoveAt(0);

                poolTransform = poolObject.transform;
            }
            else
            {
                if (limit
                    && active.Count >= maxCount)
                {
                    return null;
                }

                poolObject = Instantiate(prefab);
                poolTransform = poolObject.transform;
                Rename(poolTransform);
            }

            poolTransform.position = position;
            poolTransform.rotation = rotation;

            if (poolTransform.parent != transform)
            {
                poolTransform.parent = transform;
            }

            active.Add(poolObject);

            poolObject.SetActive(true);

            poolObject.BroadcastMessage("OnSpawn", SendMessageOptions.DontRequireReceiver);

            return poolObject;
        }


        private void Rename(Transform instance)
        {
            instance.name += (TotalCount + 1).ToString("#000");
        }
    }
}
