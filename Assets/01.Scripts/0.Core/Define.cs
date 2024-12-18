using Cinemachine;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using ArmySystem;

namespace Define
{
    namespace CamDefine
    {
        public static class Cam
        {
            private static Camera _mainCam;
            public static Camera MainCam
            {
                get
                {
                    if (_mainCam == null)
                    {
                        _mainCam = Camera.main;
                    }
                    return _mainCam;
                }
            }

            private static CinemachineVirtualCamera _shakeCam;
            public static CinemachineVirtualCamera ShakeCam
            {
                get
                {
                    if (_shakeCam == null)
                    {
                        _shakeCam = MainCam.transform.Find("ShakeCam").GetComponent<CinemachineVirtualCamera>();
                    }
                    return _shakeCam;
                }
            }
        }
    }

    namespace RayCast
    {
        public static class RayCasts
        {
            public static Ray MousePointRay => CamDefine.Cam.MainCam.ScreenPointToRay(Mouse.current.position.ReadValue());

        }
    }
    namespace Resources
    {
        using UnityEngine;
        public static class VResources
        {
            static Dictionary<string, Object> resourceCache = new();
            public static T Load<T>(string path) where T : Object
            {
                if (!resourceCache.ContainsKey(path))
                    resourceCache[path] = Resources.Load<T>(path);
                return (T)resourceCache[path];
            }

            public static void UnloadAsset(Object @object)
            {
                Resources.UnloadAsset(@object);
            }

            public static void UnloadAssetsAll()
            {
                if (resourceCache.Count > 0)
                {
                    resourceCache.Clear();
                    Resources.UnloadUnusedAssets();
                }
            }
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
            }
        }
    }

    public class Util
    {
        public static T GetOrAddComponent<T>(GameObject go) where T : UnityEngine.Component
        {
            T component = go.GetComponent<T>();
            if (component == null)
            {
                component = go.AddComponent<T>();
            }
            return component;
        }
    }
}
