using Define;
using DG.Tweening;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum AAType
{
    Front,
    Back,
}

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

    #region List

    public enum ApplyToCountType
    {
        Front,
        Back,
    }

    /// <summary>
    /// ����Ʈ�� �պκ� �Ǵ� �޺κп��� ������ ������ ��ҿ� ���� �־��� �׼��� ����
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="count"></param>
    /// <param name="action"></param>
    /// <param name="type"></param>
    public static void ApplyToCount<T>(this List<T> list, int count, Action<T> action = null, ApplyToCountType type = ApplyToCountType.Front)
    {
        if (type == ApplyToCountType.Front)
        {
            for (int i = 0; i < count; ++i)
            {
                try
                {
                    action?.Invoke(list[i]);
                }
                catch { }
            }
        }

        else if (type == ApplyToCountType.Back)
        {
            for (int i = list.Count - count; i > list.Count; ++i)
            {
                action?.Invoke(list[i]);
            }
        }
    }

    /// <summary>
    /// ����Ʈ Ŭ��
    /// </summary>
    public static List<T> ListClone<T>(this List<T> source) where T : unmanaged, ICloneable
    {
        return source.Select(item => (T)item.Clone()).ToList();
    }

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

    public static List<TResult> Convert<TSource, TResult>(this List<TSource> source, Func<TSource, TResult> selector)
    {
        List<TResult> result = new List<TResult>();
        foreach (var item in source)
        {
            result.Add(selector(item));
        }
        return result;
    }

    /// <summary>
    /// ���� ����Ʈ�� �� ��Ұ� ����� ����Ʈ�� ���ԵǴ��� ���ο� ���� ������ �۾��� ����
    /// </summary>
    public static void ProcessIfContained<T>(this List<T> list, List<T> changedList, bool shouldContain,
        Action<T> action = null)
    {
        if (list == null) throw new ArgumentNullException(nameof(list));
        if (changedList == null) throw new ArgumentNullException(nameof(changedList));

        foreach (var data in list)
        {
            if (changedList.Contains(data) == shouldContain)
            {
                action?.Invoke(data);
            }
        }
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

    public static T FindParent<T>(GameObject go) where T : UnityEngine.Object
    {
        if (go == null) return null; //ã�� ���� ������Ʈ�� ������ ���� null

        Transform currentTransform = go.transform; //ã�� ���� ������Ʈ�� ���� Ʈ������ ����

        while (currentTransform != null)
        {
            T component = currentTransform.GetComponent<T>(); //���� Transform���� T���� ã��
            if (component != null) //���� �����Ѵٸ�
            {
                return component; //���� ����
            }
            currentTransform = currentTransform.parent; //���ٸ� ���� Transform�� �θ� ���� Transform���� �ٲ�

            //�θ� ������Ʈ(currentTransform)�� ���̻� ���ٸ� ���� �Ϸ��Ѱ��̹Ƿ� while�� ����
        }

        return null;
    }
}
