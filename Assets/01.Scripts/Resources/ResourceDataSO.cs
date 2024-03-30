using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "SO/ResourceData")]
public class ResourceDataSO : ScriptableObject
{
    public string resourceName;
    public Sprite resourceIcon;
    public Sprite workerIcon;
}
