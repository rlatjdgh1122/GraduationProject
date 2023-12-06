using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Define
{
    namespace Algorithem
    {
        public static class Algorithm
        {
            public static class AlignmentRule
            {

                public static List<Vector3> GetPostionListAround(Vector3 startPos, float[] ringDis, int[] ringTrmCount)
                {
                    List<Vector3> trms = new List<Vector3>();
                    trms.Add(startPos);
                    for (int ring = 0; ring < ringTrmCount.Length; ring++)
                    {
                        List<Vector3> rigPosList = GetPostionListAround(startPos, ringDis[ring], ringTrmCount[ring]);
                        trms.AddRange(rigPosList);
                    }

                    return trms;
                }
                public static List<Vector3> GetPostionListAround(Vector3 startPos, float distance, int trmCount)
                {
                    List<Vector3> trms = new List<Vector3>();
                    trms.Add(startPos);
                    for (int i = 0; i < trmCount; i++)
                    {
                        float angle = i * (360f / trmCount);
                        Vector3 dir = Quaternion.Euler(0, angle, 0) * new Vector3(0, 0, 1f);
                        Vector3 pos = startPos + dir * distance;
                        trms.Add(pos);
                    }

                    return trms;
                }
            }
        }
    }
}
