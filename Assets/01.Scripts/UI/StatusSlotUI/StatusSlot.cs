using ArmySystem;
using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class StatusSlot : MonoBehaviour
{
    private Army _army = null;

    [SerializeField] private TextMeshProUGUI _legionNameTxt = null;

    [SerializeField] private Image _synergyImage;
    [SerializeField] private Image _skillImage;
    [SerializeField] private Image _ultimateImage;

    [SerializeField] private Sprite tt;
    [SerializeField] private Sprite yy;
    [SerializeField] private Sprite uu;
    [SerializeField] private Sprite ee;
    [SerializeField] private Sprite ss;
    [SerializeField] private Sprite rr;

    private Image[] _penguinIconList;

    private void Awake()
    {
        Transform penguinIconParent = transform.Find("SoliderIcon");
        _penguinIconList = penguinIconParent.GetComponentsInChildren<Image>();
    }

    public void SetArmy(Army army)
    {
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.N))
        {
            ArmyUIInfo info = new ArmyUIInfo
            (
                tt,yy,uu,2, "시기다른 랩퍼"
            );

            ChangeLegionHandler(info);
        }
        if (Input.GetKeyDown(KeyCode.M))
        {
            ArmyUIInfo info = new ArmyUIInfo
            (
                ee, ss, rr, 4, "최예원"
            );

            ChangeLegionHandler(info);
        }
    }

    /// <summary>
    /// 선택한 군단의 아이콘, 스킬 아이콘, 궁극기 아이콘, 군단 내 펭귄의 수 받기
    /// </summary>
    /// <param name="synergySprite">군단 아이콘</param>
    /// <param name="skillSprite">스킬 아이콘</param>
    /// <param name="ultimateSprite">궁극기 아이콘</param>
    /// <param name="penguinCount">군단 내 펭귄의 수 받기</param>
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

        for(int i = 0; i <  _penguinIconList.Length; i++)
        {
            if(i < penguinCount)
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
        _synergyImage.sprite = armyInfo.SynergySprite;
        _skillImage.sprite = armyInfo.SkillSprite;
        _ultimateImage.sprite = armyInfo.UltimateSprite;

        ImageTransparent(_synergyImage);
        ImageTransparent(_skillImage);
        ImageTransparent(_ultimateImage);

        _synergyImage.DOFade(1, 0.5f);
        _skillImage.DOFade(1, 0.5f);
        _ultimateImage.DOFade(1, 0.5f);
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
