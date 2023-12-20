using TMPro;
using UnityEngine;

enum SlotType
{
    Sword,
    Arrow,
    Shield
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

        if (_isMoveUI)
            WaveManager.Instance.CanTimer = false;
        else
            WaveManager.Instance.CanTimer = true;
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
                if (string.Equals(child.name, $"ItemObject[{SlotType.Sword.ToString()}](Clone)"))
                    legion.Sword++;
                if (string.Equals(child.name, $"ItemObject[{SlotType.Shield.ToString()}](Clone)"))
                    legion.Shield++;
            }
        }
    }

    public void LegionUIDownUnitReset()
    {
        for(int i = 0; i < Legion.Instance.LegionCnt.Count; i++)
        {
            Legion.Instance.LegionCnt[i].Arrow = 0;
            Legion.Instance.LegionCnt[i].Sword = 0;
            Legion.Instance.LegionCnt[i].Shield = 0;
        }
    }


    #region 군단 UI 추가
    public void AddUI()
    {
        InstantiateHeroes(SlotType.Sword);
        InstantiateHeroes(SlotType.Arrow);
        InstantiateHeroes(SlotType.Shield);
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
        if (WaveManager.Instance.IsPhase) _isMoveUI = false;
        else _isMoveUI = !_isMoveUI;
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