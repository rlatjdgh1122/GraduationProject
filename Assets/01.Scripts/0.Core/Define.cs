using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Define
{
    namespace CamDefine
    {
        public static class Cam
        {
            public static Camera MainCam => Camera.main;
        }
    }

    namespace Algorithem //안씀!!
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
                        float angle = i * (360f / (trmCount - 1));
                        Vector3 dir = Quaternion.Euler(0, angle, 0) * new Vector3(0, 0, 1f);
                        Vector3 pos = startPos + dir * distance;
                        trms.Add(pos);
                    }

                    return trms;
                }

                public static Vector3 GetCenterPostion(List<Vector3> GetPostionListAround)
                {
                    if (GetPostionListAround.Count >= 0)
                        return GetPostionListAround[0];
                    else
                    {
                        Debug.LogError("리스트에 값이 없습니다.");
                        return Vector3.zero;
                    }
                }

                public static Vector3 GetArmyCenterPostion(Army army)
                {
                    List<Vector3> posList = new();
                    for (int i = 0; i < army.Soldiers.Count; i++)
                    {
                        posList.Add(army.Soldiers[i].transform.position);
                    }

                    return GetCenterPostion(posList);
                }
            }
        }
    }
}
