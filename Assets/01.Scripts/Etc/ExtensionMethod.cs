using Define;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethod
{
    #region Mathf Ȯ��

    public static void Percent(this Mathf mathf, int value, int comparisonValue)
    {

    }
    #endregion

    #region Dictionary

    public static void TryClear<key, value>(this Dictionary<key, value> dic, Action<value> action = null)
    {
        if (action != null)
        {
            foreach (var item in dic)
            {
                action?.Invoke(item.Value);
            }

        }//end if
         

        if (dic.Count > 0) dic.Clear();
    }

    public static void TryClear<key, value>(this Dictionary<key, value> dic, Action<key, value> action)
    {
        if (action != null)
        {
            foreach (var item in dic)
            {
                action?.Invoke(item.Key, item.Value);
            }

        }//end if


        if (dic.Count > 0) dic.Clear();
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
        Action<T> thisAction, Action<T> otherAction)
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
    /// <summary>
    /// ���� ������ �������� ����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="obj"> ������Ʈ�� ���� ����</param>
    /// <param name="otherAction"> �������� �׼�</param>
    public static void ObjExcept<T>(this List<T> list, T obj, Action<T> otherAction)
    {
        foreach (var i in list)
        {
            if (i.Equals(obj))
            {

            }
            else
            {
                otherAction?.Invoke(i);
            }
        }
    }

    public static void RemoveList<T>(this List<T> thisList, List<T> otherList)
    {
        foreach (var item in otherList)
        {
            thisList.Remove(item);
        }
    }


    public static void TryClear<T>(this List<T> list, Action<T> action = null)
    {
        if (action != null)
        {
            foreach (var item in list)
            {
                action?.Invoke(item);
            }
        } //end if

        if (list.Count > 0) list.Clear();
    }

    #endregion

    #region RectTransform
    // RectTransform ������ �����ϴ� ��ųʸ�
    // static���� �����Ͽ� ���α׷��� ������ �� �ѹ��� �޸𸮿� �Ҵ� �ǰ� ��. ���� static �̹Ƿ� ��� �ν��Ͻ����� �����͸� ������.
    // readonly�� ���� ���Ҵ��� �Ұ��ϵ����� = ��ųʸ��� ���α׷� ���� ���� ������� ������ �ǹ�
    private static readonly Dictionary<GameObject, RectTransform> _rectTransformDic = new Dictionary<GameObject, RectTransform>();

    public static RectTransform rectTransform(this GameObject gameObject)
    {
        // ĳ�õ� RectTransform�� �ִ��� Ȯ���ϰ� ��ȯ
        if (_rectTransformDic.TryGetValue(gameObject, out RectTransform cachedRectTransform))
        {
            return cachedRectTransform;
        }

        // ĳ�õ� RectTransform�� ���� ��� GetComponent�� ã�Ƽ� ĳ���ϰ� ��ȯ
        RectTransform rectTransform = gameObject.GetComponent<RectTransform>();
        _rectTransformDic.Add(gameObject, rectTransform);
        return rectTransform;
    }

    public static Tweener DOAnchorPos(this GameObject gameObject, Vector2 endValue, float duration)
    {
        RectTransform rectTransform = gameObject.rectTransform();
        if (rectTransform != null)
        {
            return rectTransform.DOAnchorPos(endValue, duration);
        }

        // rectTransfor�� ���� ������Ʈ�� �ƴϸ�, �׳� DOMove�� �̵�
        return gameObject.transform.DOMove(endValue, duration);
    }
    #endregion

    #region GetOrAddComponent
    public static T GetOrAddComponent<T>(this GameObject go) where T : UnityEngine.Component
    {
        return Util.GetOrAddComponent<T>(go);
    }
    #endregion
}
