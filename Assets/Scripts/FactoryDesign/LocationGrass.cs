using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocationGrass : MonoBehaviour, ILocation
{
    public LocationGrass()
    {
        GenerateLocation();
    }

    public void GenerateLocation()
    {
        Debug.Log("GenerateLocation");
    }
}
