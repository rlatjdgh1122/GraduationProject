using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEntityFactory
{
    protected virtual Penguin CreatePenguin(Penguin penguin, Transform _initTrm, Vector3? setVec = null)
    {
        return null;
    }
}
