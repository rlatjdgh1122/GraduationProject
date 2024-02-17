using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using Object = UnityEngine.Object;

public static class ExtensionMethod
{
    #region Mathf Ȯ��

    public static void Percent(this Mathf mathf, int value, int comparisonValue)
    {

    }
    #endregion



    #region Linq Ȯ��

    /// <summary>
    /// ���� �� �������� ������ ����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="index"> ����縦 ���� �����Ұ���</param>
    /// <param name="thisAction"> ���� �׼�</param>
    /// <param name="otherAction"> �������� �׼�</param>
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
    /// ���� �� �������� ������ ����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="obj"> ������Ʈ�� ���� ����</param>
    /// <param name="thisAction"> ���� �׼�</param>
    /// <param name="otherAction"> �������� �׼�</param>
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
