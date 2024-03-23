using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PooledProjectile : MonoBehaviour
{
    [SerializeField] float lifeTime = 3f;

    //Why on enable and not start
    //OnEnable is called when the object is activated.
    private void OnEnable()
    {
        Invoke(nameof(ReturnToPool), lifeTime);
    }

    void ReturnToPool()
    {
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        ProjectilePoolManager.Instance.ReturnToPool(this);
    }
}
