using ArmySystem;
using SynergySystem;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SynergyBuildingMouseEventHandler : MouseEventHandler
{
    [SerializeField] private SynergyBuilding _owner = null;
    [SerializeField] private Transform _spawnPostion = null;

    public int InitMaxHealingTime = 30;
    [Range(1f, 10f)] public float DetectionRange = 3f;

    public bool IsHealing = false;

    public SynergyType BuildingSynergyType => _owner.BuildingSynergyType;

    private Coroutine _healingTimeCorouine = null;
    private Army _seletedArmy = null;

    private int _maxHealingTime = 0;

    private void Start()
    {
        _maxHealingTime = InitMaxHealingTime;
    }
    protected override void OnMouseEnter()
    {
        _owner.OutlineCompo.enabled = true;

        //���콺 �÷����� �ش� �ó��� �̹��� + �����̸� �����ֱ�

        if (IsHealing)
        {
            //��Ÿ�� �����ֱ�
        }
    }

    protected override void OnMouseExit()
    {
        _owner.OutlineCompo.enabled = false;
    }

    public override void OnClick()
    {
        if (IsHealing) return;

        IsHealing = true;

        if (_healingTimeCorouine != null)
        {
            StopCoroutine(_healingTimeCorouine);
        }

        _healingTimeCorouine = StartCoroutine(HealingTimeCorou());
    }


    public void SetArmy(Army army)
    {
        _seletedArmy = army;
    }

    private IEnumerator HealingTimeCorou()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _maxHealingTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        IsHealing = false;

        UIManager.Instance.ShowWarningUI($"{_seletedArmy.LegionName}���� ȸ�� �Ϸ��");
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
    }

}
