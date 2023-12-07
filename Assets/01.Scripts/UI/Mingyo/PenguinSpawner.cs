using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PenguinSpawner : MonoBehaviour
{
    [SerializeField] private RectTransform _spawnUI;
    [SerializeField] private Transform _spawnPoint;

    [SerializeField] private float onSpawnUIYPosValue = 320;


    Penguin spawnPenguin;

    public bool isSpawnUIOn { get; private set; }

    Vector3 _onSpawnUIVec;
    Vector3 _offSpawnUIVec;

    private void Start()
    {
        _offSpawnUIVec = _spawnUI.position;
        _onSpawnUIVec = _offSpawnUIVec + new Vector3(0, onSpawnUIYPosValue, 0);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            Vector3 targetVec = isSpawnUIOn ? _offSpawnUIVec : _onSpawnUIVec;
            UIManager.Instance.UIMoveDot(_spawnUI, targetVec, 0.7f, Ease.OutCubic);
            UpdateSpawnUIBool();
        }
    }

    public void SpawnPenguin<T>(Vector3 vec) where T : Penguin
    {
        Type type = typeof(T);
        spawnPenguin = PoolManager.Instance.Pop(type.Name) as T;

        spawnPenguin.transform.position = vec;
        spawnPenguin.transform.rotation = Quaternion.identity;
    }

    private void UpdateSpawnUIBool()
    {
        isSpawnUIOn = isSpawnUIOn ? false : true;
    }

    #region SpawnPenguinButtonHandler

    public void BasicPenguinSpawnHandler()
    {
        SpawnPenguin<BasicPenguin>(_spawnPoint.position);
    }

    public void ArcherPenguinSpawnHandler()
    {
        SpawnPenguin<ArcherPenguin>(_spawnPoint.position);
    }

    #endregion
}
