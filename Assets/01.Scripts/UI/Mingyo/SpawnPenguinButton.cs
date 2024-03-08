using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;


public class SpawnPenguinButton : MonoBehaviour
{
    private Button _btn;
    private Sprite _face;
    private TextMeshProUGUI _name;

    [SerializeField] private int _price;
    [Tooltip("������ ������ ������. Prefab�� �־��ָ� �ȴ�.")]
    [SerializeField] private PenguinStat _penguinStat;
    [SerializeField] private Penguin spawnPenguin;
    
    //private PenguinFactory _penguinFactory; // ���丮
    private PenguinStoreUI _penguinStore;

    protected virtual void Awake()
    {
        _penguinStore = transform.parent.parent.parent.GetComponent<PenguinStoreUI>();
        _face = transform.Find("PenguinImg").GetComponent<Sprite>();
        _name = transform.Find("PenguinName").GetComponent<TextMeshProUGUI>();
        //_penguinFactory = GameObject.Find("PenguinSpawner/PenguinFactory").GetComponent<PenguinFactory>();
        //_btn = GetComponent<Button>();
    }

    public void SlotUpdate()
    {
        _face = _penguinStat.PenguinIcon;
        _name.text = _penguinStat.PenguinName;
    }

    public void SpawnPenguinEventHandler() //Inspector ��ư �̺�Ʈ���� ������ �Լ�
    {
        _penguinStore.PenguinInformataion(_penguinStat, _price);
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
        //    _penguinFactory.SpawnPenguinHandler(spawnPenguin); // ���丮���� �����ϴ� �Լ� ����
        //});
    }

}
