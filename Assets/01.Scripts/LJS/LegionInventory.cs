using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

enum SlotType
{
    Sword,
    Arrow
}

public class LegionInventory : MonoBehaviour
{
    [SerializeField] private RectTransform _legionInventory;
    [SerializeField] private float _targetPos;
    [SerializeField] private float _duration;

    [SerializeField] private GameObject[] _slot;
    [SerializeField] private TextMeshProUGUI[] _cntText;

    private Vector3 _firstUIPos;
    private bool _isMoveUI;

    private Legion _legionManager;

    private void Awake()
    {
        _legionManager = Legion.Instance;
        _firstUIPos = _legionInventory.position;
    }

    private void Update()
    {
        #region 임시 움직임
        if (Input.GetKeyDown(KeyCode.J))
        {
            MoveLegionUI();
        }
        #endregion

        HeroCntTexOutput();
    }

    public void HeroCntTexOutput()
    {
        for (int i = 0; i < _cntText.Length; i++)
        {
            _cntText[i].text = $"{_slot[i].transform.childCount}";
        }
    }
    public void LegionUIDownUnitCnt()
    {
        foreach (var legion in Legion.Instance.LegionCnt)
        {
            foreach (Transform child in legion._LegionPannel)
            {
                if (string.Equals(child.name, $"ItemObject[{SlotType.Arrow.ToString()}](Clone)"))
                    legion.Arrow++;
                else
                    legion.Sword++;
            }
        }
    }

    public void LegionUIDownUnitReset()
    {
        for(int i = 0; i < Legion.Instance.LegionCnt.Count; i++)
        {
            Legion.Instance.LegionCnt[i].Arrow = 0;
            Legion.Instance.LegionCnt[i].Sword = 0;

        }
    }


    #region 군단 UI 추가
    public void AddUI()
    {
        InstantiateHeroes(SlotType.Sword);
        InstantiateHeroes(SlotType.Arrow);
    }

    private void InstantiateHeroes(SlotType slotType)
    {
        int slotIndex = (int)slotType;

        for (int j = 0; j < _legionManager.LegionUIList[slotIndex].HeroCnt; j++)
        {
            GameObject uiObj = Instantiate(Legion.Instance.LegionUIList[slotIndex].SlotUIPrefab);
            uiObj.transform.SetParent(_slot[slotIndex].transform);
            uiObj.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        }

        Legion.Instance.LegionUIList[slotIndex].HeroCnt = 0;
    }
    #endregion

    #region UI 움직임
    public void MoveLegionUI()
    {
        _isMoveUI = !_isMoveUI;
        Vector3 targetPos;

        if(_isMoveUI)
        {
            targetPos = new Vector3(_targetPos + _firstUIPos.x, _firstUIPos.y, 0);
            AddUI(); //임시
            LegionUIDownUnitReset(); //임시
        }
        else
        {
            targetPos = _firstUIPos;
            LegionUIDownUnitCnt();
        }

        UIManager.Instance.UIMoveDot(_legionInventory, targetPos, _duration);
    }
    #endregion
}

/*
 using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

enum SlotType
{
    Basic,
    Archer
}

public class LegionInventory : MonoBehaviour
{
    [SerializeField] private RectTransform _legionInventory;

    [SerializeField] private float _targetPos;
    [SerializeField] private float _duration;

    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private Transform _slotPanel;
    [SerializeField] private TextMeshProUGUI[] _cntText;

    int _enumCount = Enum.GetValues(typeof(SlotType)).Length;
    private GameObject[] _slotArr;

    private Vector3 _firstUIPos;
    private bool _isMoveUI;

    private Legion _legionManager;

    private void Awake()
    {
        _legionManager = Legion.Instance;
        _firstUIPos = _legionInventory.position;

        for(int i = 0; i <_enumCount; i++)
        {
            GameObject slot = Instantiate(_slotPrefab);
            slot.transform.SetParent(_slotPanel);
        }
    }

    private void Update()
    {
        #region 임시 움직임
        if (Input.GetKeyDown(KeyCode.J))
        {
            MoveLegionUI();
        }
        #endregion

        HeroCntTexOutput();
    }

    public void HeroCntTexOutput()
    {
        for (int i = 0; i < _cntText.Length; i++)
        {
            _cntText[i].text = $"{_slotArr[i].transform.childCount}";
        }
    }

    public void AddUI()
    {
        InstantiateHeroes(SlotType.Basic);
        InstantiateHeroes(SlotType.Archer);
    }

    private void InstantiateHeroes(SlotType slotType)
    {
        int slotIndex = (int)slotType;

        for (int j = 0; j < _legionManager.LegionUIList[slotIndex].HeroCnt; j++)
        {
            GameObject uiObj = Instantiate(Legion.Instance.LegionUIList[slotIndex].SlotUIPrefab);
            uiObj.transform.SetParent(_slotArr[slotIndex].transform);
            uiObj.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        }

        Legion.Instance.LegionUIList[slotIndex].HeroCnt = 0;
    }

    public void MoveLegionUI()
    {
        _isMoveUI = !_isMoveUI;
        Vector3 targetPos;

        if(_isMoveUI)
        {
            targetPos = new Vector3(_targetPos + _firstUIPos.x, _firstUIPos.y, 0);
            AddUI();
        }
        else
        {
            targetPos = _firstUIPos;
        }

        UIManager.Instance.UIMoveDot(_legionInventory, targetPos, _duration);
    }
}
*/ //바꿀 코드