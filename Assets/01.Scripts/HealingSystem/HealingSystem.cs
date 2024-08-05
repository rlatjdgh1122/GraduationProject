using ArmySystem;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class HealingSystem : MonoBehaviour
{
    [SerializeField] private Transform _spawnStartTrm = null;
    [SerializeField] private Transform _spawnEndTrm = null;
    [Range(1f, 10f)] public float DetectionRange = 3f;
    public int InitMaxHealingTime = 30;
    public bool IsHealing = false;

    public UnityEvent<float> OnStartHealingEvent = null;
    public UnityEvent OnEndHealingEvent = null;
    public UnityEvent OnBrokenBuildingEvent = null;

    private HealingController _controller = null;
    private Coroutine _healingTimeCorouine = null;
    private Army _seletedArmy = null;

    private int _maxHealingTime = 0;
    private float _healingTime = 0;

    private void Start()
    {
        _maxHealingTime = InitMaxHealingTime;

    }

    public void Setting()
    {
        _controller = new HealingController(transform, _spawnStartTrm.position, _spawnEndTrm.position, DetectionRange);
    }

    public void SetArmy(Army army)
    {
        _seletedArmy = army;
        _controller.SetArmy(army);
    }

    public void OnClick()
    {
        _controller.GoToBuilding(StartHealing);
    }

    public void LevelUp(int value)
    {
        _maxHealingTime -= value;
    }

    public void LeaveBuilding()
    {
        if (IsHealing == false) return;

        UIManager.Instance.ShowWarningUI($"{_seletedArmy.LegionName}군단 회복 최소됨");

        if (_healingTimeCorouine != null)
            StopCoroutine(_healingTimeCorouine);

        IsHealing = false;
        _controller.LeaveBuilding();

        OnEndHealingEvent?.Invoke();
    }


    private void StartHealing()
    {
        UIManager.Instance.ShowWarningUI($"{_seletedArmy.LegionName}군단 회복 시작");

        IsHealing = true;
        if (_healingTimeCorouine != null)
        {
            StopCoroutine(_healingTimeCorouine);
        }

        _healingTimeCorouine = StartCoroutine(HealingTimeCorou());

        _healingTime = _seletedArmy.AlivePenguins.Count / _maxHealingTime;
        OnStartHealingEvent?.Invoke(_healingTime);
    }

    private void EndHealing()
    {
        IsHealing = false;
        _controller.EndHealing();

        UIManager.Instance.ShowWarningUI($"{_seletedArmy.LegionName}군단 회복 완료됨");

        OnEndHealingEvent?.Invoke();
    }

    private IEnumerator HealingTimeCorou()
    {
        float elapsedTime = 0f;

        while (elapsedTime < _healingTime)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        EndHealing();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, DetectionRange);
    }


}
