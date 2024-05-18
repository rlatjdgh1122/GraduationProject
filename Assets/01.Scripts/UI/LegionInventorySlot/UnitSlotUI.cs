using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UnitSlotUI : SlotUI
{
    private TextMeshProUGUI _countText;
    private Image _lockImg;
    private Image _lockIconImg;
    private Image _generalImg;

    private bool _locked => _stackSize <= 0;
    private int _stackSize = 0;

    public EntityInfoDataSO _keyData;

    protected override void Awake()  
    {
        base.Awake();

        unitImage = transform.Find("Penguin").GetComponent<Image>();
        _generalImg = transform.Find("GeneralPenguin").GetComponent<Image>();
        _countText = transform.Find("CountBK/Count").GetComponent<TextMeshProUGUI>();
        _lockImg = transform.Find("Lock").GetComponent<Image>();
        _lockIconImg = transform.Find("Icon").GetComponent<Image>();
    }

    public void Create(EntityInfoDataSO data)
    {
        _keyData = data;

        if (data.JobType == PenguinJobType.Solider)
        {
            unitImage.sprite = data.PenguinIcon;
            unitImage.DOFade(1, 0);
        }
        else
        {
            _generalImg.sprite = data.PenguinIcon;
            _generalImg.DOFade(1, 0);
        }

        gameObject.name = $"{data.PenguinName} ½½·Ô";

        UpdateSlot();
    }

    public override void EnterSlot(EntityInfoDataSO data)
    {
        if (data != _keyData) return;

        ++_stackSize;

        UpdateSlot();
    }

    public override void ExitSlot(EntityInfoDataSO data)
    {
        if (data != _keyData || _stackSize <= 0) return;

        _stackSize--;

        UpdateSlot();
    }

    public override void UpdateSlot()
    {
        _lockImg.gameObject.SetActive(_locked);

        _countText.text = $"{_stackSize} ¸¶¸®";
    }

    public void LockSlot()
    {
        unitImage.color = Color.black;
        _countText.text = string.Empty;
        _lockIconImg.gameObject.SetActive(true);
    }

    public void UnLockSlot()
    {
        unitImage.color = Color.white;
        _lockIconImg.gameObject.SetActive(false);

        UpdateSlot();
    }

    public override void OnPointerDown(PointerEventData eventData)
    {
        if (_locked) return;

        UnitInventoryData data = new UnitInventoryData(_keyData, _stackSize);

        LegionInventoryManager.Instance.SelectInfoData(data);
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        if(!_locked)
            base.OnPointerEnter(eventData);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        if(!_locked)
            base.OnPointerExit(eventData);
    }
}
