using ArmySystem;
using DG.Tweening;
using SkillSystem;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedStatusSlot : MonoBehaviour, IValueChangeUnit<ArmyUIInfo>
{
    [SerializeField] private TextMeshProUGUI _legionNameTxt = null;

    [SerializeField] private Image _synergyImage = null;
    [SerializeField] private Image _skillImage = null;
    [SerializeField] private Image _ultimateImage = null;

    [SerializeField] private Transform _UITrm = null;
    [SerializeField] private SelectedSlotSkillUI SkillUI = null;

    protected UltimateUI UltimateUI = null;

    private Image[] _penguinIconList;
    private Army _army = null;

    private void Awake()
    {
        Transform penguinIconParent = transform.Find("SoliderIcon");
        _penguinIconList = penguinIconParent.GetComponentsInChildren<Image>();
    }

    private void OffRegister(Army army)
    {
        if (SkillUI != null)
        {
            army.SkillController.OnSkillUsedEvent -= SkillUI.OnSkillUsed;
            army.SkillController.OnChangedMaxValueEvent -= SkillUI.OnChangedMaxValue;
            army.SkillController.OnSkillActionEnterEvent -= SkillUI.OnSkillActionEnter;
            army.SkillController.OnSkillReadyEvent -= SkillUI.OnSkillReady;
        }

        if (UltimateUI != null)
        {
            army.UltimateController.OnSkillUsedEvent -= UltimateUI.OnUltimateUsed;
            army.UltimateController.OnChangedMaxValueEvent -= UltimateUI.OnChangedMaxValue;
            army.UltimateController.OnSkillActionEnterEvent -= UltimateUI.OnUltimateActionEnter;
            //_army.UltimateController.OnSkillReadyEvent -= UltimateUI.OnSkillReady;
        }
    }

    private void OnRegister(Army army)
    {
        if (SkillUI != null)
        {
            army.SkillController.OnSkillUsedEvent += SkillUI.OnSkillUsed;
            army.SkillController.OnChangedMaxValueEvent += SkillUI.OnChangedMaxValue;
            army.SkillController.OnSkillActionEnterEvent += SkillUI.OnSkillActionEnter;
            army.SkillController.OnSkillReadyEvent += SkillUI.OnSkillReady;
        }

        if (UltimateUI != null)
        {
            army.UltimateController.OnSkillUsedEvent += UltimateUI.OnUltimateUsed;
            army.UltimateController.OnChangedMaxValueEvent += UltimateUI.OnChangedMaxValue;
            army.UltimateController.OnSkillActionEnterEvent += UltimateUI.OnUltimateActionEnter;
        }
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
    }


    public void SetSkillUI(float currentValue, float fillAmount, SkillType skillType, Sprite image)
    {
        //������� �Դٸ� �屺�� �ִٴ°�
        //���⼭ ��ų ��������
        SkillUI.Setting(currentValue, fillAmount, skillType);
        _skillImage.sprite = image;

    }

    public void SetSynergyUI(Sprite image)
    {
        //������� �Դٸ� �屺�� �ִٴ°�
        //���⼭ ��ų ��������
        _synergyImage.sprite = image;

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

        _legionNameTxt.text = armyName;
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
