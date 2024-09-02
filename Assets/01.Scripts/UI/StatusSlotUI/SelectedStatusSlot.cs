using ArmySystem;
using DG.Tweening;
using SkillSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using static GeneralSettingData;
using static SynergySettingData;

public class SelectedStatusSlot : MonoBehaviour, IValueChangeUnit<ArmyUIInfo>
{
    [SerializeField] private TextMeshProUGUI _legionNameTxt = null;

    [SerializeField] private Image _synergyImage = null;
    [SerializeField] private Image _skillImage = null;
    [SerializeField] private Image _ultimateImage = null;

    [SerializeField] private Sprite _lockSprite = null;

    [SerializeField] private Transform _UITrm = null;
    [SerializeField] private SeletedSlotSynergyUI _synergyUI = null;
    [SerializeField] private SelectedSlotSkillUI _skillUI = null;
    [SerializeField] private SelectedSlotUltimateUI _ultimateUI = null;

    private Image[] _penguinIconList;
    private Army _army = null;

    private bool _isSkillLock = false;
    private bool _isUltimateLock = false;

    private void Awake()
    {
        Transform penguinIconParent = transform.Find("SoliderIcon");
        _penguinIconList = penguinIconParent.GetComponentsInChildren<Image>();
    }

    private void OffRegister(Army army)
    {
        if (!army.General) return;

        if (_skillUI != null)
        {
            army.SkillController.OnSkillUsedEvent -= _skillUI.OnSkillUsed;
            army.SkillController.OnChangedMaxValueEvent -= _skillUI.OnChangedMaxValue;
            army.SkillController.OnSkillActionEnterEvent -= _skillUI.OnSkillActionEnter;
            army.SkillController.OnSkillReadyEvent -= _skillUI.OnSkillReady;
        }

        if (_ultimateUI != null)
        {
            army.UltimateController.OnSkillUsedEvent -= _ultimateUI.OnUltimateUsed;
            army.UltimateController.OnChangedMaxValueEvent -= _ultimateUI.OnChangedMaxValue;
            army.UltimateController.OnSkillActionEnterEvent -= _ultimateUI.OnUltimateActionEnter;
            //_army.UltimateController.OnSkillReadyEvent -= UltimateUI.OnSkillReady;
        }
    }

    private void OnRegister(Army army)
    {
        if (!army.General) return;

        if (_skillUI != null)
        {
            army.SkillController.OnSkillUsedEvent += _skillUI.OnSkillUsed;
            army.SkillController.OnChangedMaxValueEvent += _skillUI.OnChangedMaxValue;
            army.SkillController.OnSkillActionEnterEvent += _skillUI.OnSkillActionEnter;
            army.SkillController.OnSkillReadyEvent += _skillUI.OnSkillReady;

            _army.SkillController.Init();
        }

        if (_ultimateUI != null)
        {
            army.UltimateController.OnSkillUsedEvent += _ultimateUI.OnUltimateUsed;
            army.UltimateController.OnChangedMaxValueEvent += _ultimateUI.OnChangedMaxValue;
            army.UltimateController.OnSkillActionEnterEvent += _ultimateUI.OnUltimateActionEnter;
        }
    }

    public void Init()
    {
        _skillUI.Setting(0f, 0f);
        _ultimateUI.Setting(0f);
    }

    public void SetArmy(Army army)
    {
        if (_army != null)
        {
            OffRegister(_army);
        }
        _army = army;
        //�������ͷ� ��������
        OnRegister(_army);

        //�����Ϲ� �ٲ���
        LegionNameChangedHandler(army.LegionName);
        EnablePenguinInSelectLegion(army.Info.PenguinCount);

        _skillImage.sprite = _lockSprite;
        _ultimateImage.sprite = _lockSprite;
        _isSkillLock = true;
        _isUltimateLock = true;

        _skillUI.Setting(0f, 0f);
        _ultimateUI.Setting(0f);
    }


    public void SetSkillUI(float currentValue, float fillAmount, SkillData data)
    {
        //������� �Դٸ� �屺�� �ִٴ°�
        //���⼭ ��ų ��������

        _isSkillLock = false;
        data.IsLock = _isSkillLock;
        _skillUI.SetData(data);
        _skillUI.Setting(currentValue, fillAmount, data.SkillType);
        _skillImage.sprite = data.Icon;

    }
    public void SetUltimateUI(float fillAmount, UltimateData data)
    {
        _isUltimateLock = false; 
        data.IsLock = _isUltimateLock;
        _ultimateUI.SetData(data);
        _ultimateUI.Setting(fillAmount);
        _ultimateImage.sprite = data.Icon;
    }


    public void SetSynergyUI(SynergyData data)
    {
        //������� �Դٸ� �屺�� �ִٴ°�
        //���⼭ ��ų ��������
        _synergyUI.SetData(data);
        _synergyImage.sprite = data.Icon;

    }

    /// <summary>
    /// ���� ������ �ٲ�
    /// </summary>
    /// <param name="newValue"></param>
    public void ChangedValue(ArmyUIInfo newValue)
    {
        EnablePenguinInSelectLegion(newValue.PenguinCount);
    }

    /// <summary>
    /// ������ ������ ������, ��ų ������, �ñر� ������, ���� �� ����� �� �ޱ�
    /// </summary>
    public void ChangeLegionHandler(ArmyUIInfo armyInfo)
    {
        ChangeSkillsInCurrentUI(armyInfo);
        EnablePenguinInSelectLegion(armyInfo.PenguinCount);
        LegionNameChangedHandler(armyInfo.ArmyName);
    }


    private void LegionNameChangedHandler(string armyName)
    {
        _legionNameTxt.alpha = 0f;

        _legionNameTxt.text = $"{armyName}����";
        _legionNameTxt.DOFade(1f, 0.5f);
    }

    /// <summary>
    /// ������ ������ ��� ����ŭ �̹��� ���ֱ�
    /// </summary>
    /// <param name="penguinCount">��� ��</param>
    private void EnablePenguinInSelectLegion(int penguinCount)
    {
        UIManager.Instance.InitializHudTextSequence();

        foreach (var penguin in _penguinIconList)
        {
            ImageTransparent(penguin);
        }

        for (int i = 0; i < _penguinIconList.Length; i++)
        {
            if (i < penguinCount)
            {
                UIManager.Instance.HudTextSequence
                    .Append(_penguinIconList[i].DOFade(1, 0.05f));
            }
        }
    }

    /// <summary>
    /// ������ ������ ��ų �̹��� �ٲ��ֱ�
    /// </summary>
    private void ChangeSkillsInCurrentUI(ArmyUIInfo armyInfo)
    {
        /*   synergyIcon.sprite = armyInfo.SynergySprite;
           skillIcon.sprite = armyInfo.SkillSprite;
           ultimateIcon.sprite = armyInfo.UltimateSprite;

           ImageTransparent(synergyIcon);
           ImageTransparent(skillIcon);
           ImageTransparent(ultimateIcon);

           synergyIcon.DOFade(1, 0.5f);
           skillIcon.DOFade(1, 0.5f);
           ultimateIcon.DOFade(1, 0.5f);*/
    }

    /// <summary>
    /// �̹����� �����ϰ� ������ֱ�
    /// </summary>
    private void ImageTransparent(Image image)
    {
        Color alpha;

        alpha = image.color;
        alpha.a = 0f;

        image.color = alpha;
    }


}
