using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TransformWithTag : MonoBehaviour
{
    [SerializeField] Vector3 size = Vector3.one;

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireCube(transform.position, size);
    }
}
