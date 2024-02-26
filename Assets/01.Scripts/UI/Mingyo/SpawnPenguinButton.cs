using DG.Tweening;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SpawnPenguinButton : MonoBehaviour
{
    private Button _btn;

    [Tooltip("��Ÿ���� �˷��� �̹��� (�ڽĿ� �ִ�)")]
    [SerializeField] private Image _coolingimg;

    [Tooltip("��Ÿ��")]
    [SerializeField] private float cooltime;

    [Tooltip("������ ������ ������. Prefab�� �־��ָ� �ȴ�.")]
    [SerializeField] private Penguin spawnPenguin;
    
    private PenguinFactory _penguinFactory; // ���丮

    protected virtual void Awake()
    {
        _penguinFactory = GameObject.Find("PenguinSpawner/PenguinFactory").GetComponent<PenguinFactory>();
        _btn = GetComponent<Button>();
    }

    public void SpawnPenguinEventHandler() //Inspector ��ư �̺�Ʈ���� ������ �Լ�
    {
        if(WaveManager.Instance.IsBattlePhase)
        {
            UIManager.Instance.InitializeWarningTextSequence();
            _penguinFactory.SetSpawnFailHudText("���� ������� ������ �� �����ϴ�");
            UIManager.Instance.WarningTextSequence.Prepend(_penguinFactory.SpawnFailHudText.DOFade(1f, 0.5f))
            .Join(_penguinFactory.SpawnFailHudText.rectTransform.DOMoveY(UIManager.Instance.ScreenCenterVec.y, 0.5f))
            .Append(_penguinFactory.SpawnFailHudText.DOFade(0f, 0.5f))
            .Join(_penguinFactory.SpawnFailHudText.rectTransform.DOMoveY(UIManager.Instance.ScreenCenterVec.y - 50f, 0.5f));

            return;
        }

        if (!WaveManager.Instance.IsBattlePhase) // ���� �غ�ð��ȿ� ������ �� �ִٸ� �����Ѵ�.
        {
            //TestLegion.Instance.LegionUIList[0].HeroCnt++;

            ButtonCooldown();
        }
    }

    private void ButtonCooldown() // ��ư ������ ����� �Լ�
    {
        _btn.interactable = false;
        _coolingimg.fillAmount = 1f;

        DOTween.To(() => _coolingimg.fillAmount, f => _coolingimg.fillAmount = f, 0f, cooltime).OnComplete(() => // �����ð��� �� �Ǿ��ٸ�
        {
            _btn.interactable = true;
            _penguinFactory.SpawnPenguinHandler(spawnPenguin); // ���丮���� �����ϴ� �Լ� ����
        });
    }

}
