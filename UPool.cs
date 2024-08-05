using System.Collections.Generic;
using UnityEngine;

namespace Unity_one_love
{
    public class UPool<T> where T : MonoBehaviour
    {
        private List<T> pool = new();
        private T prefab;
        private Transform parent;
        private uint maxSize;
        private bool autoExpand;

        public UPool(T prefab, Transform parent, uint maxSize, bool autoExpand)
        {
            this.autoExpand = autoExpand;
            this.maxSize = maxSize;
            this.parent = parent;
            this.prefab = prefab;
        }

        public void Init()
        {
            for (int i = 0; i < maxSize; i++)
            {
                T element = Object.Instantiate(prefab, parent);
                pool.Add(element);
                element.gameObject.SetActive(false);
            }
        }

        public T Spawn()
        {
            foreach (T element in pool)
            {
                if (!element.gameObject.activeInHierarchy)
                {
                    element.gameObject.SetActive(true);
                    return element;
                }
            }

            if (autoExpand)
            {
                T element = Object.Instantiate(prefab, parent);
                pool.Add(element);
                element.gameObject.SetActive(true);
                return element;
            }

            Debug.LogError("Pool is overflow");
            return null;
        }


        public void Despawn(T target)
        {
            if (pool.Contains(target))
            {
                target.gameObject.SetActive(false);
            }
        }
    }
}