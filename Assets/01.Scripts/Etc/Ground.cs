using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ground : MonoBehaviour
{
    private bool isPlacedBuilding;

    public bool IsPlacedBuilding => isPlacedBuilding;

    public void PlacedBuilding()
    {
        isPlacedBuilding = true;
    }
}
