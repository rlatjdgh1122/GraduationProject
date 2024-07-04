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

    //������ �ٲ𶧸��� ������ ���� ����(���ؼ��� ���� �װ�)

    public void ChangedSlot(StatusSlot slot, Army army)
    {
        _army = army;
        //��ų�̶� �ñر� �����ؼ� �����ϰ�
        //�������ͷ� ��������
        if (slot.SkillUI)
        {
            GameObject obj = Instantiate(slot.SkillUI.gameObject, _UITrm);
        }

        if (slot.SkillUI)
        {

        }
        //�����Ϲ� �ٲ���
        LegionNameChangedHandler(army.LegionName);
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
