using Define;
using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;

public static class ExtensionMethod
{
    #region Mathf 확장

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
    /// 나를 제외한 나머지만 동작
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <param name="list"></param>
    /// <param name="obj"> 오브젝트를 나로 설정</param>
    /// <param name="otherAction"> 나머지의 액션</param>
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
    // RectTransform 참조를 저장하는 딕셔너리
    // static으로 선언하여 프로그램이 시작할 때 한번만 메모리에 할당 되게 함. 또한 static 이므로 모든 인스턴스에서 데이터를 공유함.
    // readonly를 통해 재할당이 불가하도록함 = 딕셔너리가 프로그램 실행 도중 변경되지 않음을 의미
    private static readonly Dictionary<GameObject, RectTransform> _rectTransformDic = new Dictionary<GameObject, RectTransform>();

    public static RectTransform rectTransform(this GameObject gameObject)
    {
        // 캐시된 RectTransform이 있는지 확인하고 반환
        if (_rectTransformDic.TryGetValue(gameObject, out RectTransform cachedRectTransform))
        {
            return cachedRectTransform;
        }

        // 캐시된 RectTransform이 없는 경우 GetComponent로 찾아서 캐시하고 반환
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

        // rectTransfor을 쓰는 오브젝트가 아니면, 그냥 DOMove로 이동
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
