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
    //[SerializeField] private RectTransform _spawnUI;
    //protected Transform[] _spawnPoints;

    //[SerializeField] private float onSpawnUIYPosValue = 320;
    //[SerializeField] private LayerMask _spawnerLayer;
    //[SerializeField] Transform _spawnPoint;

    //List<DummyPenguin> _dummyPenguinList = new List<DummyPenguin>();

    //Penguin _spawnPenguin;

    //private bool isSpawnUIOn;

    //Vector3 _onSpawnUIVec;
    //Vector3 _offSpawnUIVec;

    //[SerializeField] private Transform _btnTrm;
    //private SpawnButton[] _btnArr;
    
    //Dictionary<PenguinType, SpawnPenguinBtnInfo> _penguinSpawnBtnDic = new Dictionary<PenguinType, SpawnPenguinBtnInfo>();

    //private void OnEnable()
    //{
    //    WaveManager.Instance.OnPhaseStartEvent += DummyPenguinSetPosition;
    //    WaveManager.Instance.OnIceArrivedEvent += ResetDummyPenguinList;
    //}

    //private void OnDisable()
    //{
    //    WaveManager.Instance.OnPhaseStartEvent -= DummyPenguinSetPosition;
    //    WaveManager.Instance.OnIceArrivedEvent -= ResetDummyPenguinList;
    //}

    //private void Awake()
    //{
    //    _spawnPoints = _spawnPoint.GetComponentsInChildren<Transform>();
    //    _btnArr = _btnTrm.GetComponentsInChildren<SpawnButton>();

    //    foreach(Transform t in _spawnPoints)
    //    {

    //        Debug.Log(t.position);
    //    }
    //}

    //private void Start()
    //{
    //    _offSpawnUIVec = _spawnUI.position;
    //    _onSpawnUIVec = _offSpawnUIVec + new Vector3(0, onSpawnUIYPosValue, 0);
    //    InitPenguinSpawnBtnDic();
    //    GameManager.Instance.PlusDummyPenguinCount();
    //}

    //private void InitPenguinSpawnBtnDic()
    //{
    //    PenguinType[] penguinTypes = (PenguinType[])Enum.GetValues(typeof(PenguinType));

    //    for (int i = 0; i < penguinTypes.Length; i++)
    //    {
    //        Debug.Log(penguinTypes[i]);
    //        _penguinSpawnBtnDic.Add(penguinTypes[i], _btnArr[i].Info);
    //    }
    //}

    //private void Update()
    //{
    //    if (Input.GetMouseButtonDown(0))
    //    {
    //        if (GameManager.Instance.TryRaycast(GameManager.Instance.RayPosition(),
    //                                            out var hit, Mathf.Infinity, _spawnerLayer))
    //        {
    //            Vector3 targetVec = isSpawnUIOn ? _offSpawnUIVec : _onSpawnUIVec;
    //            UIManager.Instance.UIMoveDot(_spawnUI, targetVec, 0.7f, Ease.OutCubic);
    //            UpdateSpawnUIBool();
    //            hit.collider.transform.GetComponent<Outline>().enabled = isSpawnUIOn;
    //        }
    //    }
    //}

    //public void SpawnPenguin<T>(Vector3 vec) where T : Penguin // Å¸ÀÔ¿¡ ¸Â°Ô Æë±Ï »ý¼º
    //{
    //    Type type = typeof(T);
    //    Debug.Log(type);
    //    _spawnPenguin = PoolManager.Instance.Pop(type.Name) as T;

    //    _spawnPenguin.transform.position = vec;
    //    _spawnPenguin.transform.rotation = Quaternion.identity;
    //}

    //public void SpawnDummyPenguin(Vector3 vec, string type) // Å¸ÀÔ¿¡ ¸Â°Ô Æë±Ï »ý¼º
    //{
    //    DummyPenguin dummy = PoolManager.Instance.Pop($"{type}DummyPenguin") as DummyPenguin;

    //    dummy.transform.position = vec;
    //    dummy.transform.rotation = Quaternion.identity;
    //    Debug.Log(dummy.transform.position);

    //    _dummyPenguinList.Add(dummy);
    //}

    //private void UpdateSpawnUIBool()
    //{
    //    isSpawnUIOn = isSpawnUIOn ? false : true;
    //}

    //#region SpawnPenguinButtonHandler

    //public void BasicPenguinSpawnHandler()
    //{
    //    UIManager.Instance.ButtonCooldown
    //        (_penguinSpawnBtnDic[PenguinType.Basic],
    //        () => SpawnDummyPenguin(_spawnPoints[GameManager.Instance.GetDummyPenguinCount].position, "Basic"));
    //    Debug.Log(_spawnPoints[GameManager.Instance.GetDummyPenguinCount].position);
    //}

    //public void ArcherPenguinSpawnHandler()
    //{
    //    UIManager.Instance.ButtonCooldown
    //        (_penguinSpawnBtnDic[PenguinType.Archer],
    //        () => SpawnDummyPenguin(_spawnPoints[GameManager.Instance.GetDummyPenguinCount].position, "Archer"));
    //}

    //private void DummyPenguinSetPosition()
    //{
    //    for(int i = 0; i < _dummyPenguinList.Count; i++)
    //    {
    //        _dummyPenguinList[i].MoveToTent();
    //    }
    //}

    //private void ResetDummyPenguinList()
    //{
    //    GameManager.Instance.ResetDummyPenguinCount();
    //    _dummyPenguinList.Clear();
    //}

    //#endregion
}
