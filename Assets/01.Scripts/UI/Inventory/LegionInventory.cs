using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[Serializable]
public class Legion
{
    public int price;                               //���� ����
    public bool Locked;                             //������ ����ִ°�
    public Transform LegionPanels;                  //���� UI �θ�
    public int MaxCount;                           //�ִ� ���� ���� ��
    public int CurrentCount = 0;      //���� ���� ���� ��
    public bool MaxGereral { get; set; }           //���ܿ� �屺�� �����ִ°�

    [HideInInspector] public List<LegionInventoryData> LegionInven;
    public Dictionary<PenguinStat, LegionInventoryData> legionDictionary { get; private set; } = new();
}

public class LegionInventory : Singleton<LegionInventory>
{
    #region Property
    [Header("PenguinInventory")]
    //==========================================
    //��� �κ��丮
    //�屺
    public List<LegionInventoryData> generalInven = new();
    public Dictionary<PenguinTypeEnum, LegionInventoryData> generalDictionary { get; private set; } = new();

    //����
    public List<LegionInventoryData> soliderInven = new();
    public Dictionary<PenguinTypeEnum, LegionInventoryData> soliderDictionary { get; private set; } = new();
    //==========================================

    [Header("LegionInventory")]
    //���� �κ��丮
    public List<Legion> LegionList;

    [Header("Inventory UI")]
    [SerializeField] private Transform _generalParent;
    [SerializeField] private Transform _soliderParent;
    [Header("LegionInventoryUI")]
    [SerializeField] private LegionInventoryUI _legionUI;

    private UnitSlotUI[] _generalSlots;
    private UnitSlotUI[] _soliderSlots;

    [Header("!!Register PenguinSO!!")]
    [SerializeField] private PenguinStat[] _legionSO;
    private List<PenguinStat> _generalSO = new();
    private List<PenguinStat> _soliderSO = new();
    #endregion

    public override void Awake()
    {
        base.Awake();

        _generalSlots = _generalParent.GetComponentsInChildren<UnitSlotUI>();
        _soliderSlots = _soliderParent.GetComponentsInChildren<UnitSlotUI>();

        foreach (PenguinStat soliderSO in _legionSO)
        {
            if (soliderSO.UniqueType == PenguinUniqueType.Fight)
            {

                if (soliderSO.JobType == PenguinJobType.General)
                {
                    _generalSO.Add(soliderSO);
                }
                else
                {
                    _soliderSO.Add(soliderSO);
                }

            }
        }
    }

    private void Start()
    {
        LegionCountInformation(0);

        OffSlotByStartScene(_generalSlots, _generalSO);
        OffSlotByStartScene(_soliderSlots, _soliderSO);
    }

    public void OffSlotByStartScene(UnitSlotUI[] slot, List<PenguinStat> so)
    {
        for (int i = 0; i < slot.Length; i++)
        {
            if (i >= so.Count)
            {
                slot[i].gameObject.SetActive(false);
            }
        }

        for (int i = 0; i < so.Count; i++)
        {
            AddPenguin(so[i]);
            RemovePenguin(so[i]);
        }
    }

    public void AddPenguin(PenguinStat type) //��� �߰��ϴ� �Լ�(��� Ÿ������ �з�)
    {
        if (type.UniqueType == PenguinUniqueType.Fight)
        {
            if (type.JobType == PenguinJobType.General) //���� �屺�̸�
            {
                AddToWarLoad(type);
            }
            else if (type.JobType == PenguinJobType.Solider) //���� �����
            {
                AddToSolider(type);
            }
        }
        else //�ϲ��̶��
        {
            Debug.Log("�ϲ���");
            return;
        }

        UpdateSlotUI();
    }

    private void UpdateSlotUI()
    {
        for (int i = 0; i < _soliderSlots.Length; i++)
        {
            _soliderSlots[i].CleanUpSlot();
        }
        for (int i = 0; i < _generalSlots.Length; i++)
        {
            _generalSlots[i].CleanUpSlot();
        }

        for (int i = 0; i < generalInven.Count; ++i)
        {
            _generalSlots[i].UpdateSlot(generalInven[i]);
        }
        for (int i = 0; i < soliderInven.Count; ++i)
        {
            _soliderSlots[i].UpdateSlot(soliderInven[i]);
        }

    }

    #region ��� �κ��� ��� �߰�

    public void AddToWarLoad(PenguinStat penguin) //�߰��� �ؾߵǴµ� Ÿ���� ���� �׷���?
                                                  //++���ְ� ��� key�� ���°ɷ� ��ü
    {
        if (generalDictionary.TryGetValue(penguin.PenguinType, out LegionInventoryData legionInven))//���� ��� �κ��� ������
        {
            legionInven.penguinData = penguin;
            legionInven.AddStack();//�� ����
        }
        else//������
        {
            LegionInventoryData newInven = new LegionInventoryData(penguin);
            generalInven.Add(newInven);//���翡 ������ �߰�
            generalDictionary.Add(penguin.PenguinType, newInven);
        }
    }

    public void AddToSolider(PenguinStat penguin)
    {

        if (soliderDictionary.TryGetValue(penguin.PenguinType, out LegionInventoryData legionInven))//���� ��� �κ��� ������
        {
            legionInven.penguinData = penguin;
            legionInven.AddStack();//�� ����
        }
        else//������
        {
            LegionInventoryData newInven = new LegionInventoryData(penguin);
            soliderInven.Add(newInven);//���翡 ������ �߰�
            soliderDictionary.Add(penguin.PenguinType, newInven);
        }
    }

    #endregion

    public void RemovePenguin(PenguinStat penguin, int count = 1)
    {
        if (generalDictionary.TryGetValue(penguin.PenguinType, out LegionInventoryData warloadPenguin))
        {
            warloadPenguin.penguinData = penguin;
            warloadPenguin.RemoveStack(count);
        }
        else if (soliderDictionary.TryGetValue(penguin.PenguinType, out LegionInventoryData soliderPenguin))
        {
            soliderPenguin.penguinData = penguin;
            soliderPenguin.RemoveStack(count);
        }
        UpdateSlotUI();
    }

    public void AddToLegion(PenguinStat penguin, int legionNumber)
    {

        if (LegionList[legionNumber].legionDictionary.TryGetValue(penguin, out LegionInventoryData legionInven))
        {//���� ���ܿ� �� �����Ͱ� �ִٸ�

            legionInven.penguinData = penguin;
            legionInven.AddStack(); //����
        }
        else//���ٸ�
        {
            LegionInventoryData newInven = new LegionInventoryData(penguin);
            LegionList[legionNumber].LegionInven.Add(newInven);//���� �κ��� ������ �߰�
            LegionList[legionNumber].legionDictionary.Add(penguin, newInven);
        }
    }


    public void RemoveLegion(PenguinStat penguin, int i, int count = 1)
    {
        LegionList[i].legionDictionary.TryGetValue(penguin, out LegionInventoryData legion);

        if (legion.stackSize <= count) //�����κ����� ������ ����
        {
            LegionList[i].LegionInven.Remove(legion);
            LegionList[i].legionDictionary.Remove(penguin);
        }
        else //���� �κ����� ����
        {

            legion.RemoveStack(count);
        }

        AddPenguin(penguin); //���ܿ��� ���� ����� ��� �κ��� �߰�

        UpdateSlotUI();
    }

    public void ChangeLegion(int number)
    {
        for (int i = 0; i < LegionList.Count; i++)
        {
            if (i == number)
            {
                LegionList[i].LegionPanels.gameObject.SetActive(true);
            }
            else
            {
                LegionList[i].LegionPanels.gameObject.SetActive(false);
            }
        }
    }

    public void LegionCountInformation(int i)
    {
        _legionUI.LegionCountInformation(i);
    }

    public void ShowMessage(string message)
    {
        _legionUI.ShowMessage(message);
    }
}
