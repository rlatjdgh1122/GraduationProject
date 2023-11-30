using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using UnityEngine;

public class Pool<T> where T : PoolableMono //poolablemono를 무조건 상속받은 애만
{
    private Stack<T> pool = new Stack<T>();
    private T _prefab;
    private Transform _parent;
    public Pool(T prefab, Transform parent, int count)
    {
        _prefab = prefab;
        _parent = parent;

        for (int i = 0; i < count; i++)
        {
            T obj = GameObject.Instantiate(_prefab, _parent);

            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
            //클론이라는 이름을 지움

            obj.gameObject.SetActive(false);
            pool.Push(obj);
        }
    }
    public T Pop()
    {
        T obj = null;
        if (pool.Count <= 0)
        {
            obj = GameObject.Instantiate(_prefab, _parent);

            obj.gameObject.name = obj.gameObject.name.Replace("(Clone)", "");
        }
        else
        {
            obj = pool.Pop();
            obj.gameObject.SetActive(true);
        }

        return obj;
    }
    public void Push(T obj)
    {
        obj.gameObject.SetActive(false);
        pool.Push(obj);
    }
}
