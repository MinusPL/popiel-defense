using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingManager : MonoBehaviour
{
    public GameObject archerBlueprintPrefab;
    public GameObject mageBlueprintPrefab;

    public bool isPlacing = false;

    public void PlacingArcher()
    {
        if (!isPlacing)
        {
            Instantiate(archerBlueprintPrefab);
            isPlacing = true;
        }
    }

    public void PlacingMage()
    {
        if (!isPlacing)
        {
            Instantiate(mageBlueprintPrefab);
            isPlacing = true;
        }
    }

    public void Unlock()
	{
        isPlacing = false;
	}
}
