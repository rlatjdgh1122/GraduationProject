using System.Collections.Generic;
using Unity.Burst.Intrinsics;
using Unity.VisualScripting;
using UnityEngine;

namespace ArmySystem
{
    public class ArmyFollowCam
    {
        public GameObject Obj = null;
        public Vector3 mousePos = Vector3.zero;
        public float moveSpeed = 5f;
        public bool isInGeneral = false;
    }
}



