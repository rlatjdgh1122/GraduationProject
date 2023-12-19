using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;
using UnityEngine.AI;

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
    [SerializeField]
    private Transform[] _legionSpawnPoints;
    [SerializeField] private Transform _initTrm;

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
        WaveManager.Instance.OnPhaseStartEvent -= SpawnPenguinLegionHandler;

        WaveManager.Instance.OnIceArrivedEvent -= ResetDummyPenguinList;
    }

    private void OnEnable()
    {
        WaveManager.Instance.OnPhaseStartEvent += DummyPenguinMoveToTent;
        WaveManager.Instance.OnPhaseStartEvent += SpawnPenguinLegionHandler;

        WaveManager.Instance.OnIceArrivedEvent += ResetDummyPenguinList;
    }

    private void Awake()
    {
        _dummySpawnPoints = _spawnPoint.GetComponentsInChildren<Transform>();
        _btnArr = _btnTrm.GetComponentsInChildren<SpawnButton>();
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

    public T SpawnPenguin<T>(Vector3 vec) where T : Penguin // 타입에 맞게 펭귄 생성
    {
        Type type = typeof(T);
        _spawnPenguin = PoolManager.Instance.Pop(type.Name) as T;

        _spawnPenguin.transform.position = _initTrm.position;
        _spawnPenguin.transform.rotation = Quaternion.identity;

        //여기 시간에 약간의 쿨타임을 가지고 나오게 하기.
        _spawnPenguin.NavAgent.enabled = true;
        _spawnPenguin.SetFirstPosition(vec); //위치 살짝 이상함


        return (T)_spawnPenguin;
    }

    public void SpawnDummyPenguin(Vector3 vec, string type) // 타입에 맞게 펭귄 생성
    {
        DummyPenguin dummy = PoolManager.Instance.Pop($"{type}DummyPenguin") as DummyPenguin;
        
        dummy.Init(_initTrm.position);
        dummy.transform.position = vec;
        dummy.transform.LookAt(_campFireTrm);

        _dummyPenguinList.Add(dummy);
    }

    private void UpdateSpawnUIBool()
    {
        isSpawnUIOn = isSpawnUIOn ? false : true;
    }

    private void SpawnPenguinLegionHandler()
    {
        StartCoroutine(SpawnPenguinLegion());
    }

    private IEnumerator SpawnPenguinLegion()
    {
        yield return new WaitForSeconds(4f);

        for (int i = 0; i < Legion.Instance.LegionCnt.Count; i++)
        {
            for (int j = 0; j < Legion.Instance.LegionCnt[i].Sword; j++)
            {
                MeleePenguin penguin = SpawnPenguin<MeleePenguin>(_legionSpawnPoints[j].localPosition);
                ArmySystem.Instace.JoinArmy(i, penguin);

            }

            for (int j = 0; j < Legion.Instance.LegionCnt[i].Arrow; j++)
            {
                ArcherPenguin penguin = SpawnPenguin<ArcherPenguin>(_legionSpawnPoints[j].localPosition);
                ArmySystem.Instace.JoinArmy(i, penguin);
            }
        }
        
    }
    #region SpawnPenguinButtonHandler

    public void BasicPenguinSpawnHandler() // 이거를 struct를 받는 걸로 바꾸셈
    {
        
        if(WaveManager.Instance.RemainingPhaseReadyTime >= _penguinSpawnBtnDic[PenguinTypeEnum.Basic].CoolTime)
        {
            int index = GameManager.Instance.GetDummyPenguinCount;
            Legion.Instance.LegionUIList[0].HeroCnt++;

            ButtonCooldown
                (_penguinSpawnBtnDic[PenguinTypeEnum.Basic],
                () => SpawnDummyPenguin(_dummySpawnPoints[index].position, "Basic"));
        }
    }

    public void ArcherPenguinSpawnHandler()
    {
        if (WaveManager.Instance.RemainingPhaseReadyTime >= _penguinSpawnBtnDic[PenguinTypeEnum.Basic].CoolTime)
        {
            int index = GameManager.Instance.GetDummyPenguinCount;
            Legion.Instance.LegionUIList[1].HeroCnt++;

            ButtonCooldown
                (_penguinSpawnBtnDic[PenguinTypeEnum.Archer],
                () => SpawnDummyPenguin(_dummySpawnPoints[index].position, "Archer"));
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


        DOTween.To(() => btnInfo.CoolingImg.fillAmount, f => btnInfo.CoolingImg.fillAmount = f, 0f, btnInfo.CoolTime).OnComplete(() =>
        {
            spawnAction?.Invoke();
            btnInfo.Btn.interactable = true;
        });
    }

    #endregion
}
