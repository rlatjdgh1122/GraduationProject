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
    protected Army myArmy = null;

    [SerializeField] private TextMeshProUGUI _legionNameTxt = null;

    [SerializeField] private Image _synergyImage;
    [SerializeField] private Image _skillImage;
    [SerializeField] private Image _ultimateImage;


    private Image[] _penguinIconList;

    private void Awake()
    {
        Transform penguinIconParent = transform.Find("SoliderIcon");
        _penguinIconList = penguinIconParent.GetComponentsInChildren<Image>();
    }

    public void SetArmy(Army army)
    {
    }

    public void Init()
    {
        //��ųUI�� �ñر�UI Init������
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
    /// ������ ������ ��ų �̹��� �ٲ��ֱ�
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
