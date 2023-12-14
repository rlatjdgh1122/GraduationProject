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
    public PenguinTypeEnum PenguinType;

    public SpawnPenguinBtnInfo(PenguinTypeEnum penguinType, Button button, Image image, float coolTime)
    {
        PenguinType = penguinType;
        Btn = button;
        CoolingImg = image;
        CoolTime = coolTime;
    }
}

public enum PenguinTypeEnum
{
    Basic,
    Archer
}

public class PenguinSpawner : MonoBehaviour
{
    [SerializeField] private RectTransform _spawnUI;
    private Transform[] _dummySpawnPoints;
    private Transform[] _legionSpawnPoints;
    [SerializeField] Vector3 _initTrm;

    [SerializeField] private float onSpawnUIYPosValue = 320;
    [SerializeField] private LayerMask _spawnerLayer;
    [SerializeField] Transform _spawnPoint;
    [SerializeField] private Transform _campFireTrm;

    List<DummyPenguin> _dummyPenguinList = new List<DummyPenguin>();

    Penguin _spawnPenguin;

    private bool isSpawnUIOn;

    Vector3 _onSpawnUIVec;
    Vector3 _offSpawnUIVec;

    [SerializeField] private Transform _btnTrm;
    private SpawnButton[] _btnArr;
    
    Dictionary<PenguinTypeEnum, SpawnPenguinBtnInfo> _penguinSpawnBtnDic = new Dictionary<PenguinTypeEnum, SpawnPenguinBtnInfo>();


    private void OnDisable()
    {
        WaveManager.Instance.OnPhaseStartEvent -= DummyPenguinMoveToTent;
        WaveManager.Instance.OnIceArrivedEvent -= ResetDummyPenguinList;
    }

    private void Awake()
    {
        _dummySpawnPoints = _spawnPoint.GetComponentsInChildren<Transform>();
        _btnArr = _btnTrm.GetComponentsInChildren<SpawnButton>();

        WaveManager.Instance.OnPhaseStartEvent += DummyPenguinMoveToTent;
        WaveManager.Instance.OnIceArrivedEvent += ResetDummyPenguinList;
    }

    private void Start()
    {
        _offSpawnUIVec = _spawnUI.position;
        _onSpawnUIVec = _offSpawnUIVec + new Vector3(0, onSpawnUIYPosValue, 0);
        InitPenguinSpawnBtnDic();
        GameManager.Instance.PlusDummyPenguinCount();
    }

    private void InitPenguinSpawnBtnDic()
    {
        PenguinTypeEnum[] penguinTypes = (PenguinTypeEnum[])Enum.GetValues(typeof(PenguinTypeEnum));

        for (int i = 0; i < penguinTypes.Length; i++)
        {
            Debug.Log(penguinTypes[i]);
            _penguinSpawnBtnDic.Add(penguinTypes[i], _btnArr[i].Info);
        }
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !WaveManager.Instance.IsPhase)
        {
            if (GameManager.Instance.TryRaycast(GameManager.Instance.RayPosition(),
                                                out var hit, Mathf.Infinity, _spawnerLayer))
            {
                Vector3 targetVec = isSpawnUIOn ? _offSpawnUIVec : _onSpawnUIVec;
                UIManager.Instance.UIMoveDot(_spawnUI, targetVec, 0.7f, Ease.OutCubic);
                UpdateSpawnUIBool();
                hit.collider.transform.GetComponent<Outline>().enabled = isSpawnUIOn;
            }
        }
    }

    public void SpawnPenguin<T>(Vector3 vec) where T : Penguin // 타입에 맞게 펭귄 생성
    {
        Type type = typeof(T);
        Debug.Log(type);
        _spawnPenguin = PoolManager.Instance.Pop(type.Name) as T;

        _spawnPenguin.transform.position = _initTrm;
        _spawnPenguin.transform.rotation = Quaternion.identity;
    }

    public void SpawnDummyPenguin(Vector3 vec, string type) // 타입에 맞게 펭귄 생성
    {
        DummyPenguin dummy = PoolManager.Instance.Pop($"{type}DummyPenguin") as DummyPenguin;
        
        dummy.Init(_initTrm);
        dummy.transform.position = vec;
        dummy.transform.LookAt(_campFireTrm);

        _dummyPenguinList.Add(dummy);
    }

    private void UpdateSpawnUIBool()
    {
        isSpawnUIOn = isSpawnUIOn ? false : true;
    }

    private void SpawnPenguinLegion(int index)
    {
        for (int i = 0; i < Legion.Instance.LegionCnt[index + 1].Sword; i++)
        {
            SpawnPenguin<BasicPenguin>(_legionSpawnPoints[i].position);
        }

        for (int i = 0; i < Legion.Instance.LegionCnt[index + 1].Arrow; i++)
        {
            SpawnPenguin<ArcherPenguin>(_legionSpawnPoints[i].position);
        }
    }
    #region SpawnPenguinButtonHandler

    public void BasicPenguinSpawnHandler() // 이거를 struct를 받는 걸로 바꾸셈
    {
        
        if(WaveManager.Instance.RemainingPhaseReadyTime >= _penguinSpawnBtnDic[PenguinTypeEnum.Basic].CoolTime)
        {
            int index = GameManager.Instance.GetDummyPenguinCount;
            ButtonCooldown
                (_penguinSpawnBtnDic[PenguinTypeEnum.Basic],
                () => SpawnDummyPenguin(_dummySpawnPoints[index].position, "Basic"));
            Legion.Instance.LegionUIList[0].HeroCnt++;
        }
    }

    public void ArcherPenguinSpawnHandler()
    {
        if (WaveManager.Instance.RemainingPhaseReadyTime >= _penguinSpawnBtnDic[PenguinTypeEnum.Basic].CoolTime)
        {
            int index = GameManager.Instance.GetDummyPenguinCount;
            ButtonCooldown
                (_penguinSpawnBtnDic[PenguinTypeEnum.Archer],
                () => SpawnDummyPenguin(_dummySpawnPoints[index].position, "Archer"));
            Legion.Instance.LegionUIList[1].HeroCnt++;
        }
    }

    private void DummyPenguinMoveToTent()
    {
        for(int i = 0; i < _dummyPenguinList.Count; i++)
        {
            _dummyPenguinList[i].MoveToTent();
        }
    }

    private void ResetDummyPenguinList()
    {
        GameManager.Instance.ResetDummyPenguinCount();
        _dummyPenguinList.Clear();
    }

    public void ButtonCooldown(SpawnPenguinBtnInfo btnInfo, Action spawnAction)
    {
        btnInfo.Btn.interactable = false;
        btnInfo.CoolingImg.fillAmount = 1f;

        GameManager.Instance.PlusDummyPenguinCount();

        DOTween.To(() => btnInfo.CoolingImg.fillAmount, f => btnInfo.CoolingImg.fillAmount = f, 0f, btnInfo.CoolTime).OnUpdate(() => Debug.Log(btnInfo.CoolingImg.fillAmount)).OnComplete(() =>
        {
            spawnAction?.Invoke();
            btnInfo.Btn.interactable = true;
        });
    }

    #endregion
}
