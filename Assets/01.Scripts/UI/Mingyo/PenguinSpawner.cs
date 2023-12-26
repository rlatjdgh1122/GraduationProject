using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public enum PenguinTypeEnum
{
    Basic,
    Archer,
    Shield
}

public class PenguinSpawner : MonoBehaviour
{
    [SerializeField] private RectTransform _spawnUI;

    [SerializeField] private float onSpawnUIYPosValue = 320;
    [SerializeField] private LayerMask _spawnerLayer;

    List<DummyPenguin> _dummyPenguinList = new List<DummyPenguin>();

    Penguin _spawnPenguin;

    private bool isSpawnUIOn;

    Vector3 _onSpawnUIVec;
    Vector3 _offSpawnUIVec;

    [SerializeField] private Transform _btnTrm;
    private SpawnPenguinButton[] _btnArr;

    //private void OnDisable()
    //{
    //    WaveManager.Instance.OnPhaseStartEvent -= DummyPenguinMoveToTent;
    //    WaveManager.Instance.OnPhaseStartEvent -= SpawnPenguinLegionHandler;

    //    WaveManager.Instance.OnIceArrivedEvent -= ResetDummyPenguinList;
    //}

    //private void OnEnable()
    //{
    //    WaveManager.Instance.OnPhaseStartEvent += DummyPenguinMoveToTent;
    //    WaveManager.Instance.OnPhaseStartEvent += SpawnPenguinLegionHandler;

    //    WaveManager.Instance.OnIceArrivedEvent += ResetDummyPenguinList;
    //}

    private void Awake()
    {
        _btnArr = _btnTrm.GetComponentsInChildren<SpawnPenguinButton>();
    }

    private void Start()
    {
        _offSpawnUIVec = _spawnUI.position;
        _onSpawnUIVec = _offSpawnUIVec + new Vector3(0, onSpawnUIYPosValue, 0);
        //GameManager.Instance.PlusDummyPenguinCount();
    }

    private void Update()
    {
        if (Input.GetMouseButtonDown(0) && !WaveManager.Instance.IsPhase)
        {
            if (GameManager.Instance.TryRaycast(GameManager.Instance.RayPosition(),
                                                out var hit, Mathf.Infinity, _spawnerLayer))
            {
                SpawnButton();
                hit.collider.transform.GetComponent<Outline>().enabled = isSpawnUIOn;
            }
        }
    }

    public void SpawnButton()
    {
        Vector3 targetVec = isSpawnUIOn ? _offSpawnUIVec : _onSpawnUIVec;
        UIManager.Instance.UIMoveDot(_spawnUI, targetVec, 0.7f, Ease.OutCubic);
        UpdateSpawnUIBool();
    }

    private void UpdateSpawnUIBool()
    {
        isSpawnUIOn = isSpawnUIOn ? false : true;
    }

    //private void SpawnPenguinLegionHandler()
    //{
    //    StartCoroutine(SpawnPenguinLegion());
    //}

    //private IEnumerator SpawnPenguinLegion()
    //{
    //    yield return new WaitForSeconds(4f);

    //    for (int i = 0; i < Legion.Instance.LegionCnt.Count; i++)
    //    {
    //        int idx = 0;

    //        for (int j = 0; j < Legion.Instance.LegionCnt[i].SpawnSwordCnt; j++)
    //        {
    //            MeleePenguin penguin = SpawnPenguin<MeleePenguin>(_legionSpawnPoints[idx].position);
    //            ArmySystem.Instace.JoinArmy(i, penguin);
    //            idx++;
    //        }

    //        idx++;

    //        for (int j = 0; j < Legion.Instance.LegionCnt[i].SpawnArrowCnt; j++)
    //        {
    //            ArcherPenguin penguin = SpawnPenguin<ArcherPenguin>(_legionSpawnPoints[idx].position);
    //            ArmySystem.Instace.JoinArmy(i, penguin);
    //            idx++;
    //        }
            
    //        idx++;

    //        for (int j = 0; j < Legion.Instance.LegionCnt[i].SpawnShieldCnt; j++)
    //        {
    //            ShieldPenguin penguin = SpawnPenguin<ShieldPenguin>(_legionSpawnPoints[idx].position);
    //            ArmySystem.Instace.JoinArmy(i, penguin);
    //            idx++;
    //        }

    //        Legion.Instance.LegionCnt[i].SpawnSwordCnt = 0;
    //        Legion.Instance.LegionCnt[i].SpawnShieldCnt = 0;
    //        Legion.Instance.LegionCnt[i].SpawnArrowCnt = 0;
    //    }
    //}
}
