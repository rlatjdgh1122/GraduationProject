using ArmySystem;
using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SelectedStatusSlot : MonoBehaviour, IValueChangeUnit<ArmyUIInfo>
{
    [SerializeField] private TextMeshProUGUI _legionNameTxt = null;
    [SerializeField] private Transform _UITrm = null;

    protected SkillUI skillUI = null;
    protected UltimateUI ultimateUI = null;

    private Image[] _penguinIconList;
    private Army _army = null;

    private void Awake()
    {
        Transform penguinIconParent = transform.Find("SoliderIcon");
        _penguinIconList = penguinIconParent.GetComponentsInChildren<Image>();
    }

    //군단이 바뀔때마다 정보에 따라 실햄(이준서가 만든 그거)

    public void ChangedSlot(StatusSlot slot, Army army)
    {
        _army = army;
        //스킬이랑 궁극기 복사해서 연결하고
        //레지스터로 연결해줌
        if (slot.SkillUI)
        {
            GameObject obj = Instantiate(slot.SkillUI.gameObject, _UITrm);
        }

        if (slot.SkillUI)
        {

        }
        //군단일므 바꿔줌
        LegionNameChangedHandler(army.LegionName);
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
