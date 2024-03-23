using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectilePoolManager : GenericPoolManager<PooledProjectile>
{
    //[SerializeField] PooledProjectile pooledProjectilePrefab;
    //Queue<PooledProjectile> pooledProjectiles = new Queue<PooledProjectile>();

    //public PooledProjectile Get()
    //{
    //    if(pooledProjectiles.Count <= 0)
    //    {
    //        //Generate a new one
    //        AddProjectile(1);
    //    }

    //    return pooledProjectiles.Dequeue();
    //}

    //private void AddProjectile(int count)
    //{
    //    for(int i=0; i<count; i++)
    //    {
    //        var projectile = Instantiate(pooledProjectilePrefab);
    //        projectile.gameObject.SetActive(false);
    //        pooledProjectiles.Enqueue(projectile);
    //    }
    //}

    //public void ReturnToPool(PooledProjectile pooledProjectile)
    //{
    //    pooledProjectile.gameObject.SetActive(false);
    //    pooledProjectiles.Enqueue(pooledProjectile);
    //}
}
