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

    #region List

    public enum ApplyToCountType
    {
        Front,
        Back,
    }

    /// <summary>
    /// 리스트의 앞부분 또는 뒷부분에서 지정된 개수의 요소에 대해 주어진 액션을 적용
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
    /// 리스트 클론
    /// </summary>
    public static List<T> ListClone<T>(this List<T> source) where T : unmanaged, ICloneable
    {
        return source.Select(item => (T)item.Clone()).ToList();
    }

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
    /// 원본 리스트의 각 요소가 변경된 리스트에 포함되는지 여부에 따라 지정된 작업을 수행
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

    public static T FindParent<T>(GameObject go) where T : UnityEngine.Object
    {
        if (go == null) return null; //찾는 기준 오브젝트가 없을때 리턴 null

        Transform currentTransform = go.transform; //찾는 기준 오브젝트의 현재 트랜스폼 저장

        while (currentTransform != null)
        {
            T component = currentTransform.GetComponent<T>(); //현재 Transform에서 T값을 찾음
            if (component != null) //만약 존재한다면
            {
                return component; //값을 보냄
            }
            currentTransform = currentTransform.parent; //없다면 현재 Transform의 부모를 현재 Transform으로 바꿈

            //부모 오브젝트(currentTransform)이 더이상 없다면 목적 완료한것이므로 while문 종료
        }

        return null;
    }
}
