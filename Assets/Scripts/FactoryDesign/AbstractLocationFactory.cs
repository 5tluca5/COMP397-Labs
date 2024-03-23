using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AbstractLocationFactory : MonoBehaviour
{
    [SerializeField] protected GameObject prefab;
    public abstract ILocation CreateLocation();
}
