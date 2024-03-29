using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactoryGrass : AbstractLocationFactory
{
    public override ILocation CreateLocation()
    {
        LocationGrass locationGrass;
        if (prefab.TryGetComponent(out locationGrass))
            return locationGrass;
        else
        {
            locationGrass = prefab.AddComponent<LocationGrass>();
            locationGrass.GenerateLocation();

        }
        return locationGrass;
    }

}
