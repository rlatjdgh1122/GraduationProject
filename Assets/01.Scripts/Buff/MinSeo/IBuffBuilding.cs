using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuffBuilding
{
    public abstract Collider[] BuffRunning(Collider[] _curcolls, Collider[] previousColls);
}
