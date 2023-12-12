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

    private void FixedUpdate()
    {
        #region 임시 움직임
        if (Input.GetKeyDown(KeyCode.J))
        {
            MoveLegionUI();
        }
        #endregion

        for(int i = 0; i < _cntText.Length; i++)
        {
            _cntText[i].text = $"{_slot[i].transform.childCount}";
        }
    }

    public void AddUI() //임시 추가
    {
        int cnt1 = 0, cnt2 = 0;
        for (int j = 0; j < _legionManager.LegionUIList[(int)SlotType.Basic].HeroCnt; j++)
        {
            GameObject uiObj = Instantiate(Legion.Instance.LegionUIList[0].SlotUIPrefab);
            uiObj.transform.SetParent(_slot[0].transform);
            uiObj.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        }
        Legion.Instance.LegionUIList[0].HeroCnt = cnt1;
        for (int j = 0; j < _legionManager.LegionUIList[(int)SlotType.Archer].HeroCnt; j++)
        {
            GameObject uiObj = Instantiate(Legion.Instance.LegionUIList[1].SlotUIPrefab);
            uiObj.transform.SetParent(_slot[1].transform);
            uiObj.GetComponent<RectTransform>().anchoredPosition = Vector3.zero;
        }
        Legion.Instance.LegionUIList[1].HeroCnt = cnt2;
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