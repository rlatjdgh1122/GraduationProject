using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public static class ExtensionMethod
{
    #region Mathf 확장

    public static void Percent(this Mathf mathf, int value, int comparisonValue)
    {

    }
    #endregion



    #region Linq 확장

    /// <summary>
    /// 나와 그 나머지를 나눠서 동작
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="index"> 몇번재를 나로 설정할건지</param>
    /// <param name="thisAction"> 나의 액션</param>
    /// <param name="otherAction"> 나머지의 액션</param>
    public static void IdxExcept<T>(this List<T> list, int index,
        Action<T> thisAction = null, Action<T> otherAction = null)
    {
        for (int i = 0; i < list.Count; ++i)
        {
            if (i == index)
            {
                thisAction?.Invoke(list[i]);
            }
            else
            {
                otherAction?.Invoke(list[i]);
            }
        }
    }

    /// <summary>
    /// 나와 그 나머지를 나눠서 동작
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="obj"> 오브젝트를 나로 설정</param>
    /// <param name="thisAction"> 나의 액션</param>
    /// <param name="otherAction"> 나머지의 액션</param>
    public static void ObjExcept<T>(this List<T> list, T obj,
        Action<T> thisAction = null, Action<T> otherAction = null)
    {
        foreach (var i in list)
        {
            if (i.Equals(obj))
            {
                thisAction?.Invoke(i);
            }
            else
            {
                otherAction?.Invoke(i);
            }
        }
    }
    #endregion
}
