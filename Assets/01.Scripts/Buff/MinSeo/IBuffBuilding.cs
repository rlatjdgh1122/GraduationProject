using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IBuffBuilding
{
    public abstract int BuffRunning(Collider[] _curcolls, int previousCollsLength);
}
