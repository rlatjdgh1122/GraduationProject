using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SpawnPenguinButton : MonoBehaviour
{
    private int _price;

    private PenguinStat _penguinStat;
    private Penguin _spawnPenguin;
    private PenguinStoreUI _penguinStore;
    
    private Image _icon;
    private TextMeshProUGUI _nameText;
    private TextMeshProUGUI _priceText;

    protected virtual void Awake()
    {
        _penguinStore = transform.parent.parent.parent.GetComponent<PenguinStoreUI>();
        _icon = transform.Find("PenguinImg/PenguinFace").GetComponent<Image>();
        _nameText = transform.Find("PenguinName").GetComponent<TextMeshProUGUI>();
        _priceText = transform.Find("Cost/CostText").GetComponent<TextMeshProUGUI>();
    }

    public void InstantiateSelf(PenguinStat stat, Penguin spawnPenguin, int price)
    {
        _penguinStat = stat;
        _spawnPenguin = spawnPenguin;

        _price = price;
    }

    public void SlotUpdate()
    {
        Debug.Log(_penguinStat);
        _icon.sprite = _penguinStat.PenguinIcon;
        _nameText.text = _penguinStat.PenguinName;
        _priceText.text = _price.ToString();
    }

    public void SpawnPenguinEventHandler() //Inspector ��ư �̺�Ʈ���� ������ �Լ�
    {
        _penguinStore.PenguinInformataion(_spawnPenguin, _penguinStat, _price);
        _penguinStore.OnEnableBuyPanel();


        //if(WaveManager.Instance.IsBattlePhase)
        //{
        //    UIManager.Instance.InitializHudTextSequence();
        //    _penguinFactory.SetSpawnFailHudText("���� ������� ������ �� �����ϴ�");

        //    UIManager.Instance.SpawnHudText(_penguinFactory.FailHudText);
        //    return;
        //}

        //if (!WaveManager.Instance.IsBattlePhase) // ���� �غ�ð��ȿ� ������ �� �ִٸ� �����Ѵ�.
        //{

        //}
    }

    private void ButtonCooldown() // ��ư ������ ����� �Լ�
    {
        //_btn.interactable = false;
        //_coolingimg.fillAmount = 1f;

        //UIManager.Instance.InitializHudTextSequence();
        //UIManager.Instance.SpawnHudText(_penguinFactory.SuccesHudText);

        //LegionInventory.Instance.AddPenguin(spawnPenguin.ReturnGenericStat<PenguinStat>());

        //DOTween.To(() => _coolingimg.fillAmount, f => _coolingimg.fillAmount = f, 0f, cooltime).OnComplete(() => // �����ð��� �� �Ǿ��ٸ�
        //{
        //    _btn.interactable = true;
        //_penguinFactory.SpawnPenguinHandler(_spawnPenguin); // ���丮���� �����ϴ� �Լ� ����
        //});
    }

}
