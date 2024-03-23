using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GenericPoolManager<T> : PersistentSingleton<GenericPoolManager<T>> where T : Component
{
    [SerializeField] T pooledPrefab;
    [SerializeField] Queue<T> pools = new Queue<T>();

    public T Get()
    {
        if (pools.Count <= 0)
        {
            //Generate a new one
            Add();
        }

        return pools.Dequeue();
    }

    private void Add(int count = 1)
    {
        for (int i = 0; i < count; i++)
        {
            var projectile = Instantiate(pooledPrefab.gameObject).GetComponent<T>();
            projectile.gameObject.SetActive(false);
            pools.Enqueue(projectile);
        }
    }

    public void ReturnToPool(T pooledObject)
    {
        pooledObject.gameObject.SetActive(false);
        pools.Enqueue(pooledObject);
    }
}
