using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpawnHero : MonoBehaviour
{
    public int _maxCnt;
    [SerializeField] private float _coolTime;
    [SerializeField] private GameObject _prefab;
    [SerializeField] private Transform _spawnPoint;
    [SerializeField] private Transform _reloadBarTrm;
    public bool canSpawn;
    
    [HideInInspector] public float _curCnt;
    private float _curTime;

    private void Awake()
    {
        _curTime = _coolTime;
    }

    public void Spawn()
    {
        if(_curCnt < _maxCnt && canSpawn)
        {
            GameObject Hero = Instantiate(_prefab, _spawnPoint.position, Quaternion.identity);
            _curTime = _coolTime;
            canSpawn = false;
            _curCnt++;
        }
    }

    public string SpawnString()
    {
        string answer;
        return answer = $"{_curCnt} / {_maxCnt}";
    }

    public string SpawnCoolString()
    {
        string answer;
        int value = (int)Mathf.Clamp(_curTime / _coolTime * 100, 0, 100);

        if(value <= 0)
        {
            return answer = "COMPLETE";
        }
        else
        {
            return answer = $"{value}";
        }
    }

    public float CoolTime()
    {
        return _curTime;
    }

    private void Update()
    {
        if(_curTime <= 0)
        {
            canSpawn = true;
        }
        else
        {
            _curTime -= Time.deltaTime;
            canSpawn = false;
        }
    }
}
