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
        //레지스터로 연결해줌
        OnRegister(_army);

        //군단일므 바꿔줌
        LegionNameChangedHandler(army.LegionName);
    }


    public void SetSkillUI(float currentValue, float fillAmount, SkillType skillType, Sprite image)
    {
        //여기까지 왔다면 장군이 있다는거
        //여기서 스킬 연결하자
        SkillUI.Setting(currentValue, fillAmount, skillType);
        _skillImage.sprite = image;

    }

    public void SetSynergyUI(Sprite image)
    {
        //여기까지 왔다면 장군이 있다는거
        //여기서 스킬 연결하자
        _synergyImage.sprite = image;

    }

    /// <summary>
    /// 군단 정보가 바뀔때
    /// </summary>
    /// <param name="newValue"></param>
    public void ChangedValue(ArmyUIInfo newValue)
    {
        EnablePenguinInSelectLegion(newValue.PenguinCount);
    }

    /// <summary>
    /// 선택한 군단의 아이콘, 스킬 아이콘, 궁극기 아이콘, 군단 내 펭귄의 수 받기
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
    /// 선택한 군단의 펭귄 수만큼 이미지 켜주기
    /// </summary>
    /// <param name="penguinCount">펭귄 수</param>
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
    /// 선택한 군단의 스킬 이미지 바꿔주기
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
    /// 이미지를 투명하게 만들어주기
    /// </summary>
    private void ImageTransparent(Image image)
    {
        Color alpha;

        alpha = image.color;
        alpha.a = 0f;

        image.color = alpha;
    }


}
