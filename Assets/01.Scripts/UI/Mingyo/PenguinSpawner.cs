using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class SpawnPenguinBtnInfo
{
    public Button Btn;
    public Image CoolingImg;
    public float CoolTime;

    public SpawnPenguinBtnInfo(Button button, Image image, float coolTime)
    {
        Btn = button;
        CoolingImg = image;
        CoolTime = coolTime;
    }
}

public enum PenguinType
{
    Basic,
    Archer
}

public class PenguinSpawner : MonoBehaviour
{
    [SerializeField] private RectTransform _spawnUI;
    [SerializeField] protected Transform _spawnPoint;

    [SerializeField] private float onSpawnUIYPosValue = 320;

    Penguin spawnPenguin;

    public bool isSpawnUIOn { get; private set; }

    Vector3 _onSpawnUIVec;
    Vector3 _offSpawnUIVec;

    [SerializeField] private Transform _btnTrm;
    private SpawnButton[] _btnArr;
    
    Dictionary<PenguinType, SpawnPenguinBtnInfo> _penguinSpawnBtnDic = new Dictionary<PenguinType, SpawnPenguinBtnInfo>();

    private void Start()
    {
        _offSpawnUIVec = _spawnUI.position;
        _onSpawnUIVec = _offSpawnUIVec + new Vector3(0, onSpawnUIYPosValue, 0);
        _btnArr = _btnTrm.GetComponentsInChildren<SpawnButton>();
        InitPenguinSpawnBtnDic();
    }

    private void InitPenguinSpawnBtnDic()
    {
        PenguinType[] penguinTypes = (PenguinType[])Enum.GetValues(typeof(PenguinType));

        for (int i = 0; i < penguinTypes.Length; i++)
        {
            Debug.Log(penguinTypes[i]);
            PenguinType penguin = penguinTypes[i];
            _penguinSpawnBtnDic.Add(penguin, _btnArr[i].Info);
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.B)) //일단 임시로 b키 누르면 생성 UI 뜸
        {
            Vector3 targetVec = isSpawnUIOn ? _offSpawnUIVec : _onSpawnUIVec;
            UIManager.Instance.UIMoveDot(_spawnUI, targetVec, 0.7f, Ease.OutCubic);
            UpdateSpawnUIBool();
        }
    }

    public void SpawnPenguin<T>(Vector3 vec) where T : Penguin // 타입에 맞게 펭귄 생성
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
        UIManager.Instance.ButtonCooldown
            (_penguinSpawnBtnDic[PenguinType.Basic],
            () => SpawnPenguin<BasicPenguin>(_spawnPoint.position));
    }

    public void ArcherPenguinSpawnHandler()
    {
        UIManager.Instance.ButtonCooldown
            (_penguinSpawnBtnDic[PenguinType.Archer],
            () => SpawnPenguin<ArcherPenguin>(_spawnPoint.position));
    }

    #endregion
}
