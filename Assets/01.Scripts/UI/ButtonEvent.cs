using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public delegate void CurCountChangeEvent(int newCnt, int maxCount);

enum HeroType
{
    Sword,
    Bow
}

public class ButtonEvent : MonoBehaviour
{
    public float CoolTime;
    [SerializeField] private int _maxCnt;
    public GameObject[] _prefab;
    [SerializeField] private Transform _spawnPoint;
    public bool CanSpawn { get; set; }
    private int _curCnt = 0;

    public event CurCountChangeEvent OnCurCountChangedEvent;

    public int CurCnt
    {
        get
        {
            return _curCnt;
        }
        set
        {
            _curCnt = value;
            OnCurCountChangedEvent?.Invoke(_curCnt, _maxCnt); //값이 바뀌면 이벤트 호출
        }
    }

    private void Start()
    {

        OnCurCountChangedEvent?.Invoke(_curCnt, _maxCnt);
        StartCoroutine(SpawnCoroutine());

    }

    public void Spawn(int i)
    {
        if (CanSpawn && CurCnt < _maxCnt)
        {
            CurCnt++;
            GameObject Hero = Instantiate(_prefab[i], _spawnPoint.position, Quaternion.identity);
            StartCoroutine(SpawnCoroutine());
        }
    }

    private IEnumerator SpawnCoroutine()
    {
        CanSpawn = false;
        yield return new WaitForSeconds(CoolTime);
        CanSpawn = true;
    }
}
