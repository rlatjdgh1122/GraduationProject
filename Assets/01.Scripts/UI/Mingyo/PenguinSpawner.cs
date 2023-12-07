using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class PenguinSpawner : MonoBehaviour
{
    [SerializeField] private RectTransform _spawnUI;

    Penguin spawnPenguin;

    public bool isSpawnUIOn { get; private set; }

    [SerializeField]
    private float onSpawnUIYPosValue = 320;

    Vector3 onSpawnUIVec;
    Vector3 offSpawnUIVec;

    private void Start()
    {
        offSpawnUIVec = _spawnUI.position;
        onSpawnUIVec = offSpawnUIVec + new Vector3(0, onSpawnUIYPosValue, 0);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            Vector3 targetVec = isSpawnUIOn ? offSpawnUIVec : onSpawnUIVec;
            UIManager.Instance.UIMoveDot(_spawnUI, targetVec, 0.7f, Ease.OutCubic, UpdateSpawnUIBool);
        }
    }

    public void SetSpawnPenguin<T>(Vector3 vec) where T : Penguin
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
}
